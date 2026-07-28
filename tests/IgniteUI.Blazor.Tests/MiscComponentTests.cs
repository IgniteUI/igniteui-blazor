using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class AccordionTests : ComponentWithContractTestBase<IgbAccordion>
{
    /// <summary> Static arrange for contract tests adding two child panel items </summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbAccordion>> arrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbExpansionPanel>(0);
            builder.AddAttribute(1, "id", "panel-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbExpansionPanel>(2);
            builder.AddAttribute(3, "id", "panel-2");
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbAccordion> InteropContract { get; } = new ComponentContract<IgbAccordion>()
        .Method(c => c.HideAllAsync(), c => c.HideAll(), "hideAll")
        .Method(c => c.ShowAllAsync(), c => c.ShowAll(), "showAll")
        .Event(c => c.Opening,
            arrange,
            argsJson: (interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}""",
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Opened,
            arrange,
            argsJson: (interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}""",
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Closing,
            arrange,
            argsJson: (interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}""",
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Closed,
            arrange,
            argsJson: (interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}""",
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Accordion_RendersCorrectElement()
    {
        var cut = Render<IgbAccordion>();
        Assert.NotNull(cut.Find("igc-accordion"));
    }

    [Fact]
    public void Accordion_TypeMetadata_IsCorrect()
    {
        var accordion = new IgbAccordion();
        Assert.Equal("WebAccordion", accordion.Type);
    }

    [Fact]
    public void Accordion_SingleExpand_RendersAttribute()
    {
        var cut = Render<IgbAccordion>(parameters =>
            parameters.Add(p => p.SingleExpand, true));

        var element = cut.Find("igc-accordion");
        Assert.NotNull(element.GetAttribute("single-expand"));
    }

    [Fact]
    public void Accordion_ChildContent_Renders()
    {
        var cut = Render<IgbAccordion>(parameters =>
            parameters.AddChildContent("Accordion content"));

        Assert.Contains("Accordion content", cut.Markup);
    }

    [Fact]
    public void Accordion_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbAccordion).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

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
    public void Banner_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBanner).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

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

public class RippleTests : BlazorComponentTestBase
{
    [Fact]
    public void Ripple_RendersCorrectElement()
    {
        var cut = Render<IgbRipple>();
        Assert.NotNull(cut.Find("igc-ripple"));
    }

    [Fact]
    public void Ripple_TypeMetadata_IsCorrect()
    {
        var ripple = new IgbRipple();
        Assert.Equal("WebRipple", ripple.Type);
    }
}
