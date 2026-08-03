using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ToastTests : ComponentWithContractTestBase<IgbToast>
{
    protected override ComponentContract<IgbToast> InteropContract { get; } = new ComponentContract<IgbToast>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Toast_RendersCorrectElement()
    {
        var cut = Render<IgbToast>();
        Assert.NotNull(cut.Find("igc-toast"));
    }

    [Fact]
    public void Toast_TypeMetadata_IsCorrect()
    {
        var toast = new IgbToast();
        Assert.Equal("WebToast", toast.Type);
    }

    [Fact]
    public void Toast_InheritsFromBaseAlertLike()
    {
        Assert.True(typeof(IgbToast).IsSubclassOf(typeof(IgbBaseAlertLike)));
    }

    [Fact]
    public void Toast_Open_RendersAttribute()
    {
        var cut = Render<IgbToast>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-toast");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Toast_DisplayTime_RendersAttribute()
    {
        var cut = Render<IgbToast>(parameters =>
            parameters.Add(p => p.DisplayTime, 3000.0));

        var element = cut.Find("igc-toast");
        Assert.Equal("3000", element.GetAttribute("display-time"));
    }

    [Fact]
    public void Toast_KeepOpen_RendersAttribute()
    {
        var cut = Render<IgbToast>(parameters =>
            parameters.Add(p => p.KeepOpen, true));

        var element = cut.Find("igc-toast");
        Assert.NotNull(element.GetAttribute("keep-open"));
    }

    [Fact]
    public void Toast_ChildContent_Renders()
    {
        var cut = Render<IgbToast>(parameters =>
            parameters.AddChildContent("Operation successful"));

        Assert.Contains("Operation successful", cut.Markup);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbToast</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Toast_DefaultValues_MatchWebComponent()
    {
        var toast = new IgbToast();

        Assert.Equal(4000, toast.DisplayTime);
    }
}
