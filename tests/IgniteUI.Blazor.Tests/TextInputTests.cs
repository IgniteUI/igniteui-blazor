using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class TextareaTests : BlazorComponentTestBase
{
    [Fact]
    public void Textarea_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTextarea>();
        Assert.NotNull(cut.Find("igc-textarea"));
    }

    [Fact]
    public void Textarea_TypeMetadata_IsCorrect()
    {
        var textarea = new IgbTextarea();
        Assert.Equal("WebTextarea", textarea.Type);
    }

    [Fact]
    public void Textarea_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Value, "Hello world"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Hello world", element.GetAttribute("value"));
    }

    [Fact]
    public void Textarea_Placeholder_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Placeholder, "Type here..."));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Type here...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Textarea_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Textarea_Required_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Textarea_ReadOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Textarea_Rows_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Rows, 5.0));

        var element = cut.Find("igc-textarea");
        Assert.Equal("5", element.GetAttribute("rows"));
    }

    [Fact]
    public void Textarea_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Label, "Comments"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Comments", element.GetAttribute("label"));
    }

    [Fact]
    public void Textarea_Resize_RendersAttribute()
    {
        var cut = RenderComponent<IgbTextarea>(parameters =>
            parameters.Add(p => p.Resize, TextareaResize.Vertical));

        var element = cut.Find("igc-textarea");
        Assert.Equal("vertical", element.GetAttribute("resize"));
    }

    [Fact]
    public void Textarea_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbTextarea).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class MaskInputTests : BlazorComponentTestBase
{
    [Fact]
    public void MaskInput_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbMaskInput>();
        Assert.NotNull(cut.Find("igc-mask-input"));
    }

    [Fact]
    public void MaskInput_TypeMetadata_IsCorrect()
    {
        var mask = new IgbMaskInput();
        Assert.Equal("WebMaskInput", mask.Type);
    }

    [Fact]
    public void MaskInput_Mask_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Mask, "000-000-0000"));

        var element = cut.Find("igc-mask-input");
        Assert.Equal("000-000-0000", element.GetAttribute("mask"));
    }

    [Fact]
    public void MaskInput_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbMaskInput>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-mask-input");
        Assert.NotNull(element.GetAttribute("disabled"));
    }
}
