# Client Scripts — `*Script` Parameters

Every component parameter ending in `Script` takes the **name** of a JavaScript function registered on the client, not code. Two kinds exist:

| Kind | Examples | Function receives | Returns |
|---|---|---|---|
| Template | `ItemTemplateScript`, `GroupHeaderTemplateScript`, `MessageHeaderScript`, … | a context object | an `html` template result, a string, a primitive — anything lit can render |
| Event handler | `ChangeScript`, `ClosingScript`, `FocusScript`, … — one per C# event | the DOM `CustomEvent` | nothing |

**Package scope.** On `IgniteUI.Blazor.Lite`, register through the `api.js` module below; the `igRegisterScript` / `igRemoveScript` / `igTemplating` window globals no longer exist there. The full product (`IgniteUI.Blazor` / `IgniteUI.Blazor.Trial` 26.2, grids, charts, Dock Manager) serves the same `api.js` through its Lite dependency, and `registerScript` works for its `*Script` parameters too. Its older `igRegisterScript(name, fn, shouldCall)` window global is deprecated: it logs a warning on every call and exists only when the page loads `_content/IgniteUI.Blazor/app.bundle.js` (otherwise `ReferenceError`, and the script is never registered). If you keep it, pass `shouldCall = false` for handlers and templates.

## Registering

```html
<script type="module" src="my-scripts.js"></script>
```

```js
// wwwroot/my-scripts.js
import { registerScript, html } from './_content/IgniteUI.Blazor/api.js';

registerScript('CityItem', ({ item }) => html`<b>${item.Name}</b> <small>${item.Country}</small>`);

registerScript('OnDialogClosing', (evt) => {
    if (!confirm('Discard changes?')) {
        evt.preventDefault();
    }
});
```

- The specifier is relative to the importing script's own URL: `./_content/…` from a script at the wwwroot root, `../_content/…` from a subfolder. A classic script's `import()` resolves it against the document base.
- **Register at the top level of a `type="module"` script, or any time before Blazor starts.** Module scripts run once the page is parsed, while Blazor still has to fetch the component bundle before it can render, so registrations from a page-level module script are in place in time. If a registration lands after a component rendered, the library applies it as soon as it is registered. For a hard guarantee, register from the app's own JS initializer (`beforeWebStart`).
- The registered function is the script. For the few parameters that take a *value* rather than a function (e.g. `DataScript`), pass `true` as the third argument (`shouldCall`): the function is then called once when the parameter resolves and its result is used.
- Other exports: `removeScript(name)`, and `html` — the lit-html template tag, the same instance the components render with. Typings ship at `_content/IgniteUI.Blazor/api.d.ts`.
- From a classic (non-module) script use a dynamic import (no top-level `await` there): `import('./_content/IgniteUI.Blazor/api.js').then(({ registerScript, html }) => { /* register */ });`

## Templates

A template function receives one context object and returns an `html` template result, a string, a primitive value — anything lit can render. `html` is only needed for markup; plain text can be returned directly, and an empty string renders nothing.

The context mirrors the component's .NET template context: the same data is exposed under the same names — the item for a list item, the message for a chat message — so start from the component's C# template context type when writing one. One exception: a Combo group header gets `{ item }`, a record holding the group's value under the `GroupKey` field, which the C# `IgbComboGroupHeaderContext` of `IgniteUI.Blazor.Lite` exposes as `Key`.

```razor
@* Full product: T="string" in place of TValue and TItem *@
<IgbCombo TValue="string" TItem="City" Data="Cities" ValueKey="Id" DisplayKey="Name" GroupKey="Country"
          ItemTemplateScript="CityItem" GroupHeaderTemplateScript="CountryHeader" />
```

```js
registerScript('CityItem', ({ item }) => html`
    <b>${item.Name}</b>
    <small>${item.Country}</small>
`);

registerScript('CountryHeader', ({ item }) => `🌍 ${item.Country}`);
```

```razor
<IgbChat Options="ChatOptions" />

@code {
    private IgbChatOptions ChatOptions = new()
    {
        CurrentUserId = "user",
        Renderers = new IgbChatRenderers { MessageHeaderScript = "AgentHeader" }
    };
}
```

```js
registerScript('AgentHeader', ({ message }) => {
    if (message.sender === 'user') {
        return '';
    }
    return html`
        <div style="display: flex; align-items: center; gap: 8px;">
            <igc-avatar shape="circle" src="/agent.jpg"></igc-avatar>
            <b>Support Agent</b>
        </div>
    `;
});
```

`html` is the Lit HTML template tag: `${}` interpolates values, `@click=${fn}` binds listeners, and so on. Ignite UI web components (`<igc-avatar>`, `<igc-icon-button>`, …) work inside a template as long as the custom element is defined: registering the module (`AddIgniteUIBlazor(typeof(IgbAvatarModule), …)` in `Program.cs`) defines it, as does using the corresponding Blazor component anywhere in the app. Without either, `<igc-avatar>` is an undefined element and renders nothing.

## Event handler scripts

```razor
<IgbDialog ClosingScript="OnDialogClosing">…</IgbDialog>
```

```js
registerScript('OnDialogClosing', (evt) => {
    if (!confirm('Discard changes?')) {
        evt.preventDefault();   // igcClosing is cancelable
    }
});
```

The function is attached as a listener for the matching `igc*` DOM event on the component's element. `evt.detail` holds the event arguments where the event has any; cancelable events (`Opening`/`Closing` on dialogs, dropdowns and the like, `ActiveStepChanging` on the stepper, tree expand/collapse and selection changes, `Change` on some inputs) are cancelled with `evt.preventDefault()`. The grids (`IgbGrid`, `IgbTreeGrid`, `IgbHierarchicalGrid`) and `IgbPaginator` are different: none of their events is cancelable, and `preventDefault()` is ignored. Cancel a grid action by setting `evt.detail.cancel = true` in its `*Script` handler (`CellEditScript`, `RowEditScript`, `SortingScript`, `FilteringScript`, `RowAddScript`, `RowDeleteScript`, the paginator's `PagingScript`, …).

A script handler that calls back into .NET (`dotNetRef.invokeMethodAsync('Method', …)`) lands in a `[JSInvokable]` method, and Blazor does not re-render after it. End that method with one `StateHasChanged()` when it changes what the page shows — no `InvokeAsync` is needed, the call already runs on the renderer's dispatcher. Prefer the C# event when you don't need the script's synchronous `preventDefault()`. If that event is an `EventCallback<T>`, its handler re-renders automatically; the full product's `Action<T>` events (gauge `ValueChanged`, chart move events such as `SeriesPointerMove` / `SeriesMouseMove`, `IgbGrid` `RowAdded` / `RowDeleted` / `CellEditDone`, …) do not, so keep one `StateHasChanged()` in those handlers. Chart series Enter / Leave / Click and the pie's `SliceEnter` / `SliceHover` / `SliceLeave` are `EventCallback`s and need none.
