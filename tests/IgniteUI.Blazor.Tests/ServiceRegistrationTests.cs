using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Covers <c>AddIgniteUIBlazor</c> and the settings object it builds: which services are
/// registered, and how modules preload through the typed <see cref="IgbModuleRef"/> params,
/// the <see cref="IgbModuleCollection"/> configurator, and the legacy Type collection carried
/// by settings. Module registration itself is asserted through the runtime, which invokes
/// each module's static <c>Register</c> as it is constructed.
/// </summary>
public class ServiceRegistrationTests
{
    /// <summary>Stand-in resource module: same shape as the component modules, recording its calls.</summary>
    private sealed class FirstModule : IIgbModule
    {
        public static readonly List<IIgniteUIBlazor> Registrations = [];
        public static void Register(IIgniteUIBlazor runtime) => Registrations.Add(runtime);
    }

    private sealed class SecondModule : IIgbModule
    {
        public static readonly List<IIgniteUIBlazor> Registrations = [];
        public static void Register(IIgniteUIBlazor runtime) => Registrations.Add(runtime);
    }

    /// <summary>Not a module: used to assert the typed reference rejects arbitrary types.</summary>
    private sealed class NotAModule
    {
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
        Assert.Null(SettingsOf(host).ModuleRegistrations);
    }

    /// <summary>
    /// Module types convert implicitly to <see cref="IgbModuleRef"/>, so params call sites keep
    /// the familiar <c>typeof(...)</c> shape while the registration resolves eagerly instead of
    /// reflecting over an unannotated Type at runtime (which trimming would silently break).
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_ParamsModules_AreExposedAsRegistrations()
    {
        using var host = new Host(s => s.AddIgniteUIBlazor(typeof(FirstModule), typeof(SecondModule)));

        Assert.Null(SettingsOf(host).ModulesToLoad);
        Assert.Equal(2, SettingsOf(host).ModuleRegistrations!.Count);
    }

    [Fact]
    public void IgbModuleRef_RejectsTypesThatAreNotModules()
    {
        Assert.Throws<ArgumentException>(() => new IgbModuleRef(typeof(NotAModule)));
        Assert.Throws<ArgumentNullException>(() => new IgbModuleRef(null));
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

    /// <summary>
    /// The legacy Type collection carried by the settings and the typed params register through
    /// their respective paths — the runtime invokes both.
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_WithSettingsAndParams_RegistersThroughBothPaths()
    {
        FirstModule.Registrations.Clear();
        SecondModule.Registrations.Clear();
        var settings = IgniteUIBlazorSettings.Create()
            .WithModulesToLoad(new([typeof(FirstModule)]));

        using var host = new Host(s => s.AddIgniteUIBlazor(settings, typeof(SecondModule)));
        var runtime = host.Services.GetRequiredService<IIgniteUIBlazor>();

        Assert.Same(runtime, Assert.Single(FirstModule.Registrations));
        Assert.Same(runtime, Assert.Single(SecondModule.Registrations));
    }

    /// <summary>
    /// A module listed both in the settings' legacy Type collection and in the typed params has
    /// its <c>Register</c> invoked by each path. That is harmless for real modules — client
    /// resource loading dedupes by module name in the runtime — and callers should simply not
    /// list a module twice.
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_WithModuleListedInBothPaths_InvokesRegisterPerPath()
    {
        FirstModule.Registrations.Clear();
        var settings = IgniteUIBlazorSettings.Create()
            .WithModulesToLoad(new([typeof(FirstModule)]));

        using var host = new Host(s => s.AddIgniteUIBlazor(settings, typeof(FirstModule)));
        host.Services.GetRequiredService<IIgniteUIBlazor>();

        Assert.Equal(2, FirstModule.Registrations.Count);
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
    /// modules register. The overload resolves each reference at registration time.
    /// </summary>
    [Fact]
    public void AddIgniteUIBlazor_MutatingOriginalArray_DoesNotAffectRegisteredModules()
    {
        FirstModule.Registrations.Clear();
        SecondModule.Registrations.Clear();
        var modules = new IgbModuleRef[] { typeof(FirstModule) };

        using var host = new Host(s => s.AddIgniteUIBlazor(modules));

        // Mutate after registration, before the first resolve.
        modules[0] = typeof(SecondModule);
        host.Services.GetRequiredService<IIgniteUIBlazor>();

        Assert.Single(FirstModule.Registrations);
        Assert.Empty(SecondModule.Registrations);
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
}
