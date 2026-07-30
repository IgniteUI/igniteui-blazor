using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class RippleTests : BlazorComponentTestBase
{
    [Fact]
    public void Ripple_RendersCorrectElement()
    {
        var cut = Render<IgbRipple>();
        Assert.NotNull(cut.Find("igc-ripple"));
    }

    [Fact]
    public void Ripple_TypeMetadata_IsCorrect()
    {
        var ripple = new IgbRipple();
        Assert.Equal("WebRipple", ripple.Type);
    }
}
