# Performance targets and measurements

This document is the published performance budget for `IgniteUI.Blazor.Lite`. It exists so that a size or latency regression is a decision somebody makes on the record, rather than something a consumer discovers after upgrading.

Budgets are enforced, not aspirational: [`eng/bundle-budgets.json`](../eng/bundle-budgets.json) holds the numbers and [`eng/Check-BundleBudget.ps1`](../eng/Check-BundleBudget.ps1) fails the release when an asset exceeds one. The `evidence` job of the release workflow runs that check against the assets that were actually built and attaches `performance-report.md` and `performance-report.json` to the GitHub release.

Every release also publishes its budget in the workflow run summary: the same measured-versus-budget tables, plus any breaches, are written to the job summary of the run that produced the package. The numbers for a given release are therefore readable directly from its run, without downloading an artefact or trusting the table below to still be current.

## Scope

These budgets cover the static web assets the package ships under `_content/IgniteUI.Blazor`. They do not cover the consuming application's own bundle, the Blazor framework payload, or the .NET runtime download in a WebAssembly host — those are outside anything this package controls.

## Asset size budgets

Measured on 2026-08-27 from a production webpack build (`npm run build` followed by `npm run copythemes`) on Node 22, against `igniteui-webcomponents` 7.2.4.

| Group | Measured raw KiB | Raw budget | Measured gzip KiB | Gzip budget |
| --- | ---: | ---: | ---: | ---: |
| `loader` | 2.5 | 8 | 1.0 | 4 |
| `app` | 408.8 | 460 | 103.8 | 118 |
| `web-components-core` | 272.2 | 310 | 34.5 | 40 |
| `web-components` | 1912.4 | 2100 | 255.6 | 285 |
| `lazy-chunks` | 88.2 | 120 | 27.8 | 40 |
| `license-notices` | 0.7 | 8 | n/a | n/a |
| `source-maps` | 6119.1 | 6900 | n/a | n/a |
| `themes` | 312.8 | 345 | 30.9 | 36 |

| Total | Measured raw KiB | Raw budget | Measured gzip KiB | Gzip budget |
| --- | ---: | ---: | ---: | ---: |
| `served-javascript` | 2684.2 | 2950 | 422.6 | 470 |
| `served-assets` | 2997.0 | 3300 | 453.5 | 510 |
| `package-static-web-assets` | 9116.8 | 10240 | n/a | n/a |

Bundle filenames are content-hashed, so budgets are expressed as patterns rather than filenames, and a file's group is whichever pattern matches it first. `eng/bundle-budgets.json` lists specific patterns before catch-alls for exactly this reason — for example `app.*.bundle.js` is listed ahead of the generic `*.bundle.js`, so an app bundle is budgeted as `app`, not folded into `lazy-chunks`. An asset that matches no pattern at all fails the check, so a new bundle cannot enter the package without someone budgeting for it.

Two things worth knowing about these numbers:

- Source maps are two thirds of the package on disk but are never requested unless a developer opens devtools, so they carry a raw budget and no gzip budget. `served-assets` is the number that describes what a user actually downloads.
- Themes ship as eight prebuilt stylesheets and a consumer references one of them, so the `themes` figure is the whole set, not the per-page cost.

## Runtime targets

The following targets apply to the reference scenario — the Interactive Server test bed in [`tests/IgniteUI.Blazor.Lite.TestBed`](../tests/IgniteUI.Blazor.Lite.TestBed) rendering a single component on a warm server over a local connection, measured on the CI runner class.

| Metric | Target |
| --- | --- |
| Time from `blazor.web.js` start to the package's JS initializer resolving | < 250 ms |
| First component upgrade (custom element defined and rendered) after initializer | < 150 ms |
| Property write from .NET to reflected DOM state | < 50 ms |
| User interaction to `EventCallback` invocation on the server | < 100 ms plus circuit round-trip |

**These runtime targets are published but not yet measured per release.** The bundle-size half of this budget is enforced today; the timing harness that produces the runtime half is tracked separately and lands with the accessibility automation. Until it does, treat the table above as the committed target and the absence of a recorded measurement as a known gap rather than a passing result.

## Changing a budget

Raising a budget is allowed and sometimes correct — a new component or an upstream `igniteui-webcomponents` release legitimately adds bytes. What is not allowed is raising it silently. Update the number in `eng/bundle-budgets.json`, update the measured column in this table, and say why in the changelog entry for the release that carries the increase.

## Reproducing locally

```pwsh
npm ci
npm run build
npm run copythemes
./eng/Check-BundleBudget.ps1
```

The report is written to `artifacts/perf/`. Pass `-ReportOnly` to measure without failing, which is what you want when reseeding budgets after an intentional increase.
