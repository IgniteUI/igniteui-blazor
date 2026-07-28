using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests for Badge, Avatar, Chip, Icon, Ripple, Divider properties.
/// </summary>
public class BadgeExtendedTests : BlazorComponentTestBase
{
    // Badge extended
    [Fact]
    public void Badge_Variant_Info()
    {
        var cut = Render<IgbBadge>(p =>
            p.Add(x => x.Variant, StyleVariant.Info));

        Assert.Equal("info", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Success()
    {
        var cut = Render<IgbBadge>(p =>
            p.Add(x => x.Variant, StyleVariant.Success));

        Assert.Equal("success", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Warning()
    {
        var cut = Render<IgbBadge>(p =>
            p.Add(x => x.Variant, StyleVariant.Warning));

        Assert.Equal("warning", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_Variant_Danger()
    {
        var cut = Render<IgbBadge>(p =>
            p.Add(x => x.Variant, StyleVariant.Danger));

        Assert.Equal("danger", cut.Find("igc-badge").GetAttribute("variant"));
    }

    [Fact]
    public void Badge_ChildContent_Renders()
    {
        var cut = Render<IgbBadge>(p =>
            p.AddChildContent("99+"));

        Assert.Contains("99+", cut.Find("igc-badge").InnerHtml);
    }

    // Avatar extended
    [Fact]
    public void Avatar_Shape_Circle()
    {
        var cut = Render<IgbAvatar>(p =>
            p.Add(x => x.Shape, AvatarShape.Circle));

        Assert.Equal("circle", cut.Find("igc-avatar").GetAttribute("shape"));
    }

    [Fact]
    public void Avatar_Shape_Rounded()
    {
        var cut = Render<IgbAvatar>(p =>
            p.Add(x => x.Shape, AvatarShape.Rounded));

        Assert.Equal("rounded", cut.Find("igc-avatar").GetAttribute("shape"));
    }

    [Fact]
    public void Avatar_Shape_Square()
    {
        var cut = Render<IgbAvatar>(p =>
            p.Add(x => x.Shape, AvatarShape.Square));

        Assert.Equal("square", cut.Find("igc-avatar").GetAttribute("shape"));
    }

    [Fact]
    public void Avatar_ChildContent_Renders()
    {
        var cut = Render<IgbAvatar>(p =>
            p.AddChildContent("<img src=\"avatar.png\" />"));

        Assert.Contains("img", cut.Find("igc-avatar").InnerHtml);
    }

    // Chip extended
    [Fact]
    public void Chip_Variant_Primary()
    {
        var cut = Render<IgbChip>(p =>
            p.Add(x => x.Variant, StyleVariant.Primary));

        Assert.Equal("primary", cut.Find("igc-chip").GetAttribute("variant"));
    }

    [Fact]
    public void Chip_Variant_Info()
    {
        var cut = Render<IgbChip>(p =>
            p.Add(x => x.Variant, StyleVariant.Info));

        Assert.Equal("info", cut.Find("igc-chip").GetAttribute("variant"));
    }

    [Fact]
    public void Chip_Variant_Success()
    {
        var cut = Render<IgbChip>(p =>
            p.Add(x => x.Variant, StyleVariant.Success));

        Assert.Equal("success", cut.Find("igc-chip").GetAttribute("variant"));
    }

    [Fact]
    public void Chip_Variant_Warning()
    {
        var cut = Render<IgbChip>(p =>
            p.Add(x => x.Variant, StyleVariant.Warning));

        Assert.Equal("warning", cut.Find("igc-chip").GetAttribute("variant"));
    }

    [Fact]
    public void Chip_Variant_Danger()
    {
        var cut = Render<IgbChip>(p =>
            p.Add(x => x.Variant, StyleVariant.Danger));

        Assert.Equal("danger", cut.Find("igc-chip").GetAttribute("variant"));
    }

    [Fact]
    public void Chip_ChildContent_Renders()
    {
        var cut = Render<IgbChip>(p =>
            p.AddChildContent("Technology"));

        Assert.Contains("Technology", cut.Find("igc-chip").InnerHtml);
    }

    // Icon extended
    [Fact]
    public void Icon_ChildContent_Renders()
    {
        var cut = Render<IgbIcon>(p =>
            p.AddChildContent("<svg></svg>"));

        Assert.Contains("svg", cut.Find("igc-icon").InnerHtml);
    }

    // Ripple
    [Fact]
    public void Ripple_RendersCorrectElement()
    {
        var cut = Render<IgbRipple>();
        cut.Find("igc-ripple").Should_Exist();
    }

    // Divider extended
    [Fact]
    public void Divider_RendersCorrectElement()
    {
        var cut = Render<IgbDivider>();
        cut.Find("igc-divider").Should_Exist();
    }

    [Fact]
    public void Divider_LineType_Dashed()
    {
        var cut = Render<IgbDivider>(p =>
            p.Add(x => x.LineType, DividerType.Dashed));

        Assert.Equal("dashed", cut.Find("igc-divider").GetAttribute("type"));
    }
}
