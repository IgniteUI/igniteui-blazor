using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SnackbarTests : ComponentWithContractTestBase<IgbSnackbar>
{
    protected override ComponentContract<IgbSnackbar> InteropContract { get; } = new ComponentContract<IgbSnackbar>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Event(c => c.Action);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Snackbar_RendersCorrectElement()
    {
        var cut = Render<IgbSnackbar>();
        Assert.NotNull(cut.Find("igc-snackbar"));
    }

    [Fact]
    public void Snackbar_TypeMetadata_IsCorrect()
    {
        var snackbar = new IgbSnackbar();
        Assert.Equal("WebSnackbar", snackbar.Type);
    }

    [Fact]
    public void Snackbar_InheritsFromBaseAlertLike()
    {
        Assert.True(typeof(IgbSnackbar).IsSubclassOf(typeof(IgbBaseAlertLike)));
    }

    [Fact]
    public void Snackbar_ActionText_RendersAttribute()
    {
        var cut = Render<IgbSnackbar>(parameters =>
            parameters.Add(p => p.ActionText, "UNDO"));

        var element = cut.Find("igc-snackbar");
        Assert.Equal("UNDO", element.GetAttribute("action-text"));
    }

    [Fact]
    public void Snackbar_Open_RendersAttribute()
    {
        var cut = Render<IgbSnackbar>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-snackbar");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Snackbar_DisplayTime_RendersAttribute()
    {
        var cut = Render<IgbSnackbar>(parameters =>
            parameters.Add(p => p.DisplayTime, 5000.0));

        var element = cut.Find("igc-snackbar");
        Assert.Equal("5000", element.GetAttribute("display-time"));
    }

    [Fact]
    public void Snackbar_KeepOpen_RendersAttribute()
    {
        var cut = Render<IgbSnackbar>(parameters =>
            parameters.Add(p => p.KeepOpen, true));

        var element = cut.Find("igc-snackbar");
        Assert.NotNull(element.GetAttribute("keep-open"));
    }

    [Fact]
    public void Snackbar_ChildContent_Renders()
    {
        var cut = Render<IgbSnackbar>(parameters =>
            parameters.AddChildContent("Item deleted"));

        Assert.Contains("Item deleted", cut.Markup);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbSnackbar</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Snackbar_DefaultValues_MatchWebComponent()
    {
        var snackbar = new IgbSnackbar();

        Assert.Equal(4000, snackbar.DisplayTime);
    }
}
