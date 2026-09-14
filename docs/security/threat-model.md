# Threat model — IgniteUI.Blazor.Lite

This document explains where the trust boundaries of `IgniteUI.Blazor.Lite` lie, what the library guarantees at each of them, and what remains the consuming application's responsibility. It builds on Microsoft's threat mitigation guidance for Blazor rather than repeating it; the [Threat mitigation](#4-threat-mitigation) section lists only what is specific to this library.

To report a suspected vulnerability, follow [`SECURITY.md`](../../SECURITY.md). Do not open a public issue.

## 1. Scope

**In scope**

- The managed library and the JavaScript bundle it ships: the .NET component classes, the interop layer between them and the browser, the bundled `igniteui-webcomponents`, `igniteui-core` and `lit-html`.
- The build and release pipelines that produce and sign the NuGet package.

**Out of scope**

- The consuming application, including its authentication, authorization, data access and Content Security Policy.
- The internals of `igniteui-webcomponents`, `igniteui-core` and `lit-html`. Their behaviour at the rendering boundary is in scope; their implementation is a trusted-but-verified dependency.
- The ASP.NET Core Blazor framework. Its guarantees are assumed, not re-verified here.
- Storybook stories, tests and samples.

## 2. How the library works

Every component the application places in a Razor page is a **.NET component instance** that owns one **web component** in the browser. The .NET side is the source of truth: parameter values, bound data and template registrations flow to the browser as **renderer messages**, and the web component renders them inside its shadow DOM. In the other direction the browser sends back DOM events, return values of methods the .NET side invoked, and requests to instantiate templates the application registered. All of that traffic enters .NET through a single **interop callback object** that is scoped to the Blazor circuit (or, in WebAssembly, to the runtime) and addresses the target component by an opaque container id.

On WebAssembly there is an additional **unmarshalled data channel**: tabular bound data is handed to the bundle as typed column buffers that JavaScript reads directly from the managed heap instead of receiving JSON.

```mermaid
flowchart LR
  subgraph NET[".NET side"]
    C[".NET component instances"]
    K["Interop callback object<br/>(one per circuit / runtime)"]
    A["Consuming app<br/>event handlers, templates, bound data"]
  end
  subgraph BR["Browser"]
    L["Bundle: loader / renderer"]
    E["Web components<br/>+ lit-html (shadow DOM)"]
  end
  A --> C
  C -- "renderer messages (JSON)" --> L
  C -. "unmarshalled column buffers (WASM only)" .-> L
  L -- "events, return values, template requests" --> K
  K --> C --> A
  L --> E
```

## 3. Trust boundaries by hosting model

Where the trust boundary sits depends entirely on how the application hosts Blazor. The library ships the same code for every model, so this section is the key to reading the rest of the document.

| Hosting model | Where .NET runs | Boundary | What Microsoft's guidance says |
|---|---|---|---|
| **Interactive Server** | On the server, inside a SignalR circuit | Browser → circuit. Every call from the bundle into the interop callback object is a call into the server. | [Interactive server-side rendering](https://learn.microsoft.com/aspnet/core/blazor/security/interactive-server-side-rendering): *"Treat any .NET method exposed to JavaScript as you would a public endpoint to the app."* Circuit and message-size limits bound resource exhaustion. |
| **WebAssembly** | In the browser, same origin as the page script | None inside the browser. The .NET runtime, the bundle and any attacker script share one origin on the user's machine; the security boundary is the application's own HTTP APIs. | No dedicated page exists because nothing on the client is trusted. Standard client-side rules apply: no secrets in the app, all authorization on the server. |
| **Static SSR / prerendering** | On the server, once, with no interactivity | Server → HTML. No interop runs; the bundle only hydrates whatever the server emitted. | [Static server-side rendering](https://learn.microsoft.com/aspnet/core/blazor/security/static-server-side-rendering). For this library the only exposure is which bound data is written into the page. |
| **Hybrid (WebView)** | In a native process hosting a WebView | Browser → native process. The same interop surface as Interactive Server, but the callee has the privileges of the host application rather than of a web server. | [Blazor Hybrid security](https://learn.microsoft.com/aspnet/core/blazor/hybrid/security/). Treat the WebView content as untrusted and keep the interop surface minimal. |

Three flows cross these boundaries:

- **Outbound: .NET → browser.** Renderer messages and, on WebAssembly, unmarshalled column buffers. The concern is *confidentiality*: which data leaves the .NET side.
- **Inbound: browser → .NET.** Events, method return values and template requests through the interop callback object. Under Interactive Server and Hybrid this is attacker-reachable input; the concern is *integrity* and *availability* of the .NET side.
- **Rendering: data → markup.** Bound values becoming DOM, on the server through Blazor's render tree and in the browser through lit-html. The concern is *script injection* into the application's origin.

The unmarshalled data channel is not a trust boundary. JavaScript reads memory at pointers and layouts the .NET side produced, and page script can already read the whole WebAssembly heap on its own. It is a memory-layout correctness concern, covered below, not an attacker-facing one.

## 4. Threat mitigation

The consuming application must first apply Microsoft's guidance for its hosting model. On top of that, the library guarantees the following properties. They are verified against the code and re-verified whenever the interop layer, the rendering path or the bundled third-party JavaScript changes.

### 4.1 Inbound interop

| Threat | What the library does |
|---|---|
| A client forges a call targeting another user's components | Impossible by construction. The interop callback object is reached only through a Blazor `DotNetObjectReference` that belongs to one circuit, so a caller can address only components registered in its own circuit. The container id is a lookup key inside that circuit, never an authority. |
| A client raises an event the application did not subscribe to | Dropped. An event is dispatched only if the .NET component instance has a handler registered for that exact event name; unknown names are ignored. |
| A client-supplied payload selects a .NET type or executes code | Not possible. Payloads are parsed with source-generated `System.Text.Json` into plain dictionaries and primitive values. Object references in a payload are resolved by id against a registry of elements the .NET side created; no client-supplied type name is ever used to instantiate anything. |
| A client instantiates arbitrary templates or content | Only templates the application registered on the component can be requested, and they render through Blazor `RenderFragment`s like any other Razor content. |
| Resource exhaustion through interop floods or oversized payloads | Handled by the framework limits the application configures (`CircuitOptions`, `MaximumReceiveMessageSize`, interop timeout). The library adds no independent caps and relies on those defaults being kept. Inbound work is processed one invocation at a time per connection, so a flood from one client costs it a connection per unit of parallelism; limiting connections per user is the application's job, as for any SignalR hub. |

What remains after these guarantees is exactly what remains for a plain Blazor `@onclick`: a compromised client can fire a handler the application wired up, with arguments of the shape the application expects. See [§5](#5-guidance-for-application-developers).

### 4.2 Rendering

| Threat | What the library does |
|---|---|
| A bound value is interpreted as HTML on the server | Never. Server-side markup is produced through `RenderTreeBuilder.AddContent` and `AddAttribute`, which encode. The only `AddMarkupContent` calls carry whitespace literals. Bound values are never wrapped in `MarkupString`. |
| A bound value is interpreted as HTML in the browser | Never through the library. The bundle renders via `lit-html`, whose text and attribute bindings escape. `igniteui-core` contains a string-concatenating `innerHTML` fallback renderer, but the bundle installs lit-html's renderer at module load, so the fallback is unreachable. |
| Dynamic code in the bundle requires `unsafe-eval` | Not required. The bundle contains no `eval` or `new Function`, and the production build uses `hidden-source-map`, not an eval-based devtool. Applications can run the bundle under a CSP without `unsafe-eval`. |

Content the application renders *inside* a component through templates or child content is the application's own Razor markup and is subject to the same rules as anywhere else in the app.

### 4.3 Outbound data

| Threat | What the library does |
|---|---|
| Bound data reaches the browser beyond what is displayed | Data-bound components serialize the **public properties and fields** of the bound item type, not only the members a template happens to render. Prerendering writes that serialized state into the HTML. This is by design; see [§5](#5-guidance-for-application-developers). |
| Unmarshalled column buffers are read at the wrong layout | The .NET writer and the JavaScript reader agree on a fixed column layout, covered by unit tests. On .NET 8 the channel uses `InvokeUnmarshalled`; on .NET 9 and later it passes raw pointers through the in-process runtime. Outside WebAssembly the same data travels as JSON. |

## 5. Guidance for application developers

- **Treat event arguments as user input.** Anything a handler receives from a component event originated in the browser. Validate it as you would a form post before acting on it, and never derive authorization from it.
- **Bind projections, not entities.** Every public property and field of a bound item type is serialized to the browser and, under prerendering, into the page HTML. Map to a view model that holds only what the component needs.
- **Authorize before binding.** Components perform no authentication or authorization. Data handed to a component has already passed the application's filters.
- **Keep the framework limits.** Leave `CircuitOptions`, `MaximumReceiveMessageSize` and the interop timeout at their defaults or lower them. The library depends on them for availability.
- **Apply a Content Security Policy.** The library needs neither `unsafe-eval` nor inline script. Most inbound threats presuppose attacker script already running in the page, which a CSP and an XSS-free application prevent. Load third-party scripts only from origins the policy allows, with Subresource Integrity where possible, following [Microsoft's Blazor CSP guidance](https://learn.microsoft.com/aspnet/core/blazor/security/content-security-policy).
- **In Hybrid apps, treat WebView content as untrusted.** The interop callback object has the privileges of the host process; follow Microsoft's Blazor Hybrid guidance for the WebView.

## 6. Supply chain, build and release

- **Static analysis** — GitHub CodeQL code scanning (default setup) analyses C# and JavaScript/TypeScript on pushes and pull requests.
- **Dependency alerts** — GitHub Dependabot alerts cover the NuGet and npm manifests; Dependabot version updates are configured for GitHub Actions with a 14-day cooldown and security updates fast-tracked.
- **Release integrity** — Authenticode signing of all DLLs followed by a signature validation gate; NuGet package signing followed by `dotnet nuget verify`.
- **Credential hygiene** — Azure OIDC federation and NuGet Trusted Publishing with short-lived OIDC-issued keys; no long-lived publish secrets.
- **Least privilege and pinning** — repository-level `contents: read`, job-scoped `id-token: write`, publishing gated behind the protected `nuget-org-publish` environment, release actions pinned to commit SHAs.
- **Reproducible inputs** — `npm ci` without package-manager caching in release builds, `<Deterministic>true</Deterministic>`, central package version management.
- **Compiler safety** — the library compiles with `Nullable` enabled and nullable warnings as errors, and is trim-compatible with the trim analyzer warning-free. `AllowUnsafeBlocks` is enabled solely for the unmarshalled data channel.
- **Testing** — bUnit unit tests, including the unmarshalled channel, and Playwright integration tests run in CI.

## 7. Reporting and disclosure

Suspected vulnerabilities are reported privately as described in [`SECURITY.md`](../../SECURITY.md), which also states the acknowledgement and triage timelines. Fixed issues are disclosed through GitHub Security Advisories and release notes once a fix is available. Findings that require action by application developers are added to [§5](#5-guidance-for-application-developers).

## 8. References

- [`SECURITY.md`](../../SECURITY.md) — vulnerability reporting and disclosure policy.
- [Threat mitigation guidance for ASP.NET Core Blazor interactive server-side rendering](https://learn.microsoft.com/aspnet/core/blazor/security/interactive-server-side-rendering)
- [Threat mitigation guidance for ASP.NET Core Blazor static server-side rendering](https://learn.microsoft.com/aspnet/core/blazor/security/static-server-side-rendering)
- [ASP.NET Core Blazor Hybrid security considerations](https://learn.microsoft.com/aspnet/core/blazor/hybrid/security/)
- [ASP.NET Core Blazor authentication and authorization](https://learn.microsoft.com/aspnet/core/blazor/security/)
- [Prevent cross-site scripting (XSS) in ASP.NET Core](https://learn.microsoft.com/aspnet/core/security/cross-site-scripting)
- [Microsoft Security Development Lifecycle: threat modeling](https://www.microsoft.com/en-us/securityengineering/sdl/threatmodeling)
