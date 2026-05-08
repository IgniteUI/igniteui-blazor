using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ButtonGroupTests : BlazorComponentTestBase
{
    [Fact]
    public void ButtonGroup_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbButtonGroup>();
        cut.Find("igc-button-group").Should_Exist();
    }

    [Fact]
    public void ButtonGroup_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-button-group").GetAttribute("disabled"));
    }

    [Fact]
    public void ButtonGroup_Selection_Single()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.Add(x => x.Selection, ButtonGroupSelection.Single));

        Assert.Equal("single", cut.Find("igc-button-group").GetAttribute("selection"));
    }

    [Fact]
    public void ButtonGroup_Selection_Multiple()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.Add(x => x.Selection, ButtonGroupSelection.Multiple));

        Assert.Equal("multiple", cut.Find("igc-button-group").GetAttribute("selection"));
    }

    [Fact]
    public void ButtonGroup_Selection_SingleRequired()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.Add(x => x.Selection, ButtonGroupSelection.SingleRequired));

        Assert.Equal("single-required", cut.Find("igc-button-group").GetAttribute("selection"));
    }

    [Fact]
    public void ButtonGroup_Alignment_Vertical()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.Add(x => x.Alignment, ContentOrientation.Vertical));

        Assert.Equal("vertical", cut.Find("igc-button-group").GetAttribute("alignment"));
    }

    [Fact]
    public void ButtonGroup_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbButtonGroup>(p =>
            p.AddChildContent("<igc-toggle-button>A</igc-toggle-button>"));

        Assert.Contains("A", cut.Find("igc-button-group").InnerHtml);
    }

    [Fact]
    public void ButtonGroup_TypeMetadata()
    {
        var bg = new IgbButtonGroup();
        Assert.Equal("WebButtonGroup", bg.Type);
    }
}
