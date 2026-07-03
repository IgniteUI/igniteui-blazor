using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class SplitterTests : BlazorComponentTestBase
{
    [Fact]
    public void Splitter_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSplitter>();
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
    }

    [Theory]
    [InlineData(SplitterOrientation.Horizontal, "horizontal")]
    [InlineData(SplitterOrientation.Vertical, "vertical")]
    public void Splitter_Orientation_SerializesAsAttribute(SplitterOrientation orientation, string expected)
    {
        var cut = RenderComponent<IgbSplitter>(parameters =>
            parameters.Add(p => p.Orientation, orientation));

        Assert.Equal(expected, cut.Find("igc-splitter").GetAttribute("orientation"));
    }

    [Fact]
    public void Splitter_DisableCollapse_RendersAttribute()
    {
        var cut = RenderComponent<IgbSplitter>(parameters =>
            parameters.Add(p => p.DisableCollapse, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("disable-collapse"));
    }

    [Fact]
    public void Splitter_DisableResize_RendersAttribute()
    {
        var cut = RenderComponent<IgbSplitter>(parameters =>
            parameters.Add(p => p.DisableResize, true));

        Assert.NotNull(cut.Find("igc-splitter").GetAttribute("disable-resize"));
    }

    [Fact]
    public void Splitter_SizeConstraints_RendersAttributes()
    {
        var cut = RenderComponent<IgbSplitter>(parameters =>
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
    public void Splitter_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSplitter>(parameters =>
            parameters.AddChildContent("<div slot=\"start\">Start</div><div slot=\"end\">End</div>"));

        var innerHtml = cut.Find("igc-splitter").InnerHtml;
        Assert.Contains("Start", innerHtml);
        Assert.Contains("End", innerHtml);
    }
}
