# Grid State Persistence

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Basic Usage](#basic-usage)
- [Selective State Options](#selective-state-options)
- [Saving and Restoring](#saving-and-restoring)
- [Key Rules](#key-rules)

---

## Basic Usage

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule), typeof(IgbGridStateModule));
```

Add `IgbGridState` as a direct child of `IgbGrid`:

```razor
<IgbGrid @ref="Grid" Data="Products" PrimaryKey="ProductID" AutoGenerate="false">
    <IgbGridState @ref="GridState" />
    <IgbColumn Field="ProductName" Sortable="true" Filterable="true" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" Sortable="true" />
    <IgbPaginator PerPage="15" />
</IgbGrid>

@code {
    IgbGrid Grid { get; set; }
    IgbGridState GridState { get; set; }
}
```

---

## Selective State Options

By default, `IgbGridState` persists all supported features. To opt out of specific features, set `Options`:

```razor
<IgbGridState @ref="GridState" />

@code {
    IgbGridState GridState { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && GridState != null)
        {
            GridState.Options = new IgbGridStateOptions
            {
                Sorting = true,
                Filtering = true,
                Paging = true,
                Selection = false,         // don't persist selection
                CellSelection = false,     // don't persist cell selection
                RowPinning = true,
                ColumnPinning = true,
                ColumnHiding = true,
                ColumnMoving = true,
                ColumnResizing = true,
                GroupBy = true,
                Expansion = false          // don't persist expanded rows
            };
        }
    }
}
```

---

## Saving and Restoring

```razor
<IgbGrid @ref="Grid" Data="Products" PrimaryKey="ProductID">
    <IgbGridState @ref="GridState" />
    <IgbColumn Field="ProductName" Sortable="true" />
</IgbGrid>

<IgbButton @onclick="SaveState">Save Layout</IgbButton>
<IgbButton @onclick="RestoreState">Restore Layout</IgbButton>

@code {
    IgbGrid Grid { get; set; }
    IgbGridState GridState { get; set; }

    string SavedState { get; set; } = string.Empty;

    async Task SaveState()
    {
        // Pass empty array to save ALL features
        SavedState = await GridState.GetStateAsStringAsync(new string[0]);
        // Persist to local storage, session, database, etc.
        await LocalStorage.SetItemAsync("gridState", SavedState);
    }

    async Task RestoreState()
    {
        var json = await LocalStorage.GetItemAsync<string>("gridState");
        if (!string.IsNullOrEmpty(json))
        {
            // Pass empty array to restore ALL features
            await GridState.ApplyStateFromStringAsync(json, new string[0]);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Auto-restore on page load
            await RestoreState();
        }
    }
}
```

### Selective Save / Restore

Pass an array of feature names to save or restore only those features:

```csharp
// Save only sorting and filtering
var partialState = await GridState.GetStateAsStringAsync(new[] { "sorting", "filtering" });

// Restore only sorting and filtering
await GridState.ApplyStateFromStringAsync(partialState, new[] { "sorting", "filtering" });
```

> **AGENT INSTRUCTION:** Confirm feature string names (e.g., `"sorting"`, `"filtering"`, `"paging"`) from `get_doc` for the current version. String values are case-sensitive.

---

## Key Rules

1. **Always call `get_doc` before writing state persistence code.** Method names and option property names are version-specific.
2. **`IgbGridStateModule` must be registered in `Program.cs`** in addition to `IgbGridModule`.
3. **`IgbGridState` must be a direct child of `IgbGrid`** - it does not work as a nested descendant.
4. **`PrimaryKey` must be set on the grid** for state persistence to work reliably (especially for selection and row pinning).
5. **Pass `new string[0]` (empty array) to save/restore ALL features.** Pass an array of feature name strings to save/restore specific features.
6. **Auto-restore in `OnAfterRenderAsync(bool firstRender)`**, not in `OnInitialized`, because the grid must be rendered before state can be applied.
