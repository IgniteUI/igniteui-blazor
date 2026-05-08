using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class StepperTests : BlazorComponentTestBase
{
    [Fact]
    public void Stepper_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbStepper>();
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
        var cut = RenderComponent<IgbStep>();
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
        var cut = RenderComponent<IgbStep>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-step");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Step_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbStep>(parameters =>
            parameters.AddChildContent("Step Content"));

        Assert.Contains("Step Content", cut.Markup);
    }
}
