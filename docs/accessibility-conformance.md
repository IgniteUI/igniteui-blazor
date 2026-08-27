# Accessibility conformance

This document records the accessibility conformance claim for `IgniteUI.Blazor.Lite`, the scope that claim covers, how it is verified, and which failures are known and unfixed at the time of writing.

It is deliberately specific about what has and has not been verified. A conformance document that implies more testing than was performed is worse than no document, because a consumer who needs the claim cannot tell which parts to trust.

## Conformance claim

`IgniteUI.Blazor.Lite` targets **WCAG 2.2 Level AA**.

The components in this package are thin .NET wrappers around the custom elements published by [`igniteui-webcomponents`](https://github.com/IgniteUI/igniteui-webcomponents). Roles, names, states, keyboard interaction, and focus management are implemented in the underlying web component; the wrapper's accessibility responsibility is to pass parameters through faithfully and to avoid breaking the element's own contract. Accessibility defects therefore usually belong to one of two places, and this document distinguishes them.

## Scope

| Dimension | Covered |
| --- | --- |
| Components | Every `Igb*` component exported from `IgniteUI.Blazor.Controls` |
| Render modes | Interactive Server and Interactive WebAssembly |
| Browsers | Chromium, Firefox, and WebKit current stable |
| Assistive technology | NVDA, JAWS, and VoiceOver — see the smoke matrix below |

Static server rendering is explicitly **out of scope for the conformance claim**. Components in that mode render as unupgraded custom elements with no interactive behaviour and no ARIA semantics, because the custom element definitions are never executed. See [render mode support](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/general-getting-started-blazor-web-app#add-ignite-ui-for-blazor-component) for the supported configurations.

## Contributor requirements

[`CONTRIBUTING.md`](../.github/CONTRIBUTING.md) requires every contributor to implement and test against Section 508, WCAG, WAI-ARIA, and full keyboard navigation, and the pull request template carries an explicit accessibility verification checkbox. That is the standing bar for new work in this repository.

## Verification method

Three layers, with different cadences:

1. **Automated scanning.** An axe-core scan runs over every component in the Playwright integration suite, asserting the `wcag2a`, `wcag2aa`, `wcag21a`, `wcag21aa`, and `wcag22aa` rule sets. It gates pull requests and the release, and the resulting report is attached to the GitHub release as evidence.
2. **Keyboard operation.** Covered by the same suite: tab order, roving tab stops, arrow-key navigation, activation, and focus restoration.
3. **Screen reader smoke testing.** Manual, once per major release, against the matrix below.

Automated scanning catches roughly the subset of WCAG that is machine-checkable. It is a regression net, not a conformance proof — the once-per-major manual assessment is what substantiates the AA claim.

### Status of the automation

**The axe-core scan and the keyboard suite described above are not yet in place.** They are being implemented on a separate branch against the existing Playwright integration suite in [`tests/IgniteUI.Blazor.Lite.IntegrationTests`](../tests/IgniteUI.Blazor.Lite.IntegrationTests). Until that lands, layers 1 and 2 are a documented commitment rather than an enforced gate, and no per-release scan artefact exists. This section is written in the present tense for the process that is being built; the gap is stated here rather than papered over.

### Screen reader smoke matrix

| Screen reader | Browser | Platform | Last recorded run |
| --- | --- | --- | --- |
| NVDA | Chrome | Windows 11 | not yet recorded |
| JAWS | Chrome | Windows 11 | not yet recorded |
| VoiceOver | Safari | macOS | not yet recorded |

The first recorded run lands with the next release. Rows are filled in with the release version and date the run was performed against; a row that says "not yet recorded" means exactly that.

## Known failures

Accepted, unfixed accessibility defects at Level AA are listed here for as long as they remain unfixed.

| Issue | Component | Summary | Origin |
| --- | --- | --- | --- |
| [#336](https://github.com/IgniteUI/igniteui-blazor/issues/336) | `IgbRadioGroup` / `IgbRadio` | A radio rendered after the group has upgraded is not adopted into the group: it receives no group name, leaves a second tabbable element in the group, and is excluded from arrow-key reconciliation. Keyboard operation, focus order, and the group relationship exposed to assistive technology all degrade together. Binding a value repairs the duplicate checked state but not membership, name, or arrow navigation. | Upstream `igniteui-webcomponents` |

## Reporting an accessibility problem

Open an issue at [github.com/IgniteUI/igniteui-blazor/issues](https://github.com/IgniteUI/igniteui-blazor/issues) describing the component, the render mode, the assistive technology and browser, and the expected versus observed behaviour. If the defect is in the underlying custom element it will be reproduced against [`igniteui-webcomponents`](https://github.com/IgniteUI/igniteui-webcomponents) and tracked there, with the tracking issue linked back into the table above.
