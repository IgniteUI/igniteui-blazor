using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests verifying common BaseRendererControl functionality across all components.
/// </summary>
public class BaseControlTests : BlazorComponentTestBase
{
    [Fact]
    public void Component_ClassParameter_OverridesDefault()
    {
        var cut = Render<IgbButton>(parameters =>
            parameters.Add(p => p.Class, "custom-class"));

        var element = cut.Find("igc-button");
        Assert.Equal("custom-class", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_AdditionalAttributes_AreRendered()
    {
        var cut = Render<IgbButton>(parameters =>
            parameters.Add(p => p.AdditionalAttributes,
                new Dictionary<string, object> { { "data-testid", "btn-1" } }));

        var element = cut.Find("igc-button");
        Assert.Equal("btn-1", element.GetAttribute("data-testid"));
    }

    [Fact]
    public void Component_DataIgId_IsPresent()
    {
        var cut = Render<IgbButton>();
        var element = cut.Find("igc-button");
        Assert.NotNull(element.GetAttribute("data-ig-id"));
    }

    [Fact]
    public void Component_DefaultClass_FollowsNaming()
    {
        var cut = Render<IgbBadge>();
        var element = cut.Find("igc-badge");
        Assert.Equal("igb-web-badge", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Button()
    {
        var cut = Render<IgbButton>();
        var element = cut.Find("igc-button");
        Assert.Equal("igb-web-button", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Checkbox()
    {
        var cut = Render<IgbCheckbox>();
        var element = cut.Find("igc-checkbox");
        Assert.Equal("igb-web-checkbox", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Switch()
    {
        var cut = Render<IgbSwitch>();
        var element = cut.Find("igc-switch");
        Assert.Equal("igb-web-switch", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Avatar()
    {
        var cut = Render<IgbAvatar>();
        var element = cut.Find("igc-avatar");
        Assert.Equal("igb-web-avatar", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Icon()
    {
        var cut = Render<IgbIcon>();
        var element = cut.Find("igc-icon");
        Assert.Equal("igb-web-icon", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Dialog()
    {
        var cut = Render<IgbDialog>();
        var element = cut.Find("igc-dialog");
        Assert.Equal("igb-web-dialog", element.GetAttribute("class"));
    }

    [Fact]
    public void Component_DefaultClass_Slider()
    {
        var cut = Render<IgbSlider>();
        var element = cut.Find("igc-slider");
        Assert.Equal("igb-web-slider", element.GetAttribute("class"));
    }

    [Fact]
    public void ReturnToDate_TimezoneLessValue_IsUtc()
    {
        var result = Render<IgbCalendar>().Instance.ReturnToDate("0001-01-01T00:00:00.0000000");

        Assert.Equal(DateTime.MinValue, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void ReturnToDate_OffsetBearingValue_IsNormalizedToUtc()
    {
        var result = Render<IgbCalendar>().Instance.ReturnToDate(new DateStringValue("2026-01-02T03:04:05+02:00"));

        Assert.Equal(new DateTime(2026, 1, 2, 1, 4, 5, DateTimeKind.Utc), result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Theory]
    [InlineData(typeof(IgbButton), "WebButton")]
    [InlineData(typeof(IgbCheckbox), "WebCheckbox")]
    [InlineData(typeof(IgbSwitch), "WebSwitch")]
    [InlineData(typeof(IgbAvatar), "WebAvatar")]
    [InlineData(typeof(IgbBadge), "WebBadge")]
    [InlineData(typeof(IgbIcon), "WebIcon")]
    [InlineData(typeof(IgbChip), "WebChip")]
    [InlineData(typeof(IgbRating), "WebRating")]
    [InlineData(typeof(IgbDialog), "WebDialog")]
    [InlineData(typeof(IgbSlider), "WebSlider")]
    [InlineData(typeof(IgbLinearProgress), "WebLinearProgress")]
    [InlineData(typeof(IgbCircularProgress), "WebCircularProgress")]
    [InlineData(typeof(IgbSnackbar), "WebSnackbar")]
    [InlineData(typeof(IgbToast), "WebToast")]
    [InlineData(typeof(IgbRadio), "WebRadio")]
    [InlineData(typeof(IgbRadioGroup), "WebRadioGroup")]
    [InlineData(typeof(IgbInput), "WebInput")]
    [InlineData(typeof(IgbCard), "WebCard")]
    [InlineData(typeof(IgbTabs), "WebTabs")]
    [InlineData(typeof(IgbTab), "WebTab")]
    [InlineData(typeof(IgbList), "WebList")]
    [InlineData(typeof(IgbAccordion), "WebAccordion")]
    public void AllComponents_HaveCorrectType(System.Type componentType, string expectedType)
    {
        var instance = (BaseRendererControl)Activator.CreateInstance(componentType)!;
        Assert.Equal(expectedType, instance.RendererType);
    }

    private sealed class DateStringValue(string value)
    {
        public override string ToString() => value;
    }

    [Theory]
    [InlineData(typeof(IgbButton))]
    [InlineData(typeof(IgbCheckbox))]
    [InlineData(typeof(IgbSwitch))]
    [InlineData(typeof(IgbAvatar))]
    [InlineData(typeof(IgbBadge))]
    [InlineData(typeof(IgbIcon))]
    [InlineData(typeof(IgbChip))]
    [InlineData(typeof(IgbRating))]
    [InlineData(typeof(IgbDialog))]
    [InlineData(typeof(IgbSlider))]
    [InlineData(typeof(IgbLinearProgress))]
    [InlineData(typeof(IgbCircularProgress))]
    [InlineData(typeof(IgbSnackbar))]
    [InlineData(typeof(IgbToast))]
    [InlineData(typeof(IgbRadio))]
    [InlineData(typeof(IgbRadioGroup))]
    [InlineData(typeof(IgbInput))]
    [InlineData(typeof(IgbCard))]
    [InlineData(typeof(IgbSelect))]
    [InlineData(typeof(IgbTextarea))]
    public void AllComponents_InheritFromBaseRendererControl(System.Type componentType)
    {
        Assert.True(typeof(BaseRendererControl).IsAssignableFrom(componentType));
    }
}
