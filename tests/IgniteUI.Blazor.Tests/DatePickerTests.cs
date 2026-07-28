using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class DatePickerTests : ComponentWithContractTestBase<IgbDatePicker>
{
    protected override ComponentContract<IgbDatePicker> InteropContract { get; } = new ComponentContract<IgbDatePicker>()
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
            returns: new DateTime(2026, 3, 15, 9, 30, 0, DateTimeKind.Utc))
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Event(c => c.Change,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            assert: args => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), args.Detail.ToUniversalTime()))
        .Event(c => c.Input,
            argsJson: """{"detail": "2026-01-02T03:04:05.000Z"}""",
            assert: args => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc), args.Detail.ToUniversalTime()))
        .Prop(c => c.Open, true)
        .Prop(c => c.KeepOpenOnSelect, true)
        .Prop(c => c.KeepOpenOnOutsideClick, true)
        .Prop(c => c.Label, "Pick a date")
        .Prop(c => c.Mode, PickerMode.Dialog, wire: "dialog")
        .Prop(c => c.NonEditable, true)
        .Prop(c => c.ReadOnly, true)
        .Prop(c => c.Value, new DateTime(2026, 3, 15, 9, 30, 0))
        .Prop(c => c.ActiveDate, new DateTime(2026, 4, 1))
        .Prop(c => c.Min, new DateTime(2026, 1, 1))
        .Prop(c => c.Max, new DateTime(2026, 12, 31))
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
        .Prop(c => c.Invalid, true);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void DatePicker_TypeMetadata()
    {
        var picker = new IgbDatePicker();
        Assert.Equal("WebDatePicker", picker.Type);
    }

    [Fact]
    public void DatePicker_Label_Property()
    {
        var picker = new IgbDatePicker();
        picker.Label = "Pick a date";
        Assert.Equal("Pick a date", picker.Label);
    }

    [Fact]
    public void DatePicker_NonEditable_Property()
    {
        var picker = new IgbDatePicker();
        picker.NonEditable = true;
        Assert.True(picker.NonEditable);
    }

    [Fact]
    public void DatePicker_ReadOnly_Property()
    {
        var picker = new IgbDatePicker();
        picker.ReadOnly = true;
        Assert.True(picker.ReadOnly);
    }

    [Fact]
    public void DatePicker_Disabled_Property()
    {
        var picker = new IgbDatePicker();
        picker.Disabled = true;
        Assert.True(picker.Disabled);
    }

    [Fact]
    public void DatePicker_Open_Property()
    {
        var picker = new IgbDatePicker();
        picker.Open = true;
        Assert.True(picker.Open);
    }

    [Fact]
    public void DatePicker_Required_Property()
    {
        var picker = new IgbDatePicker();
        picker.Required = true;
        Assert.True(picker.Required);
    }

    [Fact]
    public void DatePicker_Mode_Property()
    {
        var picker = new IgbDatePicker();
        picker.Mode = PickerMode.Dropdown;
        Assert.Equal(PickerMode.Dropdown, picker.Mode);
    }

    [Fact]
    public void DatePicker_Value_Property()
    {
        var picker = new IgbDatePicker();
        var date = new DateTime(2024, 6, 15);
        picker.Value = date;
        Assert.Equal(date, picker.Value);
    }
}
