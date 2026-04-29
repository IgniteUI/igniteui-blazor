# Feedback — Ignite UI for Blazor

This reference covers feedback and overlay components: Dialog, Snackbar, Toast, and Banner.

---

## IgbDialog

A modal or non-modal dialog for user interaction, confirmations, and forms.

### Basic usage

```razor
<IgbButton Click="OpenDialog">Open Dialog</IgbButton>

<IgbDialog @ref="dialog" Title="Confirm Action">
    <p>Are you sure you want to proceed?</p>
    <div slot="footer">
        <IgbButton Variant="ButtonVariant.Flat" Click="CloseDialog">Cancel</IgbButton>
        <IgbButton Variant="ButtonVariant.Contained" Click="ConfirmDialog">Confirm</IgbButton>
    </div>
</IgbDialog>

@code {
    private IgbDialog dialog = default!;

    private async Task OpenDialog() => await dialog.ShowAsync();
    private async Task CloseDialog() => await dialog.HideAsync();
    private async Task ConfirmDialog()
    {
        // Handle confirm
        await dialog.HideAsync();
    }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Open` | `bool` | Whether the dialog is open. Supports `@bind-Open`. |
| `Title` | `string` | Dialog title text |
| `CloseOnOutsideClick` | `bool` | Closes the dialog when clicking outside (default: `false`) |
| `KeepOpenOnEscape` | `bool` | Prevents closing on Escape key (default: `false`) |
| `ReturnValue` | `string` | Return value passed when the dialog closes |

### Slots

| Slot | Description |
|---|---|
| `title` | Custom title content |
| `footer` | Footer area (typically action buttons) |
| (default) | Dialog body content |

### Methods

| Method | Returns | Description |
|---|---|---|
| `ShowAsync()` | `Task` | Opens the dialog as modal |
| `HideAsync()` | `Task` | Closes the dialog |

### Events

| Event | Type | Description |
|---|---|---|
| `Opening` | `EventCallback` | Fires before the dialog opens |
| `Opened` | `EventCallback` | Fires after the dialog opens |
| `Closing` | `EventCallback` | Fires before the dialog closes |
| `Closed` | `EventCallback` | Fires after the dialog closes |

### Dialog with form

```razor
<IgbDialog @ref="formDialog" Title="Edit Profile">
    <IgbInput @bind-Value="name" Label="Name" />
    <IgbInput @bind-Value="email" Type="InputType.Email" Label="Email" />
    <div slot="footer">
        <IgbButton Variant="ButtonVariant.Flat" Click="() => formDialog.HideAsync()">Cancel</IgbButton>
        <IgbButton Variant="ButtonVariant.Contained" Click="SaveProfile">Save</IgbButton>
    </div>
</IgbDialog>

@code {
    private IgbDialog formDialog = default!;
    private string name = "";
    private string email = "";

    private async Task SaveProfile()
    {
        // Save logic
        await formDialog.HideAsync();
    }
}
```

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDialogModule));
```

---

## IgbSnackbar

A brief message that appears at the bottom of the screen with an optional action button.

### Basic usage

```razor
<IgbButton Click="ShowSnackbar">Show Snackbar</IgbButton>

<IgbSnackbar @ref="snackbar" DisplayTime="3000">
    Item has been saved.
</IgbSnackbar>

@code {
    private IgbSnackbar snackbar = default!;

    private async Task ShowSnackbar() => await snackbar.ShowAsync();
}
```

### With action button

```razor
<IgbSnackbar @ref="snackbar" DisplayTime="5000">
    File deleted.
    <IgbButton slot="action" Variant="ButtonVariant.Flat" Click="UndoDelete">UNDO</IgbButton>
</IgbSnackbar>

@code {
    private IgbSnackbar snackbar = default!;

    private async Task UndoDelete()
    {
        // Undo logic
        await snackbar.HideAsync();
    }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `DisplayTime` | `int` | Auto-hide delay in milliseconds (default: `4000`). Set to `-1` to keep visible until manually closed. |
| `KeepOpen` | `bool` | When `true`, the snackbar stays open until explicitly hidden |

### Slots

| Slot | Description |
|---|---|
| `action` | Action button area |
| (default) | Message text |

### Methods

| Method | Returns | Description |
|---|---|---|
| `ShowAsync()` | `Task` | Shows the snackbar |
| `HideAsync()` | `Task` | Hides the snackbar |

### Events

| Event | Type | Description |
|---|---|---|
| `Action` | `EventCallback` | Fires when the action button is clicked |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSnackbarModule));
```

---

## IgbToast

A lightweight, non-blocking notification that auto-hides.

### Basic usage

```razor
<IgbButton Click="ShowToast">Show Toast</IgbButton>

<IgbToast @ref="toast" DisplayTime="3000">
    Operation completed successfully.
</IgbToast>

@code {
    private IgbToast toast = default!;

    private async Task ShowToast() => await toast.ShowAsync();
}
```

### Multiple toasts

```razor
<IgbButton Click="ShowSuccess">Success</IgbButton>
<IgbButton Click="ShowError">Error</IgbButton>

<IgbToast @ref="successToast" DisplayTime="3000">
    ✔ Saved successfully!
</IgbToast>

<IgbToast @ref="errorToast" DisplayTime="5000">
    ✖ An error occurred.
</IgbToast>

@code {
    private IgbToast successToast = default!;
    private IgbToast errorToast = default!;

    private async Task ShowSuccess() => await successToast.ShowAsync();
    private async Task ShowError() => await errorToast.ShowAsync();
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `DisplayTime` | `int` | Auto-hide delay in milliseconds (default: `4000`). Set to `-1` to keep visible until manually closed. |
| `KeepOpen` | `bool` | When `true`, the toast stays open until explicitly hidden |

### Methods

| Method | Returns | Description |
|---|---|---|
| `ShowAsync()` | `Task` | Shows the toast |
| `HideAsync()` | `Task` | Hides the toast |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbToastModule));
```

---

## IgbBanner

An inline, dismissible feedback banner for page-level messages.

### Basic usage

```razor
<IgbBanner @ref="banner">
    <IgbIcon slot="prefix" Collection="material" Name="info" />
    Your trial expires in 7 days. Upgrade to continue using all features.
    <div slot="actions">
        <IgbButton Variant="ButtonVariant.Flat" Click="DismissBanner">Dismiss</IgbButton>
        <IgbButton Variant="ButtonVariant.Flat" Click="UpgradeNow">Upgrade</IgbButton>
    </div>
</IgbBanner>

@code {
    private IgbBanner banner = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await banner.ShowAsync();
        }
    }

    private async Task DismissBanner() => await banner.HideAsync();
    private void UpgradeNow() { /* navigate to upgrade page */ }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Open` | `bool` | Whether the banner is visible. Supports `@bind-Open`. |

### Slots

| Slot | Description |
|---|---|
| `prefix` | Leading icon or graphic |
| `actions` | Action buttons |
| (default) | Message text |

### Methods

| Method | Returns | Description |
|---|---|---|
| `ShowAsync()` | `Task` | Opens the banner with animation |
| `HideAsync()` | `Task` | Closes the banner with animation |
| `ToggleAsync()` | `Task` | Toggles the banner visibility |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbBannerModule));
```

---

## Key Rules

1. **Use `@ref`** — All feedback components (Dialog, Snackbar, Toast, Banner) require a component reference (`@ref`) to call their `ShowAsync()` / `HideAsync()` methods programmatically.
2. **Async methods** — `ShowAsync()`, `HideAsync()`, `ToggleAsync()` return `Task`. Always `await` them.
3. **Modal vs non-modal** — `IgbDialog.ShowAsync()` opens a modal dialog by default. There is no separate non-modal method — control modality via `CloseOnOutsideClick` and `KeepOpenOnEscape`.
4. **DisplayTime** — `-1` means the element stays open indefinitely. Default is typically `4000` ms.
5. **Do not set `Open` and call `ShowAsync()`** — Use one approach or the other. Either bind `@bind-Open` reactively, or use `@ref` with `ShowAsync()` / `HideAsync()` imperatively.
6. **Slot names** — Use `slot="footer"` for dialog actions, `slot="action"` for snackbar buttons, `slot="actions"` for banner buttons. These are different slot names.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [directives.md](directives.md) — Buttons used in dialog footers and snackbar actions
- [form-controls.md](form-controls.md) — Form inputs used inside dialogs
- [data-display.md](data-display.md) — Lists and cards that may trigger feedback overlays
- [layout.md](layout.md) — Layout containers for page-level banners
