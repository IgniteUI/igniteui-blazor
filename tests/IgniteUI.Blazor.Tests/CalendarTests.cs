using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class CalendarTests : ComponentWithContractTestBase<IgbCalendar>
{
    protected override ComponentContract<IgbCalendar> InteropContract { get; } = new ComponentContract<IgbCalendar>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value",
            returns: new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc))
        .Getter(c => c.GetCurrentValuesAsync(), c => c.GetCurrentValues(), "Values",
            arrange: _ => { },
            returns: FromRender.Of((interop, cut) => InteropReturn.Array("""["2026-01-02T03:04:05.000Z", "2026-03-16T12:30:00.000Z"]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result!.Length);
                Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), result[0].ToUniversalTime());
                Assert.Equal(new DateTime(2026, 3, 16, 12, 30, 0, DateTimeKind.Utc), result[1].ToUniversalTime());
            })
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "date", "value": "2026-01-02T03:04:05.000Z"}}""",
            assert: args => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), ((DateTime)args.Detail).ToUniversalTime()))
        // Single selection:
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": {"retType": "date", "value": "2026-01-02T03:04:05.000Z"}}""",
            expect: new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc))
        // Multiple selection:
        .Bind(c => c.Values, c => c.ValuesChanged, via: c => c.Change,
            arrange: ps => ps.Add(c => c.Selection, CalendarSelection.Multiple),
            argsJson: """{"detail": {"retType": "Array", "type": "", "value": [{"retType": "date", "value": "2026-01-02T03:04:05.000Z"}, {"retType": "date", "value": "2026-01-03T03:04:05.000Z"}]}}""",
            expect: [
                new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc),
                new DateTime(2026, 1, 3, 3, 4, 5, DateTimeKind.Utc),
            ])
        .Prop(c => c.Selection, CalendarSelection.Range, wire: "range")
        .Prop(c => c.ShowWeekNumbers, true)
        .Prop(c => c.WeekStart, WeekDays.Monday, wire: "monday")
        .Prop(c => c.Locale, "en-US")
        .Prop(c => c.Value, new DateTime(2026, 3, 15))
        .Prop(c => c.Values,
            [new DateTime(2026, 3, 15), new DateTime(2026, 3, 16)],
            wire: new RawJson("""["2026-03-15T00:00:00.0000000", "2026-03-16T00:00:00.0000000"]"""))
        .Prop(c => c.SpecialDates,
            [new IgbDateRangeDescriptor { RangeType = DateRangeType.Specific, DateRange = new DateTime(2026, 1, 1) }],
            wire: new JsonSubset("""[{"rangeType": "specific", "dateRange": "@d:2026-01-01T00:00:00.0000000"}]"""))
        .Prop(c => c.DisabledDates,
            [
                new IgbDateRangeDescriptor { RangeType = DateRangeType.Before, DateRange = new DateTime(2025, 12, 31) },
                new IgbDateRangeDescriptor { RangeType = DateRangeType.Weekdays },
            ],
            wire: new JsonSubset("""[{"rangeType": "before", "dateRange": "@d:2025-12-31T00:00:00.0000000"}, {"rangeType": "weekdays"}]"""))
        .Prop(c => c.ActiveDate, new DateTime(2026, 4, 1))
        .Prop(c => c.HideOutsideDays, true)
        .Prop(c => c.HideHeader, true)
        .Prop(c => c.HeaderOrientation, CalendarHeaderOrientation.Vertical, wire: "vertical")
        .Prop(c => c.Orientation, ContentOrientation.Vertical, wire: "vertical")
        .Prop(c => c.VisibleMonths, 2.0)
        .Prop(c => c.ActiveView, CalendarActiveView.Months, wire: "months")
        .Prop(c => c.FormatOptions,
            new IgbCalendarFormatOptions
            {
                Weekday = "short",
                Month = "long",
            },
            wire: new JsonSubset("""{"weekday": "short", "month": "long"}"""))
        .Prop(c => c.ResourceStrings,
            new IgbCalendarResourceStrings
            {
                SelectMonth = "Choose month",
                SelectYear = "Choose year",
                WeekLabel = "Wk",
            },
            wire: new JsonSubset("""{"selectMonth": "Choose month", "selectYear": "Choose year", "weekLabel": "Wk"}"""));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Calendar_TypeMetadata()
    {
        var cal = new IgbCalendar();
        Assert.Equal("WebCalendar", cal.Type);
    }

    [Fact]
    public void Calendar_InheritsFromCalendarBase()
    {
        Assert.True(typeof(IgbCalendar).IsSubclassOf(typeof(IgbCalendarBase)));
    }

    [Fact]
    public void Calendar_HideOutsideDays_Property()
    {
        var cal = new IgbCalendar();
        cal.HideOutsideDays = true;
        Assert.True(cal.HideOutsideDays);
    }

    [Fact]
    public void Calendar_HideHeader_Property()
    {
        var cal = new IgbCalendar();
        cal.HideHeader = true;
        Assert.True(cal.HideHeader);
    }

    [Fact]
    public void Calendar_HeaderOrientation_Property()
    {
        var cal = new IgbCalendar();
        cal.HeaderOrientation = CalendarHeaderOrientation.Vertical;
        Assert.Equal(CalendarHeaderOrientation.Vertical, cal.HeaderOrientation);
    }

    [Fact]
    public void Calendar_Selection_Multiple_Property()
    {
        var cal = new IgbCalendar();
        cal.Selection = CalendarSelection.Multiple;
        Assert.Equal(CalendarSelection.Multiple, cal.Selection);
    }

    [Fact]
    public void Calendar_Selection_Range_Property()
    {
        var cal = new IgbCalendar();
        cal.Selection = CalendarSelection.Range;
        Assert.Equal(CalendarSelection.Range, cal.Selection);
    }

    [Fact]
    public void Calendar_ShowWeekNumbers_Property()
    {
        var cal = new IgbCalendar();
        cal.ShowWeekNumbers = true;
        Assert.True(cal.ShowWeekNumbers);
    }

    [Fact]
    public void Calendar_VisibleMonths_Property()
    {
        var cal = new IgbCalendar();
        cal.VisibleMonths = 3;
        Assert.Equal(3, cal.VisibleMonths);
    }

    [Fact]
    public void Calendar_ActiveView_Months_Property()
    {
        var cal = new IgbCalendar();
        cal.ActiveView = CalendarActiveView.Months;
        Assert.Equal(CalendarActiveView.Months, cal.ActiveView);
    }

    [Fact]
    public void Calendar_ActiveView_Years_Property()
    {
        var cal = new IgbCalendar();
        cal.ActiveView = CalendarActiveView.Years;
        Assert.Equal(CalendarActiveView.Years, cal.ActiveView);
    }

    [Fact]
    public void Calendar_Value_Property()
    {
        var cal = new IgbCalendar();
        var date = new DateTime(2024, 12, 25);
        cal.Value = date;
        Assert.Equal(date, cal.Value);
    }

    [Fact]
    public void Calendar_ActiveDate_Property()
    {
        var cal = new IgbCalendar();
        var date = new DateTime(2024, 1, 1);
        cal.ActiveDate = date;
        Assert.Equal(date, cal.ActiveDate);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbCalendar</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Calendar_DefaultValues_MatchWebComponent()
    {
        var calendar = new IgbCalendar();

        Assert.Equal(1, calendar.VisibleMonths);
    }
}
