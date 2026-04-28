# Grid Sizing & Density

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Grid Height and Width](#grid-height-and-width)
- [Column Width](#column-width)
- [Row Height](#row-height)
- [Grid Size](#grid-size)
- [Auto-Sizing to Container](#auto-sizing-to-container)
- [Key Rules](#key-rules)

---

## Grid Height and Width

Use `grid-size` only for density and `--ig-size`.

```razor
<!-- Fixed height and width -->
<IgbGrid Data="Data" Height="600px" Width="100%">...</IgbGrid>

<!-- Percentage height requires parent to have a fixed height -->
<div style="height: 800px;">
    <IgbGrid Data="Data" Height="100%" Width="100%">...</IgbGrid>
</div>
```

Key sizing attributes:

| Attribute | Type | Description |
|---|---|---|
| `Height` | `string` | CSS height string (`"600px"`, `"100%"`, `"80vh"`) |
| `Width` | `string` | CSS width string (`"100%"`, `"800px"`) |

> **AGENT INSTRUCTION - Height is required for virtualization**
>
> Set a real resolved `Height` whenever the grid has more than about 50 rows. `Height="100%"` only works when the parent has a defined height. Null/no resolved height disables vertical virtualization and renders all rows, which is slow for large datasets.

---

## Column Width

Control default and per-column widths:

```razor
<!-- Default column width applied to all columns without explicit width -->
<IgbGrid Data="Data" ColumnWidth="150px">
    <IgbColumn Field="Name" Width="250px" />   <!-- overrides default -->
    <IgbColumn Field="Price" />                <!-- uses default 150px -->
    <IgbColumn Field="Stock" Width="100px" />  <!-- overrides default -->
</IgbGrid>
```

Set min/max width per column for resizable columns:

```razor
<IgbColumn Field="Description" Width="200px" MinWidth="100px" MaxWidth="500px" Resizable="true" />
```

Percentage widths:

```razor
<IgbColumn Field="Name" Width="40%" />
<IgbColumn Field="Category" Width="30%" />
<IgbColumn Field="Price" Width="30%" />
```

---

## Row Height

```razor
<!-- Fixed row height -->
<IgbGrid Data="Data" RowHeight="50px">
    ...
</IgbGrid>
```

`RowHeight` takes a CSS size string such as `"50px"` or `"80px"`. Setting it to a smaller value increases the visible row density.

For very tall rows, such as rows with embedded images or multi-line content, set a larger CSS size string to prevent content overflow.

---

## Grid Size

Grid size controls the overall padding/spacing of the grid UI, including headers, cells, and the toolbar, through the CSS `--ig-size` variable.

```css
.dense-grid {
    --ig-size: var(--ig-size-small);
}
```

```razor
<IgbGrid Data="Data" Class="dense-grid">...</IgbGrid>
```

| Value | Description |
|---|---|
| `--ig-size-large` | Default, larger padding |
| `--ig-size-medium` | Medium density |
| `--ig-size-small` | Small density |

If `RowHeight` is set, it overrides size-driven row height, but `--ig-size` still affects other grid spacing.

---

## Auto-Sizing to Container

To make the grid fill its parent container without overflow:

```razor
<!-- Parent must have a defined height -->
<div class="grid-wrapper">
    <IgbGrid Data="Data" Height="100%" Width="100%">...</IgbGrid>
</div>
```

```css
.grid-wrapper {
    height: calc(100vh - 120px);
    overflow: hidden;
}
```

To auto-size to content, only for small datasets:

```razor
<!-- Confirm the exact null/omitted Height syntax with grid-sizing before writing code -->
<IgbGrid Data="SmallList" Width="100%">...</IgbGrid>
```

---

## Key Rules

1. **Always set a real resolved `Height` for large datasets** - this enables row virtualization and prevents browser performance issues.
2. **`Height="100%"` requires the parent container to have a fixed height.** A percentage height on the grid with no fixed-height ancestor does not create a usable grid height.
3. **`ColumnWidth` on the grid is the default for all columns without explicit `Width`.** Per-column `Width` overrides the grid-level default.
4. **`RowHeight` is a CSS size string.** Use values such as `"50px"` or `"80px"`, matching the Blazor `grid-size` MCP doc.
5. **Use `--ig-size: var(--ig-size-small | --ig-size-medium | --ig-size-large)`** for grid density. Do not use legacy density enum guidance for Blazor.
6. **Column virtualization behavior depends on resolved grid width and column widths.** Use `grid-sizing` for null, pixel, and percentage sizing rules before writing guidance.
