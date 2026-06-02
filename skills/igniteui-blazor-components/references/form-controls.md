# Form Controls & EditForm Integration

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Input](#input)
- [Combo Box](#combo-box)
- [Select](#select)
- [Date Picker](#date-picker)
- [Date Range Picker](#date-range-picker)
- [Calendar](#calendar)
- [Date Time Input](#date-time-input)
- [Mask Input](#mask-input)
- [Checkbox](#checkbox)
- [Radio / Radio Group](#radio--radio-group)
- [Switch](#switch)
- [Slider / Range Slider](#slider--range-slider)
- [Rating](#rating)
- [Form & Binding Notes](#form--binding-notes)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on form controls, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Input

```csharp
// Program.cs
builder.Services.AddIgniteUIBlazor(typeof(IgbInputModule));
```

```razor
<IgbInput @bind-Value="UserName" Label="Username" Placeholder="e.g. John Doe">
    <span slot="prefix">User</span>
</IgbInput>

@code {
    string UserName { get; set; } = "";
}
```

> **AGENT INSTRUCTION - Icons in slots:** Always use `IgbIcon` in `prefix`/`suffix` slots, never `<span class="material-icons">`. Font spans are `display: inline` so `vertical-align` is ignored inside the slot's flex context - the icon floats to the top. `IgbIcon` (`igc-icon`) is `display: inline-flex; align-items: center` and self-centers automatically. Register the icon in `OnAfterRenderAsync(firstRender)` after `EnsureReady()`.

Events: `InputOcurred` (fires while typing), `Change` (fires on commit/blur).

> **AGENT INSTRUCTION:** `IgbInput` has **no** `GetValueAsync()` method. Read values via the synchronous `Value` property or, preferably, use `@bind-Value` bound directly to a model property - that is the correct Blazor pattern and avoids the need to imperatively read the value at all.

---

## Combo Box

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbComboModule));
```

```razor
<IgbCombo T="string"
          Data="Cities"
          ValueKey="Id"
          DisplayKey="Name"
          Label="Select Cities"
          Placeholder="Pick a city" />

@code {
    private List<City> Cities = SampleData.Cities;

    record City(string Id, string Name, string Country);
}
```

> **AGENT INSTRUCTION:** `IgbCombo` does **not** work inside a standard HTML `<form>`. Use `<EditForm>` with `@bind-Value` instead.

---

## Select

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSelectModule));
```

```razor
<IgbSelect Label="Fruit" Placeholder="Choose a fruit">
    <IgbSelectItem Value="apple">Apple</IgbSelectItem>
    <IgbSelectItem Value="orange">Orange</IgbSelectItem>
    <IgbSelectItem Value="banana">Banana</IgbSelectItem>
</IgbSelect>
```

---

## Date Picker

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDatePickerModule));
```

```razor
<IgbDatePicker @ref="Picker" Value="@SelectedDate" />

@code {
    public IgbDatePicker Picker { get; set; }
    public DateTime SelectedDate { get; set; } = DateTime.Today;
}
```

---

## Date Range Picker

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDateRangePickerModule));
```

```razor
<IgbDateRangePicker @ref="RangePicker" />

@code {
    public IgbDateRangePicker RangePicker { get; set; }
}
```

---

## Calendar

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCalendarModule));
```

```razor
<IgbCalendar />
```

Use Calendar when the UI needs an always-visible date picker rather than an input/dropdown picker. For multiple selection, range selection, special dates, disabled dates, visible months, week numbers, and localization, use the exact property names from the `calendar` MCP doc before writing markup.

---

## Date Time Input

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDateTimeInputModule));
```

```razor
<IgbDateTimeInput @bind-Value="SelectedDateTime"
                  InputFormat="MM/dd/yyyy HH:mm"
                  SpinLoop="true" />

@code {
    public DateTime? SelectedDateTime { get; set; } = DateTime.Now;
}
```

---

## Mask Input

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbMaskInputModule));
```

```razor
<IgbMaskInput Mask="(000) 000-0000"
              Label="Phone Number"
              Placeholder="(555) 123-4567" />
```

---

## Checkbox

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCheckboxModule));
```

```razor
<IgbCheckbox @bind-Checked="IsSubscribed">Subscribe to newsletter</IgbCheckbox>

@code {
    bool IsSubscribed { get; set; }
}
```

---

## Radio / Radio Group

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbRadioModule), typeof(IgbRadioGroupModule));
```

```razor
<IgbRadioGroup Alignment="@ContentOrientation.Vertical" Name="plan">
    <IgbRadio Value="basic">Basic</IgbRadio>
    <IgbRadio Value="pro">Pro</IgbRadio>
    <IgbRadio Value="enterprise">Enterprise</IgbRadio>
</IgbRadioGroup>
```

---

## Switch

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSwitchModule));
```

```razor
<IgbSwitch @bind-Checked="IsDarkMode">Dark Mode</IgbSwitch>

@code {
    bool IsDarkMode { get; set; }
}
```

---

## Slider / Range Slider

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSliderModule), typeof(IgbRangeSliderModule));
```

```razor
<!-- Single value -->
<IgbSlider Value="40" Min="0" Max="100" Step="5" Change="OnSliderChange" />

<!-- Range -->
<IgbRangeSlider Lower="20" Upper="70" Change="OnRangeChange" />

@code {
    void OnSliderChange(IgbNumberEventArgs e) => Console.WriteLine(e.Detail);
    void OnRangeChange(IgbRangeSliderValueEventArgs e)
    {
        var lower = e.Detail.Lower;
        var upper = e.Detail.Upper;
    }
}
```

---

## Rating

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbRatingModule));
```

```razor
<IgbRating @bind-Value="StarRating" Max="5" />

@code {
    double StarRating { get; set; } = 3;
}
```

---

## Form & Binding Notes

> **AGENT INSTRUCTION:** Do not assume one universal form integration pattern. Several Ignite UI Blazor components such as `IgbCombo` and `IgbRadio` do **not** work with a standard HTML `<form>` element. Use the MCP doc for the exact component, bind values explicitly, and only use a form pattern shown by the current docs.

```razor
<IgbInput @bind-Value="Model.Name" Label="Name" Required="true" />
<IgbCheckbox @bind-Checked="Model.Agreed">I agree to the terms</IgbCheckbox>
<IgbSelect @bind-Value="Model.Country" Label="Country" Name="country">
    <IgbSelectItem Value="us">United States</IgbSelectItem>
    <IgbSelectItem Value="uk">United Kingdom</IgbSelectItem>
</IgbSelect>

@code {
    public class FormModel
    {
        [Required] public string Name { get; set; } = "";
        public bool Agreed { get; set; }
        public string Country { get; set; } = "";
    }

    FormModel Model = new();

    void Save() { /* validate and persist Model using the pattern required by your app */ }
}
```

---

## Key Rules

1. **Do not wrap Ignite UI inputs in a standard HTML `<form>` unless the component doc shows that pattern.** Form behavior differs by component.
2. **Register every module you use in `Program.cs`** using the `typeof(Igb{Name}Module)` pattern.
3. **`IgbCombo` requires the `T` type parameter** - set it to the type of your `ValueKey` property, or `"object"` if there is no `ValueKey`.
4. **Use `@bind-Value` / `@bind-Checked` for two-way data binding** in Blazor. Never call `GetValueAsync()` to read a field value - that method does not exist on `IgbInput`.
5. **`IgbSlider` and `IgbRangeSlider` have separate modules** - register both if you use range sliders.
