using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class SwitchTests : BlazorComponentTestBase
{
    [Fact]
    public void Switch_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSwitch>();
        Assert.NotNull(cut.Find("igc-switch"));
    }

    [Fact]
    public void Switch_TypeMetadata_IsCorrect()
    {
        var sw = new IgbSwitch();
        Assert.Equal("WebSwitch", sw.Type);
    }

    [Fact]
    public void Switch_InheritsFromCheckboxBase()
    {
        Assert.True(typeof(IgbSwitch).IsSubclassOf(typeof(IgbCheckboxBase)));
    }

    [Fact]
    public void Switch_Checked_RendersAttribute()
    {
        var cut = RenderComponent<IgbSwitch>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Switch_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbSwitch>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Switch_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbSwitch>(parameters =>
            parameters.Add(p => p.Value, "toggle-value"));

        var element = cut.Find("igc-switch");
        Assert.Equal("toggle-value", element.GetAttribute("value"));
    }

    [Fact]
    public void Switch_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSwitch>(parameters =>
            parameters.AddChildContent("Dark mode"));

        Assert.Contains("Dark mode", cut.Markup);
    }
}
