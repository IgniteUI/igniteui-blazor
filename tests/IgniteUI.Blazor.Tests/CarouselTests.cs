using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class CarouselTests : ComponentWithContractTestBase<IgbCarousel>
{
    protected override ComponentContract<IgbCarousel> InteropContract { get; } = new ComponentContract<IgbCarousel>()
        .Method(c => c.PlayAsync(), c => c.Play(), "play")
        .Method(c => c.PauseAsync(), c => c.Pause(), "pause")
        .Method(c => c.NextAsync(), c => c.Next(), "next", returns: true)
        .Method(c => c.PrevAsync(), c => c.Prev(), "prev", returns: false)
        .Method(c => c.SelectAsync(2, CarouselAnimationDirection.Next), c => c.Select(2, CarouselAnimationDirection.Next),
            "select", returns: true,
            args: [2.0, "next"], types: ["Number", "Json"])
        .Getter(c => c.GetTotalAsync(), c => c.GetTotal(), "Total", returns: 5.0)
        .Getter(c => c.GetCurrentAsync(), c => c.GetCurrent(), "Current", returns: 2.0)
        .Getter(c => c.GetIsPlayingAsync(), c => c.GetIsPlaying(), "IsPlaying", returns: true)
        .Getter(c => c.GetIsPausedAsync(), c => c.GetIsPaused(), "IsPaused", returns: false)
        .Event(c => c.SlideChanged,
            argsJson: """{"detail": 3}""",
            assert: args => Assert.Equal(3, args.Detail))
        .Event(c => c.Playing)
        .Event(c => c.Paused);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Carousel_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCarousel>();
        cut.Find("igc-carousel").Should_Exist();
    }

    [Fact]
    public void Carousel_DisableLoop_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.DisableLoop, true));

        Assert.NotNull(cut.Find("igc-carousel").GetAttribute("disable-loop"));
    }

    [Fact]
    public void Carousel_HideNavigation_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.HideNavigation, true));

        Assert.NotNull(cut.Find("igc-carousel").GetAttribute("hide-navigation"));
    }

    [Fact]
    public void Carousel_HideIndicators_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.HideIndicators, true));

        Assert.NotNull(cut.Find("igc-carousel").GetAttribute("hide-indicators"));
    }

    [Fact]
    public void Carousel_Vertical_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.Vertical, true));

        Assert.NotNull(cut.Find("igc-carousel").GetAttribute("vertical"));
    }

    [Fact]
    public void Carousel_DisablePauseOnInteraction_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.DisablePauseOnInteraction, true));

        Assert.NotNull(cut.Find("igc-carousel").GetAttribute("disable-pause-on-interaction"));
    }

    [Fact]
    public void Carousel_IndicatorsOrientation_Start()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.IndicatorsOrientation, CarouselIndicatorsOrientation.Start));

        Assert.Equal("start", cut.Find("igc-carousel").GetAttribute("indicators-orientation"));
    }

    [Fact]
    public void Carousel_AnimationType_Fade()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.AnimationType, HorizontalTransitionAnimation.Fade));

        Assert.Equal("fade", cut.Find("igc-carousel").GetAttribute("animation-type"));
    }

    [Fact]
    public void Carousel_AnimationType_None()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.AnimationType, HorizontalTransitionAnimation.None));

        Assert.Equal("none", cut.Find("igc-carousel").GetAttribute("animation-type"));
    }

    [Fact]
    public void Carousel_Interval_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.Interval, 5000));

        Assert.Equal("5000", cut.Find("igc-carousel").GetAttribute("interval"));
    }

    [Fact]
    public void Carousel_MaximumIndicatorsCount_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.Add(x => x.MaximumIndicatorsCount, 10));

        Assert.Equal("10", cut.Find("igc-carousel").GetAttribute("maximum-indicators-count"));
    }

    [Fact]
    public void Carousel_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCarousel>(p =>
            p.AddChildContent("<div>Slide Content</div>"));

        cut.Find("igc-carousel").InnerHtml.Contains("Slide Content");
    }

    [Fact]
    public void CarouselSlide_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCarouselSlide>();
        cut.Find("igc-carousel-slide").Should_Exist();
    }

    [Fact]
    public void CarouselSlide_Active_RendersAttribute()
    {
        var cut = RenderComponent<IgbCarouselSlide>(p =>
            p.Add(x => x.Active, true));

        Assert.NotNull(cut.Find("igc-carousel-slide").GetAttribute("active"));
    }

    [Fact]
    public void CarouselIndicator_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCarouselIndicator>();
        cut.Find("igc-carousel-indicator").Should_Exist();
    }
}
