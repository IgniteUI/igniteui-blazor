using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ChipTests : ComponentWithContractTestBase<IgbChip>
{
    protected override ComponentContract<IgbChip> InteropContract { get; } = new ComponentContract<IgbChip>()
        .Getter(c => c.GetCurrentSelectedAsync(), c => c.GetCurrentSelected(), "Selected", returns: true)
        .Event(c => c.Select,
            argsJson: """{"detail": true}""",
            assert: args => Assert.True(args.Detail))
        .Event(c => c.Remove,
            argsJson: """{"detail": true}""",
            assert: args => Assert.True(args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Chip_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbChip>();
        Assert.NotNull(cut.Find("igc-chip"));
    }

    [Fact]
    public void Chip_TypeMetadata_IsCorrect()
    {
        var chip = new IgbChip();
        Assert.Equal("WebChip", chip.Type);
    }

    [Fact]
    public void Chip_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-chip");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Chip_Removable_RendersAttribute()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(p => p.Removable, true));

        var element = cut.Find("igc-chip");
        Assert.NotNull(element.GetAttribute("removable"));
    }

    [Fact]
    public void Chip_Selectable_RendersAttribute()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(p => p.Selectable, true));

        var element = cut.Find("igc-chip");
        Assert.NotNull(element.GetAttribute("selectable"));
    }

    [Fact]
    public void Chip_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-chip");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void Chip_Variant_RendersAttribute()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Info));

        var element = cut.Find("igc-chip");
        Assert.Equal("info", element.GetAttribute("variant"));
    }

    [Fact]
    public void Chip_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.AddChildContent("Tag Label"));

        Assert.Contains("Tag Label", cut.Markup);
    }

    [Fact]
    public void Chip_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbChip).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
