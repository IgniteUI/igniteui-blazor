using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class BannerTests : ComponentWithContractTestBase<IgbBanner>
{
    protected override ComponentContract<IgbBanner> InteropContract { get; } = new ComponentContract<IgbBanner>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Event(c => c.Closing)
        .Event(c => c.Closed);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Banner_RendersCorrectElement()
    {
        var cut = Render<IgbBanner>();
        Assert.NotNull(cut.Find("igc-banner"));
    }

    [Fact]
    public void Banner_TypeMetadata_IsCorrect()
    {
        var banner = new IgbBanner();
        Assert.Equal("WebBanner", banner.Type);
    }

    [Fact]
    public void Banner_Open_RendersAttribute()
    {
        var cut = Render<IgbBanner>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-banner");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Banner_Open_False_NoAttribute()
    {
        var cut = Render<IgbBanner>(parameters =>
            parameters.Add(p => p.Open, false));

        Assert.Null(cut.Find("igc-banner").GetAttribute("open"));
    }

    [Fact]
    public void Banner_ChildContent_Renders()
    {
        var cut = Render<IgbBanner>(parameters =>
            parameters.AddChildContent("Important notice"));

        Assert.Contains("Important notice", cut.Find("igc-banner").InnerHtml);
    }

    [Fact]
    public void Banner_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBanner).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
