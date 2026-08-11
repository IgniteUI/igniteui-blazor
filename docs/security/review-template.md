# Security review record — `<package>` `<version>`

> Copy this file to `review-<package>-<version>.md` for each release under review, fill it
> in, and merge it. It is the second artifact Microsoft requires alongside
> [threat-model.md](threat-model.md). `IgniteUI.Blazor.Lite` and
> `IgniteUI.Blazor.Templates` are reviewed and recorded separately.

| | |
|---|---|
| **Package / version** | <!-- TODO --> |
| **Commit reviewed** | <!-- TODO: full SHA --> |
| **Review date** | <!-- TODO --> |
| **Threat model version** | <!-- TODO: commit SHA of threat-model.md at review time --> |
| **Outcome** | <!-- Approved / Approved with conditions / Blocked --> |

## Reviewers

At least one reviewer must not be an author of the code under review.

| Name | Role | Author of reviewed code? |
|---|---|---|
| <!-- TODO --> | | |

## Coverage

Tick what was actually performed; an unticked row is a stated limitation, not an omission.

- [ ] Threat model walkthrough against the current code
- [ ] Manual review of the JS interop surface (`src/componentsBase/WebViewCallback.cs`, `BaseRendererControl`, `RendererSerializer`)
- [ ] Manual review of the unmarshalled path (`src/componentsBase/RuntimeHelper.cs`, `UnmarshalledDataSource`)
- [ ] Manual review of the serialization boundary (`JsonDataSource`, `RendererSerializer`)
- [ ] Rendering path review (`lit-html` / `igniteui-webcomponents` usage of `unsafeHTML`)
- [ ] Dependency review (`package-lock.json`, `Directory.Packages.props`)
- [ ] Static analysis results reviewed (CodeQL `csharp` + `javascript`)
- [ ] Build and release pipeline review (`.github/workflows/`)
- [ ] Package content inspection for **both** `.nupkg` files, including the template pack
- [ ] Scaffolded-app secure-defaults checklist executed against `dotnet new`
- [ ] Consumer-facing security documentation reviewed for accuracy

## Findings register — `IgniteUI.Blazor.Lite`

| ID | Summary | Sev | Disposition | Evidence / justification |
|---|---|---|---|---|
| TM-IX-01 | `WebCallback` public; client-supplied `containerId` addresses any control | High | <!-- Fixed / Mitigated / Accepted --> | |
| TM-IX-02 | `OnInvokeReturn` accepts untyped `object` | Medium | | |
| TM-IX-03 | `AdjustDynamicContentBatch` deserializes a client-supplied batch | Medium | | |
| TM-IX-04 | `_controlsMap` registration is unvalidated; `Add` throws on duplicate keys | Medium | | |
| TM-IX-05 | Untrusted event args reach consumer handlers | High | | |
| TM-MEM-01 | `unsafe` + reflected `InvokeUnmarshalled` + raw WASM-heap pointers | High | | |
| TM-MEM-02 | Silent loss of the unmarshalled fast path on runtime change | Low | | |
| TM-SER-01 | Full object graph serialized to the client | High | | |
| TM-SER-02 | Prerendered state embedded in initial HTML | Low | Accepted | Inherent to Blazor SSR; app-level cache headers |
| TM-DOM-01 | Rendering path: text vs. markup (`unsafeHTML` usage) | TBD | | |
| TM-DOM-02 | Consumer `RenderFragment` templates render consumer markup | Low | By design | Razor escapes by default; `MarkupString` is an explicit opt-in |
| TM-SC-01 | Bundled third-party JS is not independently patchable | Medium | By design | Covered by the `SECURITY.md` SLAs |
| TM-SC-02 | No CodeQL/SCA/`npm audit`/dependency-review gate | High | | |
| TM-BLD-01 | Undefined `BUILD_CONFIGURATION` in release signing/validation paths | Medium | | |
| TM-BLD-02 | Signature gate passes on an empty DLL result set | Medium | | |
| TM-BLD-03 | `Nullable` disabled on the Lite project | Low | Accepted | Generated sources are unannotated |

## Findings register — `IgniteUI.Blazor.Templates`

| ID | Summary | Sev | Disposition | Evidence / justification |
|---|---|---|---|---|
| TM-PKG-01 | `NoDefaultExcludes=true` + broad `Content Include` packs unintended files | High | | |
| TM-PKG-02 | `test-templates.ps1` / `.sh` may land under `content/` | Medium | | |
| TM-TPL-01 | Insecure defaults replicated into every scaffolded app | High | | |
| TM-TPL-02 | Template-pinned package versions go stale | Medium | | |
| TM-TPL-03 | `<Version>0.0.1</Version>` hard-coded rather than tag-driven | Low | | |

**Disposition values** — `Fixed` (code changed, link the PR) · `Mitigated` (compensating
control, name it) · `Accepted` (residual risk, requires an approver in the table below).

## Accepted risks

Every `Accepted` disposition above needs a named approver here.

| ID | Justification | Approver | Date |
|---|---|---|---|
| <!-- TODO --> | | | |

## Release gate

- [ ] No finding of severity **High** or above is left `Open`
- [ ] Every `Accepted` risk has a named approver
- [ ] TM-DOM-01 has a definitive answer
- [ ] `threat-model.md` has been updated to reflect this review

**Statement:** <!-- e.g. "As of <SHA>, <package> <x.y.z> has no open Critical or High
findings. Reviewed by <names> on <date>." -->
