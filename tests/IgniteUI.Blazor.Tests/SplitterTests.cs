using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SplitterTests : ComponentWithContractTestBase<IgbSplitter>
{
    protected override ComponentContract<IgbSplitter> InteropContract { get; } = new ComponentContract<IgbSplitter>()
        .Method(c => c.ToggleAsync(PanePosition.Start), c => c.Toggle(PanePosition.Start),
            "toggle", args: ["start"], types: ["Json"])
        .Event(c => c.ResizeStart,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startPanelSize": 120, "endPanelSize": 80, "delta": 0}}}""",
            assert: args =>
            {
                Assert.Equal(120, args.Detail.StartPanelSize);
                Assert.Equal(80, args.Detail.EndPanelSize);
                Assert.Equal(0, args.Detail.Delta);
            })
        .Event(c => c.Resizing,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startPanelSize": 130, "endPanelSize": 70, "delta": 10}}}""",
            assert: args =>
            {
                Assert.Equal(130, args.Detail.StartPanelSize);
                Assert.Equal(70, args.Detail.EndPanelSize);
                Assert.Equal(10, args.Detail.Delta);
            })
        .Event(c => c.ResizeEnd,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startPanelSize": 150, "endPanelSize": 50, "delta": 30}}}""",
            assert: args =>
            {
                Assert.Equal(150, args.Detail.StartPanelSize);
                Assert.Equal(50, args.Detail.EndPanelSize);
                Assert.Equal(30, args.Detail.Delta);
            })
        .Event(c => c.LayoutChanged,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startSize": "70%", "endSize": "30%", "startCollapsed": false, "endCollapsed": true}}}""",
            assert: args =>
            {
                Assert.Equal("70%", args.Detail.StartSize);
                Assert.Equal("30%", args.Detail.EndSize);
                Assert.False(args.Detail.StartCollapsed);
                Assert.True(args.Detail.EndCollapsed);
            });

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Splitter_RendersCorrectElement()
    {
        var cut = Render<IgbSplitter>();
        Assert.NotNull(cut.Find("igc-splitter"));
    }

    [Fact]
    public void Splitter_DefaultValues_AreExpected()
    {
        var splitter = new IgbSplitter();

        Assert.Equal(SplitterOrientation.Horizontal, splitter.Orientation);
        Assert.False(splitter.DisableCollapse);
        Assert.False(splitter.DisableResize);
        Assert.False(splitter.HideCollapseButtons);
        Assert.False(splitter.HideDragHandle);
        Assert.Null(splitter.StartMinSize);
        Assert.Null(splitter.EndMinSize);
        Assert.Null(splitter.StartMaxSize);
        Assert.Null(splitter.EndMaxSize);
        Assert.Null(splitter.StartSize);
        Assert.Null(splitter.EndSize);
        Assert.False(splitter.StartCollapsed);
        Assert.False(splitter.EndCollapsed);
    }

    [Theory]
    [InlineData(SplitterOrientation.Horizontal, "horizontal")]
    [InlineData(SplitterOrientation.Vertical, "vertical")]
    public void Splitter_Orientation_SerializesAsAttribute(SplitterOrientation orientation, string expected)
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.Orientation, orientation));

        Assert.Equal(expected, cut.Find("igc-splitter").GetAttribute("orientation"));
    }

    [Fact]
    public void Splitter_DisableCollapse_RendersAttribute()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.DisableCollapse, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("disable-collapse"));
    }

    [Fact]
    public void Splitter_DisableResize_RendersAttribute()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.DisableResize, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("disable-resize"));
    }

    [Fact]
    public void Splitter_SizeConstraints_RendersAttributes()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.StartMinSize, "10%")
                .Add(p => p.EndMaxSize, "400px")
                .Add(p => p.StartSize, "30%")
                .Add(p => p.EndSize, "70%"));

        var element = cut.Find("igc-splitter");
        Assert.Equal("10%", element.GetAttribute("start-min-size"));
        Assert.Equal("400px", element.GetAttribute("end-max-size"));
        Assert.Equal("30%", element.GetAttribute("start-size"));
        Assert.Equal("70%", element.GetAttribute("end-size"));
    }

    [Fact]
    public void Splitter_StartCollapsed_RendersAttribute()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.StartCollapsed, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("start-collapsed"));
    }

    [Fact]
    public void Splitter_EndCollapsed_RendersAttribute()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.Add(p => p.EndCollapsed, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("end-collapsed"));
    }

    [Fact]
    public void Splitter_ChildContent_Renders()
    {
        var cut = Render<IgbSplitter>(parameters =>
            parameters.AddChildContent("<div slot=\"start\">Start</div><div slot=\"end\">End</div>"));

        var innerHtml = cut.Find("igc-splitter").InnerHtml;
        Assert.Contains("Start", innerHtml);
        Assert.Contains("End", innerHtml);
    }
}
