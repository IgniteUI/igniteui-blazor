using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DropdownTests : BlazorComponentTestBase
{
    [Fact]
    public void Dropdown_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDropdown>();
        Assert.NotNull(cut.Find("igc-dropdown"));
    }

    [Fact]
    public void Dropdown_TypeMetadata_IsCorrect()
    {
        var dropdown = new IgbDropdown();
        Assert.Equal("WebDropdown", dropdown.Type);
    }

    [Fact]
    public void Dropdown_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdown>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-dropdown");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Dropdown_KeepOpenOnSelect_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdown>(parameters =>
            parameters.Add(p => p.KeepOpenOnSelect, true));

        var element = cut.Find("igc-dropdown");
        Assert.NotNull(element.GetAttribute("keep-open-on-select"));
    }

    [Fact]
    public void Dropdown_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbDropdown).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class DropdownItemTests : BlazorComponentTestBase
{
    [Fact]
    public void DropdownItem_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDropdownItem>();
        Assert.NotNull(cut.Find("igc-dropdown-item"));
    }

    [Fact]
    public void DropdownItem_TypeMetadata_IsCorrect()
    {
        var item = new IgbDropdownItem();
        Assert.Equal("WebDropdownItem", item.Type);
    }

    [Fact]
    public void DropdownItem_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Value, "item-1"));

        var element = cut.Find("igc-dropdown-item");
        Assert.Equal("item-1", element.GetAttribute("value"));
    }

    [Fact]
    public void DropdownItem_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-dropdown-item");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void DropdownItem_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-dropdown-item");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void DropdownItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbDropdownItem>(parameters =>
            parameters.AddChildContent("Option 1"));

        Assert.Contains("Option 1", cut.Markup);
    }
}
