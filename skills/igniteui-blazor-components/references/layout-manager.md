# Layout Managers — Tile Manager & Dock Manager

| Component | Package | Use for |
|---|---|---|
| `IgbTileManager` | `IgniteUI.Blazor.Lite` or full | Resizable, draggable widget dashboard on a CSS grid |
| `IgbDockManager` | `IgniteUI.Blazor` / `.Trial` only | IDE-style dockable, floating, pinnable panes |

## Tile Manager

```razor
<IgbTileManager @ref="TilesRef" ColumnCount="4" Gap="8px"
                MinColumnWidth="220px" MinRowHeight="140px"
                ResizeMode="TileManagerResizeMode.Always"
                DragMode="TileManagerDragMode.TileHeader">
    <IgbTile ColSpan="2" RowSpan="1">
        <span slot="title">Revenue</span>
        <!-- tile content -->
    </IgbTile>
    <IgbTile ColSpan="1" RowSpan="2" DisableResize="true">
        <span slot="title">KPIs</span>
    </IgbTile>
</IgbTileManager>

@code {
    IgbTileManager TilesRef { get; set; } = default!;

    Task<string> Save() => TilesRef.SaveLayoutAsync();
    Task Restore(string json) => TilesRef.LoadLayoutAsync(json);
}
```

`IgbTileManager`: `ColumnCount`, `Gap`, `MinColumnWidth`, `MinRowHeight`, `ResizeMode` (`None | Hover | Always`), `DragMode` (`None | TileHeader | Tile`), `SaveLayoutAsync()` / `LoadLayoutAsync(json)`, plus `TileDragStart` / `TileDragEnd` / `TileMaximize` / `TileFullscreen` events.

`IgbTile`: `ColSpan`, `RowSpan`, `ColStart`, `RowStart`, `Position`, `Maximized`, `DisableResize`, `DisableMaximize`, `DisableFullscreen`, `GetFullscreenAsync()`, and a `title` slot.

The saved layout stores tile geometry and state only — tile **content** always stays in the Razor markup.

## Dock Manager

Requires `IgniteUI.Blazor` or `IgniteUI.Blazor.Trial` and `IgbDockManagerModule`. Pane structure is a C# object graph on the `Layout` parameter; pane bodies are projected through named slots matched by `ContentId`.

```razor
<IgbDockManager @ref="DockRef" Layout="DockLayout" style="height: 600px;">
    <div slot="panel1">Panel 1 Content</div>
    <div slot="panel2">Panel 2 Content</div>
    <div slot="panel3">Panel 3 Content</div>
</IgbDockManager>

@code {
    IgbDockManager DockRef { get; set; } = default!;

    // IgbDockManagerLayout: RootPane (IgbSplitPane) + FloatingPanes (IgbSplitPaneCollection)
    IgbDockManagerLayout DockLayout { get; set; } = new()
    {
        RootPane = new IgbSplitPane
        {
            PaneType = DockManagerPaneType.SplitPane,
            Orientation = SplitPaneOrientation.Horizontal,
            Panes = new()
            {
                new IgbTabGroupPane
                {
                    PaneType = DockManagerPaneType.TabGroupPane,
                    Panes = new()
                    {
                        new IgbContentPane { PaneType = DockManagerPaneType.ContentPane, ContentId = "panel1", Header = "Panel 1" },
                        new IgbContentPane { PaneType = DockManagerPaneType.ContentPane, ContentId = "panel2", Header = "Panel 2" }
                    }
                },
                new IgbContentPane { PaneType = DockManagerPaneType.ContentPane, ContentId = "panel3", Header = "Panel 3", Size = 250 }
            }
        }
    };
}
```

- `IgbContentPane.ContentId` must match the projected element's `slot` exactly, or the pane renders empty.
- The Dock Manager needs an explicit height; without one it collapses to 0px.
- Layout persistence is version-sensitive. Check the installed API before writing serialization code — only pane structure and positions round-trip, never the slot content.
