using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class SelectTests : BlazorComponentTestBase
{
    [Fact]
    public void Select_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSelect>();
        Assert.NotNull(cut.Find("igc-select"));
    }

    [Fact]
    public void Select_TypeMetadata_IsCorrect()
    {
        var select = new IgbSelect();
        Assert.Equal("WebSelect", select.Type);
    }

    [Fact]
    public void Select_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-select");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Select_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-select");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Select_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-select");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Select_Placeholder_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(parameters =>
            parameters.Add(p => p.Placeholder, "Choose..."));

        var element = cut.Find("igc-select");
        Assert.Equal("Choose...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Select_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelect>(parameters =>
            parameters.Add(p => p.Label, "Country"));

        var element = cut.Find("igc-select");
        Assert.Equal("Country", element.GetAttribute("label"));
    }

    [Fact]
    public void Select_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbSelect).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class SelectItemTests : BlazorComponentTestBase
{
    [Fact]
    public void SelectItem_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSelectItem>();
        Assert.NotNull(cut.Find("igc-select-item"));
    }

    [Fact]
    public void SelectItem_TypeMetadata_IsCorrect()
    {
        var item = new IgbSelectItem();
        Assert.Equal("WebSelectItem", item.Type);
    }

    [Fact]
    public void SelectItem_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelectItem>(parameters =>
            parameters.Add(p => p.Value, "us"));

        var element = cut.Find("igc-select-item");
        Assert.Equal("us", element.GetAttribute("value"));
    }

    [Fact]
    public void SelectItem_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelectItem>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-select-item");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void SelectItem_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbSelectItem>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-select-item");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void SelectItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSelectItem>(parameters =>
            parameters.AddChildContent("United States"));

        Assert.Contains("United States", cut.Markup);
    }
}
