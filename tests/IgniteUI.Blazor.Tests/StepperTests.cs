using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class StepperTests : ComponentWithContractTestBase<IgbStepper>
{
    /// <summary> Static arrange for the GetSteps contract test adding two child steps. </summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbStepper>> arrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbStep>(0);
            builder.AddAttribute(1, "id", "step-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbStep>(2);
            builder.AddAttribute(3, "id", "step-2");
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbStepper> InteropContract { get; } = new ComponentContract<IgbStepper>()
        .Method(c => c.NavigateToAsync(1), c => c.NavigateTo(1), "navigateTo", args: [1.0], types: ["Number"])
        .Method(c => c.NextAsync(), c => c.Next(), "next")
        .Method(c => c.PrevAsync(), c => c.Prev(), "prev")
        .Method(c => c.ResetAsync(), c => c.Reset(), "reset")
        .Getter(c => c.GetStepsAsync(), c => c.GetSteps(), "Steps",
            arrange,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-step:nth-of-type(1)")}}}"}, {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-step:nth-of-type(2)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result!.Length);
                // TODO: IgbStep has no CascadingParameter registration and no FindByNameStepper impl
                // so the refs currently resolve to null elements
            })
        .Event(c => c.ActiveStepChanging,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"oldIndex": 0, "newIndex": 1}}}""",
            assert: args =>
            {
                Assert.Equal(0, args!.Detail!.OldIndex);
                Assert.Equal(1, args.Detail.NewIndex);
            })
        .Event(c => c.ActiveStepChanged,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"index": 1}}}""",
            assert: args => Assert.Equal(1, args!.Detail!.Index));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Stepper_RendersCorrectElement()
    {
        var cut = Render<IgbStepper>();
        Assert.NotNull(cut.Find("igc-stepper"));
    }

    [Fact]
    public void Stepper_TypeMetadata_IsCorrect()
    {
        var stepper = new IgbStepper();
        Assert.Equal("WebStepper", stepper.Type);
    }

    [Fact]
    public void Stepper_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbStepper).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class StepTests : BlazorComponentTestBase
{
    [Fact]
    public void Step_RendersCorrectElement()
    {
        var cut = Render<IgbStep>();
        Assert.NotNull(cut.Find("igc-step"));
    }

    [Fact]
    public void Step_TypeMetadata_IsCorrect()
    {
        var step = new IgbStep();
        Assert.Equal("WebStep", step.Type);
    }

    [Fact]
    public void Step_Disabled_RendersAttribute()
    {
        var cut = Render<IgbStep>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-step");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Step_ChildContent_Renders()
    {
        var cut = Render<IgbStep>(parameters =>
            parameters.AddChildContent("Step Content"));

        Assert.Contains("Step Content", cut.Markup);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbStepper</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Stepper_DefaultValues_MatchWebComponent()
    {
        var stepper = new IgbStepper();

        Assert.Equal(320, stepper.AnimationDuration);
    }
}
