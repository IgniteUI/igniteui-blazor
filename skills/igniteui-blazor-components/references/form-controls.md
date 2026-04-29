# Form Controls — Ignite UI for Blazor

This reference covers all form-related Ignite UI Blazor components: inputs, combos, selects, date/time pickers, calendar, checkboxes, radios, switches, sliders, and rating.

---

## IgbInput

A styled text input wrapping the `igc-input` web component.

### Basic usage

```razor
<IgbInput Type="InputType.Text" Label="Username" @bind-Value="username" />
<IgbInput Type="InputType.Password" Label="Password" @bind-Value="password" />
<IgbInput Type="InputType.Email" Label="Email" @bind-Value="email" Placeholder="user@example.com" />

@code {
    private string username = "";
    private string password = "";
    private string email = "";
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `string` | Current value. Supports `@bind-Value`. |
| `Type` | `InputType` | `Text`, `Password`, `Email`, `Number`, `Tel`, `Url`, `Search` |
| `Label` | `string` | Floating label text |
| `Placeholder` | `string` | Placeholder text |
| `Required` | `bool` | Marks the input as required |
| `Disabled` | `bool` | Disables the input |
| `Readonly` | `bool` | Makes the input read-only |
| `Outlined` | `bool` | Uses outlined style variant |

### Events

| Event | Type | Description |
|---|---|---|
| `ValueChanged` | `EventCallback<string>` | Fires when value changes |
| `Input` | `EventCallback` | Fires on every keystroke |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbInputModule));
```

---

## IgbCombo

A combo box with filtering, virtualized list, multi-selection, and grouping. The Blazor component is generic: `IgbCombo<T>`.

### Basic usage

```razor
<IgbCombo TValue="City"
          Data="cities"
          ValueKey="Id"
          DisplayKey="Name"
          Label="Select a city"
          SelectionChanged="OnSelectionChanged" />

@code {
    private List<City> cities = new()
    {
        new City { Id = 1, Name = "New York" },
        new City { Id = 2, Name = "London" },
        new City { Id = 3, Name = "Tokyo" }
    };

    private void OnSelectionChanged(IgbComboChangeEventArgs<City> args)
    {
        // args.Detail.NewValue contains the selected items
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Data` | `object` | Data source (collection of T) |
| `ValueKey` | `string` | Property name used as the value identifier |
| `DisplayKey` | `string` | Property name displayed in the dropdown |
| `GroupKey` | `string` | Property name for grouping items |
| `Label` | `string` | Floating label |
| `Placeholder` | `string` | Placeholder text |
| `DisableFiltering` | `bool` | Disables the search/filter input |
| `CaseSensitiveIcon` | `bool` | Shows case-sensitive toggle in filter |
| `SingleSelect` | `bool` | Restricts to single selection |
| `Disabled` | `bool` | Disables the combo |

### Events

| Event | Type | Description |
|---|---|---|
| `SelectionChanged` | `EventCallback<IgbComboChangeEventArgs<T>>` | Fires when selection changes |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbComboModule));
```

---

## IgbSelect

A dropdown select component for single-value selection.

### Basic usage

```razor
<IgbSelect @bind-Value="selectedRole" Label="Role">
    <IgbSelectItem Value="admin">Administrator</IgbSelectItem>
    <IgbSelectItem Value="editor">Editor</IgbSelectItem>
    <IgbSelectItem Value="viewer">Viewer</IgbSelectItem>
    <IgbSelectGroup>
        <IgbSelectHeader>More Roles</IgbSelectHeader>
        <IgbSelectItem Value="moderator">Moderator</IgbSelectItem>
    </IgbSelectGroup>
</IgbSelect>

@code {
    private string selectedRole = "viewer";
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `string` | Selected value. Supports `@bind-Value`. |
| `Label` | `string` | Label text |
| `Placeholder` | `string` | Placeholder text |
| `Open` | `bool` | Controls the open state of the dropdown |
| `Outlined` | `bool` | Uses outlined style variant |
| `Disabled` | `bool` | Disables the select |

### Child components

- `IgbSelectItem` — individual option (`Value`, `Disabled`, `Selected`)
- `IgbSelectGroup` — groups options together
- `IgbSelectHeader` — non-selectable header text in a group

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbSelectModule),
    typeof(IgbSelectItemModule),
    typeof(IgbSelectGroupModule),
    typeof(IgbSelectHeaderModule)
);
```

---

## IgbDatePicker

A date picker with calendar popup.

### Basic usage

```razor
<IgbDatePicker @bind-Value="selectedDate" Label="Start Date" />

@code {
    private DateTime? selectedDate = DateTime.Today;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `DateTime?` | Selected date. Supports `@bind-Value`. |
| `Label` | `string` | Label text |
| `Min` | `DateTime?` | Minimum selectable date |
| `Max` | `DateTime?` | Maximum selectable date |
| `Disabled` | `bool` | Disables the picker |
| `NonEditable` | `bool` | Prevents manual text input |
| `Outlined` | `bool` | Outlined style |
| `Mode` | `PickerMode` | `Dropdown` (default) or `Dialog` |

### Events

| Event | Type | Description |
|---|---|---|
| `ValueChanged` | `EventCallback<DateTime?>` | Fires when the selected date changes |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDatePickerModule));
```

---

## IgbDateRangePicker

A date range picker for selecting start and end dates.

### Basic usage

```razor
<IgbDateRangePicker Label="Date Range"
                     ValueChanged="OnRangeChanged" />

@code {
    private void OnRangeChanged(DateRangeValue range)
    {
        var start = range.Start;
        var end = range.End;
    }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `DateRangeValue` | The selected range (Start, End) |
| `Label` | `string` | Label text |
| `Min` | `DateTime?` | Minimum selectable date |
| `Max` | `DateTime?` | Maximum selectable date |
| `Disabled` | `bool` | Disables the picker |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDateRangePickerModule));
```

---

## IgbDateTimeInput

An input for formatted date/time entry without a calendar popup.

### Basic usage

```razor
<IgbDateTimeInput @bind-Value="dateTime"
                   Label="Appointment"
                   MinValue="@DateTime.Today"
                   MaxValue="@DateTime.Today.AddYears(1)" />

@code {
    private DateTime? dateTime = DateTime.Now;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `DateTime?` | Current date/time value. Supports `@bind-Value`. |
| `Label` | `string` | Label text |
| `MinValue` | `DateTime?` | Minimum allowed date/time |
| `MaxValue` | `DateTime?` | Maximum allowed date/time |
| `DisplayFormat` | `string` | Display format string |
| `InputFormat` | `string` | Input format string |
| `SpinDelta` | `DatePartDeltas` | Step increments for each date part |
| `SpinLoop` | `bool` | Whether spinning wraps around |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDateTimeInputModule));
```

---

## IgbCalendar

An inline calendar for date selection.

### Basic usage

```razor
<IgbCalendar Selection="CalendarSelection.Single"
              @bind-Value="selectedDate" />

<IgbCalendar Selection="CalendarSelection.Multi"
              @bind-Values="selectedDates" />

<IgbCalendar Selection="CalendarSelection.Range"
              @bind-Values="dateRange" />

@code {
    private DateTime? selectedDate = DateTime.Today;
    private DateTime[] selectedDates = Array.Empty<DateTime>();
    private DateTime[] dateRange = Array.Empty<DateTime>();
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Selection` | `CalendarSelection` | `Single`, `Multi`, `Range` |
| `Value` | `DateTime?` | Selected date (single mode). Supports `@bind-Value`. |
| `Values` | `DateTime[]` | Selected dates (multi/range mode). Supports `@bind-Values`. |
| `ActiveView` | `CalendarActiveView` | `Days`, `Months`, `Years` |
| `HeaderOrientation` | `CalendarHeaderOrientation` | `Horizontal`, `Vertical` |
| `VisibleMonths` | `int` | Number of months to display |
| `WeekStart` | `WeekDays` | First day of the week |
| `HideOutsideDays` | `bool` | Hides days not in the current month |
| `ShowWeekNumbers` | `bool` | Shows ISO week numbers |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCalendarModule));
```

---

## IgbCheckbox

A styled checkbox.

### Basic usage

```razor
<IgbCheckbox @bind-Checked="rememberMe">Remember me</IgbCheckbox>
<IgbCheckbox Checked="true" Disabled="true">Locked option</IgbCheckbox>
<IgbCheckbox Indeterminate="true">Parent checkbox</IgbCheckbox>

@code {
    private bool rememberMe = false;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Checked` | `bool` | Checked state. Supports `@bind-Checked`. |
| `Indeterminate` | `bool` | Indeterminate (mixed) state |
| `Disabled` | `bool` | Disables the checkbox |
| `Required` | `bool` | Marks as required |
| `LabelPosition` | `ToggleLabelPosition` | `After` (default) or `Before` |

### Events

| Event | Type | Description |
|---|---|---|
| `Change` | `EventCallback<IgbCheckboxChangeEventArgs>` | Fires when checked state changes |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCheckboxModule));
```

---

## IgbRadio & IgbRadioGroup

Radio buttons grouped for single-value selection.

### Basic usage

```razor
<IgbRadioGroup @bind-Value="selectedColor">
    <IgbRadio Value="red">Red</IgbRadio>
    <IgbRadio Value="green">Green</IgbRadio>
    <IgbRadio Value="blue">Blue</IgbRadio>
</IgbRadioGroup>

<p>Selected: @selectedColor</p>

@code {
    private string selectedColor = "red";
}
```

### IgbRadioGroup parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `string` | Selected radio value. Supports `@bind-Value`. |
| `Alignment` | `string` | Alignment direction |

### IgbRadio parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `string` | Radio value |
| `Disabled` | `bool` | Disables the radio |
| `Checked` | `bool` | Checked state |
| `LabelPosition` | `ToggleLabelPosition` | `After` or `Before` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbRadioModule),
    typeof(IgbRadioGroupModule)
);
```

---

## IgbSwitch

A toggle switch component.

### Basic usage

```razor
<IgbSwitch @bind-Checked="darkMode">Dark Mode</IgbSwitch>
<IgbSwitch Checked="true" Disabled="true">Always on</IgbSwitch>

@code {
    private bool darkMode = false;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Checked` | `bool` | Toggle state. Supports `@bind-Checked`. |
| `Disabled` | `bool` | Disables the switch |
| `Required` | `bool` | Marks as required |
| `LabelPosition` | `ToggleLabelPosition` | `After` or `Before` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSwitchModule));
```

---

## IgbSlider & IgbRangeSlider

Value and range slider components.

### IgbSlider — single value

```razor
<IgbSlider @bind-Value="volume"
            Min="0" Max="100" Step="1"
            Label="Volume" />

<p>Volume: @volume</p>

@code {
    private double volume = 50;
}
```

### IgbRangeSlider — range value

```razor
<IgbRangeSlider Lower="@lower" Upper="@upper"
                 Min="0" Max="1000" Step="10"
                 LowerChanged="v => lower = v"
                 UpperChanged="v => upper = v" />

<p>Range: @lower – @upper</p>

@code {
    private double lower = 200;
    private double upper = 800;
}
```

### Key parameters (both)

| Parameter | Type | Description |
|---|---|---|
| `Value` / `Lower` / `Upper` | `double` | Current value(s) |
| `Min` | `double` | Minimum value |
| `Max` | `double` | Maximum value |
| `Step` | `double` | Step increment |
| `Disabled` | `bool` | Disables the slider |
| `DiscreteTrack` | `bool` | Shows discrete tick marks |
| `PrimaryTicks` | `int` | Number of primary tick marks |
| `SecondaryTicks` | `int` | Number of secondary ticks between primaries |
| `TickOrientation` | `SliderTickOrientation` | `Start`, `End`, `Mirror` |
| `TickLabelRotation` | `SliderTickLabelRotation` | `None`, `Always` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbSliderModule),
    typeof(IgbRangeSliderModule)
);
```

---

## IgbRating

A star-rating component.

### Basic usage

```razor
<IgbRating @bind-Value="rating" Max="5" Label="Rate this product" />
<IgbRating Value="3.5" Readonly="true" Max="5" />

@code {
    private double rating = 0;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `double` | Current rating. Supports `@bind-Value`. |
| `Max` | `double` | Maximum rating value |
| `Readonly` | `bool` | Prevents user interaction |
| `Disabled` | `bool` | Disables the rating |
| `Label` | `string` | Accessible label |
| `Step` | `double` | Value increment step |
| `Single` | `bool` | Single selection mode (only one symbol active) |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbRatingModule));
```

---

## Forms Integration

Ignite UI Blazor components work with Blazor's built-in form system.

### EditForm with DataAnnotations validation

```razor
@using System.ComponentModel.DataAnnotations

<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <IgbInput @bind-Value="model.Name" Label="Full Name" Required="true" />
    <IgbInput @bind-Value="model.Email" Type="InputType.Email" Label="Email" Required="true" />

    <IgbSelect @bind-Value="model.Department" Label="Department">
        <IgbSelectItem Value="engineering">Engineering</IgbSelectItem>
        <IgbSelectItem Value="marketing">Marketing</IgbSelectItem>
        <IgbSelectItem Value="sales">Sales</IgbSelectItem>
    </IgbSelect>

    <IgbCheckbox @bind-Checked="model.AcceptTerms">I accept the terms</IgbCheckbox>

    <IgbDatePicker @bind-Value="model.StartDate" Label="Start Date" />

    <IgbButton Variant="ButtonVariant.Contained" type="submit">Submit</IgbButton>
</EditForm>

@code {
    private EmployeeModel model = new();

    private void HandleSubmit()
    {
        // Process form
    }

    public class EmployeeModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Department { get; set; } = "";

        public bool AcceptTerms { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
```

---

## Key Rules

1. **Always register modules** — every `IgbXxx` component requires `typeof(IgbXxxModule)` in `AddIgniteUIBlazor()`.
2. **Two-way binding** — use `@bind-Value`, `@bind-Checked`, etc. The framework generates both the value property and the `ValueChanged` callback.
3. **Enum parameters** — use the C# enum type (e.g., `InputType.Email`, `CalendarSelection.Range`), not string values.
4. **Generic components** — `IgbCombo<T>` requires the `TValue` type parameter when used in markup or the generic is inferred from the `Data` property.
5. **Nullable dates** — `IgbDatePicker`, `IgbDateTimeInput` use `DateTime?` for their `Value`.
6. **Do not mix Angular/React patterns** — there are no `[(ngModel)]`, `formControlName`, or `onChange` props. Use Blazor `@bind-*` and `EventCallback`.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [feedback.md](feedback.md) — Dialog, Snackbar, Toast, Banner
- [directives.md](directives.md) — Buttons and Tooltip
- [layout.md](layout.md) — Tabs, Stepper, Accordion, NavDrawer
