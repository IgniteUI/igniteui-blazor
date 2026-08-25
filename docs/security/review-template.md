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

## Findings register

> Completed review records are kept in the maintainers' private security tracker while any
> finding remains open. Do not describe an unfixed, exploitable weakness in a public copy
> of this record — reference it by identifier only and disclose it after a fix ships,
> following the process in [threat-model.md](threat-model.md#7-how-findings-are-handled).

One row per finding, per package.

| ID | Package | Area / boundary | Sev | Disposition | Evidence / justification |
|---|---|---|---|---|---|
| <!-- TODO --> | | | | <!-- Fixed / Mitigated / By design / Accepted --> | |

**Disposition values** — `Fixed` (code changed, link the PR) · `Mitigated` (compensating control, name it) · `By design` (documented consumer responsibility) ·
`Accepted` (residual risk, requires an approver in the table below).

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
