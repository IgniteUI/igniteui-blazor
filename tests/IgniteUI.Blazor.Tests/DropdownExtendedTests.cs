using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests for Dropdown sub-components and properties.
/// </summary>
public class DropdownExtendedTests : BlazorComponentTestBase
{
    [Fact]
    public void Dropdown_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDropdown>(p =>
            p.AddChildContent("<igc-dropdown-item>Item</igc-dropdown-item>"));

        Assert.Contains("Item", cut.Find("igc-dropdown").InnerHtml);
    }

    [Fact]
    public void DropdownGroup_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDropdownGroup>();
        cut.Find("igc-dropdown-group").Should_Exist();
    }

    [Fact]
    public void DropdownGroup_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDropdownGroup>(p =>
            p.AddChildContent("<igc-dropdown-item>Grouped</igc-dropdown-item>"));

        Assert.Contains("Grouped", cut.Find("igc-dropdown-group").InnerHtml);
    }

    [Fact]
    public void DropdownHeader_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDropdownHeader>();
        cut.Find("igc-dropdown-header").Should_Exist();
    }

    [Fact]
    public void DropdownHeader_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDropdownHeader>(p =>
            p.AddChildContent("Category"));

        Assert.Contains("Category", cut.Find("igc-dropdown-header").InnerHtml);
    }

    [Fact]
    public void DropdownItem_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdownItem>(p =>
            p.Add(x => x.Value, "item-1"));

        Assert.Equal("item-1", cut.Find("igc-dropdown-item").GetAttribute("value"));
    }

    [Fact]
    public void DropdownItem_Active_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdownItem>(p =>
            p.Add(x => x.Active, true));

        Assert.NotNull(cut.Find("igc-dropdown-item").GetAttribute("active"));
    }

    [Fact]
    public void DropdownItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDropdownItem>(p =>
            p.AddChildContent("Option A"));

        Assert.Contains("Option A", cut.Find("igc-dropdown-item").InnerHtml);
    }
}
