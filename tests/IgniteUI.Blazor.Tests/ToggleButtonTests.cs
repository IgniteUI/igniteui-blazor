using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ToggleButtonTests : ComponentWithContractTestBase<IgbToggleButton>
{
    protected override ComponentContract<IgbToggleButton> InteropContract { get; } = new ComponentContract<IgbToggleButton>()
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }),
            "focus", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ClickAsync(), c => c.Click(), "click");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void ToggleButton_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbToggleButton>();
        cut.Find("igc-toggle-button").Should_Exist();
    }

    [Fact]
    public void ToggleButton_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbToggleButton>(p =>
            p.Add(x => x.Value, "bold"));

        Assert.Equal("bold", cut.Find("igc-toggle-button").GetAttribute("value"));
    }

    [Fact]
    public void ToggleButton_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbToggleButton>(p =>
            p.Add(x => x.Selected, true));

        Assert.NotNull(cut.Find("igc-toggle-button").GetAttribute("selected"));
    }

    [Fact]
    public void ToggleButton_Selected_False_NoAttribute()
    {
        var cut = RenderComponent<IgbToggleButton>(p =>
            p.Add(x => x.Selected, false));

        Assert.Null(cut.Find("igc-toggle-button").GetAttribute("selected"));
    }

    [Fact]
    public void ToggleButton_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbToggleButton>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-toggle-button").GetAttribute("disabled"));
    }

    [Fact]
    public void ToggleButton_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbToggleButton>(p =>
            p.AddChildContent("<span>B</span>"));

        Assert.Contains("B", cut.Find("igc-toggle-button").InnerHtml);
    }

    [Fact]
    public void ToggleButton_TypeMetadata()
    {
        var btn = new IgbToggleButton();
        Assert.Equal("WebToggleButton", btn.Type);
    }
}
