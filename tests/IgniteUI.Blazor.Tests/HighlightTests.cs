using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class HighlightTests : ComponentWithContractTestBase<IgbHighlight>
{
    protected override ComponentContract<IgbHighlight> InteropContract { get; } = new ComponentContract<IgbHighlight>()
        .Getter(c => c.GetSizeAsync(), c => c.GetSize(), "Size", returns: 3.0)
        .Getter(c => c.GetCurrentAsync(), c => c.GetCurrent(), "Current", returns: 1.0)
        .Method(c => c.NextAsync(new IgbHighlightNavigation { PreventScroll = true }), c => c.Next(new IgbHighlightNavigation { PreventScroll = true }),
            "next", args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.PreviousAsync(new IgbHighlightNavigation { PreventScroll = false }), c => c.Previous(new IgbHighlightNavigation { PreventScroll = false }),
            "previous", args: [new JsonSubset("""{"preventScroll": false}""")], types: ["Json"])
        .Method(c => c.SetActiveAsync(2, new IgbHighlightNavigation { PreventScroll = true }), c => c.SetActive(2, new IgbHighlightNavigation { PreventScroll = true }),
            "setActive", args: [2.0, new JsonSubset("""{"preventScroll": true}""")], types: ["Number", "Json"])
        .Method(c => c.SearchAsync(), c => c.Search(), "search");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

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
