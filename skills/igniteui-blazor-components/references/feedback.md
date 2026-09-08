# Feedback & Overlay Components

Module for each is `Igb<Name>Module`. All four expose `Open`, `ShowAsync()`, `HideAsync()`, `ToggleAsync()` (each `Task<bool>`) and `Closing` / `Closed` (`IgbVoidEventArgs`). Drive them with `@ref` + the `Async` methods; the sync twins (`Show()`, `Hide()`) need `IJSInProcessRuntime` and throw on Blazor Server.

## Choosing one

| Component | Modal? | Action button | Use for |
|---|---|---|---|
| `IgbDialog` | yes, blocks the page | footer buttons you supply | confirmations, forms, anything that must be answered |
| `IgbSnackbar` | no, floats | one, via `ActionText` + `Action` | brief message with an undo/retry affordance |
| `IgbToast` | no, floats | none | fire-and-forget status message |
| `IgbBanner` | no, inline — pushes content down | any, via `slot="actions"` | persistent low-urgency notice (offline, consent) |

## Dialog

```razor
<IgbButton @onclick="OpenDialog">Delete</IgbButton>

<IgbDialog @ref="ConfirmDialog" Title="Delete Item"
           KeepOpenOnEscape="false" CloseOnOutsideClick="false" HideDefaultAction="true">
    <p>Are you sure? This action cannot be undone.</p>
    <IgbButton slot="footer" Variant="ButtonVariant.Flat" @onclick="CloseDialog">Cancel</IgbButton>
    <IgbButton slot="footer" Variant="ButtonVariant.Contained" @onclick="ConfirmDelete">Delete</IgbButton>
</IgbDialog>

@code {
    IgbDialog ConfirmDialog { get; set; } = default!;

    Task OpenDialog()  => ConfirmDialog.ShowAsync();
    Task CloseDialog() => ConfirmDialog.HideAsync();

    async Task ConfirmDelete()
    {
        await ConfirmDialog.HideAsync();
        // perform delete
    }
}
```

- Footer buttons need `slot="footer"`; without it they render in the body.
- `KeepOpenOnEscape` defaults to `false` — Escape closes the dialog. Set it to `true` to force an explicit choice.
- `CloseOnOutsideClick` defaults to `false`; set it to `true` for light dismiss.
- `HideDefaultAction` removes the built-in OK button when you supply your own footer.
- `Title` sets the header; `ReturnValue` carries a result back after close.

## Snackbar & Toast

```razor
<IgbSnackbar @ref="Snack" DisplayTime="3000" ActionText="Undo" Action="OnUndo"
             Position="AbsolutePosition.Bottom">
    Changes saved successfully.
</IgbSnackbar>

<IgbToast @ref="ToastRef" DisplayTime="4000" Position="AbsolutePosition.Top">
    Operation completed.
</IgbToast>

@code {
    IgbSnackbar Snack { get; set; } = default!;
    IgbToast ToastRef { get; set; } = default!;

    Task ShowMessage() => Snack.ShowAsync();
    void OnUndo(IgbVoidEventArgs e) { /* undo logic */ }
}
```

Both inherit `DisplayTime` (ms), `KeepOpen`, `Position` (`AbsolutePosition`), and `Positioning`. `DisplayTime="0"` or `KeepOpen="true"` keeps the notification up until `HideAsync()`. Only `IgbSnackbar` has `ActionText` / `Action`.

## Banner

```razor
<IgbBanner @ref="BannerRef">
    <IgbIcon slot="prefix" IconName="wifi_off" Collection="material" />
    You are currently offline.
    <IgbButton slot="actions" @onclick="Retry">Retry</IgbButton>
</IgbBanner>

@code {
    IgbBanner BannerRef { get; set; } = default!;
    Task ShowBanner() => BannerRef.ShowAsync();
}
```

Slots: `prefix` for an icon or illustration, `actions` for buttons, default for the message. The banner is in-flow, so it reflows the page rather than overlaying it.
