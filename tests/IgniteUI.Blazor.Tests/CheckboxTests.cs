using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class CheckboxTests : ComponentWithContractTestBase<IgbCheckbox>
{
    protected override ComponentContract<IgbCheckbox> InteropContract { get; } = new ComponentContract<IgbCheckbox>()
        .Getter(c => c.GetCurrentCheckedAsync(), c => c.GetCurrentChecked(), "Checked", returns: true)
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }), "focus",
            args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.ClickAsync(), c => c.Click(), "click")
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Please check this box"), c => c.SetCustomValidity("Please check this box"), "setCustomValidity",
            args: ["Please check this box"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "checkbox-value"}}}""",
            assert: args =>
            {
                Assert.True(args.Detail!.Checked);
                Assert.Equal("checkbox-value", args.Detail.Value);
            })
        .Bind(c => c.Checked, c => c.CheckedChanged, via: c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"checked": true, "value": "checkbox-value"}}}""",
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
    public void Checkbox_RendersCorrectElement()
    {
        var cut = Render<IgbCheckbox>();
        Assert.NotNull(cut.Find("igc-checkbox"));
    }

    [Fact]
    public void Checkbox_TypeMetadata_IsCorrect()
    {
        var checkbox = new IgbCheckbox();
        Assert.Equal("WebCheckbox", checkbox.Type);
    }

    [Fact]
    public void Checkbox_InheritsFromCheckboxBase()
    {
        Assert.True(typeof(IgbCheckbox).IsSubclassOf(typeof(IgbCheckboxBase)));
    }

    [Fact]
    public void Checkbox_Checked_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Checked, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("checked"));
    }

    [Fact]
    public void Checkbox_Indeterminate_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Indeterminate, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("indeterminate"));
    }

    [Fact]
    public void Checkbox_Disabled_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Checkbox_Required_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Checkbox_LabelPosition_Before()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.LabelPosition, ToggleLabelPosition.Before));

        var element = cut.Find("igc-checkbox");
        Assert.Equal("before", element.GetAttribute("label-position"));
    }

    [Fact]
    public void Checkbox_Value_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Value, "test-value"));

        var element = cut.Find("igc-checkbox");
        Assert.Equal("test-value", element.GetAttribute("value"));
    }

    [Fact]
    public void Checkbox_Invalid_RendersAttribute()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.Add(p => p.Invalid, true));

        var element = cut.Find("igc-checkbox");
        Assert.NotNull(element.GetAttribute("invalid"));
    }

    [Fact]
    public void Checkbox_ChildContent_Renders()
    {
        var cut = Render<IgbCheckbox>(parameters =>
            parameters.AddChildContent("Accept terms"));

        Assert.Contains("Accept terms", cut.Markup);
    }
}
