using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using System;

namespace IgniteUI.Blazor.Tests;

public abstract class DatePickerTests<TValue> : ComponentWithContractTestBase<IgbDatePicker<TValue>>
{
    protected DatePickerTests(
        TValue currentValue,
        TValue changedValue,
        Action<TValue?> assertChangedValue,
        Action<TValue?> assertClearedValue,
        TValue value,
        TValue activeDate,
        TValue min,
        TValue max)
    {
        InteropContract = new ComponentContract<IgbDatePicker<TValue>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.ClearAsync(), c => c.Clear(), "clear")
        .Method(c => c.StepUpAsync(DatePart.Month, 2), c => c.StepUp(DatePart.Month, 2), "stepUp",
            args: ["month", 2.0], types: ["Json", "Number"])
        .Method(c => c.StepDownAsync(DatePart.Year, 3), c => c.StepDown(DatePart.Year, 3), "stepDown",
            args: ["year", 3.0], types: ["Json", "Number"])
        .Method(c => c.SelectAsync(), c => c.Select(), "select")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Please choose a valid date"), c => c.SetCustomValidity("Please choose a valid date"),
            "setCustomValidity", args: ["Please choose a valid date"], types: ["String"])
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value",
            returns: currentValue)
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Event(c => c.Change,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            assert: args => assertChangedValue(args.Detail))
        .Event(c => c.Change,
            argsJson: """{"detail": null}""",
            assert: args => assertClearedValue(args.Detail))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            expect: changedValue)
        .Event(c => c.Input,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            assert: args => assertChangedValue(args.Detail))
        .Event(c => c.Input,
            argsJson: """{"detail": null}""",
            assert: args => assertClearedValue(args.Detail))
        .Prop(c => c.Open, true)
        .Prop(c => c.ScrollStrategy, PopoverScrollStrategy.Close, wire: "close")
        .Prop(c => c.KeepOpenOnSelect, true)
        .Prop(c => c.KeepOpenOnOutsideClick, true)
        .Prop(c => c.Label, "Pick a date")
        // The description's "name" is the renderer's id for the component, so Name crosses as "formName".
        .Prop(c => c.Name, "field", wire: "field", wireName: "formName")
        .Prop(c => c.Mode, PickerMode.Dialog, wire: "dialog")
        .Prop(c => c.NonEditable, true)
        .Prop(c => c.ReadOnly, true)
        .Prop(c => c.Value, value)
        .Prop(c => c.ActiveDate, activeDate)
        .Prop(c => c.Min, min)
        .Prop(c => c.Max, max)
        .Prop(c => c.HeaderOrientation, CalendarHeaderOrientation.Vertical, wire: "vertical")
        .Prop(c => c.Orientation, ContentOrientation.Vertical, wire: "vertical")
        .Prop(c => c.HideHeader, true)
        .Prop(c => c.HideOutsideDays, true)
        .Prop(c => c.DisabledDates,
            [
                new IgbDateRangeDescriptor { RangeType = DateRangeType.Weekends },
                new IgbDateRangeDescriptor
                {
                    RangeType = DateRangeType.Specific,
                    DateRange = new DateTime(2026, 12, 25, 0, 0, 0, DateTimeKind.Utc),
                },
            ],
            wire: new JsonSubset("""[{"rangeType": "weekends"}, {"rangeType": "specific", "dateRange": "@d:2026-12-25T00:00:00.0000000Z"}]"""))
        .Prop(c => c.SpecialDates,
            [
                new IgbDateRangeDescriptor { RangeType = DateRangeType.Weekdays },
            ],
            wire: new JsonSubset("""[{"rangeType": "weekdays"}]"""))
        .Prop(c => c.Outlined, true)
        .Prop(c => c.Placeholder, "mm/dd/yyyy")
        .Prop(c => c.VisibleMonths, 2.0)
        .Prop(c => c.ShowWeekNumbers, true)
        .Prop(c => c.DisplayFormat, "MM/dd/yyyy")
        .Prop(c => c.InputFormat, "MM/dd/yyyy")
        .Prop(c => c.Prompt, "_")
        .Prop(c => c.Locale, "en-US")
        .Prop(c => c.ResourceStrings,
            new IgbCalendarResourceStrings
            {
                SelectMonth = "Choose month",
                SelectYear = "Choose year",
                WeekLabel = "Wk",
            },
            wire: new JsonSubset("""{"selectMonth": "Choose month", "selectYear": "Choose year", "weekLabel": "Wk"}"""))
        .Prop(c => c.WeekStart, WeekDays.Monday, wire: "monday")
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true)
        .Prop(c => c.Value, default(TValue?), wire: null);
    }

    protected override ComponentContract<IgbDatePicker<TValue>> InteropContract { get; }

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void DatePicker_TypeMetadata()
    {
        var picker = new IgbDatePicker<TValue>();
        Assert.Equal("WebDatePicker", picker.RendererType);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbDatePicker{TValue}</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void DatePicker_DefaultValues_MatchWebComponent()
    {
        var picker = new IgbDatePicker<TValue>();

        Assert.Equal(1, picker.VisibleMonths);
    }
}

public sealed class DatePickerDateTimeTests : DatePickerTests<DateTime>
{
    public DatePickerDateTimeTests()
        : base(
            new DateTime(2026, 3, 15, 9, 30, 0, DateTimeKind.Utc),
            new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            actual => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), actual.ToUniversalTime()),
            actual => Assert.Equal(default, actual),
            new DateTime(2026, 3, 15, 9, 30, 0),
            new DateTime(2026, 4, 1),
            new DateTime(2026, 1, 1),
            new DateTime(2026, 12, 31))
    {
    }
}

public sealed class DatePickerNullableDateTimeTests : DatePickerTests<DateTime?>
{
    public DatePickerNullableDateTimeTests()
        : base(
            new DateTime(2026, 3, 15, 9, 30, 0, DateTimeKind.Utc),
            new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            actual => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), actual?.ToUniversalTime()),
            Assert.Null,
            new DateTime(2026, 3, 15, 9, 30, 0),
            new DateTime(2026, 4, 1),
            new DateTime(2026, 1, 1),
            new DateTime(2026, 12, 31))
    {
    }
}

public sealed class DatePickerStringTests : DatePickerTests<string>
{
    public DatePickerStringTests()
        : base(
            "2026-03-15T09:30:00.000Z",
            "2026-01-02T03:04:05.000Z",
            actual => Assert.Equal("2026-01-02T03:04:05.000Z", actual),
            Assert.Null,
            "2026-03-15",
            "2026-04-01",
            "2026-01-01",
            "2026-12-31")
    {
    }
}

