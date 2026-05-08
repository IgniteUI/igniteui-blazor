using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class IconButtonTests : BlazorComponentTestBase
{
    [Fact]
    public void IconButton_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbIconButton>();
        cut.Find("igc-icon-button").Should_Exist();
    }

    [Fact]
    public void IconButton_IconName_RendersAsNameAttribute()
    {
        // IconName has WCWidgetMemberName("Name") → serialized key "iconName" → remapped to "name"
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.IconName, "home"));

        Assert.Equal("home", cut.Find("igc-icon-button").GetAttribute("name"));
    }

    [Fact]
    public void IconButton_Collection_RendersAttribute()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Collection, "material"));

        Assert.Equal("material", cut.Find("igc-icon-button").GetAttribute("collection"));
    }

    [Fact]
    public void IconButton_Mirrored_RendersAttribute()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Mirrored, true));

        Assert.NotNull(cut.Find("igc-icon-button").GetAttribute("mirrored"));
    }

    [Fact]
    public void IconButton_Variant_Flat()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Variant, IconButtonVariant.Flat));

        Assert.Equal("flat", cut.Find("igc-icon-button").GetAttribute("variant"));
    }

    [Fact]
    public void IconButton_Variant_Outlined()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Variant, IconButtonVariant.Outlined));

        Assert.Equal("outlined", cut.Find("igc-icon-button").GetAttribute("variant"));
    }

    [Fact]
    public void IconButton_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-icon-button").GetAttribute("disabled"));
    }

    [Fact]
    public void IconButton_Href_RendersAttribute()
    {
        var cut = RenderComponent<IgbIconButton>(p =>
            p.Add(x => x.Href, "https://example.com"));

        Assert.Equal("https://example.com", cut.Find("igc-icon-button").GetAttribute("href"));
    }

    [Fact]
    public void IconButton_InheritsFromButtonBase()
    {
        Assert.True(typeof(IgbIconButton).IsSubclassOf(typeof(IgbButtonBase)));
    }

    [Fact]
    public void IconButton_TypeMetadata()
    {
        var btn = new IgbIconButton();
        Assert.Equal("WebIconButton", btn.Type);
    }
}
