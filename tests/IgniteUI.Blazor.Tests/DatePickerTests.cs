using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DatePickerTests : BlazorComponentTestBase
{
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
