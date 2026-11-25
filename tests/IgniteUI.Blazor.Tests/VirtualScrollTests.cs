using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class VirtualScrollTests : ComponentWithContractTestBase<IgbVirtualScroll<VirtualScrollTests.ScrollItem>>
{
    public class ScrollItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    protected override ComponentContract<IgbVirtualScroll<VirtualScrollTests.ScrollItem>> InteropContract { get; } = new ComponentContract<IgbVirtualScroll<VirtualScrollTests.ScrollItem>>()
        .Method(c => c.ScrollToIndexAsync(3), c => c.ScrollToIndex(3), "scrollToIndex",
            args: [3.0], types: ["Number"])
        .Method(c => c.ScrollToIndexAsync(5, new IgbScrollIntoViewOptions { Behavior = "smooth", Block = "center" }),
            c => c.ScrollToIndex(5, new IgbScrollIntoViewOptions { Behavior = "smooth", Block = "center" }), "scrollToIndex",
            args: [5.0, new JsonSubset("""{"behavior": "smooth", "block": "center"}""")], types: ["Number", "Json"])
        .Prop(c => c.Data,
            new[]
            {
                new ScrollItem { Id = 1, Text = "First" },
                new ScrollItem { Id = 2, Text = "Second" },
            },
            // Data source: crosses as a refChanged transfer under a generated ref id that the
            // description advertises as "dataRef" (JSON marshalling channel, as on Blazor Server).
            wire: new JsonSubset("""[{"Id": 1, "Text": "First"}, {"Id": 2, "Text": "Second"}]"""))
        // The JSON escape hatches ride the same data ref as Data.
        .Prop(c => c.DataJson, LocalJson.From("""[{"Id": 1, "Text": "First"}]"""),
            wireName: "data",
            wire: new JsonSubset("""[{"Id": 1, "Text": "First"}]"""))
        .Prop(c => c.DataJson, RemoteJson.From("https://example.test/items.json"),
            wireName: "data",
            wire: "https://example.test/items.json")
        .Prop(c => c.Orientation, ContentOrientation.Horizontal, wire: "horizontal")
        .Prop(c => c.OverScan, 5)
        .Prop(c => c.EstimatedItemSize, 32)
        .Event(c => c.StateChange,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startIndex": 10, "endIndex": 24, "viewportSize": 600, "totalSize": 50000}}}""",
            assert: args =>
            {
                Assert.Equal(10, args.Detail.StartIndex);
                Assert.Equal(24, args.Detail.EndIndex);
                Assert.Equal(600, args.Detail.ViewportSize);
                Assert.Equal(50000, args.Detail.TotalSize);
            })
        .Event(c => c.DataRequest,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"startIndex": 100, "count": 20}}}""",
            assert: args =>
            {
                Assert.Equal(100, args.Detail.StartIndex);
                Assert.Equal(20, args.Detail.Count);
            });

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void VirtualScroll_RendersCorrectElement()
    {
        var cut = Render<IgbVirtualScroll<VirtualScrollTests.ScrollItem>>();
        Assert.True(cut.Find("igc-virtual-scroll").HasAttribute("data-ig-root"));
    }

    [Fact]
    public void VirtualScroll_TypeMetadata_IsCorrect()
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem>();
        Assert.Equal("WebVirtualScroll", virtualScroll.RendererType);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbVirtualScroll</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void VirtualScroll_DefaultValues_MatchWebComponent()
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem>();

        Assert.Equal(ContentOrientation.Vertical, virtualScroll.Orientation);
        Assert.Equal(2, virtualScroll.OverScan);
        Assert.Equal(50, virtualScroll.EstimatedItemSize);
    }

    [Fact]
    public void VirtualScroll_InheritsFromIgbComponentBase()
    {
        Assert.True(typeof(IgbVirtualScroll<ScrollItem>).IsSubclassOf(typeof(IgbComponentBase)));
    }
}
