# Layout Manager Components - Dock Manager & Tile Manager

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Dock Manager](#dock-manager)
  - [Basic Setup](#basic-setup)
  - [Pane Types](#pane-types)
  - [Layout Serialization](#layout-serialization)
  - [Events](#events)
- [Tile Manager](#tile-manager)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on when to use each layout manager component, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Dock Manager

> **Docs:** [Dock Manager](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/layouts/dock-manager)

Dock Manager provides an IDE-like dockable pane layout. Users can drag panes to dock, float, pin/unpin, and close them at runtime.

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDockManagerModule));
```

Key properties on `IgbDockManager`:

| Property | Type | Description |
|---|---|---|
| `Layout` | `IgbDockManagerLayout` | Gets/sets the layout configuration |
| `ActivePane` | `IgbContentPane` | Gets/sets the currently active (focused) pane |
| `MaximizedPane` | `IgbDockManagerPane` | Gets/sets the maximized pane |
| `AllowFloatingPanesResize` | `bool` | Allows resizing floating panes (default: `true`) |
| `AllowInnerDock` | `bool` | Allows inner-docking panes (default: `true`) |
| `AllowMaximize` | `bool` | Allows maximizing panes (default: `true`) |
| `AllowSplitterDock` | `bool` | Allows docking by dragging over a splitter (default: `false`) |
| `CloseBehavior` | `PaneActionBehavior` | `AllPanes` or `SelectedPane` — which pane(s) close when clicking close in a TabGroup (default: `AllPanes`) |
| `UnpinBehavior` | `PaneActionBehavior` | `AllPanes` or `SelectedPane` — which pane(s) unpin when clicking unpin (default: `AllPanes`) |
| `ContainedInBoundaries` | `bool` | Keeps floating panes inside the Dock Manager bounds (default: `false`) |
| `ProximityDock` | `bool` | Enables docking by proximity instead of indicators (default: `false`) |
| `ShowPaneHeaders` | `DockManagerShowPaneHeaders` | `Always` or `OnHoverOnly` (default: `Always`) |
| `ShowHeaderIconOnHover` | `DockManagerShowHeaderIconOnHover` | Which tab icons show on hover: `None`, `All`, `CloseOnly`, `MoreOptionsOnly` (default: `None`) |
| `DisableKeyboardNavigation` | `bool` | Disables built-in keyboard shortcuts (default: `false`) |

Key methods on `IgbDockManager`:

| Method | Returns | Description |
|---|---|---|
| `FocusPane(string contentId)` | `void` | Programmatically focuses a pane by its content ID |
| `GetCurrentLayout()` | `IgbDockManagerLayout` | Returns the current layout object reflecting runtime state |

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

### Pane Types

| Class | Description |
|---|---|
| `IgbSplitPane` | Container that splits horizontally or vertically into child panes |
| `IgbTabGroupPane` | Container that groups content panes into tabs |
| `IgbContentPane` | Leaf pane with actual content (maps to a named slot) |
| `IgbDocumentHost` | Special container for document-style tabbed panes (like an IDE editor area). Contains a `RootPane` (`IgbSplitPane`) |

Key properties on `IgbContentPane`:

| Property | Type | Description |
|---|---|---|
| `ContentId` | `string` | Must match the `slot` name on the projected content element |
| `Header` | `string` | Pane tab header title |
| `HeaderId` | `string` | Slot name for a custom header template element. Falls back to `Header` text if not set |
| `Size` | `double` | Size relative to sibling panes (default: `100`) |
| `AllowClose` | `bool` | Shows/hides the close button (default: `true`) |
| `AllowPinning` | `bool` | Allows pin/unpin (default: `true`) |
| `AllowMaximize` | `bool` | Allows maximize |
| `AllowFloating` | `bool` | Allows tearing off into a floating window (default: `true`) |
| `AllowDocking` | `bool` | Allows the user to dock the pane (default: `true`) |
| `IsPinned` | `bool` | Set to `false` to start as an unpinned (collapsed to edge) pane |
| `IsMaximized` | `bool` | Whether the pane is maximized (default: `false`) |
| `Hidden` | `bool` | Hides the pane from the UI (default: `false`) |
| `Disabled` | `bool` | Disables the pane (default: `false`) |
| `DocumentOnly` | `bool` | Restricts the pane so it can only be docked inside a document host |
| `UnpinnedLocation` | `UnpinnedLocation` | Edge where the unpinned flyout appears. Auto-calculated from document host if not set |
| `UnpinnedSize` | `double` | Absolute size of the pane when unpinned (default: `200`) |
| `Id` | `string` | Pane identifier. Auto-generated if not set |

Key properties on `IgbSplitPane`:

| Property | Type | Description |
|---|---|---|
| `Orientation` | `SplitPaneOrientation` | `Horizontal` or `Vertical` |
| `Panes` | `IgbDockManagerPaneCollection` | Child panes |
| `Size` | `double` | Size relative to sibling panes (default: `100`) |
| `FloatingLocation` | `IgbDockManagerPoint` | Absolute position of a floating pane |
| `FloatingWidth` | `double` | Absolute width of a floating pane (default: `100`) |
| `FloatingHeight` | `double` | Absolute height of a floating pane (default: `100`) |
| `FloatingResizable` | `bool` | Whether floating pane resizing is allowed |
| `AllowEmpty` | `bool` | Whether the pane stays in the UI when it has no children |
| `IsMaximized` | `bool` | Whether the split pane is maximized (default: `false`) |
| `UseFixedSize` | `bool` | Sizes children in pixels instead of relative; allows scrollable overflow (default: `false`) |
| `Id` | `string` | Pane identifier. Auto-generated if not set |

Key properties on `IgbTabGroupPane`:

| Property | Type | Description |
|---|---|---|
| `Panes` | `IgbContentPaneCollection` | Child content panes displayed as tabs |
| `SelectedIndex` | `double` | Index of the initially selected tab |
| `Size` | `double` | Size relative to sibling panes (default: `100`) |
| `AllowEmpty` | `bool` | Whether the tab group stays in the UI when it has no children |
| `IsMaximized` | `bool` | Whether the tab group is maximized (default: `false`) |
| `Id` | `string` | Pane identifier. Auto-generated if not set |

Key properties on `IgbDocumentHost`:

| Property | Type | Description |
|---|---|---|
| `RootPane` | `IgbSplitPane` | The root split pane inside the document host |
| `Size` | `double` | Size relative to sibling panes (default: `100`) |
| `Id` | `string` | Pane identifier. Auto-generated if not set |

### Layout Serialization

Persisting a Dock Manager layout is version-sensitive. The current `dock-manager` MCP overview documents the layout object graph, pane content slots, styling, and keyboard behavior; it does not show a verified Blazor serialization sample in this reference. Before generating persistence code, inspect the current installed API and use only the documented event signatures and methods for that version.

> **AGENT INSTRUCTION - Layout serialization:** The serialized JSON contains only pane structure and positions. It does **not** serialize the slot content. The slot content markup must remain static in the Razor template; only pane arrangement changes at runtime.

### Events

**Blazor EventCallbacks** (direct Blazor binding):

| Event | Type | Description |
|---|---|---|
| `LayoutChange` | `EventCallback<IgbLayoutChangeEventArgs>` | Fires when the layout is modified by user interaction (drag, close, resize) |
| `LayoutChanged` | `EventCallback<IgbDockManagerLayout>` | Fires after the layout has changed; provides the updated layout object |

**Web-component-level events** (accessible via JavaScript interop, not as Blazor EventCallbacks):

| Event | Description |
|---|---|
| `paneClose` | Fires when a pane is closed; detail contains the closed pane(s) |
| `activePaneChanged` | Fires when the active (focused) pane changes |
| `splitterResizeStart` | Fires when a splitter drag begins |
| `splitterResizeEnd` | Fires when a splitter drag ends |

---

## Tile Manager

> **Docs:** [Tile Manager](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/layouts/tile-manager)

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

Key attributes on `IgbTileManager`: `ColumnCount`, `Gap` (CSS length string such as `"8px"`), `MinColumnWidth`, `MinRowHeight`, `ResizeMode` (`TileManagerResizeMode.*`), `DragMode` (`TileManagerDragMode.*`).

Key attributes on `IgbTile`: `ColSpan`, `RowSpan`, `ColStart`, `RowStart`, `Maximized`, `Position`, `DisableFullscreen`, `DisableMaximize`, `DisableResize`.

Slots on `IgbTile`: `title` (header), `actions` (header action buttons), `fullscreen-action`, `maximize-action`, `side-adorner`, `corner-adorner`, `bottom-adorner`. Default slot = tile body content.

Methods on `IgbTileManager`: `SaveLayout()` and `LoadLayout(string)` persist tile order, size, and position. `GetTiles()` returns the current `IgbTile[]` array. Tile content is not serialized.

---

## Key Rules

1. **`IgbContentPane.ContentId` must exactly match the `slot` attribute of the projected HTML element.** A mismatch causes the pane to render empty.
2. **Dock Manager must have an explicit height** (via CSS or inline style). Without a height it renders as 0px.
3. **Layout serialization only persists structure, not content.** Slot content is always defined in Razor markup.
4. **`IgbTileManager` uses CSS Grid internally.** Set `ColumnCount` to control the number of columns.
5. **Do not invent Dock Manager serialization APIs.** Use `dock-manager` and the installed API before writing persistence code.
6. **Tile Manager serialization uses `SaveLayout()` / `LoadLayout(string)`.** The saved payload stores tile layout properties, not tile content.
