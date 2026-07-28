using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class DialogTests : ComponentWithContractTestBase<IgbDialog>
{
    protected override ComponentContract<IgbDialog> InteropContract { get; } = new ComponentContract<IgbDialog>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Event(c => c.Closing)
        .Event(c => c.Closed);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Dialog_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDialog>();
        Assert.NotNull(cut.Find("igc-dialog"));
    }

    [Fact]
    public void Dialog_TypeMetadata_IsCorrect()
    {
        var dialog = new IgbDialog();
        Assert.Equal("WebDialog", dialog.Type);
    }

    [Fact]
    public void Dialog_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-dialog");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Dialog_Title_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.Title, "Confirm Action"));

        var element = cut.Find("igc-dialog");
        Assert.Equal("Confirm Action", element.GetAttribute("title"));
    }

    [Fact]
    public void Dialog_KeepOpenOnEscape_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.KeepOpenOnEscape, true));

        var element = cut.Find("igc-dialog");
        Assert.NotNull(element.GetAttribute("keep-open-on-escape"));
    }

    [Fact]
    public void Dialog_CloseOnOutsideClick_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.CloseOnOutsideClick, true));

        var element = cut.Find("igc-dialog");
        Assert.NotNull(element.GetAttribute("close-on-outside-click"));
    }

    [Fact]
    public void Dialog_HideDefaultAction_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.HideDefaultAction, true));

        var element = cut.Find("igc-dialog");
        Assert.NotNull(element.GetAttribute("hide-default-action"));
    }

    [Fact]
    public void Dialog_ReturnValue_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.Add(p => p.ReturnValue, "confirmed"));

        var element = cut.Find("igc-dialog");
        Assert.Equal("confirmed", element.GetAttribute("return-value"));
    }

    [Fact]
    public void Dialog_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDialog>(parameters =>
            parameters.AddChildContent("Dialog content here"));

        Assert.Contains("Dialog content here", cut.Markup);
    }

    [Fact]
    public void Dialog_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbDialog).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
