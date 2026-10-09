# Form Controls

Module for every component below is `Igb<Name>Module` (`IgbInputModule`, `IgbComboModule`, …); `IgbSlider` and `IgbRangeSlider` have separate modules. Registration is covered in [`setup.md`](./setup.md).

Verify exact members with `get_api_reference` / `get_doc` when the MCP server is available — the tables here list the members these controls are normally driven by, not their full API.

The examples compile in both packages, and the types shown are the full `IgniteUI.Blazor`'s. In `IgniteUI.Blazor.Lite`, Calendar, Color Picker, Date Picker, Date Range Picker, Date Time Input, Radio Group, Rating, Select and Slider are generic over `TValue`, the type of their value; each section lists the types it takes. Razor infers `TValue` from `@bind-Value` or `Value`, but not through a method group, so set it when there is no value, or when a method handles an event that carries the value. Those events carry generic args, such as `IgbNumberEventArgs<TValue>`:

```razor
<IgbSlider TValue="double" @bind-Value="Volume" Change="OnVolumeChange" />

@code {
    double Volume { get; set; } = 40;
    void OnVolumeChange(IgbNumberEventArgs<double> e) => Console.WriteLine(e.Detail);
}
```

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

In `IgniteUI.Blazor.Lite`, `IgbSelect<TValue>` is generic: it takes `string`, `char`, an enum, or a numeric type, each optionally nullable. Each item infers its own `TValue` from its `Value`, so item values must have the select's exact type: an `int?` select needs `int?` values, not `int`. A bare `Value="apple"` is a C# expression there, reads as the identifier `apple`, and does not compile. `Change` carries `IgbSelectItemComponentEventArgs<TValue>`, whose `Detail` is the selected `IgbSelectItem<TValue>`. In the full product `Value` is a `string`.

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
    DateTime? SelectedDate { get; set; }
    DateTime? MinDate { get; set; } = DateTime.Today;
    DateTime? MaxDate { get; set; } = DateTime.Today.AddYears(1);
    DateTime CalendarValue { get; set; } = DateTime.Today;
    DateTime? SelectedDateTime { get; set; } = DateTime.Now;
}
```

In the full product `IgbCalendar.Value` is a non-nullable `DateTime` and the pickers are nullable. Multi and range calendar selection come from `Selection` (`CalendarSelection.Single | Multiple | Range`).

In `IgniteUI.Blazor.Lite`, `IgbDatePicker<TValue>`, `IgbDateRangePicker<TValue>`, `IgbCalendar<TValue>` and `IgbDateTimeInput<TValue>` take `DateTime`, `DateTime?`, or `string`, the Date Range Picker's `Value` being an `IgbDateRangeValue<TValue>`; `Min`, `Max` and `ActiveDate` take the same `TValue`, and the Calendar's `Values` is a `TValue[]`. Dates reported back are UTC (`Kind = Utc`), and `string` values come back in round-trip `"o"` form (e.g. `2026-01-02T03:04:05.0000000Z`). A cleared `DateTime?` or `string` is `null`; a cleared `DateTime` is `DateTime.MinValue`.

## Checkbox, Switch, Radio

`IgbCheckbox` and `IgbSwitch` share a base: `Checked` / `@bind-Checked`, `Value`, `LabelPosition`, `Disabled`, `Required`, `Invalid`, and a `Change` carrying `IgbCheckboxChangeEventArgs`. `IgbCheckbox` adds `Indeterminate`.

```razor
<IgbCheckbox @bind-Checked="IsSubscribed">Subscribe to newsletter</IgbCheckbox>
<IgbSwitch @bind-Checked="IsDarkMode">Dark Mode</IgbSwitch>

<IgbRadioGroup @bind-Value="Plan" Alignment="ContentOrientation.Vertical">
    <IgbRadio name="plan" Value="@("basic")">Basic</IgbRadio>
    <IgbRadio name="plan" Value="@("pro")">Pro</IgbRadio>
    <IgbRadio name="plan" Value="@("enterprise")">Enterprise</IgbRadio>
</IgbRadioGroup>
```

The selected option is the group's `Value`; radio values are written as for `IgbSelectItem`. The radios are only mutually exclusive when they share a `name`: give every radio in a group the same lowercase `name`, unique to that group, or an unbound group lets each radio stay checked. In the full product, do **not** set `Name` to group them — `Name` is the framework's element identity, not the HTML radio name.

In `IgniteUI.Blazor.Lite`, `IgbRadioGroup<TValue>` is generic: it takes `string`, `char`, `bool`, an enum, or a numeric type, each optionally nullable; each radio infers `TValue` from its own `Value`, which must have the group's type, and `Change` carries `IgbRadioChangeEventArgs<TValue>`. `Name` on `IgbRadioGroup` passes the name to each of its radios. An enum binds its members directly:

```razor
<IgbRadioGroup Name="tier" @bind-Value="Tier">
    <IgbRadio Value="PlanTier.Basic">Basic</IgbRadio>
    <IgbRadio Value="PlanTier.Pro">Pro</IgbRadio>
</IgbRadioGroup>
```

## Slider, Range Slider, Rating

```razor
<IgbSlider @bind-Value="Volume" @bind-Value:after="OnVolumeChanged" Min="0" Max="100" Step="5" />
<IgbRangeSlider Lower="20" Upper="70" Min="0" Max="100" Change="OnRangeChange" />
<IgbRating @bind-Value="StarRating" Max="5" AllowReset="true" />

@code {
    double Volume { get; set; } = 40;
    double StarRating { get; set; } = 3;

    void OnVolumeChanged() => Console.WriteLine(Volume);
    void OnRangeChange(IgbRangeSliderValueEventArgs e)
        => Console.WriteLine($"{e.Detail.Lower}-{e.Detail.Upper}");
}
```

`Min`, `Max`, `Step`, `LowerBound`, `UpperBound`, `PrimaryTicks`, `SecondaryTicks`, `DiscreteTrack` and the label/tooltip options come from the shared slider base and apply to both sliders. `IgbRangeSlider` uses `Lower` / `Upper` instead of `Value`. `@bind-Value:after` reacts to a new value without naming the event args, which differ by package: in the full product `IgbRating.Value` and `IgbSlider.Value` are `double` and their events carry `IgbNumberEventArgs`; in `IgniteUI.Blazor.Lite`, `IgbSlider<TValue>` and `IgbRating<TValue>` take `int`, `long`, `short`, `float`, `double`, or `decimal`, and the slider's `Input` / `Change` and the rating's `Change` / `Hover` carry `IgbNumberEventArgs<TValue>`.

## Color Picker

```razor
<IgbColorPicker @bind-Value="Background" Label="Background" />

<IgbColorPicker @bind-Value="Accent" Label="Accent"
                Mode="ColorPickerMode.Input" Format="ColorFormat.Rgb"
                ShowAlpha="true" Swatches="Palette" />

@code {
    string? Background { get; set; } = "#875fc4";
    string? Accent { get; set; }
    static readonly string[] Palette = ["#e91e63", "#3f51b5", "#009688"];
}
```

An HSV canvas with hue and alpha sliders, an editable color string, preset swatches, and the native EyeDropper where the browser has one. `Value` is a CSS color string (hex, `rgb(a)`, `hsl(a)`, or a named color); an empty or invalid string clears it. `Format` (`Hex | Rgb | Hsl`) changes only the notation, not the color, and `HideFormats` drops the format switcher. `Mode` is `Default` (trigger button) or `Input` (editable text field with a swatch prefix).

`Input` fires on every color change while `Change` fires on commit and drives `@bind-Value`; `Opening` / `Opened` / `Closing` / `Closed` track the picker surface. `Required`, `Disabled`, `Invalid` plus `CheckValidityAsync()` / `ReportValidityAsync()` / `SetCustomValidityAsync(message)` behave as on the other form controls.

In `IgniteUI.Blazor.Lite`, `IgbColorPicker<TValue>` takes `string` as well as `System.Drawing.Color`, either nullable, so a `Color?` property binds directly:

```razor
<IgbColorPicker @bind-Value="Highlight" Label="Highlight" />

@code {
    System.Drawing.Color? Highlight { get; set; }
}
```

A cleared `string` picker reports `""`; as a `Color`, `null` or `Color.Empty` clears it and a cleared picker reports the same, and in `Hsl` format the color is rounded to whole-number hue, saturation and lightness. `Input` and `Change` carry `IgbColorPickerValueEventArgs<TValue>`, whose `Detail` is the color as `TValue`.

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
