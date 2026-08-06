using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class DateTimeInputTests : ComponentWithContractTestBase<IgbDateTimeInput>
{
    protected override ComponentContract<IgbDateTimeInput> InteropContract { get; } = new ComponentContract<IgbDateTimeInput>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: new DateTime(2026, 7, 4, 12, 30, 0, DateTimeKind.Utc))
        .Method(c => c.StepUpAsync(DatePart.Month, 2), c => c.StepUp(DatePart.Month, 2), "stepUp", args: ["month", 2.0], types: ["Json", "Number"])
        .Method(c => c.StepDownAsync(DatePart.Hours, 3), c => c.StepDown(DatePart.Hours, 3), "stepDown", args: ["hours", 3.0], types: ["Json", "Number"])
        .Method(c => c.ClearAsync(), c => c.Clear(), "clear")
        .Method(c => c.SelectAsync(), c => c.Select(), "select")
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }), "focus",
            args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.HasDatePartsAsync(), c => c.HasDateParts(), "hasDateParts", returns: true)
        .Method(c => c.HasTimePartsAsync(), c => c.HasTimeParts(), "hasTimeParts", returns: false)
        .Method(c => c.SetSelectionRangeAsync(1, 3, "forward"), c => c.SetSelectionRange(1, 3, "forward"), "setSelectionRange",
            args: [1.0, 3.0, "forward"], types: ["Number", "Number", "String"])
        .Method(c => c.SetRangeTextAsync("abc", 1, 3, "end"), c => c.SetRangeText("abc", 1, 3, "end"), "setRangeText",
            args: ["abc", 1.0, 3.0, "end"], types: ["String", "Number", "Number", "String"])
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"), "setCustomValidity",
            args: ["custom message"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            assert: args => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), args.Detail.ToUniversalTime()))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            expect: new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc))
        .Event(c => c.InputOcurred,
            argsJson: """{"detail": "typed text"}""", assert: args => Assert.Equal("typed text", args.Detail))
        .Event(c => c.Focus)
        .Event(c => c.Blur)
        .Prop(c => c.Outlined, true)
        .Prop(c => c.Placeholder, "Enter date")
        .Prop(c => c.Label, "Date")
        .Prop(c => c.InputFormat, "dd/MM/yyyy")
        .Prop(c => c.Min, new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc))
        .Prop(c => c.Max, new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc))
        .Prop(c => c.DisplayFormat, "MMMM dd, yyyy")
        .Prop(c => c.SpinDelta, new IgbDatePartDeltas { Hours = 2, Minutes = 5 },
            wire: new JsonSubset("""{"hours": 2, "minutes": 5}"""))
        .Prop(c => c.SpinLoop, true)
        .Prop(c => c.Locale, "en-GB")
        .Prop(c => c.ReadOnly, true)
        .Prop(c => c.Mask, "00/00/0000")
        .Prop(c => c.Prompt, "*")
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true)
        .Prop(c => c.Value, new DateTime(2026, 3, 4, 8, 0, 0, DateTimeKind.Utc));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_RendersCorrectElement()
    {
        var cut = Render<IgbDateTimeInput>();
        cut.Find("igc-date-time-input").Should_Exist();
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_InputFormat_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.InputFormat, "dd/MM/yyyy"));

        Assert.Equal("dd/MM/yyyy", cut.Find("igc-date-time-input").GetAttribute("input-format"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_DisplayFormat_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.DisplayFormat, "MMMM dd, yyyy"));

        Assert.Equal("MMMM dd, yyyy", cut.Find("igc-date-time-input").GetAttribute("display-format"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Disabled_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("disabled"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_ReadOnly_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.ReadOnly, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("readonly"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Label_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.Label, "Select Date"));

        Assert.Equal("Select Date", cut.Find("igc-date-time-input").GetAttribute("label"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Placeholder_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.Placeholder, "Enter date..."));

        Assert.Equal("Enter date...", cut.Find("igc-date-time-input").GetAttribute("placeholder"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Required_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.Required, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("required"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Outlined_RendersAttribute()
    {
        var cut = Render<IgbDateTimeInput>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("outlined"));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbDateTimeInput</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void DateTimeInput_DefaultValues_MatchWebComponent()
    {
        var input = new IgbDateTimeInput();

        Assert.True(input.SpinLoop);
    }
}
