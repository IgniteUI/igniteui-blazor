# Layout Managers — Ignite UI for Blazor

This reference covers the Tile Manager and Dock Manager components for building dynamic dashboard and panel layouts.

---

## IgbTileManager

A component that enables dynamic arrangement, resizing, and interaction of tiles in a grid layout. Users can drag tiles to rearrange them and resize them within the grid.

### Basic usage

```razor
<IgbTileManager ColumnCount="3"
                 MinColumnWidth="200px"
                 MinRowHeight="200px"
                 Gap="8px"
                 DragMode="TileManagerDragMode.Slide"
                 ResizeMode="TileManagerResizeMode.Grow">
    <IgbTile ColSpan="2" RowSpan="1">
        <div slot="header">
            <h3>Sales Overview</h3>
        </div>
        <div>Chart content here</div>
    </IgbTile>
    <IgbTile ColSpan="1" RowSpan="1">
        <div slot="header">
            <h3>Revenue</h3>
        </div>
        <div>Revenue KPI</div>
    </IgbTile>
    <IgbTile ColSpan="1" RowSpan="2">
        <div slot="header">
            <h3>Top Products</h3>
        </div>
        <div>Product list</div>
    </IgbTile>
    <IgbTile ColSpan="2" RowSpan="1">
        <div slot="header">
            <h3>Recent Activity</h3>
        </div>
        <div>Activity timeline</div>
    </IgbTile>
</IgbTileManager>
```

### IgbTileManager parameters

| Parameter | Type | Description |
|---|---|---|
| `ColumnCount` | `int` | Number of columns in the grid. Also sets the `--column-count` CSS property. |
| `MinColumnWidth` | `string` | Minimum column width (e.g., `"200px"`). Sets the `--min-col-width` CSS property. |
| `MinRowHeight` | `string` | Minimum row height (e.g., `"200px"`). Sets the `--min-row-height` CSS property. |
| `Gap` | `string` | Gap between tiles (e.g., `"8px"`, `"1rem"`). Sets the `--grid-gap` CSS property. |
| `DragMode` | `TileManagerDragMode` | `None`, `Slide`, `Swap` |
| `ResizeMode` | `TileManagerResizeMode` | `None`, `Grow` |

### DragMode options

| Value | Description |
|---|---|
| `None` | Drag disabled |
| `Slide` | Tiles slide to make room for the dragged tile |
| `Swap` | Dragged tile swaps position with the target tile |

### ResizeMode options

| Value | Description |
|---|---|
| `None` | Resize disabled |
| `Grow` | Tiles can be resized by dragging their edges |

### IgbTile parameters

| Parameter | Type | Description |
|---|---|---|
| `ColSpan` | `double` | Number of columns the tile spans (min 1) |
| `RowSpan` | `double` | Number of rows the tile spans (min 1) |
| `ColStart` | `double` | Column start position (1-based) |
| `RowStart` | `double` | Row start position (1-based) |
| `Maximized` | `bool` | Whether the tile is in maximized state |
| `DisableDrag` | `bool` | Disables dragging for this individual tile |
| `DisableResize` | `bool` | Disables resizing for this individual tile |

### IgbTile slots

| Slot | Description |
|---|---|
| `header` | Tile header area (title bar) |
| `header-actions` | Header action buttons (maximize, close, etc.) |
| (default) | Tile body content |

### Events (IgbTileManager)

| Event | Type | Description |
|---|---|---|
| `TileLayoutChanged` | `EventCallback` | Fires when tile positions change after drag/resize |

### Events (IgbTile)

| Event | Type | Description |
|---|---|---|
| `MaximizedChanged` | `EventCallback<bool>` | Fires when the tile's maximized state changes |

### Maximizable tiles

```razor
<IgbTileManager ColumnCount="3" Gap="8px"
                 DragMode="TileManagerDragMode.Slide"
                 ResizeMode="TileManagerResizeMode.Grow">
    <IgbTile ColSpan="1" RowSpan="1" @bind-Maximized="tile1Maximized">
        <div slot="header">
            <h3>Widget 1</h3>
        </div>
        <div slot="header-actions">
            <IgbIconButton Variant="IconButtonVariant.Flat"
                            Collection="material"
                            Name="@(tile1Maximized ? "fullscreen_exit" : "fullscreen")"
                            Click="() => tile1Maximized = !tile1Maximized" />
        </div>
        <div>Content</div>
    </IgbTile>
</IgbTileManager>

@code {
    private bool tile1Maximized = false;
}
```

### Dynamic tiles

```razor
<IgbTileManager ColumnCount="4" Gap="8px"
                 DragMode="TileManagerDragMode.Slide">
    @foreach (var widget in widgets)
    {
        <IgbTile @key="widget.Id" ColSpan="@widget.ColSpan" RowSpan="@widget.RowSpan">
            <div slot="header">
                <h3>@widget.Title</h3>
            </div>
            <div>@widget.Content</div>
        </IgbTile>
    }
</IgbTileManager>

@code {
    private List<Widget> widgets = new()
    {
        new(1, "Chart", "Chart content", 2, 1),
        new(2, "Stats", "Stats content", 1, 1),
        new(3, "Table", "Table content", 1, 2),
        new(4, "Map", "Map content", 2, 1)
    };

    record Widget(int Id, string Title, string Content, int ColSpan, int RowSpan);
}
```

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbTileManagerModule),
    typeof(IgbTileModule)
);
```

---

## IgbDockManager

A licensed web component that provides a Visual Studio-like docking panel layout. In Blazor, the `IgbDockManager` renders the `igc-dockmanager` web component and its layout is defined programmatically in C# code.

> **Note:** `IgbDockManager` is available only in the licensed `IgniteUI.Blazor` package, not in `IgniteUI.Blazor.Lite`.

### Basic usage

The DockManager layout is defined as a hierarchical pane structure in C#:

```razor
<IgbDockManager @ref="dockManager"
                 Style="width: 100%; height: 600px;"
                 Layout="layout" />

@code {
    private IgbDockManager dockManager = default!;

    private object layout = new
    {
        rootPane = new
        {
            type = "splitPane",
            orientation = "horizontal",
            panes = new object[]
            {
                new
                {
                    type = "contentPane",
                    header = "Properties",
                    contentId = "properties"
                },
                new
                {
                    type = "splitPane",
                    orientation = "vertical",
                    size = 200,
                    panes = new object[]
                    {
                        new
                        {
                            type = "contentPane",
                            header = "Editor",
                            contentId = "editor"
                        },
                        new
                        {
                            type = "contentPane",
                            header = "Output",
                            contentId = "output"
                        }
                    }
                }
            }
        }
    };
}
```

### Pane types

| Type | Description |
|---|---|
| `splitPane` | Container that splits into horizontal/vertical sub-panes |
| `contentPane` | A panel with content, identified by `contentId` |
| `tabGroupPane` | A group of tabbed content panes |
| `documentHost` | The main document area (like VS Code's editor area) |

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Layout` | `object` | The pane layout definition object |
| `Style` | `string` | CSS style (must set height for the dock manager to display) |

### Important notes

- The DockManager requires explicit height via `Style` or CSS.
- Pane content is rendered into slots matching the `contentId` values.
- Layout changes from user interaction (drag, undock, resize) update the `Layout` property.
- The DockManager is a complex component — refer to the official Infragistics documentation for advanced scenarios like floating panes, pinned panes, and serialization.

### Registration

```csharp
// DockManager module registration may vary — check official docs
builder.Services.AddIgniteUIBlazor();
```

---

## Key Rules

1. **TileManager grid model** — The tile grid respects `ColumnCount`, `MinColumnWidth`, and `MinRowHeight`. Tiles with `ColSpan`/`RowSpan` > 1 span multiple cells.
2. **ColSpan/RowSpan minimum** — Values less than 1 are coerced to 1.
3. **CSS custom properties** — `ColumnCount`, `MinColumnWidth`, `MinRowHeight`, and `Gap` set CSS variables (`--column-count`, `--min-col-width`, `--min-row-height`, `--grid-gap`) on the tile manager element. You can also set these via CSS stylesheets.
4. **DockManager is licensed** — The DockManager component is only available in the full `IgniteUI.Blazor` package, not in `IgniteUI.Blazor.Lite`.
5. **DockManager height** — Always explicitly set a height on `IgbDockManager`. Without a height, the component will collapse to zero height.
6. **@key for dynamic tiles** — When rendering tiles dynamically in a `@foreach` loop, always use `@key` to help Blazor's diffing algorithm.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [layout.md](layout.md) — Tabs, Accordion, NavDrawer for app-level layout
- [charts.md](charts.md) — Charts commonly placed inside tiles
- [data-display.md](data-display.md) — Lists, cards, and progress indicators for tile content
- [directives.md](directives.md) — Buttons for tile header actions
