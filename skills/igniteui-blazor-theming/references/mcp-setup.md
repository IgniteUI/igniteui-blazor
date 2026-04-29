# Setting Up the Theming MCP Server

> **Part of the [`igniteui-blazor-theming`](../SKILL.md) skill hub.**

## Contents

- [VS Code](#vs-code)
- [Cursor](#cursor)
- [Claude Desktop](#claude-desktop)
- [WebStorm / JetBrains IDEs](#webstorm--jetbrains-ides)
- [Verifying the Setup](#verifying-the-setup)

The Ignite UI Theming MCP server enables AI assistants to generate production-ready theming code. It must be configured in your editor before the theming tools become available.

The **same `igniteui-theming` MCP server** works for Angular, Blazor, React, and Web Components projects — the server detects the platform automatically from project files or accepts an explicit `platform` parameter.

---

## VS Code

Create or edit `.vscode/mcp.json` in your project:

```json
{
  "servers": {
    "igniteui-theming": {
      "command": "npx",
      "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"]
    }
  }
}
```

This works whether `igniteui-theming` is installed locally in `node_modules` or needs to be pulled from the npm registry — `npx -y` handles both cases.

> **Requires:** Node.js 18+ and npm installed and available on `PATH`.

After saving, VS Code will detect the MCP server configuration and prompt you to enable it. Accept the prompt to activate the server.

---

## Cursor

Create or edit `.cursor/mcp.json`:

```json
{
  "mcpServers": {
    "igniteui-theming": {
      "command": "npx",
      "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"]
    }
  }
}
```

---

## Claude Desktop

Edit the Claude Desktop config file:
- **macOS**: `~/Library/Application Support/Claude/claude_desktop_config.json`
- **Windows**: `%APPDATA%\Claude\claude_desktop_config.json`

```json
{
  "mcpServers": {
    "igniteui-theming": {
      "command": "npx",
      "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"]
    }
  }
}
```

---

## WebStorm / JetBrains IDEs

1. Go to **Settings → Tools → AI Assistant → MCP Servers**
2. Click **+ Add MCP Server**
3. Set Command to `npx` and Arguments to `igniteui-theming igniteui-theming-mcp`
4. Click OK and restart the AI Assistant

---

## Verifying the Setup

After configuring the MCP server, ask your AI assistant:

> "Detect which Ignite UI platform my project uses"

If the MCP server is running, the `detect_platform` tool will analyze your project files (`.csproj`, `package.json`) and return the detected platform (e.g., `blazor` or `web-components`).

If the server doesn't start:
- Check that `node` and `npx` are available on your system `PATH`.
- Check the MCP server logs in your IDE for errors.
- Try running `npx -y igniteui-theming igniteui-theming-mcp` in a terminal manually to confirm it works.

---

## What the MCP Server Provides

| Tool                            | Description                                                       |
| ------------------------------- | ----------------------------------------------------------------- |
| `detect_platform`               | Auto-detect Ignite UI platform from project files                 |
| `create_theme`                  | Generate a complete theme (palette, typography, elevations)       |
| `create_palette`                | Generate a palette from seed colors                               |
| `create_custom_palette`         | Generate a palette with explicit shade-by-shade control           |
| `create_component_theme`        | Generate component-level theme overrides                          |
| `get_component_design_tokens`   | List all available design tokens for a component                  |
| `get_color`                     | Get the correct CSS custom property reference for a palette color |
| `set_size`                      | Set the `--ig-size` layout control                                |
| `set_spacing`                   | Set the `--ig-spacing` layout control                             |
| `set_roundness`                 | Set the `--ig-radius-factor` layout control                       |
| `read_resource`                 | Read preset data (palettes, typography, elevations, guidance)     |

For Blazor projects, use `platform: "blazor"` or `platform: "web-components"` in tool parameters. Both produce CSS custom property output (no Sass).

---

## See Also

- [SKILL.md](../SKILL.md) — Theming skill hub (architecture, pre-built themes, custom themes, MCP workflow)
- [common-patterns.md](common-patterns.md) — Light/dark switching, scoped themes, CSS isolation
