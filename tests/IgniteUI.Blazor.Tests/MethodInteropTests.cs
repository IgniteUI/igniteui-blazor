using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Verifies that public component API methods drive the interop layer correctly:
/// right method identifier, right component instance, correctly serialized arguments
/// and argument types, and correctly decoded return values. Components are generated,
/// so each *shape* (no-arg, number+enum, string, object, date return, deferred
/// promise, sync variant) is covered once rather than per component.
/// </summary>
public class MethodInteropTests : BlazorComponentTestBase
{
    private IRenderedComponent<TComponent> RenderReady<TComponent>() where TComponent : IComponent
    {
        Interop.PrimeReady();
        return RenderComponent<TComponent>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task NoArgMethod_SendsInvocation_AndDecodesBooleanReturn(bool result)
    {
        Interop.SetupMethodResult("show", InteropReturn.Bool(result));
        var cut = RenderReady<IgbBanner>();

        var returned = await cut.Instance.ShowAsync();

        Assert.Equal(result, returned);
        var call = Interop.RequireCall("show", Interop.ContainerIdOf(cut));
        Assert.Empty(call.Arguments);
        Assert.Empty(call.Types);
    }

    [Fact]
    public async Task EachApiMethod_MapsToItsOwnJsIdentifier()
    {
        Interop.SetupMethodResult("show", InteropReturn.Bool(true));
        Interop.SetupMethodResult("hide", InteropReturn.Bool(true));
        Interop.SetupMethodResult("toggle", InteropReturn.Bool(true));
        var cut = RenderReady<IgbBanner>();

        await cut.Instance.ShowAsync();
        await cut.Instance.HideAsync();
        await cut.Instance.ToggleAsync();

        var containerId = Interop.ContainerIdOf(cut);
        var methods = Interop.MethodCalls
            .Where(c => c.ContainerId == containerId)
            .Select(c => c.MethodName)
            .ToArray();
        Assert.Equal(new[] { "show", "hide", "toggle" }, methods);
    }

    [Fact]
    public async Task NumberAndEnumArguments_SerializeToWireValues()
    {
        Interop.SetupMethodResult("select", InteropReturn.Bool(true));
        var cut = RenderReady<IgbCarousel>();

        await cut.Instance.SelectAsync(2, CarouselAnimationDirection.Next);

        var call = Interop.RequireCall("select", Interop.ContainerIdOf(cut));
        Assert.Equal(2, call.Arguments[0].GetDouble());
        Assert.Equal("next", call.Arguments[1].GetString()); // camelCase enum wire value
        Assert.Equal(new[] { "Number", "Json" }, call.Types);
    }

    [Fact]
    public async Task StringArgument_SerializesVerbatim()
    {
        var cut = RenderReady<IgbChat>();

        await cut.Instance.ScrollToMessageAsync("message-42");

        var call = Interop.RequireCall("scrollToMessage", Interop.ContainerIdOf(cut));
        Assert.Equal("message-42", call.Arguments[0].GetString());
        Assert.Equal(new[] { "String" }, call.Types);
    }

    [Fact]
    public async Task ObjectArgument_SerializesAsJson()
    {
        var cut = RenderReady<IgbDropdown>();

        await cut.Instance.SelectAsync("item-1");

        var call = Interop.RequireCall("select", Interop.ContainerIdOf(cut));
        Assert.Equal("item-1", call.Arguments[0].GetString());
        Assert.Equal(new[] { "Json" }, call.Types);
    }

    [Fact]
    public async Task PropertyValueRead_DecodesDateReturn()
    {
        var date = new DateTime(2026, 7, 4, 12, 30, 0, DateTimeKind.Utc);
        Interop.SetupMethodResult("p:Value", InteropReturn.Date(date));
        var cut = RenderReady<IgbCalendar>();

        var value = await cut.Instance.GetCurrentValueAsync();

        Assert.Equal(date, value.ToUniversalTime());
        Assert.NotNull(Interop.FindCall("p:Value", Interop.ContainerIdOf(cut)));
    }

    [Fact]
    public async Task PromiseReturn_DefersUntilJsCompletes()
    {
        Interop.SetupMethodResult("toggle", InteropReturn.Promise);
        var cut = RenderReady<IgbBanner>();

        var pending = cut.Instance.ToggleAsync();
        Assert.False(pending.IsCompleted);

        Interop.CompletePromise(Interop.RequireCall("toggle"), InteropReturn.Bool(true));

        Assert.True(await pending);
    }

    [Fact]
    public void SyncMethodVariant_InvokesThroughInProcessRuntime()
    {
        Interop.SetupMethodResult("show", InteropReturn.Bool(true));
        var cut = RenderReady<IgbBanner>();

        var shown = cut.Instance.Show();

        Assert.True(shown);
        Assert.NotNull(Interop.FindCall("show", Interop.ContainerIdOf(cut)));
    }
}
