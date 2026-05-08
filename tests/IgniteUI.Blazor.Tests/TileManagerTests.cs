using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class TileManagerTests : BlazorComponentTestBase
{
    [Fact]
    public void TileManager_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTileManager>();
        cut.Find("igc-tile-manager").Should_Exist();
    }

    [Fact]
    public void TileManager_ColumnCount_RendersAttribute()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.ColumnCount, 4));

        Assert.Equal("4", cut.Find("igc-tile-manager").GetAttribute("column-count"));
    }

    [Fact]
    public void TileManager_ResizeMode_Hover()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.ResizeMode, TileManagerResizeMode.Hover));

        Assert.Equal("hover", cut.Find("igc-tile-manager").GetAttribute("resize-mode"));
    }

    [Fact]
    public void TileManager_ResizeMode_Always()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.ResizeMode, TileManagerResizeMode.Always));

        Assert.Equal("always", cut.Find("igc-tile-manager").GetAttribute("resize-mode"));
    }

    [Fact]
    public void TileManager_DragMode_Tile()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.DragMode, TileManagerDragMode.Tile));

        Assert.Equal("tile", cut.Find("igc-tile-manager").GetAttribute("drag-mode"));
    }

    [Fact]
    public void TileManager_DragMode_TileHeader()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.DragMode, TileManagerDragMode.TileHeader));

        Assert.Equal("tile-header", cut.Find("igc-tile-manager").GetAttribute("drag-mode"));
    }

    [Fact]
    public void TileManager_MinColumnWidth_RendersAttribute()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.MinColumnWidth, "200px"));

        Assert.Equal("200px", cut.Find("igc-tile-manager").GetAttribute("min-column-width"));
    }

    [Fact]
    public void TileManager_MinRowHeight_RendersAttribute()
    {
        var cut = RenderComponent<IgbTileManager>(p =>
            p.Add(x => x.MinRowHeight, "150px"));

        Assert.Equal("150px", cut.Find("igc-tile-manager").GetAttribute("min-row-height"));
    }

    [Fact]
    public void Tile_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTile>();
        cut.Find("igc-tile").Should_Exist();
    }

    [Fact]
    public void Tile_ColSpan_RendersAttribute()
    {
        var cut = RenderComponent<IgbTile>(p =>
            p.Add(x => x.ColSpan, 2));

        Assert.Equal("2", cut.Find("igc-tile").GetAttribute("col-span"));
    }

    [Fact]
    public void Tile_RowSpan_RendersAttribute()
    {
        var cut = RenderComponent<IgbTile>(p =>
            p.Add(x => x.RowSpan, 3));

        Assert.Equal("3", cut.Find("igc-tile").GetAttribute("row-span"));
    }

    [Fact]
    public void Tile_ColStart_RendersAttribute()
    {
        var cut = RenderComponent<IgbTile>(p =>
            p.Add(x => x.ColStart, 1));

        Assert.Equal("1", cut.Find("igc-tile").GetAttribute("col-start"));
    }

    [Fact]
    public void Tile_RowStart_RendersAttribute()
    {
        var cut = RenderComponent<IgbTile>(p =>
            p.Add(x => x.RowStart, 2));

        Assert.Equal("2", cut.Find("igc-tile").GetAttribute("row-start"));
    }

    [Fact]
    public void Tile_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTile>(p =>
            p.AddChildContent("<div>Tile content</div>"));

        Assert.Contains("Tile content", cut.Find("igc-tile").InnerHtml);
    }
}
