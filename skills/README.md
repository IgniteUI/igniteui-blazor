# Ignite UI for Blazor - AI Agent Skills & Instructions

This folder contains **skill files** for AI coding agents and the [`AGENTS.md`](./AGENTS.md) instruction file. The skills are thin, workflow-focused layers over the Ignite UI MCP servers: the MCP servers supply all component documentation and API facts on demand, and the skills supply what the servers cannot - workflows, decision guides, and Blazor-specific pitfalls - so agents produce correct code without hallucinating stale APIs.

---

## Available Skills

### [`igniteui-blazor-components`](./igniteui-blazor-components/SKILL.md)

All non-grid Ignite UI for Blazor components: form controls, layout and navigation containers, data display, feedback overlays, Dock Manager, Tile Manager, and visualizations (charts, gauges, maps, sparklines). Covers package selection, the silent-failure setup checklist, and Blazor-specific gotchas.

### [`igniteui-blazor-grids`](./igniteui-blazor-grids/SKILL.md)

All Ignite UI Blazor data grid types with a selection decision guide:

| Component | Package | Use case |
|---|---|---|
| `IgbGridLite` | `IgniteUI.Blazor.GridLite` (MIT) | Read-only display with sorting, filtering, virtualization |
| `IgbGrid` | `IgniteUI.Blazor` | Flat tabular data - editing, grouping, paging, export |
| `IgbTreeGrid` | `IgniteUI.Blazor` | Self-referencing tree data (org charts, BOM) |
| `IgbHierarchicalGrid` | `IgniteUI.Blazor` | Multi-schema parent-child data |
| `IgbPivotGrid` | `IgniteUI.Blazor` | Pivot table analytics |

### [`igniteui-blazor-theming`](./igniteui-blazor-theming/SKILL.md)

Theme switching (Bootstrap, Material, Fluent, Indigo - light/dark), palette and component-theme generation via the `igniteui-theming` MCP server, design tokens, CSS parts, and Blazor CSS scoping rules (`::deep`, `igc-*` selectors).

### [`igniteui-blazor-generate-from-image-design`](./igniteui-blazor-generate-from-image-design/SKILL.md)

End-to-end workflow for implementing a Blazor view from a design image: analyze the image, discover components via MCP, generate a theme, implement with mock data, and refine visual fidelity. Ships with a visual-pattern → component mapping reference and a curated gotchas file.

---

## MCP Servers

The skills assume both servers are available; each SKILL.md includes a fallback config snippet. Setup details live in the product docs (`get_doc` names: `ai-assisted-development-overview`, `cli-mcp`, `theming-mcp`).

| Server | Purpose | Key tools |
|---|---|---|
| **`igniteui-cli`** | Component docs, API reference, setup guides | `list_components`, `get_doc`, `search_docs`, `search_api`, `get_api_reference`, `get_project_setup_guide` |
| **`igniteui-theming`** | Palette, component-theme, and layout-token CSS generation | `create_palette`, `create_custom_palette`, `get_component_design_tokens`, `create_component_theme`, `get_color`, `set_roundness`, `set_spacing`, `set_size`, `read_resource` |

```json
{
  "mcpServers": {
    "igniteui-cli": { "command": "npx", "args": ["-y", "igniteui-cli", "mcp"] },
    "igniteui-theming": { "command": "npx", "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"] }
  }
}
```

(VS Code `.vscode/mcp.json` uses a top-level `servers` key instead of `mcpServers`.)

## Installing the Skills

Copy the skill directories into your AI tool's skill discovery path - `.agents/skills/` is supported by most tools; Claude uses `.claude/skills/`. Convenient installers:

```bash
gh skill install IgniteUI/igniteui-blazor      # GitHub CLI
npx skills add IgniteUI/IgniteUI.Blazor        # skills CLI
```

See the `skills` doc (`get_doc(framework: "blazor", name: "skills")`) for per-tool discovery locations and all install options.

---

## AGENTS.md - What It Is and Where to Put It

[`AGENTS.md`](./AGENTS.md) is a **general-purpose AI agent instruction file** for developers building Blazor applications *with* Ignite UI for Blazor. It is **not** the AGENTS.md for this library's own repository.

Copy it (and optionally the `skills/` folder) into your Blazor application project, then place it in the location your AI tool reads for project-level instructions:

| AI Tool | File to create | Notes |
|---|---|---|
| **GitHub Copilot** (VS Code) | `.github/copilot-instructions.md` | Must be named exactly `copilot-instructions.md` inside `.github/` |
| **Claude Code** | `CLAUDE.md` (project root) | Read automatically on session start |
| **Cursor** | `.cursor/rules/igniteui-blazor.mdc` or `.cursorrules` | `.cursorrules` is the legacy path |
| **Windsurf** | `.windsurfrules` (project root) | Read automatically |
| **Codex CLI** | `AGENTS.md` (project root) | Read from cwd and parent directories |
| **Aider** | `CONVENTIONS.md` or `--read AGENTS.md` flag | Pass at startup |
