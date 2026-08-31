using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class TileManagerTests : ComponentWithContractTestBase<IgbTileManager>
{
    /// <summary> Static arrange for contract tests adding two tiles </summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbTileManager>> arrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbTile>(0);
            builder.AddAttribute(1, "id", "tile-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbTile>(2);
            builder.AddAttribute(3, "id", "tile-2");
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbTileManager> InteropContract { get; } = new ComponentContract<IgbTileManager>()
        .Method(c => c.SaveLayoutAsync(), c => c.SaveLayout(), "saveLayout", returns: "{\"tiles\":[]}")
        .Method(c => c.LoadLayoutAsync("{\"tiles\":[]}"), c => c.LoadLayout("{\"tiles\":[]}"), "loadLayout",
            args: ["{\"tiles\":[]}"], types: ["String"])
        .Getter(c => c.GetTilesAsync(), c => c.GetTiles(), "Tiles",
            arrange,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(1)")}}}"}, {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result!.Length);
                Assert.Same(cut.FindComponents<IgbTile>()[0].Instance, result[0]);
                Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, result[1]);
            })
        .Event(c => c.TileDragStart,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileDragEnd,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileDragCancel,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileResizeStart,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileResizeEnd,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileResizeCancel,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail))
        .Event(c => c.TileFullscreen,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$$"""{"detail": {"retType": "object", "type": "", "value": {"tile": {"refType": "name", "id": "{{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}}"}, "state": true}}}"""),
            assert: (cut, args) =>
            {
                Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail.Tile);
                Assert.True(args.Detail.State);
            })
        .Event(c => c.TileMaximize,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$$"""{"detail": {"retType": "object", "type": "", "value": {"tile": {"refType": "name", "id": "{{{{interop.ContainerIdOf(cut, "igc-tile:nth-of-type(2)")}}}}"}, "state": false}}}"""),
            assert: (cut, args) =>
            {
                Assert.Same(cut.FindComponents<IgbTile>()[1].Instance, args.Detail.Tile);
                Assert.False(args.Detail.State);
            });

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void TileManager_RendersCorrectElement()
    {
        var cut = Render<IgbTileManager>();
        cut.Find("igc-tile-manager").Should_Exist();
    }

    [Fact]
    public void TileManager_ColumnCount_RendersAttribute()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.ColumnCount, 4));

        Assert.Equal("4", cut.Find("igc-tile-manager").GetAttribute("column-count"));
    }

    [Fact]
    public void TileManager_ResizeMode_Hover()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.ResizeMode, TileManagerResizeMode.Hover));

        Assert.Equal("hover", cut.Find("igc-tile-manager").GetAttribute("resize-mode"));
    }

    [Fact]
    public void TileManager_ResizeMode_Always()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.ResizeMode, TileManagerResizeMode.Always));

        Assert.Equal("always", cut.Find("igc-tile-manager").GetAttribute("resize-mode"));
    }

    [Fact]
    public void TileManager_DragMode_Tile()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.DragMode, TileManagerDragMode.Tile));

        Assert.Equal("tile", cut.Find("igc-tile-manager").GetAttribute("drag-mode"));
    }

    [Fact]
    public void TileManager_DragMode_TileHeader()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.DragMode, TileManagerDragMode.TileHeader));

        Assert.Equal("tile-header", cut.Find("igc-tile-manager").GetAttribute("drag-mode"));
    }

    [Fact]
    public void TileManager_MinColumnWidth_RendersAttribute()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.MinColumnWidth, "200px"));

        Assert.Equal("200px", cut.Find("igc-tile-manager").GetAttribute("min-column-width"));
    }

    [Fact]
    public void TileManager_MinRowHeight_RendersAttribute()
    {
        var cut = Render<IgbTileManager>(p =>
            p.Add(x => x.MinRowHeight, "150px"));

        Assert.Equal("150px", cut.Find("igc-tile-manager").GetAttribute("min-row-height"));
    }

    [Fact]
    public void Tile_RendersCorrectElement()
    {
        var cut = Render<IgbTile>();
        cut.Find("igc-tile").Should_Exist();
    }

    [Fact]
    public void Tile_ColSpan_RendersAttribute()
    {
        var cut = Render<IgbTile>(p =>
            p.Add(x => x.ColSpan, 2));

        Assert.Equal("2", cut.Find("igc-tile").GetAttribute("col-span"));
    }

    [Fact]
    public void Tile_RowSpan_RendersAttribute()
    {
        var cut = Render<IgbTile>(p =>
            p.Add(x => x.RowSpan, 3));

        Assert.Equal("3", cut.Find("igc-tile").GetAttribute("row-span"));
    }

    [Fact]
    public void Tile_ColStart_RendersAttribute()
    {
        var cut = Render<IgbTile>(p =>
            p.Add(x => x.ColStart, 1));

        Assert.Equal("1", cut.Find("igc-tile").GetAttribute("col-start"));
    }

    [Fact]
    public void Tile_RowStart_RendersAttribute()
    {
        var cut = Render<IgbTile>(p =>
            p.Add(x => x.RowStart, 2));

        Assert.Equal("2", cut.Find("igc-tile").GetAttribute("row-start"));
    }

    [Fact]
    public void Tile_ChildContent_Renders()
    {
        var cut = Render<IgbTile>(p =>
            p.AddChildContent("<div>Tile content</div>"));

        Assert.Contains("Tile content", cut.Find("igc-tile").InnerHtml);
    }

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="count"/> <see cref="IgbTile"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbTileManager>> TileManagerWith(int count) => ps =>
        ps.AddChildContent(builder =>
        {
            for (var i = 0; i < count; i++)
            {
                builder.OpenComponent<IgbTile>(i);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void TileManager_ChildTiles_RegisterOnInitialize()
    {
        var cut = Render<IgbTileManager>(TileManagerWith(2));

        Assert.Equal(
            cut.FindComponents<IgbTile>().Select(t => t.Instance),
            cut.Instance.ContentItems);
    }

    [Fact]
    public void TileManager_DisposedChildTile_LeavesTheCollection()
    {
        var cut = Render<IgbTileManager>(TileManagerWith(2));
        var survivor = cut.FindComponents<IgbTile>()[0].Instance;

        cut.Render(TileManagerWith(1));

        Assert.Same(survivor, Assert.Single(cut.Instance.ContentItems));
    }

    [Fact]
    public void TileManager_AllChildTilesDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbTileManager>(TileManagerWith(2));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ContentItems);
    }

    #endregion
}

public class TileTests : ComponentWithContractTestBase<IgbTile>
{
    // The tile's own events carry a self-reference ({"refType": "name", "id": "mainControl"})
    // that must resolve back to the .NET instance; TileFullscreen/TileMaximize wrap it in a
    // composite detail ({tile, state}).
    protected override ComponentContract<IgbTile> InteropContract { get; } = new ComponentContract<IgbTile>()
        .Getter(c => c.GetFullscreenAsync(), c => c.GetFullscreen(), "Fullscreen", returns: true)
        .Event(c => c.TileDragStart,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileDragEnd,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileDragCancel,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileResizeStart,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileResizeEnd,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileResizeCancel,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (tile, args) => Assert.Same(tile, args.Detail))
        .Event(c => c.TileFullscreen,
            """{"detail": {"retType": "object", "type": "", "value": {"tile": {"refType": "name", "id": "mainControl"}, "state": true}}}""",
            assert: (tile, args) =>
            {
                Assert.Same(tile, args.Detail.Tile);
                Assert.True(args.Detail.State);
            })
        .Event(c => c.TileMaximize,
            """{"detail": {"retType": "object", "type": "", "value": {"tile": {"refType": "name", "id": "mainControl"}, "state": false}}}""",
            assert: (tile, args) =>
            {
                Assert.Same(tile, args.Detail.Tile);
                Assert.False(args.Detail.State);
            });

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbTile</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Tile_DefaultValues_MatchWebComponent()
    {
        var tile = new IgbTile();

        Assert.Equal(1, tile.ColSpan);
        Assert.Equal(1, tile.RowSpan);
        Assert.Equal(-1, tile.Position);
    }
}
