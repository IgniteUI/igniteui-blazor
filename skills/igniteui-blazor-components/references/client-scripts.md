# Client Scripts — `*Script` Parameters

Every component parameter ending in `Script` takes the **name** of a JavaScript function registered on the client, not code. Two kinds exist:

**Package scope.** The `api.js` module ships with `IgniteUI.Blazor.Lite`. The full product (`IgniteUI.Blazor` / `IgniteUI.Blazor.Trial`, grids, charts, Dock Manager) does not ship it yet: register there through the `igRegisterScript(name, fn, shouldCall)` window global, which its `app.bundle.js` script tag installs. Its `shouldCall` defaults to `true`, so pass `false` for handlers and templates.

| Kind | Examples | Function receives | Returns |
|---|---|---|---|
| Template | `ItemTemplateScript`, `GroupHeaderTemplateScript`, `MessageHeaderScript`, … | a context object | an `html` template result, a string, a primitive — anything lit can render |
| Event handler | `ChangeScript`, `ClosingScript`, `FocusScript`, … — one per C# event | the DOM `CustomEvent` | nothing |

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
- **Register at the top level of a `type="module"` script, or any time before Blazor starts.** Module scripts run once the page is parsed, while Blazor still has to fetch the component bundle before it can render, so registrations from a page-level module script are in place in time. If a registration ever lands after a component rendered, the library logs a console warning naming the script (late registration is not retried yet). For a hard guarantee, register from the app's own JS initializer (`beforeWebStart`).
- The registered function is the script. For the few parameters that take a *value* rather than a function (e.g. `DataScript`), pass `true` as the third argument (`shouldCall`): the function is then called once when the parameter resolves and its result is used. The deprecated `igRegisterScript` global defaults that argument to `true` instead.
- Other exports: `removeScript(name)`, and `html` — the lit-html template tag, the same instance the components render with. Typings ship at `_content/IgniteUI.Blazor/api.d.ts`.
- From a classic (non-module) script use a dynamic import (no top-level `await` there): `import('./_content/IgniteUI.Blazor/api.js').then(({ registerScript, html }) => { /* register */ });`
- The `igRegisterScript` / `igRemoveScript` / `igTemplating.html` window globals still work with the same signatures once the library has loaded, but are deprecated and warn in the console. A classic script that calls them while the page parses (before Blazor starts) needs the `<script src="_content/IgniteUI.Blazor/app.bundle.js">` tag, which queues those calls until `api.js` loads; without the tag, move the call to a module script.

## Templates

A template function receives one context object and returns an `html` template result, a string, a primitive value — anything lit can render. `html` is only needed for markup; plain text can be returned directly, and an empty string renders nothing.

The context mirrors the component's .NET template context: the same data is exposed under the same names — the item for a list item, the message for a chat message — so start from the component's C# template context type when writing one.

```razor
<IgbCombo T="City" Data="Cities" ValueKey="Id" DisplayKey="Name" GroupKey="Country"
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

`html` is the Lit HTML template tag: `${}` interpolates values, `@click=${fn}` binds listeners, and so on. Ignite UI web components (`<igc-avatar>`, `<igc-icon-button>`, …) work inside a template; if the component is not used as a Blazor component elsewhere in the app, register its module explicitly with `AddIgniteUIBlazor(typeof(IgbAvatarModule), …)` in `Program.cs`.

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

The function is attached as a listener for the matching `igc*` DOM event on the component's element. `evt.detail` holds the event arguments where the event has any; cancelable events (`Opening`/`Closing` on dialogs, dropdowns and the like, `ActiveStepChanging` on the stepper, tree expand/collapse and selection changes, `Change` on some inputs) are cancelled with `evt.preventDefault()`.
