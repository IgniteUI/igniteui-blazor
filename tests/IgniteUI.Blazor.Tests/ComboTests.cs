using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ComboTests : BlazorComponentTestBase
{
    [Fact]
    public void Combo_TypeMetadata()
    {
        var combo = new IgbCombo<object>();
        Assert.Equal("WebCombo", combo.Type);
    }

    [Fact]
    public void Combo_Label_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Label = "Select item";
        Assert.Equal("Select item", combo.Label);
    }

    [Fact]
    public void Combo_Placeholder_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Placeholder = "Choose...";
        Assert.Equal("Choose...", combo.Placeholder);
    }

    [Fact]
    public void Combo_PlaceholderSearch_Property()
    {
        var combo = new IgbCombo<object>();
        combo.PlaceholderSearch = "Search...";
        Assert.Equal("Search...", combo.PlaceholderSearch);
    }

    [Fact]
    public void Combo_Open_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Open = true;
        Assert.True(combo.Open);
    }

    [Fact]
    public void Combo_Disabled_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Disabled = true;
        Assert.True(combo.Disabled);
    }

    [Fact]
    public void Combo_Required_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Required = true;
        Assert.True(combo.Required);
    }

    [Fact]
    public void Combo_Outlined_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Outlined = true;
        Assert.True(combo.Outlined);
    }

    [Fact]
    public void Combo_SingleSelect_Property()
    {
        var combo = new IgbCombo<object>();
        combo.SingleSelect = true;
        Assert.True(combo.SingleSelect);
    }

    [Fact]
    public void Combo_Autofocus_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Autofocus = true;
        Assert.True(combo.Autofocus);
    }

    [Fact]
    public void Combo_ValueKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.ValueKey = "id";
        Assert.Equal("id", combo.ValueKey);
    }

    [Fact]
    public void Combo_DisplayKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.DisplayKey = "name";
        Assert.Equal("name", combo.DisplayKey);
    }

    [Fact]
    public void Combo_GroupKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.GroupKey = "category";
        Assert.Equal("category", combo.GroupKey);
    }

    [Fact]
    public void Combo_DisableFiltering_Property()
    {
        var combo = new IgbCombo<object>();
        combo.DisableFiltering = true;
        Assert.True(combo.DisableFiltering);
    }

    [Fact]
    public void Combo_Invalid_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Invalid = true;
        Assert.True(combo.Invalid);
    }

    [Fact]
    public void Combo_CaseSensitiveIcon_Property()
    {
        var combo = new IgbCombo<object>();
        combo.CaseSensitiveIcon = true;
        Assert.True(combo.CaseSensitiveIcon);
    }
}
