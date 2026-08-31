# Form Controls

Module for every component below is `Igb<Name>Module` (`IgbInputModule`, `IgbComboModule`, …); `IgbSlider` and `IgbRangeSlider` have separate modules. Registration is covered in [`setup.md`](./setup.md).

Verify exact members with `get_api_reference` / `get_doc` when the MCP server is available — the tables here list the members these controls are normally driven by, not their full API.

## Text inputs

`IgbInput` and `IgbTextarea` share a base: `Label`, `Placeholder`, `Outlined`, `Disabled`, `Required`, `Invalid`, plus `Value` / `ValueChanged`.

```razor
<IgbInput @bind-Value="UserName" Label="Username" Placeholder="e.g. John Doe">
    <IgbIcon slot="prefix" IconName="person" Collection="material" />
</IgbInput>

<IgbTextarea @bind-Value="Notes" Label="Notes" Rows="4" Resize="TextareaResize.Vertical" />
```

| Member | Type | Notes |
|---|---|---|
| `Value` / `@bind-Value` | `string` | The correct way to read and write the value |
| `DisplayType` | `InputType` | `IgbInput` only — text, password, email, number, … |
| `InputOcurred` | `EventCallback<IgbComponentValueChangedEventArgs>` | Fires while typing (name is spelled with one `c`) |
| `Change` | `EventCallback<IgbComponentValueChangedEventArgs>` | Fires on commit / blur |
| `Focus`, `Blur` | `EventCallback<IgbVoidEventArgs>` | |
| `Rows`, `Resize`, `Wrap`, `MaxLength` | | `IgbTextarea` only |

`IgbInput` has **no** `GetValueAsync()`. Bind with `@bind-Value` instead of reading imperatively.

Icons in `prefix` / `suffix` slots must be `IgbIcon`. A `<span class="material-icons">` is `display: inline`, so `vertical-align` is ignored inside the slot's flex box and the glyph drifts to the top.

## Mask Input

```razor
<IgbMaskInput @bind-Value="Phone" Mask="(000) 000-0000" Label="Phone Number" Prompt="_" />
```

`0` = digit, `L` = letter, `A` = alphanumeric. `ValueMode` (`MaskInputValueMode`) selects whether `Value` includes the literal mask characters.

## Combo Box

```razor
<IgbCombo T="City" Data="Cities" ValueKey="Id" DisplayKey="Name"
          Label="Select Cities" Placeholder="Pick a city" />

@code {
    private List<City> Cities = SampleData.Cities;
    record City(string Id, string Name, string Country);
}
```

The generic parameter is **`T`**, not `TValue` — set it to the data item type. `IgbCombo` does not participate in a plain HTML `<form>`; bind its value explicitly.

## Select

```razor
<IgbSelect @bind-Value="Fruit" Label="Fruit" Placeholder="Choose a fruit">
    <IgbSelectItem Value="apple">Apple</IgbSelectItem>
    <IgbSelectItem Value="orange">Orange</IgbSelectItem>
</IgbSelect>
```

`Value` is `string?`. `Change` carries `IgbSelectItemComponentEventArgs`. `IgbSelectHeader` and `IgbSelectGroup` add section headings and grouping.

## Date and time

| Component | Value type | Use for |
|---|---|---|
| `IgbDatePicker` | `DateTime?` | Input + dropdown calendar |
| `IgbDateRangePicker` | `IgbDateRangeValue?` | Start/end range; `UseTwoInputs`, `UsePredefinedRanges` |
| `IgbCalendar` | `DateTime` | Always-visible calendar surface |
| `IgbDateTimeInput` | `DateTime?` | Masked date/time entry, no dropdown |

```razor
<IgbDatePicker @bind-Value="SelectedDate" Label="Start date" Min="@MinDate" Max="@MaxDate" />

<IgbCalendar @bind-Value="CalendarValue" Selection="CalendarSelection.Single"
             VisibleMonths="2" ShowWeekNumbers="true" WeekStart="WeekDays.Monday" />

<IgbDateTimeInput @bind-Value="SelectedDateTime" InputFormat="MM/dd/yyyy HH:mm" SpinLoop="true" />

@code {
    DateTime? SelectedDate { get; set; }        // picker / date-time input are nullable
    DateTime CalendarValue { get; set; } = DateTime.Today;   // IgbCalendar.Value is non-nullable
    DateTime? SelectedDateTime { get; set; } = DateTime.Now;
}
```

`IgbCalendar.Value` is a non-nullable `DateTime`; the pickers are nullable. Multi and range calendar selection come from `Selection` (`CalendarSelection.Single | Multiple | Range`).

## Checkbox, Switch, Radio

`IgbCheckbox` and `IgbSwitch` share a base: `Checked` / `@bind-Checked`, `Value`, `LabelPosition`, `Disabled`, `Required`, `Invalid`, and a `Change` carrying `IgbCheckboxChangeEventArgs`. `IgbCheckbox` adds `Indeterminate`.

```razor
<IgbCheckbox @bind-Checked="IsSubscribed">Subscribe to newsletter</IgbCheckbox>
<IgbSwitch @bind-Checked="IsDarkMode">Dark Mode</IgbSwitch>

<IgbRadioGroup @bind-Value="Plan" Alignment="ContentOrientation.Vertical">
    <IgbRadio Value="basic">Basic</IgbRadio>
    <IgbRadio Value="pro">Pro</IgbRadio>
    <IgbRadio Value="enterprise">Enterprise</IgbRadio>
</IgbRadioGroup>
```

Radios are grouped by being children of `IgbRadioGroup`, and the selected option is the group's `Value`. Do **not** set `Name` to group them — `Name` is the framework's element identity, not the HTML radio name.

## Slider, Range Slider, Rating

```razor
<IgbSlider @bind-Value="Volume" Min="0" Max="100" Step="5" Change="OnSliderChange" />
<IgbRangeSlider Lower="20" Upper="70" Min="0" Max="100" Change="OnRangeChange" />
<IgbRating @bind-Value="StarRating" Max="5" AllowReset="true" />

@code {
    double Volume { get; set; } = 40;
    double StarRating { get; set; } = 3;

    void OnSliderChange(IgbNumberEventArgs e) => Console.WriteLine(e.Detail);
    void OnRangeChange(IgbRangeSliderValueEventArgs e)
        => Console.WriteLine($"{e.Detail.Lower}-{e.Detail.Upper}");
}
```

`Min`, `Max`, `Step`, `LowerBound`, `UpperBound`, `PrimaryTicks`, `SecondaryTicks`, `DiscreteTrack` and the label/tooltip options come from the shared slider base and apply to both sliders. `IgbRangeSlider` uses `Lower` / `Upper` instead of `Value`. `IgbRating.Value` and `IgbSlider.Value` are `double`.

## Binding and validation

```razor
<IgbInput @bind-Value="Model.Name" Label="Name" Required="true" Invalid="@(!IsNameValid)" />
<IgbCheckbox @bind-Checked="Model.Agreed">I agree to the terms</IgbCheckbox>
<IgbSelect @bind-Value="Model.Country" Label="Country">
    <IgbSelectItem Value="us">United States</IgbSelectItem>
    <IgbSelectItem Value="uk">United Kingdom</IgbSelectItem>
</IgbSelect>
```

- Drive every control through `@bind-Value` / `@bind-Checked`; the `Change` events are for reacting, not for reading state.
- Surface validation with the `Invalid` parameter and your own model validation. Do not assume a component participates in a plain HTML `<form>` — `IgbCombo` and `IgbRadio` do not.
