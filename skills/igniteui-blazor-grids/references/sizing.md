# Sizing — Grid Dimensions, Column Widths, Density

## Grid width and height

```razor
<IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="600px">…</IgbGrid>

<div style="height: 80vh;">
    <IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="100%">…</IgbGrid>
</div>
```

`Width` defaults to `100%` of the parent. If the columns are wider than the grid a horizontal scrollbar appears and column virtualization activates; if they are narrower they stretch to fill.

**`Height` sizes a wrapper `div`, not the grid element.** The `igc-grid` inside that wrapper always has `height: 100%`, so:

- `Height="600px"` (or any other CSS length): the grid is that tall.
- `Height="100%"`: the grid fills a parent with a definite height (a fixed height, or a flex/grid item that is stretched). In a parent without one, it acts as if unset.
- Unset: the grid is about 10 rows tall (roughly 560px at the default size). It does not grow to fit its rows, and it overflows a shorter parent.

Rows are virtualized in every case. There is no auto-height mode; to fit a bounded number of rows, compute it:

```razor
@{
    var rowCount = Math.Min(data.Count, 20);
    var gridHeight = $"{(rowCount + 1) * 51 + 1}px";   // header + rows (51px each at the default size)
}
<IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="@gridHeight">…</IgbGrid>
```

### Live data: give the grid element a pixel height

Because the element's own height is a percentage, the grid measures its container again on **every** data update. It briefly sets `display: none` on its body to read the container's height, which forces a layout and restarts every CSS animation in the visible cells, so a flash-on-change animation replays in rows that did not change (#481). Every Blazor `Height` value has this cost, `px`, `vh` and `calc()` included, because they all size the wrapper.

For grids that update often, set the element's `height` in pixels from JavaScript and keep it equal to the container. A height set this way survives later Blazor renders and new `Data`. Keep `Height="100%"` in Razor so the grid fills the container before the script runs, because the element's pixel value wins:

```js
// wwwroot/grid-live.js (features.md, "Live data", shows how it is attached to the grid and released)
// grid: the igc-grid element; host: the element that sets the available height
function keepPixelHeight(grid, host) {
    const observer = new ResizeObserver(() => {
        const height = Math.floor(host.clientHeight);   // rounded down so the grid never overflows the host
        if (height > 0) grid.height = `${height}px`;
    });
    observer.observe(host);
    return observer;   // observer.disconnect() when the page is disposed
}
```

## Column widths

**Default to no `Width` at all.** With none set, the grid distributes the available space proportionally (down to a ~136px minimum per column) and fills the container with no dead space on the right. Add widths only when the user asks for fixed sizing — and if you set a width on some columns, leave at least one without so it absorbs the remainder.

```razor
<IgbColumn Field="Id" Header="ID" Width="80px" />
<IgbColumn Field="Name" Header="Name" Width="200px" MinWidth="100px" MaxWidth="400px" Resizable="true" />
<IgbColumn Field="Notes" Header="Notes" />   @* absorbs the rest *@
```

- `Width` accepts px, `%`, or `"auto"`. `"auto"` fits header and visible content **once at initial render** and does not track later data changes.
- `MinWidth` / `MaxWidth` bound user resizing — set both whenever `Resizable="true"`.
- Widths are border-box: padding and borders are included.
- `ColumnWidth` on the grid sets a default for all columns instead of repeating a per-column `Width`.

Auto-size from code:

```razor
@code {
    private IgbGrid grid = default!;

    private void AutoSizeName() => grid.GetColumnByName("Name").Autosize(false);   // false = include cell content
    private void AutoSizeAll()  { foreach (var col in grid.Columns) col.Autosize(false); }
}
```

## Row height and density

Row height, cell padding, and header height all follow the `--ig-size` CSS custom property. All rows in a grid share one height — variable row heights are not supported.

| `--ig-size` | Row height |
|---|---|
| `var(--ig-size-large)` | ~50px (default) |
| `var(--ig-size-medium)` | ~40px |
| `var(--ig-size-small)` | ~32px |

```razor
<IgbGrid Data="data" PrimaryKey="Id" class="compact-grid">…</IgbGrid>
```

```css
/* global CSS */
.compact-grid { --ig-size: var(--ig-size-small); }
igc-grid       { --ig-size: var(--ig-size-medium); }
```

In a `.razor.css` isolation file prefix the `igc-*` selector with `::deep`. The grid exposes no separate cell-padding custom property; `--ig-size` is the single density knob.

An explicit override wins over the density scale:

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowHeight="60">…</IgbGrid>
```
