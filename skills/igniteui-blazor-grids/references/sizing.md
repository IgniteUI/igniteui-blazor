# Sizing — Grid Dimensions, Column Widths, Density

## Grid width and height

```razor
<IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="600px">…</IgbGrid>

<div style="height: 80vh;">
    <IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="100%">…</IgbGrid>
</div>
```

`Width` defaults to `100%` of the parent. If the columns are wider than the grid a horizontal scrollbar appears and column virtualization activates; if they are narrower they stretch to fill.

**`Height` has no default.** Unset, the grid renders every row with no vertical scrollbar and **row virtualization is disabled** — fine for a handful of rows, ruinous past ~50. There is no auto-height mode; to fit a bounded number of rows, compute it:

```razor
@{
    var rowCount = Math.Min(data.Count, 20);
    var gridHeight = $"{50 + rowCount * 50}px";   // header + rows, at the default density
}
<IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="@gridHeight">…</IgbGrid>
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
