using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ColorPickerTests : ComponentWithContractTestBase<IgbColorPicker>
{
    protected override ComponentContract<IgbColorPicker> InteropContract { get; } = new ComponentContract<IgbColorPicker>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: true)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: true)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Pick a color"), c => c.SetCustomValidity("Pick a color"), "setCustomValidity",
            args: ["Pick a color"], types: ["String"])
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "#ff0000")
        .Prop(c => c.Value, "#ff0000")
        .Prop(c => c.Label, "Background")
        .Prop(c => c.Format, ColorFormat.Hsl, wire: "hsl")
        .Prop(c => c.HideFormats, true)
        .Prop(c => c.ShowAlpha, true)
        .Prop(c => c.Mode, ColorPickerMode.Input, wire: "input")
        .Prop(c => c.Swatches, new[] { "#ff0000", "#00ff00" }, wire: new RawJson("""["#ff0000","#00ff00"]"""))
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true)
        .Prop(c => c.Open, true)
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Event(c => c.Input,
            argsJson: """{"detail": "#ff0000"}""",
            assert: args => Assert.Equal("#ff0000", args.Detail))
        .Event(c => c.Change,
            argsJson: """{"detail": "#00ff00"}""",
            assert: args => Assert.Equal("#00ff00", args.Detail))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "#663399"}""",
            expect: "#663399");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void ColorPicker_RendersCorrectElement()
    {
        var cut = Render<IgbColorPicker>();
        cut.Find("igc-color-picker").Should_Exist();
    }

    [Fact]
    public void ColorPicker_TypeMetadata_IsCorrect()
    {
        var colorPicker = new IgbColorPicker();
        Assert.Equal("WebColorPicker", colorPicker.Type);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbColorPicker</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void ColorPicker_DefaultValues_MatchWebComponent()
    {
        var colorPicker = new IgbColorPicker();

        Assert.Equal(ColorFormat.Hex, colorPicker.Format);
        Assert.Equal(ColorPickerMode.Default, colorPicker.Mode);
        Assert.False(colorPicker.HideFormats);
        Assert.False(colorPicker.ShowAlpha);
        Assert.False(colorPicker.Open);
    }

    [Fact]
    public void ColorPicker_InheritsFromBaseComboBox()
    {
        Assert.True(typeof(IgbColorPicker).IsSubclassOf(typeof(IgbBaseComboBox)));
    }
}
