# Structure — Setup, Columns, Templates, Sorting, Filtering, Selection

Applies to `IgbGrid`, `IgbTreeGrid`, and `IgbHierarchicalGrid`. Grid Lite and Pivot Grid differ — see [`types.md`](./types.md).

## Quick start

```csharp
// Program.cs
builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule));
```

```razor
@* _Imports.razor: @using IgniteUI.Blazor.Controls *@

<IgbGrid Data="employees" PrimaryKey="Id" AutoGenerate="false" Width="100%" Height="500px">
    <IgbColumn Field="Id" Header="ID" DataType="GridColumnDataType.Number" />
    <IgbColumn Field="Name" Header="Full Name" DataType="GridColumnDataType.String" Sortable="true" />
    <IgbColumn Field="HireDate" Header="Hire Date" DataType="GridColumnDataType.Date" Filterable="true" />
    <IgbColumn Field="Salary" Header="Salary" DataType="GridColumnDataType.Currency" />
    <IgbColumn Field="IsActive" Header="Active" DataType="GridColumnDataType.Boolean" />
</IgbGrid>

@code {
    private List<Employee> employees = new();
    protected override void OnInitialized() => employees = EmployeeService.GetAll();
}
```

With `AutoGenerate="true"` the grid creates a column per public property. Refine them through `ColumnInit`, or skip fields with `AutoGenerateExclude`:

```razor
<IgbGrid Data="employees" PrimaryKey="Id" AutoGenerate="true"
         AutoGenerateExclude='@(new string[] { "InternalCode" })'
         ColumnInit="OnColumnInit" />

@code {
    private void OnColumnInit(IgbColumnComponentEventArgs args)
    {
        var column = args.Detail;
        if (column.Field == "Salary")
        {
            column.DataType = GridColumnDataType.Currency;
            column.Editable = false;
        }
    }
}
```

## Columns

`GridColumnDataType`: `String`, `Number`, `Boolean`, `Date`, `DateTime`, `Time`, `Currency`, `Percent`, `Image`. It determines the display format, the available filter conditions, the sort comparison, and the default editor — always set it.

| Parameter | Type | Default | Purpose |
|---|---|---|---|
| `Field` | `string` | — | Property name on the data object |
| `Header` | `string` | field name | Header text |
| `DataType` | `GridColumnDataType` | `String` | Display, sort, filter, edit behavior |
| `Width` / `MinWidth` / `MaxWidth` | `string` | — | `"200px"`, `"20%"`, or `"auto"` |
| `Sortable` | `bool` | `false` | Sorting on this column |
| `Filterable` | `bool` | `true` | Filtering on this column (needs `AllowFiltering` on the grid) |
| `Editable` | `bool` | `false` | Editing on this column |
| `Resizable` | `bool` | `false` | User resizing |
| `Hidden` / `Pinned` | `bool` | `false` | Visibility / pinning |
| `Groupable` | `bool` | `false` | Grouping — `IgbGrid` only |
| `HasSummary` | `bool` | `false` | Column summaries |
| `Selectable` | `bool` | — | Column selection |
| `DisablePinning` / `DisableHiding` | `bool` | `false` | Remove from the toolbar UI |
| `CellClasses` / `HeaderClasses` | | — | Conditional CSS classes |

Leave `Width` unset unless the user asks for fixed sizing — see [`sizing.md`](./sizing.md).

## Templates

A column template is either a Blazor render fragment (`BodyTemplate`, below) or a JavaScript function named in `BodyTemplateScript` ([JavaScript cell templates](#javascript-cell-templates-bodytemplatescript)). Each render of a render-fragment cell goes through .NET, so for grids with live data, heavy scrolling, or Blazor Server, prefer the JavaScript template ([`features.md`](./features.md#virtualization--performance)).

In a render fragment the `context` needs a cast.

```razor
<IgbColumn Field="Salary" Header="Salary" DataType="GridColumnDataType.Currency" Editable="true">
    <BodyTemplate>
        @{
            var cell = (IgbCellTemplateContext)context;
            var salary = (decimal)cell.Cell.Value;
        }
        <span class="@(salary > 50000 ? "high-salary" : "")">@salary.ToString("C")</span>
    </BodyTemplate>

    <HeaderTemplate>
        <div style="display: flex; align-items: center; gap: 4px;">
            <IgbIcon IconName="payments" Collection="material" />
            <span>Salary</span>
        </div>
    </HeaderTemplate>

    <InlineEditorTemplate>
        @{ var cell = (IgbCellTemplateContext)context; }
        <IgbSelect @bind-Value="cell.Cell.EditValue">
            <IgbSelectItem Value="Active">Active</IgbSelectItem>
            <IgbSelectItem Value="Inactive">Inactive</IgbSelectItem>
        </IgbSelect>
    </InlineEditorTemplate>
</IgbColumn>
```

| Fragment | Context type | Reaches |
|---|---|---|
| `BodyTemplate` | `IgbCellTemplateContext` | `cell.Cell.Value`, `cell.Cell.Id.RowID` (the row's `PrimaryKey` value, which arrives as a `string` even for an `int` key). There is no `cell.Cell.Row`, so look the row up in your own list by that key |
| `HeaderTemplate` | `IgbColumnTemplateContext` | `ctx.Column.Header` |
| `InlineEditorTemplate` | `IgbCellTemplateContext` | `cell.Cell.EditValue` |
| `ErrorTemplate` | `IgbCellTemplateContext` | validation message slot |

Inside `IgbIcon`, the parameter is `IconName` — `Name` is the framework element identity, not the glyph.

### JavaScript cell templates (`BodyTemplateScript`)

`BodyTemplateScript` names a function registered on the client. The grid calls it in the browser with no .NET round trip, and it returns lit markup. Register it with `registerScript` and `html` from `_content/IgniteUI.Blazor/api.js`, at the top level of a module script that the host page loads before Blazor starts:

```html
<!-- in <head> of wwwroot/index.html (WebAssembly) or App.razor (Blazor Web App), after the import map -->
<script type="importmap"></script>   <!-- App.razor: <ImportMap /> -->
<script type="module" src="grid-templates.js"></script>
```

```js
// wwwroot/grid-templates.js
import { registerScript, html } from './_content/IgniteUI.Blazor/api.js';

registerScript('PriceCell', (ctx) => {
    const row = ctx.cell.row.data;            // read ctx.cell once: every read builds a new object
    return html`<span class="price-cell ${row.Change < 0 ? 'down' : 'up'}">${Number(ctx.implicit).toFixed(2)}</span>`;
});
```

```razor
<IgbColumn Field="Price" DataType="GridColumnDataType.Number" BodyTemplateScript="PriceCell" />
```

- **Register before the grid renders.** A name that is missing at render time logs `[Ignite UI] script 'PriceCell' is not registered …`, and registering it later does not apply it. A module script that the host page loads is in place in time.
- **`shouldCall`, the third argument, decides whether the registered function is the script (`false`) or is called once to produce it (`true`).** It defaults to `false`. Templates, `FormatterScript` and event `*Script` handlers need `false`. `CellClassesScript`, `RowClassesScript`, `CellStylesScript` and `RowStylesScript` take an object that maps a class name or CSS property to a function. Register that object directly (any flag), or a factory that returns it with `true`:

  ```js
  registerScript('PriceClasses', { 'is-down': (rowData, columnKey, cellValue, rowIndex) => rowData.Change < 0 });
  registerScript('PriceStyles', () => ({ 'font-weight': (rowData, columnKey, cellValue) => cellValue > 100 ? '700' : '400' }), true);
  ```

  A wrong flag mostly fails silently: a template or handler registered with `true` is called once with no arguments and never applied, a factory registered without `true` is never applied, and a formatter registered with `true` breaks the grid (`TypeError: this.formatter is not a function`).
- **`igRegisterScript` and `window.igTemplating.html` are deprecated** and log a warning. Their `shouldCall` defaults to `true`, so when you move existing code to `registerScript`, drop a `false` third argument and turn a missing one into `true`.
- **Markup.** `html` is lit-html 3's template tag. Attribute, `.property`, `?boolean` and `@event` bindings work, and a template can return `html` markup, a string, a number, an array, or `''`/`null` for an empty cell. Strings are escaped, never parsed as HTML. No lit directives ship (`classMap`, `repeat`, `keyed`, …): build class strings by hand. An `@click` inside a cell still activates and selects the cell, even with `stopPropagation()`. `igc-*` elements need their module registered in `Program.cs` ([client scripts](../../igniteui-blazor-components/references/client-scripts.md#templates)).

**Context.** `ctx` is a proxy, so `Object.keys(ctx)`, `{...ctx}` and `JSON.stringify(ctx)` all come back empty. Parameter destructuring (`({ implicit, cell }) =>`) does work.

| Expression | Value |
|---|---|
| `ctx.implicit` | the cell value (`row.data[field]`) |
| `ctx.cell` | a getter that builds a new cell object on every read. Read it once into a variable |
| `ctx.cell.row.data` | the grid's own record: the same object as the entry in the `igc-grid` element's `data` array, plus an internal `___id` |
| `ctx.cell.row.index`, `ctx.cell.id.rowID`, `ctx.cell.column.field` | row index, primary-key value, column field |
| `ctx.cell.active`, `.selected`, `.editMode` | cell state |
| `ctx.cell.grid` | the grid's internal component, not the element. The element is `ctx.cell.grid.nativeElement` |

**When a template runs again.** The cell re-renders only when one of its own inputs changes, so a template that also reads *other* fields of the row is not re-run when only those fields change, and shows stale values. Show only the column's own field (plus fields that always change with it). Otherwise, replace the changed row's record with a copy before assigning the new array (`data[i] = { ...data[i] }`), which re-renders that row only. Or call `markForCheck()` on the `igc-grid` element (C#: `await grid.MarkForCheckAsync()`), which re-runs every rendered cell, synchronously.

| Change | Templates that run again |
|---|---|
| JS: a field of a record in `grid.data` changes, then `grid.data = grid.data.slice()` | only the cells of that field's column, in that row |
| JS: a record object is replaced | that row's cells |
| JS: a field changes with no new array, or the same array is assigned again | none |
| C#: any `Data` assignment, even of the same objects | every rendered cell (the list is re-serialized, so every record is a new JS object) |
| C#: `NotifyUpdateItem` / `NotifySetItem`, `UpdateCellAsync` / `UpdateRowAsync` | every rendered cell, on **each** call |
| Scrolling | the rows that come into view |
| Active cell moves (click, arrow keys) | the cell that gains and the cell that loses the active state |
| Sorting, filtering, `markForCheck()` | every rendered cell |
| Hover, row selection | none |

**DOM and CSS.** The template's root element is a flex item of the cell (`.igx-grid__td`, which is `display: flex; align-items: center`), behind a `display: contents` wrapper. It is vertically centered but shrinks to its content, so give it `flex: 1 1 auto; min-width: 0` (or `width: 100%`) to fill the cell. Ellipsis needs `overflow: hidden; text-overflow: ellipsis; white-space: nowrap`, and a filled number cell needs `text-align: right`. Text that wraps makes the grid measure a taller row, and all rows share that height. Template markup carries no Blazor CSS-isolation attribute, so style it from global CSS, or in `.razor.css` with `::deep` under an element the component renders (`.grid-host ::deep .price-cell`).

```css
/* wwwroot/css/app.css */
.price-cell { flex: 1 1 auto; min-width: 0; text-align: right; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.price-cell.down { color: var(--ig-error-500); }
```

Render-fragment `BodyTemplate`s are laid out differently: their content is inside `div#host-<guid>` wrappers. CSS written for one shape does not carry over to the other.

**Gotchas.**
- **There is no way back to the default cell template.** Setting the `igc-column` element's `bodyTemplate` to `null` throws `Cannot read properties of null (reading '___isBridged')` and leaves the cells empty (#484), and binding `BodyTemplateScript` to `null` from C# keeps the old template. Switch between registered names instead. For a plain look, register a pass-through template, `registerScript('PlainCell', (ctx) => ctx.implicit ?? '')`. Unlike the default cell, it wraps long text instead of truncating it. Likewise, don't bind `BodyTemplate` to `null` to switch a render fragment off: the column's cells go blank.
- **Blazor Server: arrays of primitives in `Data` arrive as `null`.** That includes `double[]`, `List<double>` and `string[]`, so a template that reads them gets nothing (#479). Send them as JSON text and `JSON.parse` them in the template, or as a list of objects.
- **`igc-sparkline` rendered from a template must be `destroy()`ed** when the grid discards its cell (#480). Until then it polls its size every 100 ms, also after its column is hidden, its row is filtered out, or the page is left. Watch the grid for removed nodes and destroy the disconnected sparklines:

  ```js
  // wwwroot/grid-live.js (features.md, "Live data", shows how it is attached to the grid and released)
  function watchSparklines(grid) {
      const destroyed = new WeakSet();
      const destroy = (sparkline) => { if (!destroyed.has(sparkline)) { destroyed.add(sparkline); sparkline.destroy(); } };
      const onRemoved = (records) => {
          for (const record of records)
              for (const node of record.removedNodes) {
                  if (node.nodeType !== Node.ELEMENT_NODE) continue;
                  const found = node.localName === 'igc-sparkline' ? [node] : node.getElementsByTagName('igc-sparkline');
                  for (const sparkline of found) if (!sparkline.isConnected) destroy(sparkline);
              }
      };
      const observer = new MutationObserver(onRemoved);
      observer.observe(grid, { childList: true, subtree: true });
      return () => {
          onRemoved(observer.takeRecords());
          observer.disconnect();
          for (const sparkline of grid.getElementsByTagName('igc-sparkline')) destroy(sparkline);
      };
  }
  ```

  The Blazor `IgbSparkline` inside a render-fragment `BodyTemplate` is disposed with its cell.

## Column groups & multi-row layout

```razor
<IgbColumnGroup Header="Personal Info">
    <IgbColumn Field="FirstName" Header="First Name" />
    <IgbColumn Field="LastName" Header="Last Name" />
</IgbColumnGroup>
```

`IgbColumnGroup` nests for multi-level headers. For several fields stacked inside one visual row, use `IgbColumnLayout` and position each column on a sub-grid with `RowStart` / `ColStart` / `RowEnd` / `ColEnd`:

```razor
<IgbColumnLayout>
    <IgbColumn Field="Name" Header="Name" RowStart="1" ColStart="1" ColEnd="3" />
    <IgbColumn Field="Phone" Header="Phone" RowStart="2" ColStart="1" />
    <IgbColumn Field="Email" Header="Email" RowStart="2" ColStart="2" />
</IgbColumnLayout>
```

## Pinning

```razor
<IgbColumn Field="Name" Pinned="true" />
```

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" Pinning="pinningConfig">…</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private IgbPinningConfig pinningConfig = new() { Columns = ColumnPinningPosition.End };

    private void PinName() => grid.GetColumnByName("Name").Pinned = true;
}
```

## Sorting

```razor
<IgbGrid Data="data" PrimaryKey="Id"
         SortingOptions="sortingOptions"
         SortingExpressions="sortExpressions"
         SortingDone="OnSortingDone">
    <IgbColumn Field="Department" Sortable="true" />
    <IgbColumn Field="Name" Sortable="true" />
</IgbGrid>

@code {
    private IgbSortingOptions sortingOptions = new() { Mode = SortingOptionsMode.Multiple };

    private IgbSortingExpression[] sortExpressions =
    {
        new() { FieldName = "Name", Dir = SortingDirection.Asc }
    };

    // args.Detail is IgbSortingExpression[]
    private void OnSortingDone(IgbSortingExpressionEventArgs args) { }
}
```

`SortingOptionsMode` is `Single` or `Multiple` (default: a second clicked column sorts within the first, so set `Single` when each click should replace the sort). `FieldName` is case-sensitive and must match the C# property exactly.

## Filtering

```razor
@* Filter row below the header *@
<IgbGrid Data="data" PrimaryKey="Id" AllowFiltering="true">
    <IgbColumn Field="Name" Filterable="true" DataType="GridColumnDataType.String" />
</IgbGrid>

@* Excel-style dropdown filters *@
<IgbGrid Data="data" PrimaryKey="Id" AllowFiltering="true" FilterMode="FilterMode.ExcelStyleFilter">…</IgbGrid>

@* Query-builder dialog opened from the toolbar *@
<IgbGrid Data="data" PrimaryKey="Id" AllowAdvancedFiltering="true">…</IgbGrid>
```

`AllowFiltering` and `AllowAdvancedFiltering` coexist: the filter row stays available while the toolbar button opens the advanced dialog. Available conditions follow the column's `DataType`:

| Data type | Conditions |
|---|---|
| String | Contains, StartsWith, EndsWith, Equals, DoesNotEqual, Empty, NotEmpty |
| Number | Equals, DoesNotEqual, GreaterThan, LessThan, GreaterThanOrEqual, LessThanOrEqual, Empty, NotEmpty |
| Boolean | All, True, False, Empty, NotEmpty |
| Date | Equals, DoesNotEqual, Before, After, Today, Yesterday, ThisMonth, LastMonth, NextMonth, ThisYear, LastYear, NextYear, Empty, NotEmpty |

Programmatic filtering is in [`data-operations.md`](./data-operations.md).

## Selection

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id"
         RowSelection="GridSelectionMode.Multiple"
         CellSelection="GridSelectionMode.Multiple"
         ColumnSelection="GridSelectionMode.Multiple"
         RowSelectionChanging="OnRowSelection"
         RangeSelected="OnRangeSelected">
    <IgbColumn Field="Name" Selectable="true" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;

    private void OnRowSelection(IgbRowSelectionEventArgs args)
        => _ = args.Detail.NewSelection;   // primary-key values

    private void OnRangeSelected(IgbGridSelectionRangeEventArgs args) { }

    private void ReadSelection() => _ = grid.SelectedRows;             // object[] of primary keys
    private Task SelectSome() => grid.SelectRowsAsync(new object[] { 1, 3, 5 });
    private Task ClearSelection() => grid.DeselectAllRowsAsync();
}
```

`GridSelectionMode`: `None`, `Single`, `Multiple`, `MultipleCascade` (Tree Grid — selecting a parent selects its descendants). Selection is keyed by `PrimaryKey`, so it must be set.
