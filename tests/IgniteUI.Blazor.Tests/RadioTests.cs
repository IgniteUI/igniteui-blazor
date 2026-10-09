using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace IgniteUI.Blazor.Tests;

public abstract class RadioTests<TValue> : ComponentWithContractTestBase<IgbRadio<TValue>>
{
    private readonly TValue _value;

    protected RadioTests(TValue value)
    {
        _value = value;
        // The element's value is always a string on the wire, whatever the TValue.
        var wireValue = $"\"{Convert.ToString(value, CultureInfo.InvariantCulture)}\"";
        var changeArgs = """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": """ + wireValue + "}}}";
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
                argsJson: changeArgs,
                assert: args =>
                {
                    Assert.True(args.Detail.Checked);
                    Assert.Equal(value, args.Detail.Value);
                })
            // The bound value uses checked:
            .Bind(c => c.Checked, c => c.CheckedChanged, via: c => c.Change,
                argsJson: changeArgs,
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
        Assert.True(typeof(IgbRadio<TValue>).IsSubclassOf(typeof(IgbComponentBase)));
    }
}

public sealed class RadioStringTests : RadioTests<string>
{
    public RadioStringTests()
        : base("option1")
    {
    }
}

public sealed class RadioEnumTests : RadioTests<DayOfWeek>
{
    public RadioEnumTests()
        : base(DayOfWeek.Friday)
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

/// <summary>
/// A value type's default is a real option value: without these, the first option of a 0-based list or the
/// "No" of a yes/no group reaches the element with no value and cannot be pre-selected.
/// </summary>
public class RadioNumberCultureTests : BlazorComponentTestBase
{
    /// <summary>
    /// Without this a comma-decimal culture sends "1,5", which the invariant parse on the way back reads as 15.
    /// </summary>
    [Fact]
    public void Radio_Value_Fractional_IsInvariant()
    {
        var culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
        try
        {
            var cut = Render<IgbRadio<double>>(parameters =>
                parameters.Add(p => p.Value, 1.5));

            Assert.Equal("1.5", cut.Find("igc-radio").GetAttribute("value"));
        }
        finally
        {
            CultureInfo.CurrentCulture = culture;
        }
    }

    [Fact]
    public void RadioChangeDetail_Value_Fractional_IsInvariant()
    {
        var culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
        try
        {
            var detail = new IgbRadioChangeEventArgsDetail<double> { Value = 1.5 };

            Assert.Contains("\"value\":\"1.5\"", detail.Serialize());
        }
        finally
        {
            CultureInfo.CurrentCulture = culture;
        }
    }
}

public class RadioDefaultValueTests : BlazorComponentTestBase
{
    [Fact]
    public void Radio_Value_Zero_IsStringified()
    {
        var cut = Render<IgbRadio<int>>(parameters =>
            parameters.Add(p => p.Value, 0));

        Assert.Equal("0", cut.Find("igc-radio").GetAttribute("value"));
    }

    [Fact]
    public void Radio_Value_False_IsStringified()
    {
        var cut = Render<IgbRadio<bool>>(parameters =>
            parameters.Add(p => p.Value, false));

        Assert.Equal("False", cut.Find("igc-radio").GetAttribute("value"));
    }

    [Fact]
    public void RadioGroup_Value_Zero_RendersAttribute()
    {
        var cut = Render<IgbRadioGroup<int>>(parameters =>
            parameters.Add(p => p.Value, 0));

        Assert.Equal("0", cut.Find("igc-radio-group").GetAttribute("value"));
    }

    [Fact]
    public void RadioChangeDetail_Value_Zero_IsSerialized()
    {
        var detail = new IgbRadioChangeEventArgsDetail<int> { Value = 0 };

        Assert.Contains("\"value\":\"0\"", detail.Serialize());
    }
}

public abstract class RadioGroupTests<TValue> : ComponentWithContractTestBase<IgbRadioGroup<TValue>>
{
    private readonly TValue _value;

    protected RadioGroupTests(TValue value)
    {
        _value = value;
        // The element's value is always a string on the wire, whatever the TValue.
        var wireValue = Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        var changeArgs = """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": """ + $"\"{wireValue}\"" + "}}}";
        InteropContract = new ComponentContract<IgbRadioGroup<TValue>>()
            .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value",
                returns: InteropReturn.String(wireValue), expect: value)
            .Event(c => c.Change,
                argsJson: changeArgs,
                assert: args =>
                {
                    Assert.True(args.Detail.Checked);
                    Assert.Equal(value, args.Detail.Value);
                })
            // The group binds the selected option's value:
            .Bind<TValue, IgbRadioChangeEventArgs<TValue>>(c => c.Value!, c => c.ValueChanged, via: c => c.Change,
                argsJson: changeArgs,
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
        Assert.True(typeof(IgbRadioGroup<TValue>).IsSubclassOf(typeof(IgbComponentBase)));
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

public sealed class RadioGroupEnumTests : RadioGroupTests<DayOfWeek>
{
    public RadioGroupEnumTests()
        : base(DayOfWeek.Friday)
    {
    }
}

public sealed class RadioGroupNullableEnumTests : RadioGroupTests<DayOfWeek?>
{
    public RadioGroupNullableEnumTests()
        : base(DayOfWeek.Friday)
    {
    }
}

/// <summary>
/// <see cref="IgbRadioGroup{TValue}"/> declares <c>[CascadingTypeParameter(nameof(TValue))]</c> so Razor can
/// infer a nested <c>&lt;IgbRadio&gt;</c>'s own <c>TValue</c> from its group when the element has no local
/// hint to infer from instead (e.g. no literal assigned to <see cref="IgbRadio{TValue}.Value"/>). The radio
/// itself does not receive or validate any runtime cascading value from the group, so an explicitly
/// mismatched <c>TValue</c> is accepted at render time without error.
/// </summary>
public class RadioGroupCascadingTValueTests : BlazorComponentTestBase
{
    [Fact]
    public void Radio_TValueMatchesGroup_String_RendersSuccessfully()
    {
        var cut = Render<IgbRadioGroup<string>>(ps =>
            ps.AddChildContent<IgbRadio<string>>(child =>
                child.Add(c => c.Value, "option1")));

        Assert.NotNull(cut.Find("igc-radio-group"));
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_TValueMatchesGroup_Int_RendersSuccessfully()
    {
        var cut = Render<IgbRadioGroup<int>>(ps =>
            ps.AddChildContent<IgbRadio<int>>(child =>
                child.Add(c => c.Value, 1)));

        Assert.NotNull(cut.Find("igc-radio-group"));
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_TValueMatchesGroup_NullableDateTime_RendersSuccessfully()
    {
        var cut = Render<IgbRadioGroup<DateTime?>>(ps =>
            ps.AddChildContent<IgbRadio<DateTime?>>(child =>
                child.Add(c => c.Value, new DateTime(2026, 1, 1))));

        Assert.NotNull(cut.Find("igc-radio-group"));
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_NoGroup_StandaloneRadio_RendersSuccessfully()
    {
        var cut = Render<IgbRadio<string>>(parameters =>
            parameters.Add(p => p.Value, "a"));

        Assert.NotNull(cut.Find("igc-radio"));
    }

    /// <summary>
    /// An explicitly mismatched <c>TValue</c> (as if Razor's own inference picked a different type than
    /// the group's) is not validated at runtime: the radio renders using its own <c>TValue</c>, independent
    /// of the containing group.
    /// </summary>
    [Fact]
    public void Radio_TValueMismatch_StringGroup_IntRadio_RendersWithoutError()
    {
        var cut = Render<IgbRadioGroup<string>>(ps =>
            ps.AddChildContent<IgbRadio<int>>(child =>
                child.Add(c => c.Value, 1)));

        Assert.NotNull(cut.Find("igc-radio-group"));
        var radio = cut.Find("igc-radio");
        Assert.Equal("1", radio.GetAttribute("value"));
    }

    [Fact]
    public void Radio_TValueMismatch_IntGroup_StringRadio_RendersWithoutError()
    {
        var cut = Render<IgbRadioGroup<int>>(ps =>
            ps.AddChildContent<IgbRadio<string>>(child =>
                child.Add(c => c.Value, "a")));

        Assert.NotNull(cut.Find("igc-radio-group"));
        var radio = cut.Find("igc-radio");
        Assert.Equal("a", radio.GetAttribute("value"));
    }

    [Fact]
    public void Radio_TValueMismatch_StringGroup_DateTimeRadio_RendersWithoutError()
    {
        var cut = Render<IgbRadioGroup<string>>(ps =>
            ps.AddChildContent<IgbRadio<DateTime>>(child =>
                child.Add(c => c.Value, new DateTime(2026, 1, 1))));

        Assert.NotNull(cut.Find("igc-radio-group"));
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_MultipleRadiosInSameGroup_AllMatchingTValue_RendersSuccessfully()
    {
        var cut = Render<IgbRadioGroup<string>>(ps =>
            ps.AddChildContent(builder =>
            {
                builder.OpenComponent<IgbRadio<string>>(0);
                builder.AddAttribute(1, "Value", "a");
                builder.CloseComponent();

                builder.OpenComponent<IgbRadio<string>>(2);
                builder.AddAttribute(3, "Value", "b");
                builder.CloseComponent();
            }));

        Assert.Equal(2, cut.FindAll("igc-radio").Count);
    }

    [Fact]
    public void Radio_MultipleRadiosInSameGroup_OneMismatchedTValue_RendersWithoutError()
    {
        var cut = Render<IgbRadioGroup<string>>(ps =>
            ps.AddChildContent(builder =>
            {
                builder.OpenComponent<IgbRadio<string>>(0);
                builder.AddAttribute(1, "Value", "a");
                builder.CloseComponent();

                builder.OpenComponent<IgbRadio<int>>(2);
                builder.AddAttribute(3, "Value", 1);
                builder.CloseComponent();
            }));

        Assert.Equal(2, cut.FindAll("igc-radio").Count);
    }
}
