using Bunit;
using System.Drawing;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ColorPickerTests : ComponentWithContractTestBase<IgbColorPicker<string>>
{
    protected override ComponentContract<IgbColorPicker<string>> InteropContract { get; } = new ComponentContract<IgbColorPicker<string>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: true)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: true)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Pick a color"), c => c.SetCustomValidity("Pick a color"), "setCustomValidity",
            args: ["Pick a color"], types: ["String"])
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "#ff0000")
        // An untouched or cleared element holds "".
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "")
        .Prop(c => c.Value, "#ff0000")
        .Prop(c => c.Label, "Background")
        // The description's "name" is the renderer's id for the component, so Name crosses as "formName".
        .Prop(c => c.Name, "field", wire: "field", wireName: "formName")
        .Prop(c => c.Format, ColorFormat.Hsl, wire: "hsl")
        .Prop(c => c.HideFormats, true)
        .Prop(c => c.ShowAlpha, true)
        .Prop(c => c.Mode, ColorPickerMode.Input, wire: "input")
        .Prop(c => c.Swatches, new[] { "#ff0000", "#00ff00" }, wire: new RawJson("""["#ff0000","#00ff00"]"""))
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true)
        .Prop(c => c.Open, true)
        .Prop(c => c.ScrollStrategy, PopoverScrollStrategy.Close, wire: "close")
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
            expect: "#663399")
        // A cleared element reports "", which a string binding receives as is.
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": ""}""",
            expect: "",
            initial: "#663399");

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
        var cut = Render<IgbColorPicker<string>>();
        cut.Find("igc-color-picker").Should_Exist();
    }

    [Fact]
    public void ColorPicker_TypeMetadata_IsCorrect()
    {
        var colorPicker = new IgbColorPicker<string>();
        Assert.Equal("WebColorPicker", colorPicker.RendererType);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbColorPicker</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void ColorPicker_DefaultValues_MatchWebComponent()
    {
        var colorPicker = new IgbColorPicker<string>();

        Assert.Equal(ColorFormat.Hex, colorPicker.Format);
        Assert.Equal(ColorPickerMode.Default, colorPicker.Mode);
        Assert.False(colorPicker.HideFormats);
        Assert.False(colorPicker.ShowAlpha);
        Assert.False(colorPicker.Open);
    }

    [Fact]
    public void ColorPicker_InheritsFromBaseComboBox()
    {
        Assert.True(typeof(IgbColorPicker<string>).IsSubclassOf(typeof(IgbBaseComboBox)));
    }
}

public class ColorPickerColorTests : ComponentWithContractTestBase<IgbColorPicker<Color>>
{
    private static readonly Color SemiTransparentNavy = Color.FromArgb(0x80, 0x11, 0x22, 0x33);

    protected override ComponentContract<IgbColorPicker<Color>> InteropContract { get; } = new ComponentContract<IgbColorPicker<Color>>()
        .Prop(c => c.Value, SemiTransparentNavy, wire: "#11223380")
        // Without this the element shows transparent black for an unset Color.
        .Prop(c => c.Value, Color.Empty, wire: null)
        .Event(c => c.Input,
            argsJson: """{"detail": "hsl(0 100% 50%)"}""",
            assert: args => Assert.Equal(Color.Red.ToArgb(), args.Detail.ToArgb()))
        .Event(c => c.Change,
            argsJson: """{"detail": "#11223380"}""",
            assert: args => Assert.Equal(SemiTransparentNavy.ToArgb(), args.Detail.ToArgb()))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": "rgb(255 0 0 / 0.5)"}""",
            expect: Color.FromArgb(128, 255, 0, 0))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": ""}""",
            expect: Color.Empty,
            initial: Color.Red);

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void ColorValue_UsesColorType()
    {
        Assert.Equal(typeof(Color), typeof(IgbColorPicker<Color>).GetProperty(nameof(IgbColorPicker<Color>.Value))!.PropertyType);
        Assert.Equal(typeof(Color), typeof(IgbColorPicker<Color>).GetProperty(nameof(IgbColorPicker<Color>.ValueChanged))!.PropertyType.GetGenericArguments()[0]);
    }

    [Theory]
    [InlineData("#11223380", 0x80, 0x11, 0x22, 0x33)]
    [InlineData("rgb(255 0 0 / 0.5)", 0x80, 0xFF, 0x00, 0x00)]
    [InlineData("hsl(0 100% 50%)", 0xFF, 0xFF, 0x00, 0x00)]
    // The HSL arithmetic gives green and blue as -8.7e-18 here.
    [InlineData("hsl(0 100% 1%)", 0xFF, 0x05, 0x00, 0x00)]
    public void CssColorValues_ConvertToColor(string value, int alpha, int red, int green, int blue)
    {
        var color = ColorPickerColorConverter.Parse(value);

        Assert.Equal(Color.FromArgb(alpha, red, green, blue).ToArgb(), color.ToArgb());
    }

    [Fact]
    public void Color_ConvertsToCssRgbaHex()
    {
        Assert.Equal("#11223380", ColorPickerColorConverter.ToCssColor(Color.FromArgb(0x80, 0x11, 0x22, 0x33)));
    }
}

public class ColorPickerNullableColorTests : ComponentWithContractTestBase<IgbColorPicker<Color?>>
{
    protected override ComponentContract<IgbColorPicker<Color?>> InteropContract { get; } = new ComponentContract<IgbColorPicker<Color?>>()
        .Prop(c => c.Value, Color.FromArgb(0x80, 0x11, 0x22, 0x33), wire: "#11223380")
        .Prop(c => c.Value, null, wire: null)
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": ""}""",
            expect: null,
            initial: Color.Red);

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();
}
