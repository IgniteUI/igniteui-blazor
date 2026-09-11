# Layout & Navigation Components

Module for every component below is `Igb<Name>Module`. `IgbExpansionPanel` ships inside `IgbAccordionModule`; `IgbNavDrawerHeaderItem` and `IgbTreeItem` have their own modules.

**Async vs sync methods.** Every method exists as `XAsync()` and a sync twin `X()`. Use the `Async` form: the sync twin needs `IJSInProcessRuntime` and throws `InvalidOperationException` on Blazor Server. It only works in WebAssembly and MAUI WebView.

## Tabs

```razor
<IgbTabs @ref="TabsRef" Alignment="TabsAlignment.Start" Change="OnTabChange">
    <IgbTab Label="Overview">
        <span>Content for the first tab</span>
    </IgbTab>
    <IgbTab Label="Details" Selected="true">
        <span>Content for the second tab</span>
    </IgbTab>
    <IgbTab>
        <IgbIcon slot="label" IconName="home" Collection="material" />
        <span>Content for the third tab</span>
    </IgbTab>
</IgbTabs>

@code {
    IgbTabs TabsRef { get; set; } = default!;
    void OnTabChange(IgbTabComponentEventArgs e) { }
}
```

Tab headers come from the `Label` string or from children in the `label` slot; everything left in the default slot is the panel body. There is **no** `IgbTabPanel` component. `IgbTab` has `Label`, `Selected`, `Disabled`. `IgbTabs` has `Alignment`, `Activation`, `SelectAsync(id)`, `GetSelectedAsync()`.

## Stepper

```razor
<IgbStepper @ref="StepperRef" Linear="true" Orientation="StepperOrientation.Horizontal">
    <IgbStep>
        <span slot="title">Personal Info</span>
        <IgbInput Label="Name" />
    </IgbStep>
    <IgbStep Optional="true">
        <span slot="title">Address</span>
    </IgbStep>
    <IgbStep>
        <span slot="title">Confirm</span>
    </IgbStep>
</IgbStepper>

@code {
    IgbStepper StepperRef { get; set; } = default!;
    Task GoNext() => StepperRef.NextAsync();
    Task GoPrev() => StepperRef.PrevAsync();
}
```

`IgbStepper`: `Linear`, `Orientation`, `StepType`, `TitlePosition`, `ContentTop`, `NextAsync()`, `PrevAsync()`, `NavigateToAsync(index)`, `ResetAsync()`, `ActiveStepChanging` / `ActiveStepChanged`.
`IgbStep`: `Active`, `Complete`, `Optional`, `Invalid`, `Disabled`, plus `title`, `subtitle`, `indicator` slots.

`Linear="true"` blocks skipping ahead — omit it when free navigation is intended.

## Accordion & Expansion Panel

```razor
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

@code {
    IgbExpansionPanel PanelRef { get; set; } = default!;
    Task Toggle() => PanelRef.ToggleAsync();   // Task<bool>
}
```

`IgbAccordion`: `SingleExpand` (closing others on open — the usual case), `ShowAllAsync()`, `HideAllAsync()`, and `Opening` / `Opened` / `Closing` / `Closed` carrying `IgbExpansionPanelComponentEventArgs`.
`IgbExpansionPanel`: `Open`, `Disabled`, `IndicatorPosition`, `ShowAsync()` / `HideAsync()` / `ToggleAsync()` (all `Task<bool>`). It works standalone as well as inside an accordion.

## Navbar

```razor
<IgbNavbar>
    <IgbIconButton slot="start" IconName="menu" Collection="material" @onclick="ToggleDrawer" />
    <h3>My Application</h3>
    <IgbIconButton slot="end" IconName="search" Collection="material" />
</IgbNavbar>
```

Purely slot-driven: `start`, default (title), `end`. Icons must be registered before they display — see [`data-display.md`](./data-display.md).

## Navigation Drawer

```razor
<IgbNavDrawer @ref="DrawerRef" Open="true" Position="NavDrawerPosition.Start">
    <IgbNavDrawerHeaderItem>My App</IgbNavDrawerHeaderItem>

    <IgbNavDrawerItem @ref="HomeItem" @onclick="() => Activate(HomeItem)">
        <IgbIcon slot="icon" IconName="home" Collection="material" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>
    <IgbNavDrawerItem @ref="SearchItem" @onclick="() => Activate(SearchItem)">
        <IgbIcon slot="icon" IconName="search" Collection="material" />
        <span slot="content">Search</span>
    </IgbNavDrawerItem>

    @* optional icon-only collapsed state *@
    <div slot="mini">
        <IgbNavDrawerItem>
            <IgbIcon slot="icon" IconName="home" Collection="material" />
        </IgbNavDrawerItem>
    </div>
</IgbNavDrawer>

@code {
    IgbNavDrawer DrawerRef { get; set; } = default!;
    IgbNavDrawerItem HomeItem { get; set; } = default!;
    IgbNavDrawerItem SearchItem { get; set; } = default!;

    Task ToggleDrawer() => DrawerRef.ToggleAsync();

    void Activate(IgbNavDrawerItem item)
    {
        HomeItem.Active = SearchItem.Active = false;
        item.Active = true;   // no automatic selection tracking
    }
}
```

`IgbNavDrawer`: `Open`, `Position`, `KeepOpenOnEscape`, `ShowAsync()` / `HideAsync()` / `ToggleAsync()`, `Closing` / `Closed`. `IgbNavDrawerItem`: `Active`, `Disabled`, `icon` and `content` slots.

**Overlay vs pinned sidebar.** `Position` is `Start | End | Top | Bottom | Relative`. With the first four the drawer is `position: fixed` and floats over the page with a dimming overlay. **`NavDrawerPosition.Relative` makes it a pinned, in-flow sidebar**: the panel becomes `position: relative`, the overlay is hidden, and closing it slides the panel out by a negative margin instead of leaving a gap. Use that instead of overriding `::part(base)` by hand.

```razor
<div class="app-shell">
    <IgbNavDrawer Open="true" Position="NavDrawerPosition.Relative">...</IgbNavDrawer>
    <main>@Body</main>
</div>
```

```css
/* global CSS or .razor.css — plain custom properties, no ::part needed */
igc-nav-drawer { --menu-full-width: 260px; --menu-mini-width: 60px; }
.app-shell { display: flex; height: 100vh; }
```

Width comes from the `--menu-full-width` custom property (default `15rem`); the collapsed icon rail uses `--menu-mini-width`. Content in `slot="mini"` renders whenever the drawer is closed, giving an expand/collapse rail. Do not call `ShowAsync()` from `OnAfterRenderAsync` — the component is not ready yet; drive visibility with `Open`.

## Tree

```razor
<IgbTree Selection="TreeSelection.Multiple" SingleBranchExpand="true"
         SelectionChanged="OnSelectionChanged">
    <IgbTreeItem Expanded="true" Label="Documents">
        <IgbTreeItem Label="Report.docx" />
        <IgbTreeItem Label="Notes.txt" />
    </IgbTreeItem>
    <IgbTreeItem Label="Downloads">
        <IgbTreeItem Label="archive.zip" />
    </IgbTreeItem>
</IgbTree>

@code {
    void OnSelectionChanged(IgbTreeSelectionEventArgs e) { }
}
```

`IgbTree`: `Selection` (`None | Multiple | Cascade`), `SingleBranchExpand`, `ToggleNodeOnClick`, `SelectionChanged`, `ItemExpanding` / `ItemExpanded` / `ItemCollapsing` / `ItemCollapsed`.
`IgbTreeItem`: `Label`, `Value`, `Expanded`, `Selected`, `Active`, `Disabled`, `Loading`, `Level`, `Parent`, `ExpandAsync()` / `CollapseAsync()` / `ToggleAsync()`.

## Splitter & Divider

```razor
<IgbSplitter Orientation="SplitterOrientation.Horizontal" StartSize="280px" StartMinSize="180px">
    <div slot="start">Sidebar</div>
    <div slot="end">Content</div>
</IgbSplitter>

<IgbDivider LineType="DividerType.Solid" Middle="true" />
```

`IgbSplitter`: `Orientation`, `StartSize` / `EndSize`, `StartMinSize` / `StartMaxSize` / `EndMinSize` / `EndMaxSize` (all CSS lengths — `280px`, `30%`), `StartCollapsed` / `EndCollapsed`, `DisableResize`, `DisableCollapse`, `HideDragHandle`, `HideCollapseButtons`, `ToggleAsync(PanePosition)`. `LayoutChanged` fires after a user resize or collapse with a full snapshot (`StartSize`, `EndSize`, `StartCollapsed`, `EndCollapsed`); `ResizeStart` / `Resizing` / `ResizeEnd` report pixel sizes during a drag.
`IgbDivider`: `Vertical`, `Middle`, `LineType`.
