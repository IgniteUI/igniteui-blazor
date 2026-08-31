using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class InputTests : ComponentWithContractTestBase<IgbInput>
{
    protected override ComponentContract<IgbInput> InteropContract { get; } = new ComponentContract<IgbInput>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "hello")
        .Method(c => c.StepUpAsync(2), c => c.StepUp(2), "stepUp", args: [2.0], types: ["Number"])
        .Method(c => c.StepDownAsync(2), c => c.StepDown(2), "stepDown", args: [2.0], types: ["Number"])
        .Method(c => c.SelectAsync(), c => c.Select(), "select")
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }),
            "focus", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": "new value"}""",
            assert: args => Assert.Equal("new value", args.Detail))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "new value"}""", expect: "new value")
        .Event(c => c.InputOcurred,
            argsJson: """{"detail": "typed text"}""",
            assert: args => Assert.Equal("typed text", args.Detail))
        .Event(c => c.Focus)
        .Event(c => c.Blur);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Input_RendersCorrectElement()
    {
        var cut = Render<IgbInput>();
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
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Value, "hello"));

        var element = cut.Find("igc-input");
        Assert.Equal("hello", element.GetAttribute("value"));
    }

    [Fact]
    public void Input_DisplayType_Email()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.DisplayType, InputType.Email));

        var element = cut.Find("igc-input");
        Assert.Equal("email", element.GetAttribute("type"));
    }

    [Fact]
    public void Input_DisplayType_Password()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.DisplayType, InputType.Password));

        var element = cut.Find("igc-input");
        Assert.Equal("password", element.GetAttribute("type"));
    }

    [Fact]
    public void Input_Placeholder_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Placeholder, "Enter text..."));

        var element = cut.Find("igc-input");
        Assert.Equal("Enter text...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Input_Disabled_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Input_Required_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Input_ReadOnly_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Input_MinLength_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.MinLength, 3.0));

        var element = cut.Find("igc-input");
        Assert.Equal("3", element.GetAttribute("minlength"));
    }

    [Fact]
    public void Input_MaxLength_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.MaxLength, 100.0));

        var element = cut.Find("igc-input");
        Assert.Equal("100", element.GetAttribute("maxlength"));
    }

    [Fact]
    public void Input_Autofocus_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Autofocus, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("autofocus"));
    }

    [Fact]
    public void Input_Pattern_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Pattern, "[A-Za-z]+"));

        var element = cut.Find("igc-input");
        Assert.Equal("[A-Za-z]+", element.GetAttribute("pattern"));
    }

    [Fact]
    public void Input_Label_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Label, "Username"));

        var element = cut.Find("igc-input");
        Assert.Equal("Username", element.GetAttribute("label"));
    }

    [Fact]
    public void Input_Outlined_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Outlined, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("outlined"));
    }

    [Fact]
    public void Input_Invalid_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Invalid, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("invalid"));
    }

    [Fact]
    public void Input_Min_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Min, 0));

        var element = cut.Find("igc-input");
        Assert.Equal("0", element.GetAttribute("min"));
    }

    [Fact]
    public void Input_Max_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Max, 100));

        var element = cut.Find("igc-input");
        Assert.Equal("100", element.GetAttribute("max"));
    }

    [Fact]
    public void Input_Step_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Step, 5));

        var element = cut.Find("igc-input");
        Assert.Equal("5", element.GetAttribute("step"));
    }

    [Fact]
    public void Input_Autocomplete_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.Autocomplete, "email"));

        var element = cut.Find("igc-input");
        Assert.Equal("email", element.GetAttribute("autocomplete"));
    }

    [Fact]
    public void Input_InputMode_RendersAsAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.InputMode, "numeric"));

        var element = cut.Find("igc-input");
        Assert.Equal("numeric", element.GetAttribute("inputmode"));
    }

    [Fact]
    public void Input_ValidateOnly_RendersAttribute()
    {
        var cut = Render<IgbInput>(parameters =>
            parameters.Add(p => p.ValidateOnly, true));

        var element = cut.Find("igc-input");
        Assert.NotNull(element.GetAttribute("validate-only"));
    }
}
