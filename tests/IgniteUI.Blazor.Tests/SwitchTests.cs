using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SwitchTests : ComponentWithContractTestBase<IgbSwitch>
{
    protected override ComponentContract<IgbSwitch> InteropContract { get; } = new ComponentContract<IgbSwitch>()
        .Getter(c => c.GetCurrentCheckedAsync(), c => c.GetCurrentChecked(), "Checked", returns: true)
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }),
            "focus", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.ClickAsync(), c => c.Click(), "click")
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Please enable this setting"), c => c.SetCustomValidity("Please enable this setting"),
            "setCustomValidity", args: ["Please enable this setting"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "switch-value"}}}""",
            assert: args =>
            {
                Assert.True(args.Detail!.Checked);
                Assert.Equal("switch-value", args.Detail.Value);
            })
        .Bind(c => c.Checked, c => c.CheckedChanged, via: c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "switch-value"}}}""",
            expect: true)
        .Event(c => c.Focus)
        .Event(c => c.Blur);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Switch_RendersCorrectElement()
    {
        var cut = Render<IgbSwitch>();
        Assert.NotNull(cut.Find("igc-switch"));
    }

    [Fact]
    public void Switch_TypeMetadata_IsCorrect()
    {
        var sw = new IgbSwitch();
        Assert.Equal("WebSwitch", sw.Type);
    }

    [Fact]
    public void Switch_InheritsFromCheckboxBase()
    {
        Assert.True(typeof(IgbSwitch).IsSubclassOf(typeof(IgbCheckboxBase)));
    }

    [Fact]
    public void Switch_Checked_RendersAttribute()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Switch_Disabled_RendersAttribute()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Switch_Value_RendersAttribute()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.Add(p => p.Value, "toggle-value"));

        var element = cut.Find("igc-switch");
        Assert.Equal("toggle-value", element.GetAttribute("value"));
    }

    [Fact]
    public void Switch_Required_RendersAttribute()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Switch_Invalid_RendersAttribute()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.Add(p => p.Invalid, true));

        var element = cut.Find("igc-switch");
        Assert.NotNull(element.GetAttribute("invalid"));
    }

    [Fact]
    public void Switch_ChildContent_Renders()
    {
        var cut = Render<IgbSwitch>(parameters =>
            parameters.AddChildContent("Dark mode"));

        Assert.Contains("Dark mode", cut.Markup);
    }
}
