# Project Context

Concise context for ACS-aware tools. The coding rules live in
[`.github/copilot-instructions.md`](../../.github/copilot-instructions.md) and the contribution
workflow in [`.github/CONTRIBUTING.md`](../../.github/CONTRIBUTING.md). Read them before making
changes; if they disagree with this file, they win.

## Stack

- Language: C# (nullable annotations), multi-targeted `net8.0`, `net9.0`, `net10.0`; SDK pinned
  in `global.json`
- Framework: Blazor Razor class library (`IgniteUI.Blazor.Lite`) wrapping the
  `igniteui-webcomponents` custom elements
- Interop: TypeScript, native ESM, bundled by Vite into `src/wwwroot` (static web assets)
- Trimming: the library ships `IsTrimmable`; trim diagnostics (IL2xxx) build as errors
- Tests: xUnit + bUnit (unit), NUnit + Playwright against a TestBed app (integration),
  `node --test` for the built JS output
- Docs and demos: Blazing Story stories under `stories/`, `dotnet new` templates under `templates/`
- Tooling: `dotnet format whitespace`, Prettier (via lint-staged pre-commit hook)

## Architecture

- `src/components/Blazor/`: one C# wrapper per component (`<Name>.cs`, `<Name>Module.cs`, enums).
  Classes are `partial`; hand-written extensions live in `src/componentsBase/WebInputs/`
- `src/componentsBase/`: base classes (`BaseRendererControl`), DI (`AddIgniteUIBlazor`),
  serialization (`IgbJsonContext`, `MarshalByValueFactory`), data adapters and interop plumbing
- `src/src/`: TypeScript interop (`index.ts`, `Loader.ts`, `api.ts`); `src/src/ig/` holds the
  per-component description and metadata sources (generator style: tab-indented, prettier-exempt)
- `tests/`: `IgniteUI.Blazor.Tests` (unit), `IgniteUI.Blazor.Lite.IntegrationTests` and
  `IgniteUI.Blazor.Lite.TestBed` (integration), `IgniteUI.Blazor.Lite.PublishSmoke` (trimmed
  publish), `js/` (static web asset checks)
- `stories/`: Blazing Story app, one `.stories.razor` per component
- `templates/`: `dotnet new` project and item templates
- `docs/`: `TRIMMING.md` and the security threat model
- `skills/`: public, user-facing skills that ship with the product
- `.agents/`: contributor-facing skills and this context

## Workflow

Use the contributor skills instead of guessing (see [`.agents/skills/`](../skills/README.md)):

- Tests: [write-library-tests](../skills/write-library-tests/SKILL.md)
- Trimming and IL2xxx errors: [maintain-trim-compatibility](../skills/maintain-trim-compatibility/SKILL.md)
- Stories: [blazing-story-story](../skills/blazing-story-story/SKILL.md)
- Skills: [skill-authoring](../skills/skill-authoring/SKILL.md)

Before finishing, run `npm run build`, `npm test`, `dotnet build` and
`dotnet test tests/IgniteUI.Blazor.Tests`. For a public API change or a bug fix, update
`CHANGELOG.md`.

## Do not change without explicit instruction

- Public API: `[Parameter]` properties, enum values, `EventCallback`s and public methods. Deprecate
  with `[Obsolete]` for at least one major version before removal
- `global.json`, target frameworks, `Directory.Build.*` and `Directory.Packages.props`
- `package-lock.json` or new runtime dependencies (npm or NuGet)
- Build output: `src/wwwroot/`, `bin/`, `obj/`, `dist/`
- Trim suppressions: fix IL2xxx diagnostics first; never `#pragma warning disable` them
- Formatting: run only `dotnet format whitespace --folder`; a full `dotnet format` corrupts
  multi-targeted sources
