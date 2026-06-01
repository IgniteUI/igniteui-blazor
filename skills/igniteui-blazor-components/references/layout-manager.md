# Layout Manager Components - Dock Manager & Tile Manager

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Dock Manager](#dock-manager)
  - [Basic Setup](#basic-setup)
  - [Layout Serialization](#layout-serialization)
- [Tile Manager](#tile-manager)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on when to use each layout manager component, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Dock Manager

Dock Manager provides an IDE-like dockable pane layout. Users can drag panes to dock, float, pin/unpin, and close them at runtime.

```csharp
// In Program.cs or Startup.cs
builder.Services.AddIgniteUIBlazor(typeof(IgbDockManagerModule));
```

### Basic Setup

Dock Manager uses a C# layout object graph to define pane structure. Pane content is projected via named slots.

```razor
<IgbDockManager @ref="DockRef" Layout="DockLayout" style="height: 600px;">
    <!-- Content slots - the slot name matches IgbContentPane.ContentId -->
    <div slot="panel1">Panel 1 Content</div>
    <div slot="panel2">Panel 2 Content</div>
    <div slot="panel3">Panel 3 Content</div>
</IgbDockManager>

@code {
    IgbDockManager DockRef { get; set; }

    // IgbDockManagerLayout has two top-level properties:
    //   RootPane (IgbSplitPane) - the main docked layout
    //   FloatingPanes (IgbSplitPaneCollection) - initially floating panes
    IgbDockManagerLayout DockLayout { get; set; } = new IgbDockManagerLayout
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
                        new IgbContentPane
                        {
                            PaneType = DockManagerPaneType.ContentPane,
                            ContentId = "panel1",
                            Header = "Panel 1"
                        },
                        new IgbContentPane
                        {
                            PaneType = DockManagerPaneType.ContentPane,
                            ContentId = "panel2",
                            Header = "Panel 2"
                        }
                    }
                },
                new IgbContentPane
                {
                    PaneType = DockManagerPaneType.ContentPane,
                    ContentId = "panel3",
                    Header = "Panel 3",
                    Size = 250
                }
            }
        }
    };
}
```

### Layout Serialization

Persisting a Dock Manager layout is version-sensitive. The current `dock-manager` MCP overview documents the layout object graph, pane content slots, styling, and keyboard behavior; it does not show a verified Blazor serialization sample in this reference. Before generating persistence code, inspect the current installed API and use only the documented event signatures and methods for that version.

> **AGENT INSTRUCTION - Layout serialization:** The serialized JSON contains only pane structure and positions. It does **not** serialize the slot content. The slot content markup must remain static in the Razor template; only pane arrangement changes at runtime.

---

## Tile Manager

Tile Manager provides a resizable, draggable tile/widget dashboard layout.

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTileManagerModule));
```

```razor
<IgbTileManager ColumnCount="4" Gap="8px" ResizeMode="@TileManagerResizeMode.Always" DragMode="@TileManagerDragMode.TileHeader">
    <IgbTile ColSpan="2" RowSpan="1">
        <span slot="title">Revenue Chart</span>
        <!-- tile content -->
    </IgbTile>
    <IgbTile ColSpan="1" RowSpan="2">
        <span slot="title">KPIs</span>
        <!-- tile content -->
    </IgbTile>
    <IgbTile ColSpan="1">
        <span slot="title">Recent Orders</span>
        <!-- tile content -->
    </IgbTile>
</IgbTileManager>
```

---

## Key Rules

1. **`IgbContentPane.ContentId` must exactly match the `slot` attribute of the projected HTML element.** A mismatch causes the pane to render empty.
2. **Dock Manager must have an explicit height** (via CSS or inline style). Without a height it renders as 0px.
3. **Layout serialization only persists structure, not content.** Slot content is always defined in Razor markup.
4. **`IgbTileManager` uses CSS Grid internally.** Set `ColumnCount` to control the number of columns.
5. **Do not invent Dock Manager serialization APIs.** Use `dock-manager` and the installed API before writing persistence code.
6. **Tile Manager serialization uses `SaveLayout()` / `LoadLayout(string)`.** The saved payload stores tile layout properties, not tile content.
