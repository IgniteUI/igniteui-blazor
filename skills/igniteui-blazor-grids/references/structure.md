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

Templates are Blazor render fragments on `IgbColumn`, and the `context` needs a cast.

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
| `BodyTemplate` | `IgbCellTemplateContext` | `cell.Cell.Value`, `cell.Cell.Row.Data` |
| `HeaderTemplate` | `IgbColumnTemplateContext` | `ctx.Column.Header` |
| `InlineEditorTemplate` | `IgbCellTemplateContext` | `cell.Cell.EditValue` |
| `ErrorTemplate` | `IgbCellTemplateContext` | validation message slot |

Inside `IgbIcon`, the parameter is `IconName` — `Name` is the framework element identity, not the glyph.

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

`SortingOptionsMode` is `Single` (default) or `Multiple`. `FieldName` is case-sensitive and must match the C# property exactly.

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
