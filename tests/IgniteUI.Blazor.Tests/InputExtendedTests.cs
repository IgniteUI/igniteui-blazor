using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests for Input, Textarea, MaskInput, and CheckboxBase properties.
/// </summary>
public class InputExtendedTests : BlazorComponentTestBase
{
    // Input extended tests
    [Fact]
    public void Input_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Label, "Username"));

        Assert.Equal("Username", cut.Find("igc-input").GetAttribute("label"));
    }

    [Fact]
    public void Input_Outlined_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-input").GetAttribute("outlined"));
    }

    [Fact]
    public void Input_Invalid_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Invalid, true));

        Assert.NotNull(cut.Find("igc-input").GetAttribute("invalid"));
    }

    [Fact]
    public void Input_Min_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Min, 0));

        Assert.Equal("0", cut.Find("igc-input").GetAttribute("min"));
    }

    [Fact]
    public void Input_Max_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Max, 100));

        Assert.Equal("100", cut.Find("igc-input").GetAttribute("max"));
    }

    [Fact]
    public void Input_Step_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Step, 5));

        Assert.Equal("5", cut.Find("igc-input").GetAttribute("step"));
    }

    [Fact]
    public void Input_Autocomplete_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.Autocomplete, "email"));

        Assert.Equal("email", cut.Find("igc-input").GetAttribute("autocomplete"));
    }

    [Fact]
    public void Input_InputMode_RendersAsAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.InputMode, "numeric"));

        Assert.Equal("numeric", cut.Find("igc-input").GetAttribute("inputmode"));
    }

    [Fact]
    public void Input_ValidateOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbInput>(p =>
            p.Add(x => x.ValidateOnly, true));

        Assert.NotNull(cut.Find("igc-input").GetAttribute("validate-only"));
    }

    // Textarea extended tests
    [Fact]
    public void Textarea_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.Label, "Comments"));

        Assert.Equal("Comments", cut.Find("igc-textarea").GetAttribute("label"));
    }

    [Fact]
    public void Textarea_Outlined_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-textarea").GetAttribute("outlined"));
    }

    [Fact]
    public void Textarea_MaxLength_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.MaxLength, 500));

        Assert.Equal("500", cut.Find("igc-textarea").GetAttribute("maxlength"));
    }

    [Fact]
    public void Textarea_MinLength_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.MinLength, 10));

        Assert.Equal("10", cut.Find("igc-textarea").GetAttribute("minlength"));
    }

    [Fact]
    public void Textarea_ReadOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.ReadOnly, true));

        Assert.NotNull(cut.Find("igc-textarea").GetAttribute("readonly"));
    }

    [Fact]
    public void Textarea_Spellcheck_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.Spellcheck, true));

        Assert.NotNull(cut.Find("igc-textarea").GetAttribute("spellcheck"));
    }

    [Fact]
    public void Textarea_InputMode_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.InputMode, "text"));

        Assert.Equal("text", cut.Find("igc-textarea").GetAttribute("inputmode"));
    }

    [Fact]
    public void Textarea_Wrap_Hard()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.Wrap, TextareaWrap.Hard));

        Assert.Equal("hard", cut.Find("igc-textarea").GetAttribute("wrap"));
    }

    [Fact]
    public void Textarea_ValidateOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.ValidateOnly, true));

        Assert.NotNull(cut.Find("igc-textarea").GetAttribute("validate-only"));
    }

    [Fact]
    public void Textarea_Autocomplete_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(p =>
            p.Add(x => x.Autocomplete, "on"));

        Assert.Equal("on", cut.Find("igc-textarea").GetAttribute("autocomplete"));
    }

    // MaskInput extended tests
    [Fact]
    public void MaskInput_ValueMode_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(p =>
            p.Add(x => x.ValueMode, MaskInputValueMode.WithFormatting));

        Assert.Equal("withFormatting", cut.Find("igc-mask-input").GetAttribute("value-mode"));
    }

    [Fact]
    public void MaskInput_Prompt_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(p =>
            p.Add(x => x.Prompt, "#"));

        Assert.Equal("#", cut.Find("igc-mask-input").GetAttribute("prompt"));
    }

    [Fact]
    public void MaskInput_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(p =>
            p.Add(x => x.Label, "Phone"));

        Assert.Equal("Phone", cut.Find("igc-mask-input").GetAttribute("label"));
    }

    [Fact]
    public void MaskInput_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(p =>
            p.Add(x => x.Required, true));

        Assert.NotNull(cut.Find("igc-mask-input").GetAttribute("required"));
    }

    [Fact]
    public void MaskInput_Outlined_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(p =>
            p.Add(x => x.Outlined, true));

        Assert.NotNull(cut.Find("igc-mask-input").GetAttribute("outlined"));
    }

    // Checkbox extended tests
    [Fact]
    public void Checkbox_Invalid_RendersAttribute()
    {
        var cut = RenderComponent<IgbCheckbox>(p =>
            p.Add(x => x.Invalid, true));

        Assert.NotNull(cut.Find("igc-checkbox").GetAttribute("invalid"));
    }

    [Fact]
    public void Checkbox_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCheckbox>(p =>
            p.AddChildContent("Accept terms"));

        Assert.Contains("Accept terms", cut.Find("igc-checkbox").InnerHtml);
    }

    [Fact]
    public void Switch_Invalid_RendersAttribute()
    {
        var cut = RenderComponent<IgbSwitch>(p =>
            p.Add(x => x.Invalid, true));

        Assert.NotNull(cut.Find("igc-switch").GetAttribute("invalid"));
    }

    [Fact]
    public void Switch_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbSwitch>(p =>
            p.Add(x => x.Required, true));

        Assert.NotNull(cut.Find("igc-switch").GetAttribute("required"));
    }
}
