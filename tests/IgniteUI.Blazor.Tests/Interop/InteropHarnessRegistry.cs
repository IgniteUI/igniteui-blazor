using Bunit;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests.Interop;

/// <summary>
/// Maps component types to <see cref="InteropHarness"/> implementations.
/// Everything defaults to <see cref="RendererMessageInteropHarness"/> (the current
/// interop stack). As components migrate to new interop infrastructure one by one,
/// register the new harness for just those types — their tests keep working through
/// the same seam while the rest of the suite stays on the default.
/// </summary>
public static class InteropHarnessRegistry
{
    private static readonly Dictionary<Type, Func<BunitJSInterop, InteropHarness>> Overrides = new();

    public static void Register<TComponent>(Func<BunitJSInterop, InteropHarness> factory)
        where TComponent : IComponent
        => Overrides[typeof(TComponent)] = factory;

    public static InteropHarness CreateDefault(BunitJSInterop jsInterop) =>
        new RendererMessageInteropHarness(jsInterop);

    internal static Func<BunitJSInterop, InteropHarness>? OverrideFor(Type componentType) =>
        Overrides.TryGetValue(componentType, out var factory) ? factory : null;
}
