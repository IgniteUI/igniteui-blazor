using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class HighlightTests : BlazorComponentTestBase
{
    [Fact]
    public void Highlight_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbHighlight>();
        Assert.NotNull(cut.Find("igc-highlight"));
    }

    [Fact]
    public void Highlight_DefaultValues_AreExpected()
    {
        var highlight = new IgbHighlight();

        Assert.False(highlight.CaseSensitive);
        Assert.Null(highlight.SearchText);
    }

    [Fact]
    public void Highlight_CaseSensitive_True_RendersAttribute()
    {
        var cut = RenderComponent<IgbHighlight>(parameters =>
            parameters.Add(p => p.CaseSensitive, true));

        Assert.NotNull(cut.Find("igc-highlight").GetAttribute("case-sensitive"));
    }

    [Fact]
    public void Highlight_SearchText_RendersAttribute()
    {
        var cut = RenderComponent<IgbHighlight>(parameters =>
            parameters.Add(p => p.SearchText, "lorem"));

        Assert.Equal("lorem", cut.Find("igc-highlight").GetAttribute("search-text"));
    }

    [Fact]
    public void Highlight_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbHighlight>(parameters =>
            parameters.AddChildContent("<p>Body text to search.</p>"));

        Assert.Contains("Body text to search.", cut.Find("igc-highlight").InnerHtml);
    }
}
