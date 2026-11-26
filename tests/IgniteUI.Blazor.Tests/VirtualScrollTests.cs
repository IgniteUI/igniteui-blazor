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
        // 0 is the lowest valid index: it passes the range check and crosses as a number.
        .Method(c => c.ScrollToIndexAsync(0), c => c.ScrollToIndex(0), "scrollToIndex",
            args: [0.0], types: ["Number"])
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
        // 0 is valid (no overscan) and falsy on the client, so pin that it still crosses.
        .Prop(c => c.OverScan, 0)
        .Prop(c => c.EstimatedItemSize, 32)
        // 1 is the smallest valid estimate.
        .Prop(c => c.EstimatedItemSize, 1)
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

    #region Range validation

    // These facts exercise the parameter setters directly (the validation lives there, and a
    // component reference can also assign them), so the "set outside its component" analyzer is off here.
#pragma warning disable BL0005
    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void OverScan_Negative_ThrowsAndKeepsPreviousValue(int value)
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem> { OverScan = 5 };

        var error = Assert.Throws<ArgumentOutOfRangeException>(() => virtualScroll.OverScan = value);

        Assert.Equal("value", error.ParamName);
        Assert.Equal(value, error.ActualValue);
        Assert.Contains(nameof(IgbVirtualScroll<>.OverScan), error.Message);
        Assert.Equal(5, virtualScroll.OverScan);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void OverScan_NonNegative_IsAccepted(int value)
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem> { OverScan = value };

        Assert.Equal(value, virtualScroll.OverScan);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void EstimatedItemSize_NotPositive_ThrowsAndKeepsPreviousValue(int value)
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem> { EstimatedItemSize = 32 };

        var error = Assert.Throws<ArgumentOutOfRangeException>(() => virtualScroll.EstimatedItemSize = value);

        Assert.Equal("value", error.ParamName);
        Assert.Equal(value, error.ActualValue);
        Assert.Contains(nameof(IgbVirtualScroll<>.EstimatedItemSize), error.Message);
        Assert.Equal(32, virtualScroll.EstimatedItemSize);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void EstimatedItemSize_Positive_IsAccepted(int value)
    {
        var virtualScroll = new IgbVirtualScroll<ScrollItem> { EstimatedItemSize = value };

        Assert.Equal(value, virtualScroll.EstimatedItemSize);
    }
#pragma warning restore BL0005

    /// <summary>
    /// Declared in markup, an out-of-range value fails the render. Blazor wraps the setter's
    /// exception in an <see cref="InvalidOperationException"/> naming the parameter.
    /// </summary>
    [Fact]
    public void OverScan_NegativeInMarkup_FailsRender()
    {
        var error = Assert.Throws<InvalidOperationException>(() =>
            Render<IgbVirtualScroll<ScrollItem>>(p => p.Add(c => c.OverScan, -1)));

        Assert.Contains(nameof(IgbVirtualScroll<>.OverScan), error.Message);
        Assert.IsType<ArgumentOutOfRangeException>(error.InnerException);
    }

    [Fact]
    public void EstimatedItemSize_ZeroInMarkup_FailsRender()
    {
        var error = Assert.Throws<InvalidOperationException>(() =>
            Render<IgbVirtualScroll<ScrollItem>>(p => p.Add(c => c.EstimatedItemSize, 0)));

        Assert.Contains(nameof(IgbVirtualScroll<>.EstimatedItemSize), error.Message);
        Assert.IsType<ArgumentOutOfRangeException>(error.InnerException);
    }

    /// <summary>
    /// A rejected value must not be sent on the next render: the component keeps the last valid one.
    /// </summary>
    [Fact]
    public void OverScan_RejectedOnRenderedComponent_KeepsLastValidValueOnRerender()
    {
        var cut = Render<IgbVirtualScroll<ScrollItem>>(p => p.Add(c => c.OverScan, 5));

        Assert.Throws<InvalidOperationException>(() => cut.Render(p => p.Add(c => c.OverScan, -1)));

        Assert.Equal(5, cut.Instance.OverScan);
    }

    // The component is ready, so without the range check each call would reach JS.
    // A rejected index must fail before anything is sent.

    // The async overloads throw at the call, not through the returned task, so a caller that
    // discards the task (`_ = ScrollToIndexAsync(-1)`) still sees the error. Each fact calls the
    // method without awaiting: if the error came only through the task, no exception would surface.

    [Fact]
    public void ScrollToIndexAsync_NegativeIndex_ThrowsSynchronouslyWithoutCallingJs()
    {
        Interop.PrimeReady();
        var cut = Render<IgbVirtualScroll<ScrollItem>>();

        var error = Assert.Throws<ArgumentOutOfRangeException>(
            () => Interop.OnDispatcher(() => { _ = cut.Instance.ScrollToIndexAsync(-1); }));

        Assert.Equal("index", error.ParamName);
        Assert.Equal(-1, error.ActualValue);
        Assert.Null(Interop.FindCall("scrollToIndex"));
    }

    [Fact]
    public void ScrollToIndexAsync_WithOptions_NegativeIndex_ThrowsSynchronouslyWithoutCallingJs()
    {
        Interop.PrimeReady();
        var cut = Render<IgbVirtualScroll<ScrollItem>>();

        var error = Assert.Throws<ArgumentOutOfRangeException>(
            () => Interop.OnDispatcher(() => { _ = cut.Instance.ScrollToIndexAsync(-1, new IgbScrollIntoViewOptions { Block = "center" }); }));

        Assert.Equal("index", error.ParamName);
        Assert.Equal(-1, error.ActualValue);
        Assert.Null(Interop.FindCall("scrollToIndex"));
    }

    [Fact]
    public void ScrollToIndex_NegativeIndex_ThrowsWithoutCallingJs()
    {
        Interop.PrimeReady();
        var cut = Render<IgbVirtualScroll<ScrollItem>>();

        var error = Assert.Throws<ArgumentOutOfRangeException>(
            () => Interop.OnDispatcher(() => cut.Instance.ScrollToIndex(-1)));

        Assert.Equal("index", error.ParamName);
        Assert.Null(Interop.FindCall("scrollToIndex"));
    }

    [Fact]
    public void ScrollToIndex_WithOptions_NegativeIndex_ThrowsWithoutCallingJs()
    {
        Interop.PrimeReady();
        var cut = Render<IgbVirtualScroll<ScrollItem>>();

        var error = Assert.Throws<ArgumentOutOfRangeException>(
            () => Interop.OnDispatcher(() => cut.Instance.ScrollToIndex(-1, new IgbScrollIntoViewOptions { Block = "center" })));

        Assert.Equal("index", error.ParamName);
        Assert.Null(Interop.FindCall("scrollToIndex"));
    }

    #endregion
}
