# Editing — Cell Editing, Row Editing, Batch Editing & Validation

This reference covers all editing modes for Ignite UI for Blazor grids: cell editing, row editing, batch editing (transactions), validation, and custom editors.

> **Pivot Grid does not support editing.** The Pivot Grid is read-only. The content below applies to IgbGrid, IgbTreeGrid, and IgbHierarchicalGrid.

---

## Choosing an Editing Mode

| Mode | Property | Best For | Commit Behavior |
|---|---|---|---|
| Cell editing | `Editable="true"` on column | Quick inline edits | Commits on blur, Enter, or Tab |
| Row editing | `RowEditable="true"` on grid | Multi-field row changes | Shows confirm/cancel banner; commits on confirm |
| Batch editing | `BatchEditing="true"` on grid | Offline editing, undo/redo, bulk send to server | All changes in memory until `Commit()` |

> **Recommendation:** Use **row editing** for most CRUD scenarios. It gives users a clear confirm/cancel flow and prevents partial row updates.

---

## Cell Editing

### Enable cell editing

Mark columns as editable:

```razor
<IgbGrid Data="employees" PrimaryKey="Id" AutoGenerate="false">
    <IgbColumn Field="Id" Header="ID" Editable="false" />
    <IgbColumn Field="Name" Header="Name" Editable="true" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Salary" Header="Salary" Editable="true" DataType="GridColumnDataType.Currency" />
    <IgbColumn Field="HireDate" Header="Hire Date" Editable="true" DataType="GridColumnDataType.Date" />
    <IgbColumn Field="IsActive" Header="Active" Editable="true" DataType="GridColumnDataType.Boolean" />
</IgbGrid>
```

- Double-click (or press Enter on a focused cell) enters edit mode.
- The editor type is determined by `DataType`: text input for strings, numeric input for numbers, datepicker for dates, checkbox for booleans.

### Cell editing events

| Event | Type | Description |
|---|---|---|
| `CellEditEnter` | `EventCallback<IgbGridEditEventArgs>` | Fires when a cell enters edit mode |
| `CellEdit` | `EventCallback<IgbGridEditEventArgs>` | Fires before a cell value is committed — set `args.Cancel = true` to reject |
| `CellEditDone` | `EventCallback<IgbGridEditDoneEventArgs>` | Fires after a cell value is committed |
| `CellEditExit` | `EventCallback<IgbGridEditEventArgs>` | Fires when a cell exits edit mode |

### Example: Validate before commit

```razor
<IgbGrid Data="data" PrimaryKey="Id" CellEdit="OnCellEdit">
    <IgbColumn Field="Salary" Editable="true" DataType="GridColumnDataType.Number" />
</IgbGrid>

@code {
    private void OnCellEdit(IgbGridEditEventArgs args)
    {
        if (args.Column.Field == "Salary")
        {
            var newValue = Convert.ToDecimal(args.NewValue);
            if (newValue < 0)
            {
                args.Cancel = true; // reject negative salary
            }
        }
    }
}
```

### Example: React to committed changes

```razor
<IgbGrid Data="data" PrimaryKey="Id" CellEditDone="OnCellEditDone">
    ...
</IgbGrid>

@code {
    private async Task OnCellEditDone(IgbGridEditDoneEventArgs args)
    {
        var rowData = args.RowData;
        var field = args.Column.Field;
        var newValue = args.NewValue;
        // Save to server
        await EmployeeService.UpdateFieldAsync(rowData, field, newValue);
    }
}
```

---

## Row Editing

### Enable row editing

```razor
<IgbGrid Data="employees" PrimaryKey="Id" RowEditable="true" AutoGenerate="false">
    <IgbColumn Field="Id" Header="ID" Editable="false" />
    <IgbColumn Field="Name" Header="Name" Editable="true" />
    <IgbColumn Field="Department" Header="Department" Editable="true" />
    <IgbColumn Field="Salary" Header="Salary" Editable="true" DataType="GridColumnDataType.Currency" />
</IgbGrid>
```

- When `RowEditable="true"`, editing a cell shows a row-level overlay with **Confirm** and **Cancel** buttons.
- All cells in the row are editable simultaneously.
- Changes are committed to the data source only on **Confirm**.

### Row editing events

| Event | Type | Description |
|---|---|---|
| `RowEditEnter` | `EventCallback<IgbGridEditEventArgs>` | Fires when a row enters edit mode |
| `RowEdit` | `EventCallback<IgbGridEditEventArgs>` | Fires before row changes are committed — set `args.Cancel = true` to reject |
| `RowEditDone` | `EventCallback<IgbGridEditDoneEventArgs>` | Fires after row changes are committed |
| `RowEditExit` | `EventCallback<IgbGridEditEventArgs>` | Fires when a row exits edit mode |

### Example: Save row on confirm

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowEditable="true"
         RowEditDone="OnRowEditDone">
    ...
</IgbGrid>

@code {
    private async Task OnRowEditDone(IgbGridEditDoneEventArgs args)
    {
        var updatedRow = args.RowData;
        await EmployeeService.UpdateAsync(updatedRow);
    }
}
```

### Row editing with Action Strip

Add inline add/edit/delete actions:

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowEditable="true">
    <IgbColumn Field="Name" Editable="true" />
    <IgbColumn Field="Salary" Editable="true" DataType="GridColumnDataType.Number" />
    <IgbActionStrip>
        <IgbGridEditingActions AddRow="true" />
    </IgbActionStrip>
</IgbGrid>
```

`IgbGridEditingActions` provides:
- **Edit** — enters row edit mode on the clicked row
- **Delete** — removes the row
- **Add Row** (when `AddRow="true"`) — adds a new empty row above or below

---

## Batch Editing

### Enable batch editing

```razor
<IgbGrid @ref="grid" Data="employees" PrimaryKey="Id"
         BatchEditing="true" RowEditable="true" AutoGenerate="false">
    <IgbColumn Field="Id" Header="ID" Editable="false" />
    <IgbColumn Field="Name" Header="Name" Editable="true" />
    <IgbColumn Field="Department" Header="Department" Editable="true" />
    <IgbColumn Field="Salary" Header="Salary" Editable="true" DataType="GridColumnDataType.Currency" />
</IgbGrid>

<IgbButton @onclick="Commit">Save All Changes</IgbButton>
<IgbButton @onclick="Undo">Undo</IgbButton>
<IgbButton @onclick="Redo">Redo</IgbButton>
<IgbButton @onclick="Discard">Discard All</IgbButton>

@code {
    private IgbGrid grid = default!;
    private List<Employee> employees = new();

    private async Task Commit()
    {
        grid.Transactions.Commit(grid.Data);
        // Optionally send to server
        await EmployeeService.SaveBatchAsync(employees);
    }

    private void Undo()
    {
        grid.Transactions.Undo();
    }

    private void Redo()
    {
        grid.Transactions.Redo();
    }

    private void Discard()
    {
        grid.Transactions.Clear();
    }
}
```

### How batch editing works

1. When `BatchEditing="true"`, all edits (cell changes, row adds, row deletes) are stored in an in-memory transaction log.
2. The grid visually marks changed cells/rows (added = green, modified = yellow, deleted = strikethrough).
3. Changes do **not** modify the underlying data source until `Commit()` is called.
4. `Undo()` reverses the last change; `Redo()` replays it.
5. `Clear()` discards all pending changes and restores original state.

### Transactions API

| Method | Description |
|---|---|
| `grid.Transactions.Commit(data)` | Applies all pending changes to the data source |
| `grid.Transactions.Undo()` | Reverses the last transaction |
| `grid.Transactions.Redo()` | Re-applies the last undone transaction |
| `grid.Transactions.Clear()` | Discards all pending changes |
| `grid.Transactions.GetAggregatedChanges()` | Returns all pending changes as a collection |

### Sending batch changes to a server

```razor
@code {
    private async Task SaveToServer()
    {
        var changes = grid.Transactions.GetAggregatedChanges();
        foreach (var change in changes)
        {
            switch (change.Type)
            {
                case TransactionType.ADD:
                    await Api.CreateAsync(change.NewValue);
                    break;
                case TransactionType.UPDATE:
                    await Api.UpdateAsync(change.Id, change.NewValue);
                    break;
                case TransactionType.DELETE:
                    await Api.DeleteAsync(change.Id);
                    break;
            }
        }
        grid.Transactions.Commit(grid.Data);
    }
}
```

---

## Adding & Deleting Rows Programmatically

### Add a row

```razor
@code {
    private async Task AddEmployee()
    {
        var newEmployee = new Employee
        {
            Id = employees.Max(e => e.Id) + 1,
            Name = "New Employee",
            Department = "Unassigned",
            Salary = 0
        };
        await grid.AddRowAsync(newEmployee);
    }
}
```

### Delete a row

```razor
@code {
    private async Task DeleteEmployee(int id)
    {
        await grid.DeleteRowAsync(id); // pass the primary key value
    }
}
```

When `BatchEditing="true"`, `AddRowAsync` and `DeleteRowAsync` are added to the transaction log but not immediately committed.

---

## Validation

### Built-in validation

Columns support built-in validators that run during editing:

```razor
<IgbColumn Field="Name" Editable="true" Required="true" />
<IgbColumn Field="Email" Editable="true" DataType="GridColumnDataType.String" MinLength="5" MaxLength="100" />
<IgbColumn Field="Age" Editable="true" DataType="GridColumnDataType.Number" Min="18" Max="120" />
```

| Validator Parameter | Type | Description |
|---|---|---|
| `Required` | `bool` | Value cannot be empty |
| `MinLength` | `int` | Minimum string length |
| `MaxLength` | `int` | Maximum string length |
| `Min` | `object` | Minimum numeric/date value |
| `Max` | `object` | Maximum numeric/date value |

### Validation display

When a validation rule is violated, the cell shows a red border and a tooltip with the error message. The row cannot be committed while validation errors exist (in row editing or batch editing mode).

### Custom validation

Implement custom logic in the `CellEdit` or `RowEdit` event:

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowEditable="true"
         RowEdit="OnRowEdit">
    <IgbColumn Field="StartDate" Editable="true" DataType="GridColumnDataType.Date" />
    <IgbColumn Field="EndDate" Editable="true" DataType="GridColumnDataType.Date" />
</IgbGrid>

@code {
    private void OnRowEdit(IgbGridEditEventArgs args)
    {
        var rowData = args.NewValue as ProjectTask;
        if (rowData != null && rowData.EndDate < rowData.StartDate)
        {
            args.Cancel = true;
            // Show notification to user
        }
    }
}
```

---

## Custom Editors

Provide a fully custom edit template for a column:

```razor
<IgbColumn Field="Priority" Header="Priority" Editable="true">
    <InlineEditorTemplate>
        @{
            var cell = (IgbCellTemplateContext)context;
        }
        <IgbSelect @bind-Value="cell.Cell.EditValue">
            <IgbSelectItem Value="Low">Low</IgbSelectItem>
            <IgbSelectItem Value="Medium">Medium</IgbSelectItem>
            <IgbSelectItem Value="High">High</IgbSelectItem>
            <IgbSelectItem Value="Critical">Critical</IgbSelectItem>
        </IgbSelect>
    </InlineEditorTemplate>
</IgbColumn>

<IgbColumn Field="AssignedTo" Header="Assigned To" Editable="true">
    <InlineEditorTemplate>
        @{
            var cell = (IgbCellTemplateContext)context;
        }
        <IgbCombo TValue="Person"
                   Data="people"
                   ValueKey="Id"
                   DisplayKey="Name"
                   SingleSelect="true"
                   @bind-Value="cell.Cell.EditValue" />
    </InlineEditorTemplate>
</IgbColumn>
```

---

## Key Rules

1. **Set `PrimaryKey`** — editing requires a primary key to identify rows.
2. **`Editable` is on the column, not the grid** — mark each editable column individually.
3. **`RowEditable` is on the grid** — it enables the row-editing overlay with confirm/cancel.
4. **`BatchEditing` holds changes in memory** — nothing is written to the data source until `Commit()`.
5. **Use `CellEdit` / `RowEdit` to cancel edits** — set `args.Cancel = true` before commit.
6. **Use `CellEditDone` / `RowEditDone` to persist** — these fire after the edit is committed to the grid (or transaction log).
7. **Validation blocks commit** — in row/batch editing, the row cannot be confirmed while validation errors exist.
8. **Pivot Grid is read-only** — never set `Editable`, `RowEditable`, or `BatchEditing` on an `IgbPivotGrid`.
9. **Row editing mode is recommended** — it provides the best UX with confirm/cancel and prevents partial updates.
10. **Batch editing requires explicit commit** — always provide a Save/Commit button when using batch editing.

---

## See Also

- [references/structure.md](references/structure.md) — Column setup and templates
- [references/features.md](references/features.md) — Action strip for inline add/edit/delete
- [references/data-operations.md](references/data-operations.md) — Programmatic API access
- [references/paging-remote.md](references/paging-remote.md) — Remote data patterns
- [references/state.md](references/state.md) — State persistence for editing transactions
