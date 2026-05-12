using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CalendarTests : BlazorComponentTestBase
{
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
}
