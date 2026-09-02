using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Covers <c>AddIgniteUIBlazor</c> and the settings object it builds: which services are
/// registered, and how the resource modules named in the settings combine with the ones
/// passed as params. Module registration itself is asserted through the runtime, which
/// invokes each module's static <c>Register</c> as it is constructed.
/// </summary>
public class ServiceRegistrationTests
{
    /// <summary>Stand-in resource module: same shape the runtime reflects for, recording its calls.</summary>
    private static class FirstModule
    {
        public static readonly List<IIgniteUIBlazor> Registrations = [];
        public static void Register(IIgniteUIBlazor runtime) => Registrations.Add(runtime);
    }

    private static class SecondModule
    {
        public static readonly List<IIgniteUIBlazor> Registrations = [];
        public static void Register(IIgniteUIBlazor runtime) => Registrations.Add(runtime);
    }

    /// <summary>
    /// A provider plus an ambient scope. Both services are registered scoped, so tests resolve
    /// through a scope the way a Blazor circuit or request does; scope validation is on, which
    /// turns any accidental resolve off the root provider into a failure rather than a silent pass.
    /// </summary>
    private sealed class Host : IDisposable
    {
        private readonly ServiceProvider _root;
        private readonly IServiceScope _scope;

        public Host(Action<IServiceCollection> register)
        {
            var services = new ServiceCollection();
            services.AddSingleton(Mock.Of<IJSRuntime>());
            register(services);
            _root = services.BuildServiceProvider(validateScopes: true);
            _scope = _root.CreateScope();
        }

        public IServiceProvider Services => _scope.ServiceProvider;

        public IServiceScope NewScope() => _root.CreateScope();

        public void Dispose()
        {
            _scope.Dispose();
            _root.Dispose();
        }
    }

    private static IIgniteUIBlazorSettings SettingsOf(Host host) =>
        host.Services.GetRequiredService<IIgniteUIBlazorSettings>();

    [Fact]
    public void AddIgniteUIBlazor_RegistersRuntimeAndSettings()
    {
        using var host = new Host(s => s.AddIgniteUIBlazor());

        Assert.NotNull(host.Services.GetRequiredService<IIgniteUIBlazorSettings>());
        Assert.IsType<IgniteUIBlazor>(host.Services.GetRequiredService<IIgniteUIBlazor>());
    }

    [Fact]
    public void AddIgniteUIBlazor_RegistersBothServicesAsScoped()
    {
        var services = new ServiceCollection();
        services.AddIgniteUIBlazor();

        Assert.Equal(ServiceLifetime.Scoped, services.Single(d => d.ServiceType == typeof(IIgniteUIBlazor)).Lifetime);
        Assert.Equal(ServiceLifetime.Scoped, services.Single(d => d.ServiceType == typeof(IIgniteUIBlazorSettings)).Lifetime);
    }

    [Fact]
    public void AddIgniteUIBlazor_ReturnsTheSameCollection_ForChaining()
    {
        var services = new ServiceCollection();

        Assert.Same(services, services.AddIgniteUIBlazor());
        Assert.Same(services, services.AddIgniteUIBlazor(IgniteUIBlazorSettings.Create()));
    }

    [Fact]
    public void AddIgniteUIBlazor_WithoutModules_LeavesNoneToLoad()
    {
        using var host = new Host(s => s.AddIgniteUIBlazor());

        Assert.Null(SettingsOf(host).ModulesToLoad);
    }

    [Fact]
    public void AddIgniteUIBlazor_ParamsModules_AreExposedInSettings()
    {
        using var host = new Host(s => s.AddIgniteUIBlazor(typeof(FirstModule), typeof(SecondModule)));

        Assert.Equal([typeof(FirstModule), typeof(SecondModule)], SettingsOf(host).ModulesToLoad);
    }

    /// <summary>
    /// Modules carried by the settings used to be discarded whenever no params were passed:
    /// the copy constructor preserved them and the next line overwrote the list with null.
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_WithSettings_KeepsTheirModules()
    {
        var settings = IgniteUIBlazorSettings.Create()
            .WithModulesToLoad(new([typeof(FirstModule)]));

        using var host = new Host(s => s.AddIgniteUIBlazor(settings));

        Assert.Equal([typeof(FirstModule)], SettingsOf(host).ModulesToLoad);
    }

    [Fact]
    public void AddIgniteUIBlazor_WithSettingsAndParams_CombinesBoth()
    {
        var settings = IgniteUIBlazorSettings.Create()
            .WithModulesToLoad(new([typeof(FirstModule)]));

        using var host = new Host(s => s.AddIgniteUIBlazor(settings, typeof(SecondModule)));

        Assert.Equal([typeof(FirstModule), typeof(SecondModule)], SettingsOf(host).ModulesToLoad);
    }

    [Fact]
    public void AddIgniteUIBlazor_WithModuleListedTwice_KeepsOneEntry()
    {
        var settings = IgniteUIBlazorSettings.Create()
            .WithModulesToLoad(new([typeof(FirstModule)]));

        using var host = new Host(s => s.AddIgniteUIBlazor(settings, typeof(FirstModule), typeof(SecondModule)));

        Assert.Equal([typeof(FirstModule), typeof(SecondModule)], SettingsOf(host).ModulesToLoad);
    }

    [Fact]
    public void AddIgniteUIBlazor_WithSettings_PreservesTheOtherSettings()
    {
        var settings = IgniteUIBlazorSettings.Create().WithForceJsonDataMarshalling(true);

        using var host = new Host(s => s.AddIgniteUIBlazor(settings, typeof(FirstModule)));

        Assert.True(SettingsOf(host).ForceJsonDataMarshalling);
    }

    [Fact]
    public void Runtime_InvokesRegisterOnEveryListedModule()
    {
        FirstModule.Registrations.Clear();
        SecondModule.Registrations.Clear();

        using var host = new Host(s => s.AddIgniteUIBlazor(typeof(FirstModule), typeof(SecondModule)));
        var runtime = host.Services.GetRequiredService<IIgniteUIBlazor>();

        Assert.Same(runtime, Assert.Single(FirstModule.Registrations));
        Assert.Same(runtime, Assert.Single(SecondModule.Registrations));
    }

    /// <summary>
    /// Mutating the original array after calling <c>AddIgniteUIBlazor</c> must not change which
    /// modules the settings report. The overload snapshots the array at registration time.
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_MutatingOriginalArray_DoesNotAffectRegisteredModules()
    {
        var modules = new Type[] { typeof(FirstModule) };

        using var host = new Host(s => s.AddIgniteUIBlazor(modules));

        // Mutate after registration, before the first resolve.
        modules[0] = typeof(SecondModule);

        Assert.Equal([typeof(FirstModule)], SettingsOf(host).ModulesToLoad);
    }

    [Fact]
    public void Runtime_IsScoped_SoEachScopeRegistersItsOwn()
    {
        FirstModule.Registrations.Clear();

        using var host = new Host(s => s.AddIgniteUIBlazor(typeof(FirstModule)));

        using (var scope = host.NewScope())
        {
            scope.ServiceProvider.GetRequiredService<IIgniteUIBlazor>();
        }
        using (var scope = host.NewScope())
        {
            scope.ServiceProvider.GetRequiredService<IIgniteUIBlazor>();
        }

        Assert.Equal(2, FirstModule.Registrations.Count);
    }

    /// <summary>
    /// Type-based preload is trim-safe only because every module carries a self-referencing
    /// <c>[IgbModule&lt;TSelf&gt;]</c>. A missing attribute — or one pasted from another
    /// module — silently breaks that module's preload in trimmed apps; this guards both.
    /// </summary>
    [Fact]
    public void EveryLibraryModule_CarriesSelfReferencingIgbModuleAttribute()
    {
        var moduleTypes = typeof(IgbAvatarModule).Assembly.GetTypes()
            .Where(t => t.IsClass && typeof(IIgbModule).IsAssignableFrom(t))
            .ToList();
        Assert.NotEmpty(moduleTypes);

        foreach (var type in moduleTypes)
        {
            var attributeType = type.GetCustomAttributes(inherit: false)
                .Select(a => a.GetType())
                .SingleOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IgbModuleAttribute<>));

            Assert.True(attributeType != null, $"{type.Name} is missing [IgbModule<{type.Name}>].");
            Assert.True(attributeType!.GenericTypeArguments[0] == type,
                $"{type.Name} carries [IgbModule<{attributeType.GenericTypeArguments[0].Name}>] — the attribute must reference its own type.");
        }
    }
}
