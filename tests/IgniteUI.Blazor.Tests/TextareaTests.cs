using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class TextareaTests : ComponentWithContractTestBase<IgbTextarea>
{
    protected override ComponentContract<IgbTextarea> InteropContract { get; } = new ComponentContract<IgbTextarea>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "hello")
        .Method(c => c.SelectAsync(), c => c.Select(), "select")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Input,
            argsJson: """{"detail": "typed text"}""",
            assert: args => Assert.Equal("typed text", args.Detail))
        .Event(c => c.Change,
            argsJson: """{"detail": "new value"}""",
            assert: args => Assert.Equal("new value", args.Detail))
        .Event(c => c.Focus)
        .Event(c => c.Blur);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Textarea_RendersCorrectElement()
    {
        var cut = Render<IgbTextarea>();
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
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Value, "Hello world"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Hello world", element.GetAttribute("value"));
    }

    [Fact]
    public void Textarea_Placeholder_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Placeholder, "Type here..."));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Type here...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Textarea_Disabled_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Textarea_Required_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Textarea_ReadOnly_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Textarea_Rows_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Rows, 5.0));

        var element = cut.Find("igc-textarea");
        Assert.Equal("5", element.GetAttribute("rows"));
    }

    [Fact]
    public void Textarea_Label_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Label, "Comments"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("Comments", element.GetAttribute("label"));
    }

    [Fact]
    public void Textarea_Resize_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Resize, TextareaResize.Vertical));

        var element = cut.Find("igc-textarea");
        Assert.Equal("vertical", element.GetAttribute("resize"));
    }

    [Fact]
    public void Textarea_Outlined_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Outlined, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("outlined"));
    }

    [Fact]
    public void Textarea_MaxLength_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.MaxLength, 500));

        var element = cut.Find("igc-textarea");
        Assert.Equal("500", element.GetAttribute("maxlength"));
    }

    [Fact]
    public void Textarea_MinLength_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.MinLength, 10));

        var element = cut.Find("igc-textarea");
        Assert.Equal("10", element.GetAttribute("minlength"));
    }

    [Fact]
    public void Textarea_Spellcheck_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Spellcheck, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("spellcheck"));
    }

    [Fact]
    public void Textarea_InputMode_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.InputMode, "text"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("text", element.GetAttribute("inputmode"));
    }

    [Fact]
    public void Textarea_Wrap_Hard()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Wrap, TextareaWrap.Hard));

        var element = cut.Find("igc-textarea");
        Assert.Equal("hard", element.GetAttribute("wrap"));
    }

    [Fact]
    public void Textarea_ValidateOnly_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.ValidateOnly, true));

        var element = cut.Find("igc-textarea");
        Assert.NotNull(element.GetAttribute("validate-only"));
    }

    [Fact]
    public void Textarea_Autocomplete_RendersAttribute()
    {
        var cut = Render<IgbTextarea>(parameters =>
            parameters.Add(p => p.Autocomplete, "on"));

        var element = cut.Find("igc-textarea");
        Assert.Equal("on", element.GetAttribute("autocomplete"));
    }

    [Fact]
    public void Textarea_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbTextarea).IsSubclassOf(typeof(BaseRendererControl)));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbTextarea</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Textarea_DefaultValues_MatchWebComponent()
    {
        var textarea = new IgbTextarea();

        Assert.Equal(3, textarea.Rows);
        Assert.True(textarea.Spellcheck);
    }
}
