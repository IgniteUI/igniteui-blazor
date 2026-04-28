# Grid Editing

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Cell Editing](#cell-editing)
- [Row Editing](#row-editing)
- [Edit Templates](#edit-templates)
- [Edit Events](#edit-events)
- [Adding and Deleting Rows](#adding-and-deleting-rows)
- [Key Rules](#key-rules)

---

## Cell Editing

Cell editing allows users to double-click (or press Enter) on a cell to edit it in place.

```razor
<IgbGrid Data="Products" PrimaryKey="ProductID" AutoGenerate="false">
    <IgbColumn Field="ProductID" Header="ID" />
    <IgbColumn Field="ProductName" Header="Name" Editable="true" />
    <IgbColumn Field="UnitPrice" Header="Price" DataType="GridColumnDataType.Number" Editable="true" />
    <IgbColumn Field="InStock" Header="In Stock" DataType="GridColumnDataType.Boolean" Editable="true" />
</IgbGrid>
```

Only columns with `Editable="true"` allow editing. The grid renders the appropriate input control for the column's `DataType` automatically.

---

## Row Editing

Row editing shows an overlay row with **Done** and **Cancel** buttons. Changes are committed only when the user clicks **Done** (or discarded with **Cancel**).

```razor
<IgbGrid Data="Products"
         PrimaryKey="ProductID"
         AutoGenerate="false"
         RowEditable="true"
         RowEditEnter="OnRowEditEnter"
         RowEditDone="OnRowEditDone">
    <IgbColumn Field="ProductID" />
    <IgbColumn Field="ProductName" Editable="true" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" Editable="true" />
</IgbGrid>

@code {
    void OnRowEditEnter(IgbGridEditEventArgs e) { /* row edit started */ }

    void OnRowEditDone(IgbGridEditDoneEventArgs e)
    {
        var updated = e.NewValue; // the updated row object
        // persist to database
    }
}
```

> **AGENT INSTRUCTION - Batch Editing NOT available**
>
> Ignite UI for Blazor does **not** support batch editing. All edits are committed immediately (cell editing) or per-row (row editing). Do not suggest or generate batch editing code.

---

## Edit Templates

Use custom edit templates to replace the default editor for a column:

```razor
<IgbColumn Field="CategoryID" Header="Category" Editable="true">
    <IgbCellEditorTemplateDirective>
        @{
            var cell = context as IgbCellTemplateContext;
        }
        <IgbSelect @bind-Value="@((string)cell.Cell.EditValue)">
            <IgbSelectItem Value="1">Electronics</IgbSelectItem>
            <IgbSelectItem Value="2">Clothing</IgbSelectItem>
            <IgbSelectItem Value="3">Food</IgbSelectItem>
        </IgbSelect>
    </IgbCellEditorTemplateDirective>
</IgbColumn>
```

> **AGENT INSTRUCTION:** Always call `get_doc` for the exact directive name and context type for cell editor templates. The directive type name (`IgbCellEditorTemplateDirective`) and context property names may differ between versions.

---

## Edit Events

| Event | Fires on | Args type | Cancellable |
|---|---|---|---|
| `CellEditEnter` | Cell enters edit mode | `IgbGridEditEventArgs` | Yes - set `e.Cancel = true` |
| `CellEdit` | Cell edit about to commit | `IgbGridEditEventArgs` | Yes - set `e.Cancel = true` |
| `CellEditDone` | Cell edit committed | `IgbGridEditDoneEventArgs` | No |
| `CellEditExit` | Cell exits edit mode | `IgbGridEditEventArgs` | Yes |
| `RowEditEnter` | Row enters edit mode | `IgbGridEditEventArgs` | Yes |
| `RowEdit` | Row edit about to commit | `IgbGridEditEventArgs` | Yes |
| `RowEditDone` | Row edit committed | `IgbGridEditDoneEventArgs` | No |
| `RowEditExit` | Row exits edit mode | `IgbGridEditEventArgs` | Yes |

Key properties on `IgbGridEditEventArgs`: `RowData`, `OldValue`, `NewValue`, `Column`, `Cancel`.

Key properties on `IgbGridEditDoneEventArgs`: `RowData`, `OldValue`, `NewValue`, `Column`.

```razor
<IgbGrid CellEdit="OnCellEdit" RowEditDone="OnRowSaved" ...>
```

```csharp
void OnCellEdit(IgbGridEditEventArgs e)
{
    if (e.Column.Field == "UnitPrice" && (double)e.NewValue < 0)
    {
        e.Cancel = true; // reject negative prices
    }
}

async Task OnRowSaved(IgbGridEditDoneEventArgs e)
{
    await MyApi.UpdateProductAsync(e.RowData);
}
```

---

## Adding and Deleting Rows

Programmatic row addition and deletion use grid API methods.

```razor
<IgbGrid @ref="Grid" Data="Products" PrimaryKey="ProductID">
    ...
</IgbGrid>
<IgbButton @onclick="AddRow">Add Row</IgbButton>
<IgbButton @onclick="DeleteSelected">Delete Selected</IgbButton>

@code {
    IgbGrid Grid { get; set; }
    List<Product> Products { get; set; } = SampleData.GetProducts();

    async Task AddRow()
    {
        var newRow = new Product { ProductID = GetNextId(), ProductName = "New Product" };
        await Grid.AddRowAsync(newRow);
    }

    async Task DeleteSelected()
    {
        var selected = await Grid.GetSelectedRowsAsync();
        foreach (var row in selected)
        {
            await Grid.DeleteRowAsync(row.Key);
        }
    }
}
```

> **AGENT INSTRUCTION:** Confirm method names `AddRowAsync` / `DeleteRowAsync` from `get_doc` - method signatures may change between versions.

---

## Key Rules

1. **Always call `get_doc` before writing edit code.** Event argument types and method names are version-specific.
2. **`PrimaryKey` is required for editing.** Without it, the grid cannot identify rows for updates.
3. **Each editable column must have `Editable="true"`** - setting `RowEditable` on the grid does not automatically make all columns editable.
4. **Batch editing is NOT available in Blazor.** Only Cell editing and Row editing are supported.
5. **Cancel edits in `CellEdit` or `RowEdit` events (before commit).** The `CellEditDone` / `RowEditDone` events fire after the edit is already applied and are not cancellable.
6. **For custom edit templates, confirm directive names from `get_doc`** - do not guess template directive names.
