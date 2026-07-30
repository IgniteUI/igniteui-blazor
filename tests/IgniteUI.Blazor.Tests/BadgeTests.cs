using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class BadgeTests : BlazorComponentTestBase
{
    [Fact]
    public void Badge_RendersCorrectElement()
    {
        var cut = Render<IgbBadge>();
        Assert.NotNull(cut.Find("igc-badge"));
    }

    [Fact]
    public void Badge_TypeMetadata_IsCorrect()
    {
        var badge = new IgbBadge();
        Assert.Equal("WebBadge", badge.Type);
    }

    [Fact]
    public void Badge_Outlined_RendersAttribute()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Outlined, true));

        var element = cut.Find("igc-badge");
        Assert.NotNull(element.GetAttribute("outlined"));
    }

    [Fact]
    public void Badge_Dot_RendersAttribute()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Dot, true));

        var element = cut.Find("igc-badge");
        Assert.NotNull(element.GetAttribute("dot"));
    }

    [Fact]
    public void Badge_Shape_Rounded()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Shape, BadgeShape.Rounded));

        var element = cut.Find("igc-badge");
        Assert.Equal("rounded", element.GetAttribute("shape"));
    }

    [Fact]
    public void Badge_Shape_Square()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Shape, BadgeShape.Square));

        var element = cut.Find("igc-badge");
        Assert.Equal("square", element.GetAttribute("shape"));
    }

    [Fact]
    public void Badge_Variant_RendersAttribute()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Danger));

        var element = cut.Find("igc-badge");
        Assert.Equal("danger", element.GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Info()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Info));

        Assert.Equal("info", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Success()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Success));

        Assert.Equal("success", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Warning()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Warning));

        Assert.Equal("warning", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_ChildContent_Renders()
    {
        var cut = Render<IgbBadge>(parameters =>
            parameters.AddChildContent("99+"));

        Assert.Contains("99+", cut.Find("igc-badge").InnerHtml);
    }

    [Fact]
    public void Badge_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBadge).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
