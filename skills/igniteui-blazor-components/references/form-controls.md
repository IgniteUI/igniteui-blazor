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
<IgbSelect @bind-Value="CountryCode" Label="Country" Placeholder="Choose a country">
    @foreach (var country in Countries)
    {
        <IgbSelectItem Value="@country.Code">@country.Name</IgbSelectItem>
    }
</IgbSelect>

@code {
    string? CountryCode { get; set; }
    private List<Country> Countries = SampleData.Countries;
    record Country(string Code, string Name);
}
```

Write item values as C# expressions, `Value="@country.Code"` or `Value="@("us")"`, which works in both packages. Use `Placeholder` for the empty state rather than an empty item. `IgbSelectHeader` and `IgbSelectGroup` add section headings and grouping.

In `IgniteUI.Blazor.Lite`, `IgbSelect<TValue>` is generic: it takes `string`, `char`, an enum, or a numeric type, each optionally nullable, and `@bind-Value` infers it, so a select with nothing to infer from needs `TValue` set. Each item infers its own `TValue` from its `Value`, so item values must have the select's exact type: an `int?` select needs `int?` values, not `int`. A bare `Value="apple"` is a C# expression there, reads as the identifier `apple`, and does not compile. `Change` carries `IgbSelectItemComponentEventArgs<TValue>`, whose `Detail` is the selected `IgbSelectItem<TValue>`. In the full product `Value` is a `string`.

## Date and time

| Component | Value type | Use for |
|---|---|---|
| `IgbDatePicker<TValue>` | `DateTime`, `DateTime?`, or `string` | Input + dropdown calendar |
| `IgbDateRangePicker<TValue>` | `IgbDateRangeValue<TValue>?`, where `TValue` can be `DateTime`, `DateTime?`, or `string` | Start/end range; `UseTwoInputs`, `UsePredefinedRanges` |
| `IgbCalendar<TValue>` | `DateTime`, `DateTime?`, or `string` | Always-visible calendar surface |
| `IgbDateTimeInput<TValue>` | `DateTime`, `DateTime?`, or `string` | Masked date/time entry, no dropdown |

```razor
<IgbDatePicker @bind-Value="SelectedDate" Label="Start date" Min="@MinDate" Max="@MaxDate" />

<IgbCalendar TValue="DateTime" @bind-Value="CalendarValue" Selection="CalendarSelection.Single"
             VisibleMonths="2" ShowWeekNumbers="true" WeekStart="WeekDays.Monday" />

<IgbDateTimeInput @bind-Value="SelectedDateTime" InputFormat="MM/dd/yyyy HH:mm" SpinLoop="true" />

@code {
    DateTime? SelectedDate { get; set; }
    DateTime? MinDate { get; set; } = DateTime.Today;
    DateTime? MaxDate { get; set; } = DateTime.Today.AddYears(1);
    DateTime CalendarValue { get; set; } = DateTime.Today;
    DateTime? SelectedDateTime { get; set; } = DateTime.Now;
}
```

`TValue` is inferred from `@bind-Value`; `Min`, `Max` and `ActiveDate` take the same `TValue`. Dates the component reports back are UTC (`Kind = Utc`), and `string` values come back in round-trip `"o"` form (e.g. `2026-01-02T03:04:05.0000000Z`). A cleared `DateTime?` or `string` value is `null`; a cleared `DateTime` is `default` (`DateTime.MinValue`).

Set `IgbCalendar<TValue>` to `DateTime`, `DateTime?`, or `string`. Multi and range selection use `Values` (`TValue[]`) and come from `Selection` (`CalendarSelection.Single | Multiple | Range`).

## Checkbox, Switch, Radio

`IgbCheckbox` and `IgbSwitch` share a base: `Checked` / `@bind-Checked`, `Value`, `LabelPosition`, `Disabled`, `Required`, `Invalid`, and a `Change` carrying `IgbCheckboxChangeEventArgs`. `IgbCheckbox` adds `Indeterminate`.

```razor
<IgbCheckbox @bind-Checked="IsSubscribed">Subscribe to newsletter</IgbCheckbox>
<IgbSwitch @bind-Checked="IsDarkMode">Dark Mode</IgbSwitch>

<IgbRadioGroup @bind-Value="Plan" Alignment="ContentOrientation.Vertical">
    <IgbRadio Value="@("basic")">Basic</IgbRadio>
    <IgbRadio Value="@("pro")">Pro</IgbRadio>
    <IgbRadio Value="@("enterprise")">Enterprise</IgbRadio>
</IgbRadioGroup>
```

Radios are grouped by being children of `IgbRadioGroup`, and the selected option is the group's `Value`; radio values are written as for `IgbSelectItem`. Do **not** set `Name` to group them — `Name` is the framework's element identity, not the HTML radio name.

In `IgniteUI.Blazor.Lite`, `IgbRadioGroup<TValue>` is generic: it takes `string`, `char`, `bool`, an enum, or a numeric type, each optionally nullable; each radio infers `TValue` from its own `Value`, which must have the group's type, and `Change` carries `IgbRadioChangeEventArgs<TValue>`. An enum binds its members directly:

```razor
<IgbRadioGroup @bind-Value="Tier">
    <IgbRadio Value="PlanTier.Basic">Basic</IgbRadio>
    <IgbRadio Value="PlanTier.Pro">Pro</IgbRadio>
</IgbRadioGroup>
```

## Slider, Range Slider, Rating

```razor
<IgbSlider @bind-Value="Volume" Min="0" Max="100" Step="5" Change="OnSliderChange" />
<IgbRangeSlider Lower="20" Upper="70" Min="0" Max="100" Change="OnRangeChange" />
<IgbRating @bind-Value="StarRating" Max="5" AllowReset="true" />

@code {
    double Volume { get; set; } = 40;
    double StarRating { get; set; } = 3;

    void OnSliderChange(IgbNumberEventArgs<double> e) => Console.WriteLine(e.Detail);
    void OnRangeChange(IgbRangeSliderValueEventArgs e)
        => Console.WriteLine($"{e.Detail.Lower}-{e.Detail.Upper}");
}
```

`Min`, `Max`, `Step`, `LowerBound`, `UpperBound`, `PrimaryTicks`, `SecondaryTicks`, `DiscreteTrack` and the label/tooltip options come from the shared slider base and apply to both sliders. `IgbRangeSlider` uses `Lower` / `Upper` instead of `Value`. `IgbSlider<TValue>` and `IgbRating<TValue>` take `int`, `long`, `short`, `float`, `double`, or `decimal`, inferred from `@bind-Value`; the slider's `Input` / `Change` and the rating's `Change` / `Hover` carry `IgbNumberEventArgs<TValue>`.

## Color Picker

```razor
<IgbColorPicker @bind-Value="Background" Label="Background" />

<IgbColorPicker @bind-Value="Accent" Label="Accent"
                Mode="ColorPickerMode.Input" Format="ColorFormat.Rgb"
                ShowAlpha="true" Swatches="Palette" />

<IgbColorPicker @bind-Value="Highlight" Label="Highlight" />

@code {
    string? Background { get; set; } = "#875fc4";
    string? Accent { get; set; }
    System.Drawing.Color? Highlight { get; set; }
    static readonly string[] Palette = ["#e91e63", "#3f51b5", "#009688"];
}
```

An HSV canvas with hue and alpha sliders, an editable color string, preset swatches, and the native EyeDropper where the browser has one. `IgbColorPicker<TValue>` takes `string` or `System.Drawing.Color`, either nullable; `@bind-Value` infers it, a picker without a value needs `TValue` set. As a `string`, `Value` is a CSS color string (hex, `rgb(a)`, `hsl(a)`, or a named color); an empty or invalid string clears it, and a cleared picker reports `""`. As a `Color`, `null` or `Color.Empty` clears it and a cleared picker reports the same; in `Hsl` format the color is rounded to whole-number hue, saturation and lightness. `Format` (`Hex | Rgb | Hsl`) changes only the notation, not the color, and `HideFormats` drops the format switcher. `Mode` is `Default` (trigger button) or `Input` (editable text field with a swatch prefix).

`Input` fires on every color change while `Change` fires on commit and drives `@bind-Value`; both carry an `IgbColorPickerValueEventArgs<TValue>` whose `Detail` is the color as `TValue`. `Opening` / `Opened` / `Closing` / `Closed` track the picker surface. `Required`, `Disabled`, `Invalid` plus `CheckValidityAsync()` / `ReportValidityAsync()` / `SetCustomValidityAsync(message)` behave as on the other form controls.

## Binding and validation

```razor
<IgbInput @bind-Value="Model.Name" Label="Name" Required="true" Invalid="@(!IsNameValid)" />
<IgbCheckbox @bind-Checked="Model.Agreed">I agree to the terms</IgbCheckbox>
<IgbSelect @bind-Value="Model.Country" Label="Country">
    <IgbSelectItem Value="@("us")">United States</IgbSelectItem>
    <IgbSelectItem Value="@("uk")">United Kingdom</IgbSelectItem>
</IgbSelect>
```

- Drive every control through `@bind-Value` / `@bind-Checked`; the `Change` events are for reacting, not for reading state.
- Surface validation with the `Invalid` parameter and your own model validation. Do not assume a component participates in a plain HTML `<form>` — `IgbCombo` and `IgbRadio` do not.
