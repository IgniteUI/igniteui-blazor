using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class AvatarTests : BlazorComponentTestBase
{
    [Fact]
    public void Avatar_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbAvatar>();
        Assert.NotNull(cut.Find("igc-avatar"));
    }

    [Fact]
    public void Avatar_TypeMetadata_IsCorrect()
    {
        var avatar = new IgbAvatar();
        Assert.Equal("WebAvatar", avatar.Type);
    }

    [Fact]
    public void Avatar_Src_RendersAttribute()
    {
        var cut = RenderComponent<IgbAvatar>(parameters =>
            parameters.Add(p => p.Src, "https://example.com/avatar.png"));

        var element = cut.Find("igc-avatar");
        Assert.Equal("https://example.com/avatar.png", element.GetAttribute("src"));
    }

    [Fact]
    public void Avatar_Alt_RendersAttribute()
    {
        var cut = RenderComponent<IgbAvatar>(parameters =>
            parameters.Add(p => p.Alt, "User avatar"));

        var element = cut.Find("igc-avatar");
        Assert.Equal("User avatar", element.GetAttribute("alt"));
    }

    [Fact]
    public void Avatar_Initials_RendersAttribute()
    {
        var cut = RenderComponent<IgbAvatar>(parameters =>
            parameters.Add(p => p.Initials, "JD"));

        var element = cut.Find("igc-avatar");
        Assert.Equal("JD", element.GetAttribute("initials"));
    }

    [Fact]
    public void Avatar_Shape_Circle()
    {
        var cut = RenderComponent<IgbAvatar>(parameters =>
            parameters.Add(p => p.Shape, AvatarShape.Circle));

        var element = cut.Find("igc-avatar");
        Assert.Equal("circle", element.GetAttribute("shape"));
    }

    [Fact]
    public void Avatar_Shape_Rounded()
    {
        var cut = RenderComponent<IgbAvatar>(parameters =>
            parameters.Add(p => p.Shape, AvatarShape.Rounded));

        var element = cut.Find("igc-avatar");
        Assert.Equal("rounded", element.GetAttribute("shape"));
    }

    [Fact]
    public void Avatar_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbAvatar).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
