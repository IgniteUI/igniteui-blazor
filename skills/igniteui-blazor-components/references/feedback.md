# Feedback & Overlay Components

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Dialog](#dialog)
- [Snackbar](#snackbar)
- [Toast](#toast)
- [Banner](#banner)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on feedback and overlay components, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Dialog

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDialogModule));
```

```razor
<IgbButton @onclick="OpenDialog">Open Dialog</IgbButton>

<IgbDialog @ref="ConfirmDialog"
           Title="Delete Item"
           KeepOpenOnEscape="false"
           CloseOnOutsideClick="false">
    <p>Are you sure you want to delete this item? This action cannot be undone.</p>
    <IgbButton slot="footer" Variant="@ButtonVariant.Flat" @onclick="CloseDialog">Cancel</IgbButton>
    <IgbButton slot="footer" Variant="@ButtonVariant.Contained" @onclick="ConfirmDelete">Delete</IgbButton>
</IgbDialog>

@code {
    public IgbDialog ConfirmDialog { get; set; }

    Task OpenDialog() => ConfirmDialog.ShowAsync();
    Task CloseDialog() => ConfirmDialog.HideAsync();

    async Task ConfirmDelete()
    {
        await ConfirmDialog.HideAsync();
        // perform delete
    }
}
```

---

## Snackbar

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSnackbarModule));
```

```razor
<IgbButton @onclick="ShowMessage">Save</IgbButton>

<IgbSnackbar @ref="Snack"
             DisplayTime="3000"
             ActionText="Undo"
             Action="OnUndo">
    Changes saved successfully.
</IgbSnackbar>

@code {
    IgbSnackbar Snack { get; set; }

    async Task ShowMessage()
    {
        await Snack.ShowAsync();
        // or use JS interop shortcut: id="snackbar" + onclick="snackbar.show()"
    }

    void OnUndo(IgbVoidEventArgs e) { /* undo logic */ }
}
```

> **TIP - JS interop shortcut:** Add `id="snackbar"` to `IgbSnackbar` and `onclick="snackbar.show()"` to the trigger button as a lightweight alternative when you don't need C# event handling.

---

## Toast

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbToastModule));
```

```razor
<IgbButton @onclick="ShowToast">Show Toast</IgbButton>

<IgbToast @ref="ToastRef" DisplayTime="4000">
    Operation completed successfully.
</IgbToast>

@code {
    IgbToast ToastRef { get; set; }
    Task ShowToast() => ToastRef.ShowAsync();
}
```

> **AGENT INSTRUCTION - Toast vs Snackbar:** `IgbToast` is a simple auto-dismissing notification with no action button. Use `IgbSnackbar` when you need an action button (e.g., "Undo"). Check the doc via `get_doc` for the current difference, as these components evolve.

---

## Banner

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbBannerModule));
```

```razor
<IgbBanner @ref="BannerRef">
    <IgbIcon slot="prefix" IconName="wifi_off" Collection="material" />
    You are currently offline.
    <IgbButton slot="actions" @onclick="RetryConnection">Retry</IgbButton>
</IgbBanner>

<IgbButton @onclick="ShowBanner">Show Banner</IgbButton>

@code {
    IgbBanner BannerRef { get; set; }
    Task ShowBanner() => BannerRef.ShowAsync();
    void RetryConnection() { /* retry logic */ }
}
```

---

## Key Rules

1. **Use `@ref` to obtain a component reference**, then call `ShowAsync()` / `HideAsync()` on it from C# code.
2. **`IgbDialog.KeepOpenOnEscape` is `false` by default** - meaning the dialog closes when the user presses ESC. Set it to `true` to prevent ESC from closing the dialog (forcing users to use your footer buttons).
3. **`IgbDialog.CloseOnOutsideClick` is `false` by default** - set it to `true` for light-dismiss dialogs.
4. **`IgbSnackbar.DisplayTime="0"` keeps it open until `HideAsync()` is called.** Use `KeepOpen` for the same effect.
5. **Footer buttons in `IgbDialog` must use `slot="footer"`.** Without the slot, they render in the body.
6. **`IgbBanner` is inline** (pushes content down) while `IgbDialog` is a modal overlay. Use banners for persistent, low-urgency messages and dialogs for confirmations or blocking actions.
