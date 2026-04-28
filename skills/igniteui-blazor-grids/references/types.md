# Specialized Grid Types

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [IgbTreeGrid](#igbtreegrid)
- [IgbHierarchicalGrid](#igbhierarchicalgrid)
- [IgbPivotGrid](#igbpivotgrid)
- [Key Rules](#key-rules)

---

## IgbTreeGrid

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTreeGridModule));
```

### Nested Object Data (ChildDataKey)

Use when each parent record contains a property that holds an array of children:

```razor
<IgbTreeGrid Data="Employees"
             ChildDataKey="DirectReports"
             PrimaryKey="ID"
             AutoGenerate="false"
             Height="500px"
             AllowFiltering="true">
    <IgbColumn Field="Name" Header="Name" DataType="GridColumnDataType.String" Sortable="true" />
    <IgbColumn Field="Title" Header="Title" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Age" Header="Age" DataType="GridColumnDataType.Number" />
    <IgbGridToolbar>
        <IgbGridToolbarTitle>Employee Hierarchy</IgbGridToolbarTitle>
    </IgbGridToolbar>
</IgbTreeGrid>

@code {
    public List<Employee> Employees { get; set; } = SampleData.GetEmployeeTree();

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Age { get; set; }
        public List<Employee> DirectReports { get; set; } = new();
    }
}
```

### Flat Data with Foreign Key

Use when you have a flat list of records where each record has a reference to its parent's primary key:

```razor
<IgbTreeGrid Data="FlatEmployees"
             PrimaryKey="ID"
             ForeignKey="ParentID"
             AutoGenerate="false"
             Height="500px">
    <IgbColumn Field="Name" />
    <IgbColumn Field="Title" />
</IgbTreeGrid>

@code {
    public record FlatEmployee(int ID, int? ParentID, string Name, string Title);
    public List<FlatEmployee> FlatEmployees { get; set; } = SampleData.GetFlatEmployees();
}
```

Root records have `ParentID = null` (or whatever value indicates no parent).

Key additional features of `IgbTreeGrid` (same API as `IgbGrid`): sorting, filtering, editing, toolbar, export, selection, state persistence.

---

## IgbHierarchicalGrid

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbHierarchicalGridModule), typeof(IgbRowIslandModule));
```

`IgbHierarchicalGrid` renders a master grid where each row can expand to show a full child grid. Use `IgbRowIsland` to define the child grid layout. Child grids can be nested to any depth.

```razor
<IgbHierarchicalGrid Data="Singers"
                     PrimaryKey="ID"
                     AutoGenerate="false"
                     Height="600px">
    <!-- Master grid columns -->
    <IgbColumn Field="Artist" Header="Artist" Sortable="true" />
    <IgbColumn Field="Debut" Header="Debut" DataType="GridColumnDataType.Number" />
    <IgbColumn Field="GrammyNominations" Header="Grammy Nominations" />

    <!-- Level 1 child grid - Albums -->
    <IgbRowIsland ChildDataKey="Albums" AutoGenerate="false" PrimaryKey="ID">
        <IgbColumn Field="Album" Header="Album" />
        <IgbColumn Field="LaunchDate" Header="Launch Date" DataType="GridColumnDataType.Date" />
        <IgbColumn Field="BillboardReview" Header="Billboard" DataType="GridColumnDataType.Number" />

        <!-- Level 2 child grid - Songs -->
        <IgbRowIsland ChildDataKey="Songs" AutoGenerate="false" PrimaryKey="ID">
            <IgbColumn Field="Number" Header="Track #" DataType="GridColumnDataType.Number" />
            <IgbColumn Field="Title" Header="Title" />
            <IgbColumn Field="Released" Header="Released" DataType="GridColumnDataType.Date" />
        </IgbRowIsland>
    </IgbRowIsland>
</IgbHierarchicalGrid>

@code {
    public List<Singer> Singers { get; set; } = SampleData.GetSingers();
}
```

Data model requirements: each root record must contain a property named to match `ChildDataKey` that holds the list of children:

```csharp
public class Singer
{
    public int ID { get; set; }
    public string Artist { get; set; }
    public int Debut { get; set; }
    public int GrammyNominations { get; set; }
    public List<Album> Albums { get; set; } = new();
}

public class Album
{
    public int ID { get; set; }
    public string Album { get; set; }
    public DateTime LaunchDate { get; set; }
    public int BillboardReview { get; set; }
    public List<Song> Songs { get; set; } = new();
}
```

`IgbRowIsland` supports most features of `IgbGrid` (sorting, filtering, editing, toolbar) declared as child content.

---

## IgbPivotGrid

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbPivotGridModule));
```

`IgbPivotGrid` is configured entirely via a C# `IgbPivotConfiguration` object - there are no column components.

```razor
<IgbPivotGrid @ref="PivotGrid"
              Data="SalesData"
              AutoGenerate="false"
              Height="600px">
</IgbPivotGrid>

@code {
    IgbPivotGrid PivotGrid { get; set; }
    List<SaleRecord> SalesData { get; set; } = SampleData.GetSales();

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && PivotGrid != null)
        {
            var config = new IgbPivotConfiguration
            {
                Columns = new List<IgbPivotDimension>
                {
                    new IgbPivotDimension { MemberName = "Region", Enabled = true }
                },
                Rows = new List<IgbPivotDimension>
                {
                    new IgbPivotDateDimension
                    {
                        MemberName = "Date",
                        Enabled = true,
                        Options = new IgbPivotDateDimensionOptions
                        {
                            Years = true,
                            Months = true,
                            Quarters = true
                        }
                    }
                },
                Values = new List<IgbPivotValue>
                {
                    new IgbPivotValue
                    {
                        Member = "Amount",
                        DisplayName = "Total Sales",
                        DataType = GridColumnDataType.Currency,
                        Enabled = true,
                        Aggregate = new IgbPivotAggregator
                        {
                            AggregatorName = PivotAggregationType.SUM
                        }
                    }
                },
                Filters = new List<IgbPivotDimension>
                {
                    new IgbPivotDimension { MemberName = "Product", Enabled = true }
                }
            };

            PivotGrid.PivotConfiguration = config;
        }
    }
}
```

Aggregation types: `PivotAggregationType.SUM`, `COUNT`, `MIN`, `MAX`, `AVG`.

For custom aggregators, implement the aggregation interface and set `AggregatorName` to the custom aggregator name. Check `get_doc` for the exact interface.

The Pivot Grid includes a built-in UI for users to drag dimensions between rows/columns/filters at runtime.

---

## Key Rules

1. **Always call `get_doc` for the specific grid type before writing code.** Tree Grid, Hierarchical Grid, and Pivot Grid have different configuration patterns.
2. **`IgbTreeGrid` with `ChildDataKey` requires nested objects.** For flat data with parent references, use `ForeignKey` instead.
3. **`IgbHierarchicalGrid` requires both `IgbHierarchicalGridModule` and `IgbRowIslandModule`** registered in `Program.cs`.
4. **`IgbRowIsland.ChildDataKey` must match an actual property name on the parent data type** - the grid won't expand rows if the property name is wrong.
5. **`IgbPivotGrid` configuration must be applied in `OnAfterRender(bool firstRender)`**, not in `OnInitialized`, because the grid must be rendered before `PivotConfiguration` can be set.
6. **Batch editing is NOT available for any Blazor grid type** - only Cell and Row editing are supported.
