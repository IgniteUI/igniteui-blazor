using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class SnackbarTests : BlazorComponentTestBase
{
    [Fact]
    public void Snackbar_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSnackbar>();
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
        var cut = RenderComponent<IgbSnackbar>(parameters =>
            parameters.Add(p => p.ActionText, "UNDO"));

        var element = cut.Find("igc-snackbar");
        Assert.Equal("UNDO", element.GetAttribute("action-text"));
    }

    [Fact]
    public void Snackbar_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-snackbar");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Snackbar_DisplayTime_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(parameters =>
            parameters.Add(p => p.DisplayTime, 5000.0));

        var element = cut.Find("igc-snackbar");
        Assert.Equal("5000", element.GetAttribute("display-time"));
    }

    [Fact]
    public void Snackbar_KeepOpen_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(parameters =>
            parameters.Add(p => p.KeepOpen, true));

        var element = cut.Find("igc-snackbar");
        Assert.NotNull(element.GetAttribute("keep-open"));
    }

    [Fact]
    public void Snackbar_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSnackbar>(parameters =>
            parameters.AddChildContent("Item deleted"));

        Assert.Contains("Item deleted", cut.Markup);
    }
}

public class ToastTests : BlazorComponentTestBase
{
    [Fact]
    public void Toast_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbToast>();
        Assert.NotNull(cut.Find("igc-toast"));
    }

    [Fact]
    public void Toast_TypeMetadata_IsCorrect()
    {
        var toast = new IgbToast();
        Assert.Equal("WebToast", toast.Type);
    }

    [Fact]
    public void Toast_InheritsFromBaseAlertLike()
    {
        Assert.True(typeof(IgbToast).IsSubclassOf(typeof(IgbBaseAlertLike)));
    }

    [Fact]
    public void Toast_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-toast");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Toast_DisplayTime_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(parameters =>
            parameters.Add(p => p.DisplayTime, 3000.0));

        var element = cut.Find("igc-toast");
        Assert.Equal("3000", element.GetAttribute("display-time"));
    }

    [Fact]
    public void Toast_KeepOpen_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(parameters =>
            parameters.Add(p => p.KeepOpen, true));

        var element = cut.Find("igc-toast");
        Assert.NotNull(element.GetAttribute("keep-open"));
    }

    [Fact]
    public void Toast_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbToast>(parameters =>
            parameters.AddChildContent("Operation successful"));

        Assert.Contains("Operation successful", cut.Markup);
    }
}
