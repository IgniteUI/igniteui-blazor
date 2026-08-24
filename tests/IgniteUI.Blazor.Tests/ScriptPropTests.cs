using System.Reflection;
using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Automated sweep over every generated <c>&lt;Member&gt;Script</c> parameter: setting one
/// must transmit a script reference for the target member, carrying the script name.
/// Unlike contract members (whose identifiers/args/decodes are component-specific facts),
/// Script props are a single codegen template with a mechanically derived wire name — so
/// one reflection-enumerated sweep pins the pipeline for all components at once and
/// cannot go stale (new components/props are picked up automatically). Per-component
/// interop migration is handled the same way contracts handle it: the harness is
/// resolved through <see cref="InteropHarnessRegistry"/>, so a remapped component is
/// swept through its new stack's harness — the wire form is harness-owned, the asserted
/// semantics ("the script name crosses, keyed to the target member") are not. Only a
/// component whose script *semantics* genuinely diverge graduates to an explicit
/// contract entry with the divergent expectation. No per-contract entries otherwise
/// (the integration TestBed does not exercise these either, making this their only
/// coverage).
/// </summary>
public class ScriptPropTests : BlazorComponentTestBase
{
    public static TheoryData<Type> ComponentsWithScriptProps()
    {
        var data = new TheoryData<Type>();
        foreach (var type in typeof(IgbBanner).Assembly.GetTypes())
        {
            if (type.IsAbstract || !typeof(BaseRendererControl).IsAssignableFrom(type))
            {
                continue;
            }
            var closed = type.IsGenericTypeDefinition ? type.MakeGenericType(typeof(object)) : type;
            if (ScriptPropsOf(closed).Any())
            {
                data.Add(closed);
            }
        }
        return data;
    }

    private static IEnumerable<PropertyInfo> ScriptPropsOf(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(string)
                && p.Name.EndsWith("Script", StringComparison.Ordinal)
                && p.CanWrite
                && p.GetCustomAttribute<ParameterAttribute>() is not null);

    [Theory]
    [MemberData(nameof(ComponentsWithScriptProps))]
    public void ScriptProps_TransmitScriptRefs(Type componentType)
    {
        // Resolved through the registry, so a component remapped to a new interop stack
        // is swept through its own harness — the assertion is stack-invariant.
        var interop = InteropFor(componentType);
        interop.PrimeReady();

        foreach (var prop in ScriptPropsOf(componentType))
        {
            var scriptName = $"handle{prop.Name}";
            // OpenComponent takes the runtime Type directly — no generic RenderComponent
            // reflection; the typed base handle exposes Instance for the round-trip check.
            var cut = Render(builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(1, prop.Name, scriptName);
                builder.CloseComponent();
            }).FindComponent<ComponentBase>();

            Assert.Equal(scriptName, prop.GetValue(cut.Instance));
            // The ref targets the member the script stands in for: "ClosingScript" -> "closing".
            var member = prop.Name[..^"Script".Length];
            var wireName = char.ToLowerInvariant(member[0]) + member[1..];

            var actual = interop.FindPropertyUpdate(interop.ContainerIdOf(cut), wireName);
            if (actual is null)
            {
                throw new Xunit.Sdk.XunitException(
                    $"{componentType.Name}.{prop.Name}: no script-ref transmission observed for \"{wireName}\"");
            }
            Assert.Equal(scriptName, actual.Value.GetString());

            // Clear:
            interop.ClearObserved();
            prop.SetValue(cut.Instance, null);
            var cleared = interop.FindPropertyUpdate(interop.ContainerIdOf(cut), wireName);
            if (cleared is null)
            {
                throw new Xunit.Sdk.XunitException(
                    $"{componentType.Name}.{prop.Name}: clearing it transmitted no \"{wireName}\" ref");
            }
            Assert.Equal(JsonValueKind.Null, cleared.Value.ValueKind);
        }
    }
}
