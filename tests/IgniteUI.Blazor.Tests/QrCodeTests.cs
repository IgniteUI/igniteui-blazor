using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class QrCodeTests : BlazorComponentTestBase
{
    [Fact]
    public void QrCode_RendersCorrectElement()
    {
        var cut = Render<IgbQrCode>();
        Assert.NotNull(cut.Find("igc-qr-code"));
    }

    [Fact]
    public void QrCode_TypeMetadata_IsCorrect()
    {
        var qrCode = new IgbQrCode();
        Assert.Equal("WebQrCode", qrCode.Type);
    }

    [Fact]
    public void QrCode_Value_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.Value, "https://www.infragistics.com"));

        Assert.Equal("https://www.infragistics.com", cut.Find("igc-qr-code").GetAttribute("value"));
    }

    [Fact]
    public void QrCode_Version_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.Version, 7));

        Assert.Equal("7", cut.Find("igc-qr-code").GetAttribute("version"));
    }

    [Fact]
    public void QrCode_ErrorLevel_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.ErrorLevel, QrErrorCorrectionLevel.High));

        Assert.Equal("H", cut.Find("igc-qr-code").GetAttribute("error-level"));
    }

    [Fact]
    public void QrCode_Size_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.Size, 256));

        Assert.Equal("256", cut.Find("igc-qr-code").GetAttribute("size"));
    }

    [Fact]
    public void QrCode_Margin_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.Margin, 2));

        Assert.Equal("2", cut.Find("igc-qr-code").GetAttribute("margin"));
    }

    [Fact]
    public void QrCode_LogoSrc_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.LogoSrc, "logo.png"));

        Assert.Equal("logo.png", cut.Find("igc-qr-code").GetAttribute("logo-src"));
    }

    [Fact]
    public void QrCode_LogoSize_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.LogoSize, 0.8));

        Assert.Equal("0.8", cut.Find("igc-qr-code").GetAttribute("logo-size"));
    }

    [Fact]
    public void QrCode_LogoMargin_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.LogoMargin, 6));

        Assert.Equal("6", cut.Find("igc-qr-code").GetAttribute("logo-margin"));
    }

    [Fact]
    public void QrCode_DotStyle_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.DotStyle, QrDotStyle.Rounded));

        Assert.Equal("rounded", cut.Find("igc-qr-code").GetAttribute("dot-style"));
    }

    [Fact]
    public void QrCode_SquareStyle_RendersAttribute()
    {
        var cut = Render<IgbQrCode>(p =>
            p.Add(x => x.SquareStyle, QrCornerSquareStyle.Circle));

        Assert.Equal("circle", cut.Find("igc-qr-code").GetAttribute("square-style"));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbQrCode</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void QrCode_DefaultValues_MatchWebComponent()
    {
        var qrCode = new IgbQrCode();

        Assert.Null(qrCode.Version);
        Assert.Equal(QrErrorCorrectionLevel.Medium, qrCode.ErrorLevel);
        Assert.Equal(128, qrCode.Size);
        Assert.Equal(4, qrCode.Margin);
        Assert.Equal(0.4, qrCode.LogoSize);
        Assert.Null(qrCode.LogoMargin);
        Assert.Equal(QrDotStyle.Square, qrCode.DotStyle);
        Assert.Equal(QrCornerSquareStyle.Square, qrCode.SquareStyle);
    }

    [Fact]
    public void QrCode_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbQrCode).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
