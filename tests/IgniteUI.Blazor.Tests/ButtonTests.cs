using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ButtonTests : BlazorComponentTestBase
{
    [Fact]
    public void Button_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbButton>();
        cut.Find("igc-button").Should_Exist();
    }

    [Fact]
    public void Button_DefaultVariant_IsContained()
    {
        var cut = RenderComponent<IgbButton>();
        var element = cut.Find("igc-button");
        // Default variant should not emit attribute (only dirty props are serialized)
        Assert.NotNull(element);
    }

    [Fact]
    public void Button_SetVariant_Flat()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.Variant, ButtonVariant.Flat));

        var element = cut.Find("igc-button");
        Assert.Equal("flat", element.GetAttribute("variant"));
    }

    [Fact]
    public void Button_SetVariant_Outlined()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.Variant, ButtonVariant.Outlined));

        var element = cut.Find("igc-button");
        Assert.Equal("outlined", element.GetAttribute("variant"));
    }

    [Fact]
    public void Button_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-button");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Button_NotDisabled_NoAttribute()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.Disabled, false));

        var element = cut.Find("igc-button");
        // false booleans are not rendered
        Assert.Null(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Button_Href_RendersAttribute()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.Href, "https://example.com"));

        var element = cut.Find("igc-button");
        Assert.Equal("https://example.com", element.GetAttribute("href"));
    }

    [Fact]
    public void Button_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.AddChildContent("Click Me"));

        cut.Find("igc-button").MarkupMatches(@"<igc-button class=""igb-web-button"" data-ig-id:ignore>Click Me</igc-button>");
    }

    [Fact]
    public void Button_TypeMetadata_IsCorrect()
    {
        var button = new IgbButton();
        Assert.Equal("WebButton", button.Type);
    }

    [Fact]
    public void Button_InheritsFromButtonBase()
    {
        Assert.True(typeof(IgbButton).IsSubclassOf(typeof(IgbButtonBase)));
    }

    [Fact]
    public void Button_DisplayType_RendersCorrectly()
    {
        var cut = RenderComponent<IgbButton>(parameters =>
            parameters.Add(p => p.DisplayType, ButtonBaseType.Submit));

        var element = cut.Find("igc-button");
        Assert.Equal("submit", element.GetAttribute("type"));
    }
}

internal static class ElementAssertionExtensions
{
    public static void Should_Exist(this AngleSharp.Dom.IElement element)
    {
        Assert.NotNull(element);
    }
}
