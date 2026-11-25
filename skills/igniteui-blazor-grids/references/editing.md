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
@inject EmployeeService EmployeeService
@inject ILogger<Employees> Logger

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
        if (args.Detail.Column?.Field == "Salary" && Convert.ToDecimal(args.Detail.NewValue) < 0)
        {
#pragma warning disable BL0005
            args.Detail.Valid = false;   // reject before commit
#pragma warning restore BL0005
        }
    }

    // CellEditDone is an Action<T>, not an EventCallback: the handler must return void
    // (an async Task handler fails with CS0407) and gets no automatic re-render.
    private void OnCellEditDone(IgbGridEditDoneEventArgs args)
    {
        var edit = args.Detail;
        if (edit.Column?.Field is not { } field) return;   // Column and Field are nullable
        _ = SaveAsync(() => EmployeeService.UpdateFieldAsync(edit.RowData, field, edit.NewValue));
    }

    private async Task SaveAsync(Func<Task> save)
    {
        try { await save(); }
        catch (Exception ex) { Logger.LogError(ex, "Saving the cell edit failed"); }
    }
}
```

In `CellEditDone`, `Detail.RowData` is your row object, already updated in the bound list, and `Detail.RowKey` is its key (a numeric key arrives as `double`). The fire-and-forget save runs on the renderer's dispatcher, and so does the code after its `await`. To show a save status, set a field there and call `StateHasChanged()` directly.

`CellEditDone`, `RowAdded` and `RowDeleted` are `Action<T>` parameters: return `void`, and call `StateHasChanged()` yourself if the page shows something they change (or forward to an `EventCallback` of a parent component, which re-renders that parent). Check other events' parameter type with `get_api_reference` before writing an `async Task` handler.

`RowDeleted` delivers `IgbRowDataEventArgs`: read the key from `Detail.RowKey` (`Detail.PrimaryKey` is obsolete; a numeric key arrives as `double`). When it fires, the grid's built-in delete has **already removed the row from the bound `List<T>`** — don't count deletes by removing from that same list.

Double-click, or Enter on a focused cell, enters edit mode. The editor follows the column's `DataType`: text box, numeric input, date picker, or checkbox.

| Event | Args | Fires |
|---|---|---|
| `CellEditEnter` | `IgbGridEditEventArgs` | entering edit mode |
| `CellEdit` | `IgbGridEditEventArgs` | before commit — set `args.Detail.Valid = false` to reject |
| `CellEditDone` | `IgbGridEditDoneEventArgs` | after commit — persist here |
| `CellEditExit` | `IgbGridEditDoneEventArgs` | leaving edit mode |

## Row editing

```razor
<IgbGrid Data="@projectTasks" PrimaryKey="Id" RowEditable="true" AutoGenerate="false"
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
    if (args.Detail.RowData is ProjectTask task && task.EndDate < task.StartDate)
    {
#pragma warning disable BL0005
        args.Detail.Valid = false;   // cross-field validation
#pragma warning restore BL0005
    }
    }

    private Task OnRowEditDone(IgbGridEditDoneEventArgs args)
    => ProjectTaskService.UpdateAsync(args.Detail.RowData);
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

Validate in `CellEdit` / `RowEdit` and set `args.Detail.Valid = false` to block the commit. Because `Valid` is generated as a component parameter, suppress `BL0005` only around that assignment, as shown above; do not disable it project-wide. In row editing, the row cannot be confirmed while a validation error stands. Show the message with `ErrorTemplate` on the column:

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
        <IgbCombo T="int" Data="people" ValueKey="Id" DisplayKey="Name" SingleSelect="true"
                  Value="@(cell.Cell.EditValue is int id ? new[] { id } : Array.Empty<int>())"
                  ValueChanged="@((int[] ids) => cell.Cell.EditValue = ids.FirstOrDefault())" />
    </InlineEditorTemplate>
</IgbColumn>
```

The grids ship in the full `IgniteUI.Blazor`, whose combo has one generic parameter `T`: the value type, here `Person.Id`'s `int`, or the item type when there is no `ValueKey`. `Value` is a `T[]` even with `SingleSelect`, so convert to and from the cell's `EditValue` instead of binding it directly.
