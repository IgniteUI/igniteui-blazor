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
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Opened,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Closing,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbExpansionPanel>()[1].Instance, args.Detail))
        .Event(c => c.Closed,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-expansion-panel:nth-of-type(2)")}}}"}}"""),
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
    public void Accordion_SingleExpand_False_NoAttribute()
    {
        var cut = Render<IgbAccordion>(parameters =>
            parameters.Add(p => p.SingleExpand, false));

        Assert.Null(cut.Find("igc-accordion").GetAttribute("single-expand"));
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

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="count"/> <see cref="IgbExpansionPanel"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbAccordion>> AccordionWith(int count) => ps =>
        ps.AddChildContent(builder =>
        {
            for (var i = 0; i < count; i++)
            {
                builder.OpenComponent<IgbExpansionPanel>(i);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void Accordion_ChildPanels_RegisterOnInitialize()
    {
        var cut = Render<IgbAccordion>(AccordionWith(2));

        Assert.Equal(
            cut.FindComponents<IgbExpansionPanel>().Select(p => p.Instance),
            cut.Instance.ContentItems);
    }

    [Fact]
    public void Accordion_DisposedChildPanel_LeavesTheCollection()
    {
        var cut = Render<IgbAccordion>(AccordionWith(2));
        var survivor = cut.FindComponents<IgbExpansionPanel>()[0].Instance;

        cut.Render(AccordionWith(1));

        Assert.Same(survivor, Assert.Single(cut.Instance.ContentItems));
    }

    [Fact]
    public void Accordion_AllChildPanelsDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbAccordion>(AccordionWith(2));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ContentItems);
    }

    #endregion Child collection lifecycle
}
