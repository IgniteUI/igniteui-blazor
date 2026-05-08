using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class RadioTests : BlazorComponentTestBase
{
    [Fact]
    public void Radio_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRadio>();
        Assert.NotNull(cut.Find("igc-radio"));
    }

    [Fact]
    public void Radio_TypeMetadata_IsCorrect()
    {
        var radio = new IgbRadio();
        Assert.Equal("WebRadio", radio.Type);
    }

    [Fact]
    public void Radio_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.Add(p => p.Value, "option1"));

        var element = cut.Find("igc-radio");
        Assert.Equal("option1", element.GetAttribute("value"));
    }

    [Fact]
    public void Radio_Checked_RendersAttribute()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Radio_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Radio_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-radio");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Radio_LabelPosition_Before()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.Add(p => p.LabelPosition, ToggleLabelPosition.Before));

        var element = cut.Find("igc-radio");
        Assert.Equal("before", element.GetAttribute("label-position"));
    }

    [Fact]
    public void Radio_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbRadio>(parameters =>
            parameters.AddChildContent("Option A"));

        Assert.Contains("Option A", cut.Markup);
    }

    [Fact]
    public void Radio_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadio).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class RadioGroupTests : BlazorComponentTestBase
{
    [Fact]
    public void RadioGroup_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRadioGroup>();
        Assert.NotNull(cut.Find("igc-radio-group"));
    }

    [Fact]
    public void RadioGroup_TypeMetadata_IsCorrect()
    {
        var group = new IgbRadioGroup();
        Assert.Equal("WebRadioGroup", group.Type);
    }

    [Fact]
    public void RadioGroup_Alignment_Vertical()
    {
        var cut = RenderComponent<IgbRadioGroup>(parameters =>
            parameters.Add(p => p.Alignment, ContentOrientation.Vertical));

        var element = cut.Find("igc-radio-group");
        Assert.Equal("vertical", element.GetAttribute("alignment"));
    }

    [Fact]
    public void RadioGroup_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbRadioGroup>(parameters =>
            parameters.Add(p => p.Value, "selected-option"));

        var element = cut.Find("igc-radio-group");
        Assert.Equal("selected-option", element.GetAttribute("value"));
    }

    [Fact]
    public void RadioGroup_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRadioGroup).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
