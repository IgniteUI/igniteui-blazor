using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CheckboxTests : BlazorComponentTestBase
{
    [Fact]
    public void Checkbox_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCheckbox>();
        Assert.NotNull(cut.Find("igc-checkbox"));
    }

    [Fact]
    public void Checkbox_TypeMetadata_IsCorrect()
    {
        var checkbox = new IgbCheckbox();
        Assert.Equal("WebCheckbox", checkbox.Type);
    }

    [Fact]
    public void Checkbox_InheritsFromCheckboxBase()
    {
        Assert.True(typeof(IgbCheckbox).IsSubclassOf(typeof(IgbCheckboxBase)));
    }

    [Fact]
    public void Checkbox_Checked_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Checkbox_Indeterminate_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Indeterminate, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("indeterminate"));
    }

    [Fact]
    public void Checkbox_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Checkbox_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Checkbox_LabelPosition_Before()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.LabelPosition, ToggleLabelPosition.Before));

        var element = cut.Find("igc-checkbox");
        Assert.Equal("before", element.GetAttribute("label-position"));
    }

    [Fact]
    public void Checkbox_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Value, "test-value"));

        var element = cut.Find("igc-checkbox");
        Assert.Equal("test-value", element.GetAttribute("value"));
    }

    [Fact]
    public void Checkbox_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCheckbox>(parameters =>
            parameters.AddChildContent("Accept terms"));

        Assert.Contains("Accept terms", cut.Markup);
    }
}
