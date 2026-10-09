using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public static class DateRangePickerTests
{
    public abstract class DateRangePickerTestBase<TValue> : ComponentWithContractTestBase<IgbDateRangePicker<TValue>>
    {
        protected DateRangePickerTestBase(
            IgbDateRangeValue<TValue> selectValue,
            IgbDateRangeValue<TValue> changedValue,
            IgbDateRangeValue<TValue> propertyValue,
            IgbCustomDateRange<TValue>[] customRanges,
            TValue min,
            TValue max,
            TValue activeDate,
            Action<IgbDateRangeValueDetail<TValue>> assertDetail,
            Action<TValue?> assertClearedValue,
            Action<IgbDateRangeValue<TValue>?> assertBoundValue)
        {
            InteropContract = new ComponentContract<IgbDateRangePicker<TValue>>()
                .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
                .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
                .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
                .Method(c => c.ClearAsync(), c => c.Clear(), "clear")
                .Method(c => c.SelectAsync(selectValue), c => c.Select(selectValue), "select",
                    args: [new RawJson($$"""{"refType": "name", "id": "{{selectValue.RendererName}}"}""")],
                    types: ["Json"])
                .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
                .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
                .Method(c => c.SetCustomValidityAsync("Please choose a valid range"), c => c.SetCustomValidity("Please choose a valid range"), "setCustomValidity",
                    args: ["Please choose a valid range"], types: ["String"])
                .Event(c => c.Opening)
                .Event(c => c.Opened)
                .Event(c => c.Closing)
                .Event(c => c.Closed)
                .Event(c => c.Change,
                    argsJson: """{"detail": {"retType": "object", "type": "", "value": {"start": "2026-03-01T00:00:00.000Z", "end": "2026-03-10T00:00:00.000Z"}}}""",
                    assert: args => assertDetail(args.Detail))
                .Event(c => c.Change,
                    argsJson: """{"detail": {"retType": "object", "type": "", "value": {"start": null, "end": null}}}""",
                    assert: args =>
                    {
                        Assert.NotNull(args.Detail);
                        assertClearedValue(args.Detail.Start);
                        assertClearedValue(args.Detail.End);
                    })
                .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
                    argsJson: """{"detail": {"retType": "object", "type": "", "value": {"start": "2026-03-01T00:00:00.000Z", "end": "2026-03-10T00:00:00.000Z"}}}""",
                    expect: changedValue,
                    assert: assertBoundValue)
                .Event(c => c.Input,
                    argsJson: """{"detail": {"retType": "object", "type": "", "value": {"start": "2026-03-01T00:00:00.000Z", "end": "2026-03-10T00:00:00.000Z"}}}""",
                    assert: args => assertDetail(args.Detail))
                .Prop(c => c.Open, true)
                .Prop(c => c.ScrollStrategy, PopoverScrollStrategy.Close, wire: "close")
                .Prop(c => c.KeepOpenOnSelect, true)
                .Prop(c => c.KeepOpenOnOutsideClick, true)
                .Prop(c => c.Value, propertyValue,
                    wire: new JsonSubset("""{"start": "2026-03-01T00:00:00.0000000Z", "end": "2026-03-10T00:00:00.0000000Z"}"""))
                .Prop(c => c.CustomRanges, customRanges,
                    wire: new JsonSubset("""[{"label": "This week", "dateRange": {"start": "2026-03-01T00:00:00.0000000Z", "end": "2026-03-07T00:00:00.0000000Z"}}]"""))
                .Prop(c => c.Mode, PickerMode.Dialog, wire: "dialog")
                .Prop(c => c.UseTwoInputs, true)
                .Prop(c => c.UsePredefinedRanges, true)
                .Prop(c => c.Locale, "en-US")
                .Prop(c => c.ResourceStrings,
                    new IgbDateRangePickerResourceStrings { Separator = "thru", CancelButton = "Discard", DoneButton = "OK", SelectDate = "Pick range" },
                    wire: new JsonSubset("""{ "separator": "thru", "cancelButton": "Discard", "doneButton": "OK", "selectDate": "Pick range" }"""))
                .Prop(c => c.ReadOnly, true)
                .Prop(c => c.NonEditable, true)
                .Prop(c => c.Outlined, true)
                .Prop(c => c.Label, "Date range")
                .Prop(c => c.Name, "field", wire: "field", wireName: "formName")
                .Prop(c => c.LabelStart, "From")
                .Prop(c => c.LabelEnd, "To")
                .Prop(c => c.Placeholder, "mm/dd/yyyy - mm/dd/yyyy")
                .Prop(c => c.PlaceholderStart, "mm/dd/yyyy")
                .Prop(c => c.PlaceholderEnd, "mm/dd/yyyy")
                .Prop(c => c.Prompt, "_")
                .Prop(c => c.DisplayFormat, "MM/dd/yyyy")
                .Prop(c => c.InputFormat, "MM/dd/yyyy")
                .Prop(c => c.Min, min)
                .Prop(c => c.Max, max)
                .Prop(c => c.DisabledDates,
                    [
                        new IgbDateRangeDescriptor { RangeType = DateRangeType.Weekends },
                    new IgbDateRangeDescriptor { RangeType = DateRangeType.Specific, DateRange = new DateTime(2026, 3, 1) },
                    ],
                    wire: new JsonSubset("""[{"rangeType": "weekends"}, {"rangeType": "specific", "dateRange": "@d:2026-03-01T00:00:00.0000000"}]"""))
                .Prop(c => c.SpecialDates,
                    [new IgbDateRangeDescriptor { RangeType = DateRangeType.Weekdays }],
                    wire: new JsonSubset("""[{"rangeType": "weekdays"}]"""))
                .Prop(c => c.VisibleMonths, 2.0)
                .Prop(c => c.HeaderOrientation, ContentOrientation.Vertical, wire: "vertical")
                .Prop(c => c.Orientation, ContentOrientation.Vertical, wire: "vertical")
                .Prop(c => c.HideHeader, true)
                .Prop(c => c.ActiveDate, activeDate)
                .Prop(c => c.ShowWeekNumbers, true)
                .Prop(c => c.HideOutsideDays, true)
                .Prop(c => c.WeekStart, WeekDays.Monday, wire: "monday")
                .Prop(c => c.Disabled, true)
                .Prop(c => c.Required, true)
                .Prop(c => c.Invalid, true)
                .Prop(c => c.Value, default(IgbDateRangeValue<TValue>?), wire: null);
        }

        protected override ComponentContract<IgbDateRangePicker<TValue>> InteropContract { get; }

        [Fact] public Task Methods_FollowContract() => VerifyMethodContract();
        [Fact] public void Props_FollowContract() => VerifyPropContract();
        [Fact] public void Events_FollowContract() => VerifyEventContract();
        [Fact] public void Binds_FollowContract() => VerifyBindContract();

        [Fact]
        public void DateRangePicker_DefaultValues_MatchWebComponent()
        {
            var picker = new IgbDateRangePicker<TValue>();
            Assert.Equal(2, picker.VisibleMonths);
        }
    }

    public sealed class DateRangePickerDateTimeTests : DateRangePickerTestBase<DateTime>
    {
        public DateRangePickerDateTimeTests() : base(
            CreateRange(10),
            CreateRange(10),
            CreateRange(10),
            CreateCustomRanges(),
            new DateTime(2026, 1, 1),
            new DateTime(2026, 12, 31),
            new DateTime(2026, 4, 1),
            AssertDetail,
            value => Assert.Equal(default, value), AssertRange)
        {
        }

        private static IgbDateRangeValue<DateTime> CreateRange(int endDay) => new()
        {
            Start = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2026, 3, endDay, 0, 0, 0, DateTimeKind.Utc)
        };
        private static IgbCustomDateRange<DateTime>[] CreateCustomRanges() =>
            [
                new()
            {
                Label = "This week",
                DateRange = new()
                {
                    Start = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    End = new DateTime(2026, 3, 7, 0, 0, 0, DateTimeKind.Utc)
                }
            }
            ];
        private static void AssertDetail(IgbDateRangeValueDetail<DateTime> value)
        {
            Assert.Equal(new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), value.Start.ToUniversalTime());
            Assert.Equal(new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), value.End.ToUniversalTime());
        }
        private static void AssertRange(IgbDateRangeValue<DateTime>? value)
        {
            Assert.NotNull(value);
            Assert.Equal(new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), value.Start.ToUniversalTime());
            Assert.Equal(new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), value.End.ToUniversalTime());
        }
    }

    public sealed class DateRangePickerNullableDateTimeTests : DateRangePickerTestBase<DateTime?>
    {
        public DateRangePickerNullableDateTimeTests() : base(
            CreateRange(10),
            CreateRange(10),
            CreateRange(10),
            CreateCustomRanges(),
            new DateTime(2026, 1, 1),
            new DateTime(2026, 12, 31),
            new DateTime(2026, 4, 1),
            AssertDetail,
            Assert.Null,
            AssertRange)
        {
            InteropContract.Prop(
                c => c.Value,
                new IgbDateRangeValue<DateTime?> { Start = null, End = null },
                wire: new JsonSubset("""{"start": null, "end": null}"""));
            // A nullable date's empty state is null, so MinValue is sent as a date like any other.
            InteropContract.Prop(
                c => c.Value,
                new IgbDateRangeValue<DateTime?> { Start = DateTime.MinValue, End = null },
                wire: new JsonSubset("""{"start": "0001-01-01T00:00:00.0000000", "end": null}"""));
        }

        [Fact]
        public void DateRangeValue_AllowsNullStartAndEnd()
        {
            var value = new IgbDateRangeValue<DateTime?>
            {
                Start = null,
                End = null,
            };

            Assert.Null(value.Start);
            Assert.Null(value.End);
        }

        private static IgbDateRangeValue<DateTime?> CreateRange(int endDay) => new()
        {
            Start = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2026, 3, endDay, 0, 0, 0, DateTimeKind.Utc)
        };
        private static IgbCustomDateRange<DateTime?>[] CreateCustomRanges() =>
            [
                new()
            {
                Label = "This week",
                DateRange = new()
                {
                    Start = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    End = new DateTime(2026, 3, 7, 0, 0, 0, DateTimeKind.Utc)
                }
            }
            ];
        private static void AssertDetail(IgbDateRangeValueDetail<DateTime?> value)
        {
            Assert.Equal(new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), value.Start?.ToUniversalTime());
            Assert.Equal(new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), value.End?.ToUniversalTime());
        }
        private static void AssertRange(IgbDateRangeValue<DateTime?>? value)
        {
            Assert.NotNull(value);
            Assert.Equal(new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), value.Start?.ToUniversalTime());
            Assert.Equal(new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), value.End?.ToUniversalTime());
        }
    }

    public sealed class DateRangePickerStringTests : DateRangePickerTestBase<string>
    {
        public DateRangePickerStringTests() : base(
            CreateRange("10"),
            CreateRange("10"),
            CreateRange("10"),
            CreateCustomRanges(),
            "2026-01-01",
            "2026-12-31",
            "2026-04-01",
            AssertDetail,
            Assert.Null,
            AssertRange)
        {
        }

        private static IgbDateRangeValue<string> CreateRange(string endDay) => new()
        {
            Start = "2026-03-01T00:00:00.0000000Z",
            End = $"2026-03-{endDay}T00:00:00.0000000Z"
        };
        private static IgbCustomDateRange<string>[] CreateCustomRanges() =>
            [
                new()
            {
                Label = "This week",
                DateRange = new()
                {
                    Start = "2026-03-01T00:00:00.0000000Z",
                    End = "2026-03-07T00:00:00.0000000Z"
                }
            }
            ];
        private static void AssertDetail(IgbDateRangeValueDetail<string> value)
        {
            Assert.Contains("2026-03-01", value.Start);
            Assert.Contains("2026-03-10", value.End);
        }
        private static void AssertRange(IgbDateRangeValue<string>? value)
        {
            Assert.NotNull(value);
            Assert.Contains("2026-03-01", value.Start);
            Assert.Contains("2026-03-10", value.End);
        }
    }
}
