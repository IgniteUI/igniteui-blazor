using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public abstract class RadioTests<TValue> : ComponentWithContractTestBase<IgbRadio<TValue>>
{
    private readonly TValue _value;

    protected RadioTests(TValue value)
    {
        _value = value;
        var wireValue = value is string ? $"\"{value}\"" : value!.ToString();
        InteropContract = new ComponentContract<IgbRadio<TValue>>()
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
                argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": """ + wireValue + "}}}",
                assert: args =>
                {
                    Assert.True(args.Detail.Checked);
                    Assert.Equal(value, args.Detail.Value);
                })
            // The bound value uses checked:
            .Bind(c => c.Checked, c => c.CheckedChanged, via: c => c.Change,
                argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": """ + wireValue + "}}}",
                expect: true)
            .Event(c => c.Focus)
            .Event(c => c.Blur);
    }

    protected override ComponentContract<IgbRadio<TValue>> InteropContract { get; }

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Radio_RendersCorrectElement()
    {
        var cut = Render<IgbRadio<TValue>>();
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_TypeMetadata_IsCorrect()
    {
        var radio = new IgbRadio<TValue>();
        Assert.Equal("WebRadio", radio.RendererType);
    }

    [Fact]
    public void Radio_Value_RendersAttribute()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.Value, _value));

        var element = cut.Find("igc-radio");
        Assert.Equal(_value, cut.Instance.Value);
        Assert.Equal(_value?.ToString(), element.GetAttribute("value"));
    }

    [Fact]
    public void Radio_Checked_RendersAttribute()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-radio");
        Assert.True(cut.Instance.Checked);
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Radio_Disabled_RendersAttribute()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-radio");
        Assert.True(cut.Instance.Disabled);
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Radio_Required_RendersAttribute()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-radio");
        Assert.True(cut.Instance.Required);
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Radio_LabelPosition_Before()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.LabelPosition, ToggleLabelPosition.Before));

        var element = cut.Find("igc-radio");
        Assert.Equal(ToggleLabelPosition.Before, cut.Instance.LabelPosition);
        Assert.Equal("before", element.GetAttribute("label-position"));
    }

    [Fact]
    public void Radio_Invalid_RendersAttribute()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.Add(p => p.Invalid, true));

        var element = cut.Find("igc-radio");
        Assert.True(cut.Instance.Invalid);
        Assert.NotNull(element.GetAttribute("invalid"));
    }

    [Fact]
    public void Radio_ChildContent_Renders()
    {
        var cut = Render<IgbRadio<TValue>>(parameters =>
            parameters.AddChildContent("Option A"));

        Assert.Contains("Option A", cut.Markup);
    }

    [Fact]
    public void Radio_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadio<TValue>).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public sealed class RadioStringTests : RadioTests<string>
{
    public RadioStringTests()
        : base("option1")
    {
    }
}

public sealed class RadioIntTests : RadioTests<int>
{
    public RadioIntTests()
        : base(42)
    {
    }

    /// <summary>
    /// A numeric <c>TValue</c> is not a special-cased type (unlike <c>DateTime</c>/<c>string</c>),
    /// so it must fall back to <see cref="object.ToString"/> when stringified onto the wire.
    /// </summary>
    [Fact]
    public void Radio_Value_Number_IsStringified()
    {
        var cut = Render<IgbRadio<int>>(parameters =>
            parameters.Add(p => p.Value, 42));

        var element = cut.Find("igc-radio");
        Assert.Equal(42, cut.Instance.Value);
        Assert.Equal("42", element.GetAttribute("value"));
    }
}

public abstract class RadioGroupTests<TValue> : ComponentWithContractTestBase<IgbRadioGroup<TValue>>
{
    private readonly TValue _value;

    protected RadioGroupTests(TValue value)
    {
        _value = value;
        InteropContract = new ComponentContract<IgbRadioGroup<TValue>>()
            .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: value)
            .Event(c => c.Change,
                argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "selected-option"}}}""",
                assert: args =>
                {
                    Assert.True(args.Detail.Checked);
                    Assert.Equal(value, args.Detail.Value);
                })
            // The group binds the selected option's value:
            .Bind<TValue, IgbRadioChangeEventArgs<TValue>>(c => c.Value!, c => c.ValueChanged, via: c => c.Change,
                argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "selected-option"}}}""",
                expect: value);
    }

    protected override ComponentContract<IgbRadioGroup<TValue>> InteropContract { get; }

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void RadioGroup_RendersCorrectElement()
    {
        var cut = Render<IgbRadioGroup<TValue>>();
        Assert.NotNull(cut.Find("igc-radio-group"));
    }

    [Fact]
    public void RadioGroup_TypeMetadata_IsCorrect()
    {
        var group = new IgbRadioGroup<TValue>();
        Assert.Equal("WebRadioGroup", group.RendererType);
    }

    [Fact]
    public void RadioGroup_Alignment_Vertical()
    {
        var cut = Render<IgbRadioGroup<TValue>>(parameters =>
            parameters.Add(p => p.Alignment, ContentOrientation.Vertical));

        var element = cut.Find("igc-radio-group");
        Assert.Equal("vertical", element.GetAttribute("alignment"));
    }

    [Fact]
    public void RadioGroup_Value_RendersAttribute()
    {
        var cut = Render<IgbRadioGroup<TValue>>(parameters =>
            parameters.Add(p => p.Value, _value));

        var element = cut.Find("igc-radio-group");
        Assert.Equal(_value?.ToString(), element.GetAttribute("value"));
    }

    [Fact]
    public void RadioGroup_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadioGroup<TValue>).IsSubclassOf(typeof(BaseRendererControl)));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbRadioGroup{TValue}</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void RadioGroup_DefaultValues_MatchWebComponent()
    {
        var group = new IgbRadioGroup<TValue>();

        Assert.Equal(ContentOrientation.Vertical, group.Alignment);
    }
}

public sealed class RadioGroupStringTests : RadioGroupTests<string>
{
    public RadioGroupStringTests()
        : base("selected-option")
    {
    }
}
