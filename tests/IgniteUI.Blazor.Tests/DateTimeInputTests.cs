using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DateTimeInputTests : BlazorComponentTestBase
{
    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDateTimeInput>();
        cut.Find("igc-date-time-input").Should_Exist();
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_InputFormat_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.InputFormat, "dd/MM/yyyy"));

        Assert.Equal("dd/MM/yyyy", cut.Find("igc-date-time-input").GetAttribute("input-format"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_DisplayFormat_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.DisplayFormat, "MMMM dd, yyyy"));

        Assert.Equal("MMMM dd, yyyy", cut.Find("igc-date-time-input").GetAttribute("display-format"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("disabled"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_ReadOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.ReadOnly, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("readonly"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.Label, "Select Date"));

        Assert.Equal("Select Date", cut.Find("igc-date-time-input").GetAttribute("label"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Placeholder_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.Placeholder, "Enter date..."));

        Assert.Equal("Enter date...", cut.Find("igc-date-time-input").GetAttribute("placeholder"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.Required, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("required"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_Outlined_RendersAttribute()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-date-time-input").GetAttribute("outlined"));
    }
}
