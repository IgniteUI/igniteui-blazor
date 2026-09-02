# Layout & Navigation Components

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Tabs](#tabs)
- [Stepper](#stepper)
- [Accordion & Expansion Panel](#accordion--expansion-panel)
- [Navbar](#navbar)
- [Navigation Drawer](#navigation-drawer)
- [Tree](#tree)
- [Splitter](#splitter)
- [Virtual Scroll](#virtual-scroll)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on layout and navigation components, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Tabs

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTabsModule));
```

```razor
<IgbTabs>
    <IgbTab Label="Tab 1">
      <span>Content for tab 1</span>
    </IgbTab>
    <IgbTab Label="Tab 2">
      <span>Content for tab 2</span>
    </IgbTab>
    <IgbTab>
      <div slot="label">Tab 3</div>
      <span>Content for tab 3</span>
    </IgbTab>
</IgbTabs>
```
Tab text can be set either as simple string using the `Label` property or by assigning children to the `label` slot. Any remaining children in the default slot are rendered as the tab content.

For icon tabs, use the `label` slot inside `IgbTab`:

```razor
<IgbTab>
    <IgbIcon slot="label" IconName="home" Collection="material" />
    Home
</IgbTab>
```

---

## Stepper

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbStepperModule));
```

```razor
<IgbStepper Linear="true" Orientation="StepperOrientation.Horizontal" @ref="StepperRef">
    <IgbStep>
        <span slot="title">Personal Info</span>
        <!-- step content -->
        <IgbInput Label="Name" />
    </IgbStep>
    <IgbStep>
        <span slot="title">Address</span>
        <!-- step content -->
    </IgbStep>
    <IgbStep>
        <span slot="title">Confirm</span>
        <!-- step content -->
    </IgbStep>
</IgbStepper>

@code {
    IgbStepper StepperRef { get; set; }

    void GoNext() => StepperRef.Next();
    void GoPrev() => StepperRef.Prev();
}
```

---

## Accordion & Expansion Panel

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbAccordionModule));
// IgbExpansionPanel is included in IgbAccordionModule
```

```razor
<!-- Accordion (wraps multiple expansion panels) -->
<IgbAccordion SingleExpand="true">
    <IgbExpansionPanel>
        <span slot="title">Section 1</span>
        <span slot="subtitle">Optional subtitle</span>
        <p>Content for section 1.</p>
    </IgbExpansionPanel>
    <IgbExpansionPanel Open="true">
        <span slot="title">Section 2</span>
        <p>Content for section 2.</p>
    </IgbExpansionPanel>
</IgbAccordion>

<!-- Standalone expansion panel -->
<IgbExpansionPanel @ref="PanelRef">
    <span slot="title">Details</span>
    <p>Expanded content here.</p>
</IgbExpansionPanel>

@code {
    IgbExpansionPanel PanelRef { get; set; }
    void Toggle() => PanelRef.Toggle();
}
```

---

## Navbar

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbNavbarModule));
```

```razor
<IgbNavbar>
    <IgbIconButton slot="start" IconName="menu" Collection="material" @onclick="() => DrawerRef.Toggle()" />
    <h3>My Application</h3>
    <IgbIconButton slot="end" IconName="search" Collection="material" />
    <IgbIconButton slot="end" IconName="more_vert" Collection="material" />
</IgbNavbar>
```

> **AGENT INSTRUCTION:** Register icons used by `IgbNavbar` and `IgbIconButton` before relying on them in samples. Call `await iconRef.EnsureReady()` before `RegisterIconAsync()` or `RegisterIconFromTextAsync()`.

---

## Navigation Drawer

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbNavDrawerModule),
    typeof(IgbNavDrawerHeaderItemModule)
);
```

```razor
<IgbNavDrawer @ref="DrawerRef" Open="true">
    <IgbNavDrawerHeaderItem>My App</IgbNavDrawerHeaderItem>

    <IgbNavDrawerItem @ref="HomeItem" @onclick="() => Activate(HomeItem)">
        <IgbIcon slot="icon" IconName="home" Collection="material" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>

    <IgbNavDrawerItem @ref="SearchItem" @onclick="() => Activate(SearchItem)">
        <IgbIcon slot="icon" IconName="search" Collection="material" />
        <span slot="content">Search</span>
    </IgbNavDrawerItem>
</IgbNavDrawer>

<IgbIconButton IconName="menu" Collection="material" @onclick="() => DrawerRef.Toggle()" />

@code {
    IgbNavDrawer DrawerRef { get; set; }
    IgbNavDrawerItem HomeItem { get; set; }
    IgbNavDrawerItem SearchItem { get; set; }

    List<IgbNavDrawerItem> AllItems => new() { HomeItem, SearchItem };

    void Activate(IgbNavDrawerItem item)
    {
        item.Active = true;
        foreach (var i in AllItems.Where(x => x != item))
            i.Active = false;
    }
}
```

Navbar integration:

```razor
<IgbNavbar>
    <IgbIconButton slot="start" IconName="menu" Collection="material" @onclick="() => DrawerRef.Show()" />
    <span>Home</span>
</IgbNavbar>

<IgbNavDrawer @ref="DrawerRef" Open="true" Position="NavDrawerPosition.Start">
    <IgbNavDrawerHeaderItem>Navigation</IgbNavDrawerHeaderItem>

    <IgbNavDrawerItem @ref="HomeItem" @onclick="() => Activate(HomeItem)">
        <IgbIcon slot="icon" IconName="home" Collection="material" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>

    <IgbNavDrawerItem @ref="SearchItem" @onclick="() => Activate(SearchItem)">
        <IgbIcon slot="icon" IconName="search" Collection="material" />
        <span slot="content">Search</span>
    </IgbNavDrawerItem>
</IgbNavDrawer>
```

Mini variant:

```razor
<IgbNavDrawer @ref="DrawerRef" Open="true">
    <IgbNavDrawerHeaderItem>Navigation</IgbNavDrawerHeaderItem>

    <IgbNavDrawerItem>
        <IgbIcon slot="icon" IconName="home" Collection="material" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>

    <div slot="mini">
        <IgbNavDrawerItem>
            <IgbIcon slot="icon" IconName="home" Collection="material" />
        </IgbNavDrawerItem>
    </div>
</IgbNavDrawer>
```

> **AGENT INSTRUCTION - IgbNavDrawer shadow DOM mechanics:**
>
> Regardless of `Open` state or `style` on the host, `::part(base)` is always rendered as `position: fixed; transform: translateX(-Npx)`. When the component considers itself closed it also sets `inert` on `::part(base)`. The host element itself contributes `width: 0` to the layout because the fixed part takes no space.
>
> This means:
> - `Open="true"` alone makes the panel visible but it still floats over content as an overlay.
> - `slot="mini"` content switches the component to a collapsible expand/collapse mode with an icon-only collapsed state.
> - To make the drawer occupy real space in the layout (pinned sidebar), the shadow DOM parts must be overridden in **global CSS** (not `.razor.css`): give the host an explicit width, override `::part(base)` to `position: relative; transform: none`, hide `::part(overlay)`, and remove the `inert` attribute via JS in `OnAfterRenderAsync`. Do **not** call `DrawerRef.Show()` in `OnAfterRenderAsync` - it throws "component not ready"; CSS handles visibility instead.

> **AGENT INSTRUCTION:** Icons used inside `IgbNavDrawerItem` must be registered via `IgbIcon.RegisterIconFromTextAsync()` or `RegisterIconAsync()` in `OnAfterRenderAsync(bool firstRender)` before they display. Call `await iconRef.EnsureReady()` first.

---

## Tree

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTreeModule), typeof(IgbTreeItemModule));
```

```razor
<IgbTree Selection="TreeSelection.Multiple">
    <IgbTreeItem Expanded="true" Label="Documents">
        <IgbTreeItem Label="Report.docx" />
        <IgbTreeItem Label="Notes.txt" />
    </IgbTreeItem>
    <IgbTreeItem Label="Downloads">
        <IgbTreeItem Label="archive.zip" />
    </IgbTreeItem>
</IgbTree>
```

---

## Splitter

Resizable split-pane layout dividing the view into *start* and *end* panels separated by a draggable bar.

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSplitterModule));
```

```razor
<IgbSplitter Orientation="SplitterOrientation.Horizontal"
             StartSize="30%"
             StartMinSize="100px"
             StartCollapsed="StartCollapsed"
             LayoutChanged="OnLayoutChanged"
             style="height: 400px;">
    <div slot="start">Start panel</div>
    <div slot="end">End panel</div>
</IgbSplitter>

@code {
    bool StartCollapsed { get; set; }

    void OnLayoutChanged(IgbSplitterLayoutChangedEventArgs e)
    {
        // e.Detail: StartSize, EndSize, StartCollapsed, EndCollapsed
        StartCollapsed = e.Detail.StartCollapsed;
    }
}
```

- `StartCollapsed` / `EndCollapsed` read and set the collapsed state of each pane; `ToggleAsync(PanePosition.Start)` toggles programmatically.
- `LayoutChanged` fires after a user-driven resize or expansion change with a full layout snapshot; `ResizeStart`/`Resizing`/`ResizeEnd` report pixel sizes during a drag.
- Pane sizes (`StartSize`, `EndSize`, min/max constraints) accept CSS lengths (`200px`, `30%`).

---

## Virtual Scroll

Renders large or unbounded lists efficiently - only the items in the viewport plus a configurable `OverScan` are rendered.

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbVirtualScrollModule));
```

```razor
<IgbVirtualScroll Data="Items"
                  EstimatedItemSize="40"
                  ItemTemplateScript="MyItemTemplate"
                  DataRequest="OnDataRequest"
                  style="height: 400px;" />

@code {
    Item[] Items { get; set; } = LoadFirstPage();

    void OnDataRequest(IgbVirtualScrollDataRequestEventArgs e)
    {
        // Infinite scroll: append at least e.Detail.Count items starting
        // at e.Detail.StartIndex, assigning a NEW collection reference.
        Items = [.. Items, .. LoadMore((int)e.Detail.StartIndex, (int)e.Detail.Count)];
    }
}
```

The item template is a client-side function registered before the component renders (e.g. from a JS module loaded in `OnAfterRenderAsync`); it receives the item context (`value`, `index`, `count`) and returns a template built with `window.igTemplating.html`:

```js
window.igRegisterScript('MyItemTemplate', (ctx) => {
    const html = window.igTemplating.html;
    return html`<div style="padding: 8px;">${ctx.value.Name}</div>`;
}, false);
```

- `Data` is compared by reference - assign a new collection instead of mutating in place.
- `ScrollToIndexAsync(index)` scrolls to an item (optionally with `IgbScrollIntoViewOptions` for alignment/behavior); `StateChange` reports the rendered window (`StartIndex`, `EndIndex`, `ViewportSize`, `TotalSize`).
- Supports `Orientation` (`Vertical`/`Horizontal`) and RTL layouts; item sizes are measured automatically after first render.

---

## Key Rules

1. **Stepper with `Linear="true"` prevents users from skipping steps.** Do not set `Linear` if free navigation is intended.
2. **Activate/deactivate `IgbNavDrawerItem` programmatically** by setting `item.Active` - there is no automatic selection tracking.
3. **Register icons via `RegisterIconFromTextAsync` in `OnAfterRenderAsync(bool firstRender)`**, and always call `await component.EnsureReady()` first.
4. **`IgbAccordion` with `SingleExpand="true"` closes other panels when one is opened.** This is the most common use case for accordions.
