# Ignite UI for Blazor — AI Agent Skills

Skill files for AI coding agents, plus the [`AGENTS.md`](./AGENTS.md) instruction file. Together they give an agent the Blazor coding standards and verified Ignite UI APIs needed to produce correct code instead of Angular- or React-flavored guesses.

Each skill is a `SKILL.md` routing hub with `references/` sub-files. The agent reads the hub, loads only the reference files the request touches, optionally verifies against the Ignite UI MCP servers, and writes code from that.

**The skills work without the MCP servers.** The reference files are self-contained; the MCP servers add per-version doc and token verification. No skill will configure an MCP server on its own — each includes the config snippet for when a user asks.

## Skills for building apps *with* Ignite UI for Blazor

| Skill | Covers |
|---|---|
| [`igniteui-blazor-components`](./igniteui-blazor-components/SKILL.md) | Every non-grid component: setup, form controls, layout and navigation, data display, overlays, Tile/Dock Manager, and charts, gauges, maps, sparklines |
| [`igniteui-blazor-grids`](./igniteui-blazor-grids/SKILL.md) | `IgbGridLite`, `IgbGrid`, `IgbTreeGrid`, `IgbHierarchicalGrid`, `IgbPivotGrid` — columns, editing, features, paging and remote data, sizing, state, migration |
| [`igniteui-blazor-theming`](./igniteui-blazor-theming/SKILL.md) | Built-in themes, `IgbThemeProvider`, palettes, component design tokens, CSS parts, dark mode, layout tokens |
| [`igniteui-blazor-generate-from-image-design`](./igniteui-blazor-generate-from-image-design/SKILL.md) | End-to-end workflow for implementing a view from a design image; composes the three skills above |

## Skill for working *on* this repository

| Skill | Covers |
|---|---|
| [`igniteui-blazor-lite-testing`](./igniteui-blazor-lite-testing/SKILL.md) | The bUnit unit suite and Playwright integration suite under `tests/`, and how to author interop wire contracts |

## MCP servers

| Server | Purpose | Key tools |
|---|---|---|
| `igniteui-cli` | Component docs and API reference | `list_components`, `get_doc`, `search_docs`, `search_api`, `get_api_reference` |
| `igniteui-theming` | Palette and design-token CSS generation | `create_palette`, `get_component_design_tokens`, `create_component_theme`, `set_roundness`, `set_spacing`, `set_size` |

Setup snippets are in each skill's `SKILL.md`.

## AGENTS.md

[`AGENTS.md`](./AGENTS.md) is a general-purpose agent instruction file for developers building Blazor applications **with** Ignite UI — it is not the AGENTS.md for this library's own repository.

Copy it (and optionally the `skills/` folder) into your Blazor application and place it where your tool reads project instructions:

| Tool | File |
|---|---|
| Claude Code | `CLAUDE.md` in the project root |
| GitHub Copilot (VS Code) | `.github/copilot-instructions.md` |
| Cursor | `.cursor/rules/igniteui-blazor.mdc` (or legacy `.cursorrules`) |
| Windsurf | `.windsurfrules` |
| Codex CLI | `AGENTS.md` in the project root |
| Aider | `CONVENTIONS.md`, or `--read AGENTS.md` at startup |
