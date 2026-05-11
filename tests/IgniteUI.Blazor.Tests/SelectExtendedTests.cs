using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests for Select extended properties and sub-components.
/// </summary>
public class SelectExtendedTests : BlazorComponentTestBase
{
    [Fact]
    public void Select_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(p =>
            p.Add(x => x.Label, "Choose option"));

        Assert.Equal("Choose option", cut.Find("igc-select").GetAttribute("label"));
    }

    [Fact]
    public void Select_Outlined_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-select").GetAttribute("outlined"));
    }

    [Fact]
    public void Select_Autofocus_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(p =>
            p.Add(x => x.Autofocus, true));

        Assert.NotNull(cut.Find("igc-select").GetAttribute("autofocus"));
    }

    [Fact]
    public void Select_Invalid_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(p =>
            p.Add(x => x.Invalid, true));

        Assert.NotNull(cut.Find("igc-select").GetAttribute("invalid"));
    }

    [Fact]
    public void Select_Distance_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(p =>
            p.Add(x => x.Distance, 8));

        Assert.Equal("8", cut.Find("igc-select").GetAttribute("distance"));
    }

    [Fact]
    public void SelectGroup_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSelectGroup>();
        cut.Find("igc-select-group").Should_Exist();
    }

    [Fact]
    public void SelectGroup_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelectGroup>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-select-group").GetAttribute("disabled"));
    }

    [Fact]
    public void SelectHeader_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSelectHeader>();
        cut.Find("igc-select-header").Should_Exist();
    }

    [Fact]
    public void SelectHeader_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSelectHeader>(p =>
            p.AddChildContent("Group A"));

        Assert.Contains("Group A", cut.Find("igc-select-header").InnerHtml);
    }

    [Fact]
    public void SelectItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSelectItem>(p =>
            p.Add(x => x.Value, "opt1")
             .AddChildContent("Option 1"));

        Assert.Contains("Option 1", cut.Find("igc-select-item").InnerHtml);
    }
}
