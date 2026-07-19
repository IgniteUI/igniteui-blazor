using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Base class for component tests providing shared test context setup.
/// JS interop runs on bUnit's recording runtime in loose mode (unconfigured calls
/// return <c>default</c>, like the previous swallow-everything mock, but every
/// invocation is recorded). Interop traffic is observed and driven through the
/// <see cref="InteropHarness"/> seam rather than against the wire format directly.
/// </summary>
public abstract class BlazorComponentTestBase : TestContext
{
    protected IIgniteUIBlazor IgniteUIBlazor { get; }

    /// <summary>The interop harness for components on the default (current) interop stack.</summary>
    protected InteropHarness Interop { get; }

    private Dictionary<Func<BunitJSInterop, InteropHarness>, InteropHarness>? _overrideHarnesses;

    protected BlazorComponentTestBase()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Interop = InteropHarnessRegistry.CreateDefault(JSInterop);
        Interop.ConfigureServices(Services);
        IgniteUIBlazor = Interop.Service;
    }

    /// <summary>
    /// Resolves the interop harness for a component type: the shared default unless the
    /// type was remapped in <see cref="InteropHarnessRegistry"/> (used while migrating
    /// components to new interop infrastructure one by one). Call it before rendering
    /// the component so a remapped harness can register its services first.
    /// </summary>
    protected InteropHarness InteropFor<TComponent>() where TComponent : IComponent
    {
        var factory = InteropHarnessRegistry.OverrideFor(typeof(TComponent));
        if (factory is null)
        {
            return Interop;
        }

        _overrideHarnesses ??= new();
        if (!_overrideHarnesses.TryGetValue(factory, out var harness))
        {
            harness = factory(JSInterop);
            harness.ConfigureServices(Services);
            _overrideHarnesses[factory] = harness;
        }
        return harness;
    }
}
