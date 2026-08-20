using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class MaskInputTests : ComponentWithContractTestBase<IgbMaskInput>
{
    // Interop contract — source: src/components/Blazor/MaskInput.cs, src/components/Blazor/InputBase.cs.
    // Skipped: none (SetNativeElementAsync/SetNativeElement are globally excluded).
    // IgbMaskInput does not override UseDirectRender, so — like IgbDateTimeInput — it renders
    // through the component-renderer-container and all its scalar props travel as .Prop updates.
    protected override ComponentContract<IgbMaskInput> InteropContract { get; } = new ComponentContract<IgbMaskInput>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "hello")
        .Method(c => c.SetSelectionRangeAsync(1, 3, "forward"), c => c.SetSelectionRange(1, 3, "forward"),
            "setSelectionRange", args: [1.0, 3.0, "forward"], types: ["Number", "Number", "String"])
        .Method(c => c.SetRangeTextAsync("abc", 1, 3, "end"), c => c.SetRangeText("abc", 1, 3, "end"),
            "setRangeText", args: ["abc", 1.0, 3.0, "end"], types: ["String", "Number", "Number", "String"])
        .Method(c => c.SelectAsync(), c => c.Select(), "select")
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }),
            "focus", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": "new value"}""", assert: args => Assert.Equal("new value", args.Detail))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "new value"}""", expect: "new value")
        .Event(c => c.InputOcurred,
            argsJson: """{"detail": "typed text"}""", assert: args => Assert.Equal("typed text", args.Detail))
        .Event(c => c.Focus)
        .Event(c => c.Blur)
        .Prop(c => c.ValueMode, MaskInputValueMode.WithFormatting, wire: "withFormatting")
        .Prop(c => c.Value, "555-1234")
        .Prop(c => c.Mask, "000-0000")
        .Prop(c => c.Prompt, "*")
        .Prop(c => c.ReadOnly, true)
        .Prop(c => c.Outlined, true)
        .Prop(c => c.Placeholder, "Enter value")
        .Prop(c => c.Label, "Phone")
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_RendersCorrectElement()
    {
        var cut = Render<IgbMaskInput>();
        Assert.NotNull(cut.Find("igc-mask-input"));
    }

    [Fact]
    public void MaskInput_TypeMetadata_IsCorrect()
    {
        var mask = new IgbMaskInput();
        Assert.Equal("WebMaskInput", mask.Type);
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Mask_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Mask, "000-000-0000"));

        var element = cut.Find("igc-mask-input");
        Assert.Equal("000-000-0000", element.GetAttribute("mask"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Disabled_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-mask-input");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_ValueMode_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.ValueMode, MaskInputValueMode.WithFormatting));

        var element = cut.Find("igc-mask-input");
        Assert.Equal("withFormatting", element.GetAttribute("value-mode"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Prompt_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Prompt, "#"));

        var element = cut.Find("igc-mask-input");
        Assert.Equal("#", element.GetAttribute("prompt"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Label_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Label, "Phone"));

        var element = cut.Find("igc-mask-input");
        Assert.Equal("Phone", element.GetAttribute("label"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Required_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-mask-input");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_Outlined_RendersAttribute()
    {
        var cut = Render<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Outlined, true));

        var element = cut.Find("igc-mask-input");
        Assert.NotNull(element.GetAttribute("outlined"));
    }
}
