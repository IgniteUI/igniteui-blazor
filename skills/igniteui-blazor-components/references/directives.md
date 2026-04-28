# Buttons, Ripple & Tooltip — Ignite UI for Blazor

This reference covers button components, ripple effect, and tooltip. In Blazor these are all first-class components (unlike Angular where some are directives).

---

## IgbButton

A styled button component with multiple variants.

### Variants

```razor
<IgbButton Variant="ButtonVariant.Flat">Flat</IgbButton>
<IgbButton Variant="ButtonVariant.Contained">Contained</IgbButton>
<IgbButton Variant="ButtonVariant.Outlined">Outlined</IgbButton>
<IgbButton Variant="ButtonVariant.Fab">FAB</IgbButton>
```

### With click handler

```razor
<IgbButton Variant="ButtonVariant.Contained" Click="OnSave" Disabled="@isSaving">
    @(isSaving ? "Saving..." : "Save")
</IgbButton>

@code {
    private bool isSaving = false;

    private async Task OnSave()
    {
        isSaving = true;
        // Perform save
        isSaving = false;
    }
}
```

### With icon

```razor
<IgbButton Variant="ButtonVariant.Contained">
    <IgbIcon Collection="material" Name="save" slot="prefix" />
    Save
</IgbButton>

<IgbButton Variant="ButtonVariant.Contained">
    Next
    <IgbIcon Collection="material" Name="arrow_forward" slot="suffix" />
</IgbButton>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Variant` | `ButtonVariant` | `Flat`, `Contained`, `Outlined`, `Fab` |
| `Disabled` | `bool` | Disables the button |
| `Type` | `ButtonBaseType` | `Button` (default), `Submit`, `Reset` |
| `Href` | `string` | Renders the button as a link |
| `Download` | `string` | Download attribute when using `Href` |
| `Target` | `ButtonBaseTarget` | `Self`, `Blank`, `Parent`, `Top` |

### Slots

| Slot | Description |
|---|---|
| `prefix` | Leading icon/content |
| `suffix` | Trailing icon/content |
| (default) | Button text |

### Events

| Event | Type | Description |
|---|---|---|
| `Click` | `EventCallback` | Fires on button click |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbButtonModule));
```

---

## IgbIconButton

An icon-only button with variants.

### Basic usage

```razor
<IgbIconButton Variant="IconButtonVariant.Flat"
                Collection="material" Name="favorite"
                Click="OnFavorite" />

<IgbIconButton Variant="IconButtonVariant.Contained"
                Collection="material" Name="delete"
                Click="OnDelete" />

<IgbIconButton Variant="IconButtonVariant.Outlined"
                Collection="material" Name="settings" />
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Variant` | `IconButtonVariant` | `Flat`, `Contained`, `Outlined` |
| `Collection` | `string` | Icon collection name |
| `Name` | `string` | Icon name |
| `Disabled` | `bool` | Disables the button |
| `Href` | `string` | Renders as a link |
| `Target` | `ButtonBaseTarget` | Link target |

### Events

| Event | Type | Description |
|---|---|---|
| `Click` | `EventCallback` | Fires on click |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbIconButtonModule));
```

---

## IgbToggleButton

A button that maintains a toggled (selected/unselected) state.

### Basic usage

```razor
<IgbToggleButton @bind-Selected="isBold" Variant="ButtonVariant.Outlined">
    <IgbIcon Collection="material" Name="format_bold" />
</IgbToggleButton>

<IgbToggleButton @bind-Selected="isItalic" Variant="ButtonVariant.Outlined">
    <IgbIcon Collection="material" Name="format_italic" />
</IgbToggleButton>

@code {
    private bool isBold = false;
    private bool isItalic = false;
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Selected` | `bool` | Whether the button is toggled on. Supports `@bind-Selected`. |
| `Variant` | `ButtonVariant` | `Flat`, `Contained`, `Outlined` |
| `Disabled` | `bool` | Disables the button |

### Events

| Event | Type | Description |
|---|---|---|
| `SelectedChanged` | `EventCallback<bool>` | Fires when the selected state changes |
| `Click` | `EventCallback` | Fires on click |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbToggleButtonModule));
```

---

## IgbButtonGroup

A group of buttons with selection semantics.

### Basic usage

```razor
<IgbButtonGroup Selection="ButtonGroupSelection.Single" @bind-SelectedItems="selectedItems">
    <IgbToggleButton Value="left">
        <IgbIcon Collection="material" Name="format_align_left" />
    </IgbToggleButton>
    <IgbToggleButton Value="center">
        <IgbIcon Collection="material" Name="format_align_center" />
    </IgbToggleButton>
    <IgbToggleButton Value="right">
        <IgbIcon Collection="material" Name="format_align_right" />
    </IgbToggleButton>
</IgbButtonGroup>

@code {
    private string[] selectedItems = new[] { "left" };
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Selection` | `ButtonGroupSelection` | `Single`, `SingleRequired`, `Multi` |
| `Disabled` | `bool` | Disables the entire group |

### Selection modes

| Mode | Description |
|---|---|
| `Single` | At most one button selected; clicking selected button deselects it |
| `SingleRequired` | Exactly one button selected at all times |
| `Multi` | Multiple buttons can be selected simultaneously |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbButtonGroupModule),
    typeof(IgbToggleButtonModule)
);
```

---

## IgbRipple

A ripple effect wrapper component.

### Basic usage

```razor
<IgbRipple>
    <div style="padding: 16px; cursor: pointer;">
        Click for ripple effect
    </div>
</IgbRipple>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Disabled` | `bool` | Disables the ripple effect |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbRippleModule));
```

> **Note:** Most Ignite UI button components already include a built-in ripple effect. Use `IgbRipple` only when you want to add ripple to a custom element.

---

## IgbTooltip

A tooltip that appears on hover/focus of a target element.

### Basic usage

```razor
<IgbButton id="tooltip-target">Hover me</IgbButton>
<IgbTooltip AnchorId="tooltip-target">
    This is a tooltip!
</IgbTooltip>
```

### Tooltip with component reference

```razor
<IgbButton @ref="tooltipTarget">Hover me</IgbButton>
<IgbTooltip Anchor="tooltipTarget">
    Tooltip text here
</IgbTooltip>

@code {
    private IgbButton tooltipTarget = default!;
}
```

### Positioning

```razor
<IgbButton id="my-btn">Hover</IgbButton>
<IgbTooltip AnchorId="my-btn" Placement="PopoverPlacement.Top">
    Top tooltip
</IgbTooltip>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `AnchorId` | `string` | The `id` attribute of the target element to anchor to |
| `Anchor` | `object` | A component reference to anchor to (alternative to `AnchorId`) |
| `Placement` | `PopoverPlacement` | `Top`, `Bottom`, `Left`, `Right`, `TopStart`, `TopEnd`, `BottomStart`, `BottomEnd`, `LeftStart`, `LeftEnd`, `RightStart`, `RightEnd` |
| `Offset` | `int` | Distance in pixels from the anchor |
| `Open` | `bool` | Manual control over tooltip visibility |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTooltipModule));
```

---

## Key Rules

1. **ButtonVariant vs IconButtonVariant** — `IgbButton` uses `ButtonVariant` (includes `Fab`); `IgbIconButton` uses `IconButtonVariant` (no `Fab`). Do not mix them up.
2. **Slots for icons in buttons** — Use `slot="prefix"` for leading icons and `slot="suffix"` for trailing icons within `IgbButton`. `IgbIconButton` renders the icon directly via `Collection` and `Name` parameters.
3. **Form submission** — Set `Type="ButtonBaseType.Submit"` on `IgbButton` to make it work as a submit button inside `<EditForm>`.
4. **Tooltip anchoring** — Use either `AnchorId` (string id) or `Anchor` (component ref), not both.
5. **ButtonGroup children** — `IgbButtonGroup` children should be `IgbToggleButton` instances for proper selection tracking.
6. **Ripple is optional** — Ignite UI buttons have built-in ripple. Only wrap custom elements in `IgbRipple`.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [form-controls.md](form-controls.md) — Form inputs paired with submit buttons
- [feedback.md](feedback.md) — Dialogs and snackbars that use buttons in their action areas
- [data-display.md](data-display.md) — Card actions and list items with icon buttons
- [layout.md](layout.md) — NavDrawer items with icon buttons
