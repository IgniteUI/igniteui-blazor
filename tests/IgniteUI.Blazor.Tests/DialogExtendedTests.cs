using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests covering more properties of Dialog, Accordion, Banner, 
/// ExpansionPanel, and Snackbar/Toast.
/// </summary>
public class DialogExtendedTests : BlazorComponentTestBase
{
    [Fact]
    public void Dialog_HideDefaultAction_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(p =>
            p.Add(x => x.HideDefaultAction, true));

        Assert.NotNull(cut.Find("igc-dialog").GetAttribute("hide-default-action"));
    }

    [Fact]
    public void Dialog_Title_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(p =>
            p.Add(x => x.Title, "Confirm Action"));

        Assert.Equal("Confirm Action", cut.Find("igc-dialog").GetAttribute("title"));
    }

    [Fact]
    public void Dialog_ReturnValue_RendersAttribute()
    {
        var cut = RenderComponent<IgbDialog>(p =>
            p.Add(x => x.ReturnValue, "ok"));

        Assert.Equal("ok", cut.Find("igc-dialog").GetAttribute("return-value"));
    }

    [Fact]
    public void Dialog_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDialog>(p =>
            p.AddChildContent("<p>Are you sure?</p>"));

        Assert.Contains("Are you sure?", cut.Find("igc-dialog").InnerHtml);
    }

    [Fact]
    public void Accordion_SingleExpand_RendersAttribute()
    {
        var cut = RenderComponent<IgbAccordion>(p =>
            p.Add(x => x.SingleExpand, true));

        Assert.NotNull(cut.Find("igc-accordion").GetAttribute("single-expand"));
    }

    [Fact]
    public void Accordion_SingleExpand_False_NoAttribute()
    {
        var cut = RenderComponent<IgbAccordion>(p =>
            p.Add(x => x.SingleExpand, false));

        Assert.Null(cut.Find("igc-accordion").GetAttribute("single-expand"));
    }

    [Fact]
    public void Accordion_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbAccordion>(p =>
            p.AddChildContent("<div>Panel content</div>"));

        Assert.Contains("Panel content", cut.Find("igc-accordion").InnerHtml);
    }

    [Fact]
    public void Banner_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbBanner>(p =>
            p.Add(x => x.Open, true));

        Assert.NotNull(cut.Find("igc-banner").GetAttribute("open"));
    }

    [Fact]
    public void Banner_Open_False_NoAttribute()
    {
        var cut = RenderComponent<IgbBanner>(p =>
            p.Add(x => x.Open, false));

        Assert.Null(cut.Find("igc-banner").GetAttribute("open"));
    }

    [Fact]
    public void Banner_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbBanner>(p =>
            p.AddChildContent("Important notice"));

        Assert.Contains("Important notice", cut.Find("igc-banner").InnerHtml);
    }

    [Fact]
    public void ExpansionPanel_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbExpansionPanel>(p =>
            p.Add(x => x.Open, true)
             .AddChildContent("<p>Panel body</p>"));

        Assert.Contains("Panel body", cut.Find("igc-expansion-panel").InnerHtml);
    }

    [Fact]
    public void Snackbar_ActionText_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(p =>
            p.Add(x => x.ActionText, "Undo"));

        Assert.Equal("Undo", cut.Find("igc-snackbar").GetAttribute("action-text"));
    }

    [Fact]
    public void Snackbar_DisplayTime_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(p =>
            p.Add(x => x.DisplayTime, 5000));

        Assert.Equal("5000", cut.Find("igc-snackbar").GetAttribute("display-time"));
    }

    [Fact]
    public void Snackbar_KeepOpen_RendersAttribute()
    {
        var cut = RenderComponent<IgbSnackbar>(p =>
            p.Add(x => x.KeepOpen, true));

        Assert.NotNull(cut.Find("igc-snackbar").GetAttribute("keep-open"));
    }

    [Fact]
    public void Toast_DisplayTime_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(p =>
            p.Add(x => x.DisplayTime, 3000));

        Assert.Equal("3000", cut.Find("igc-toast").GetAttribute("display-time"));
    }

    [Fact]
    public void Toast_KeepOpen_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(p =>
            p.Add(x => x.KeepOpen, true));

        Assert.NotNull(cut.Find("igc-toast").GetAttribute("keep-open"));
    }

    [Fact]
    public void Toast_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbToast>(p =>
            p.Add(x => x.Open, true));

        Assert.NotNull(cut.Find("igc-toast").GetAttribute("open"));
    }

    [Fact]
    public void Toast_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbToast>(p =>
            p.AddChildContent("File saved"));

        Assert.Contains("File saved", cut.Find("igc-toast").InnerHtml);
    }
}
