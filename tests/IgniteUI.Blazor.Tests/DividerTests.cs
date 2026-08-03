using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DividerTests : BlazorComponentTestBase
{
    [Fact]
    public void Divider_RendersCorrectElement()
    {
        var cut = Render<IgbDivider>();
        Assert.NotNull(cut.Find("igc-divider"));
    }

    [Fact]
    public void Divider_TypeMetadata_IsCorrect()
    {
        var divider = new IgbDivider();
        Assert.Equal("WebDivider", divider.Type);
    }

    [Fact]
    public void Divider_Type_Dashed()
    {
        var cut = Render<IgbDivider>(parameters =>
            parameters.Add(p => p.LineType, DividerType.Dashed));

        var element = cut.Find("igc-divider");
        Assert.Equal("dashed", element.GetAttribute("type"));
    }

    [Fact]
    public void Divider_Middle_RendersAttribute()
    {
        var cut = Render<IgbDivider>(parameters =>
            parameters.Add(p => p.Middle, true));

        var element = cut.Find("igc-divider");
        Assert.NotNull(element.GetAttribute("middle"));
    }

    [Fact]
    public void Divider_Vertical_RendersAttribute()
    {
        var cut = Render<IgbDivider>(parameters =>
            parameters.Add(p => p.Vertical, true));

        var element = cut.Find("igc-divider");
        Assert.NotNull(element.GetAttribute("vertical"));
    }
}
