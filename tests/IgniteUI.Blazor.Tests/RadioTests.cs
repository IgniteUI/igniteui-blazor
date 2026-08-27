using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class RadioTests : ComponentWithContractTestBase<IgbRadio>
{
    protected override ComponentContract<IgbRadio> InteropContract { get; } = new ComponentContract<IgbRadio>()
        .Getter(c => c.GetCurrentCheckedAsync(), c => c.GetCurrentChecked(), "Checked", returns: true)
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }),
            "focus", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.ClickAsync(), c => c.Click(), "click")
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Please select an option"), c => c.SetCustomValidity("Please select an option"),
            "setCustomValidity", args: ["Please select an option"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "option1"}}}""",
            assert: args =>
            {
                Assert.True(args.Detail!.Checked);
                Assert.Equal("option1", args.Detail.Value);
            })
        // The bound value uses checked:
        .Bind(c => c.Checked, c => c.CheckedChanged, via: c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "option1"}}}""",
            expect: true)
        .Event(c => c.Focus)
        .Event(c => c.Blur);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Radio_RendersCorrectElement()
    {
        var cut = Render<IgbRadio>();
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_TypeMetadata_IsCorrect()
    {
        var radio = new IgbRadio();
        Assert.Equal("WebRadio", radio.Type);
    }

    [Fact]
    public void Radio_Value_RendersAttribute()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.Add(p => p.Value, "option1"));

        var element = cut.Find("igc-radio");
        Assert.Equal("option1", element.GetAttribute("value"));
    }

    [Fact]
    public void Radio_Checked_RendersAttribute()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Radio_Disabled_RendersAttribute()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Radio_Required_RendersAttribute()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Radio_LabelPosition_Before()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.Add(p => p.LabelPosition, ToggleLabelPosition.Before));

        var element = cut.Find("igc-radio");
        Assert.Equal("before", element.GetAttribute("label-position"));
    }

    [Fact]
    public void Radio_ChildContent_Renders()
    {
        var cut = Render<IgbRadio>(parameters =>
            parameters.AddChildContent("Option A"));

        Assert.Contains("Option A", cut.Markup);
    }

    [Fact]
    public void Radio_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadio).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class RadioGroupTests : ComponentWithContractTestBase<IgbRadioGroup>
{
    protected override ComponentContract<IgbRadioGroup> InteropContract { get; } = new ComponentContract<IgbRadioGroup>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "selected-option")
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "selected-option"}}}""",
            assert: args =>
            {
                Assert.True(args.Detail!.Checked);
                Assert.Equal("selected-option", args.Detail.Value);
            })
        // The group binds the selected option's value:
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "selected-option"}}}""",
            expect: "selected-option");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void RadioGroup_RendersCorrectElement()
    {
        var cut = Render<IgbRadioGroup>();
        Assert.NotNull(cut.Find("igc-radio-group"));
    }

    [Fact]
    public void RadioGroup_TypeMetadata_IsCorrect()
    {
        var group = new IgbRadioGroup();
        Assert.Equal("WebRadioGroup", group.Type);
    }

    [Fact]
    public void RadioGroup_Alignment_Vertical()
    {
        var cut = Render<IgbRadioGroup>(parameters =>
            parameters.Add(p => p.Alignment, ContentOrientation.Vertical));

        var element = cut.Find("igc-radio-group");
        Assert.Equal("vertical", element.GetAttribute("alignment"));
    }

    [Fact]
    public void RadioGroup_Value_RendersAttribute()
    {
        var cut = Render<IgbRadioGroup>(parameters =>
            parameters.Add(p => p.Value, "selected-option"));

        var element = cut.Find("igc-radio-group");
        Assert.Equal("selected-option", element.GetAttribute("value"));
    }

    [Fact]
    public void RadioGroup_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadioGroup).IsSubclassOf(typeof(BaseRendererControl)));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbRadioGroup</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void RadioGroup_DefaultValues_MatchWebComponent()
    {
        var group = new IgbRadioGroup();

        Assert.Equal(ContentOrientation.Vertical, group.Alignment);
    }
}
