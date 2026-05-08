using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class IconTests : BlazorComponentTestBase
{
    [Fact]
    public void Icon_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbIcon>();
        Assert.NotNull(cut.Find("igc-icon"));
    }

    [Fact]
    public void Icon_TypeMetadata_IsCorrect()
    {
        var icon = new IgbIcon();
        Assert.Equal("WebIcon", icon.Type);
    }

    [Fact]
    public void Icon_Name_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.IconName, "home"));

        var element = cut.Find("igc-icon");
        Assert.Equal("home", element.GetAttribute("name"));
    }

    [Fact]
    public void Icon_Collection_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.Collection, "material"));

        var element = cut.Find("igc-icon");
        Assert.Equal("material", element.GetAttribute("collection"));
    }

    [Fact]
    public void Icon_Mirrored_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.Mirrored, true));

        var element = cut.Find("igc-icon");
        Assert.NotNull(element.GetAttribute("mirrored"));
    }

    [Fact]
    public void Icon_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbIcon).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
