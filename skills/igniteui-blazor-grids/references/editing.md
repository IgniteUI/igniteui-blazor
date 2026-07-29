# Editing — Cell Editing, Row Editing, Validation

Applies to `IgbGrid`, `IgbTreeGrid`, and `IgbHierarchicalGrid`. **`IgbPivotGrid` is read-only** — never set `Editable` or `RowEditable` on it. **Batch editing does not exist in Blazor** on any grid.

`PrimaryKey` is required: editing identifies rows by it.

## Choosing a mode

| Mode | Enabled by | Commits on | Best for |
|---|---|---|---|
| Cell editing | `Editable="true"` on each column | blur, Enter, Tab | quick single-value edits |
| Row editing | `RowEditable="true"` on the grid | the Done button in the row overlay | multi-field changes — **prefer this for CRUD** |

Row editing gives a clear confirm/cancel flow and prevents half-updated rows. Both modes still need `Editable="true"` on the individual columns.

## Cell editing

```razor
<IgbGrid Data="@employees" PrimaryKey="Id" AutoGenerate="false"
         CellEdit="OnCellEdit" CellEditDone="OnCellEditDone">
    <IgbColumn Field="Id" Header="ID" Editable="false" />
    <IgbColumn Field="Name" Header="Name" Editable="true" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Salary" Header="Salary" Editable="true" DataType="GridColumnDataType.Currency" />
    <IgbColumn Field="HireDate" Header="Hire Date" Editable="true" DataType="GridColumnDataType.Date" />
    <IgbColumn Field="IsActive" Header="Active" Editable="true" DataType="GridColumnDataType.Boolean" />
</IgbGrid>

@code {
    private void OnCellEdit(IgbGridEditEventArgs args)
    {
        if (args.Column.Field == "Salary" && Convert.ToDecimal(args.NewValue) < 0)
            args.Cancel = true;   // reject before commit
    }

    private async Task OnCellEditDone(IgbGridEditDoneEventArgs args)
        => await EmployeeService.UpdateFieldAsync(args.RowData, args.Column.Field, args.NewValue);
}
```

Double-click, or Enter on a focused cell, enters edit mode. The editor follows the column's `DataType`: text box, numeric input, date picker, or checkbox.

| Event | Args | Fires |
|---|---|---|
| `CellEditEnter` | `IgbGridEditEventArgs` | entering edit mode |
| `CellEdit` | `IgbGridEditEventArgs` | before commit — set `args.Cancel = true` to reject |
| `CellEditDone` | `IgbGridEditDoneEventArgs` | after commit — persist here |
| `CellEditExit` | `IgbGridEditDoneEventArgs` | leaving edit mode |

## Row editing

```razor
<IgbGrid Data="employees" PrimaryKey="Id" RowEditable="true" AutoGenerate="false"
         RowEdit="OnRowEdit" RowEditDone="OnRowEditDone">
    <IgbColumn Field="Id" Header="ID" Editable="false" />
    <IgbColumn Field="Name" Header="Name" Editable="true" />
    <IgbColumn Field="StartDate" Header="Start" Editable="true" DataType="GridColumnDataType.Date" />
    <IgbColumn Field="EndDate" Header="End" Editable="true" DataType="GridColumnDataType.Date" />
    <IgbActionStrip>
        <IgbGridEditingActions AddRow="true" />
    </IgbActionStrip>
</IgbGrid>

@code {
    private void OnRowEdit(IgbGridEditEventArgs args)
    {
        if (args.NewValue is ProjectTask t && t.EndDate < t.StartDate)
            args.Cancel = true;   // cross-field validation
    }

    private Task OnRowEditDone(IgbGridEditDoneEventArgs args)
        => EmployeeService.UpdateAsync(args.RowData);
}
```

Editing any cell opens a row overlay with Done and Cancel; the whole row is editable at once and nothing reaches the data source until Done. Events mirror the cell ones: `RowEditEnter`, `RowEdit` (cancellable), `RowEditDone`, `RowEditExit`.

`IgbGridEditingActions` inside an `IgbActionStrip` adds per-row edit, delete, and (with `AddRow="true"`) add buttons.

## Adding and deleting rows from code

```razor
@code {
    private IgbGrid grid = default!;

    private Task AddEmployee() => grid.AddRowAsync(new Employee
    {
        Id = employees.Max(e => e.Id) + 1,
        Name = "New Employee",
        Department = "Unassigned"
    });

    private Task DeleteEmployee(int id) => grid.DeleteRowAsync(id);   // primary-key value
}
```

## Validation

Validate in `CellEdit` / `RowEdit` and set `args.Cancel = true` to block the commit. In row editing, the row cannot be confirmed while a validation error stands. Show the message with `ErrorTemplate` on the column:

```razor
<IgbColumn Field="Age" Editable="true" DataType="GridColumnDataType.Number">
    <ErrorTemplate>
        <span style="color: var(--ig-error-500);">Age must be between 18 and 120</span>
    </ErrorTemplate>
</IgbColumn>
```

## Custom editors

`InlineEditorTemplate` replaces the default editor. Bind the input to `cell.Cell.EditValue` — writing to `Cell.Value` bypasses the commit pipeline.

```razor
<IgbColumn Field="Priority" Header="Priority" Editable="true">
    <InlineEditorTemplate>
        @{ var cell = (IgbCellTemplateContext)context; }
        <IgbSelect @bind-Value="cell.Cell.EditValue">
            <IgbSelectItem Value="Low">Low</IgbSelectItem>
            <IgbSelectItem Value="High">High</IgbSelectItem>
        </IgbSelect>
    </InlineEditorTemplate>
</IgbColumn>

<IgbColumn Field="AssignedTo" Header="Assigned To" Editable="true">
    <InlineEditorTemplate>
        @{ var cell = (IgbCellTemplateContext)context; }
        <IgbCombo T="Person" Data="people" ValueKey="Id" DisplayKey="Name"
                  SingleSelect="true" @bind-Value="cell.Cell.EditValue" />
    </InlineEditorTemplate>
</IgbColumn>
```

`IgbCombo`'s generic parameter is **`T`**, not `TValue`.
