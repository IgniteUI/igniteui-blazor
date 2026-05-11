using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class InputTests : BlazorComponentTestBase
{
    [Fact]
    public void Input_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbInput>();
        Assert.NotNull(cut.Find("igc-input"));
    }

    [Fact]
    public void Input_TypeMetadata_IsCorrect()
    {
        var input = new IgbInput();
        Assert.Equal("WebInput", input.Type);
    }

    [Fact]
    public void Input_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Value, "hello"));

        var element = cut.Find("igc-input");
        Assert.Equal("hello", element.GetAttribute("value"));
    }

    [Fact]
    public void Input_DisplayType_Email()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.DisplayType, InputType.Email));

        var element = cut.Find("igc-input");
        Assert.Equal("email", element.GetAttribute("type"));
    }

    [Fact]
    public void Input_DisplayType_Password()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.DisplayType, InputType.Password));

        var element = cut.Find("igc-input");
        Assert.Equal("password", element.GetAttribute("type"));
    }

    [Fact]
    public void Input_Placeholder_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Placeholder, "Enter text..."));

        var element = cut.Find("igc-input");
        Assert.Equal("Enter text...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Input_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Input_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Input_ReadOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Input_MinLength_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.MinLength, 3.0));

        var element = cut.Find("igc-input");
        Assert.Equal("3", element.GetAttribute("minlength"));
    }

    [Fact]
    public void Input_MaxLength_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.MaxLength, 100.0));

        var element = cut.Find("igc-input");
        Assert.Equal("100", element.GetAttribute("maxlength"));
    }

    [Fact]
    public void Input_Autofocus_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Autofocus, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("autofocus"));
    }

    [Fact]
    public void Input_Pattern_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(parameters =>
            parameters.Add(p => p.Pattern, "[A-Za-z]+"));

        var element = cut.Find("igc-input");
        Assert.Equal("[A-Za-z]+", element.GetAttribute("pattern"));
    }
}
