# MCP Server Setup — igniteui-cli

The `igniteui-cli` MCP (Model Context Protocol) server provides live component documentation, code samples, and scaffolding for Ignite UI across platforms including Blazor. Using the MCP server is **optional** but highly recommended — it gives the agent access to the latest API documentation and verified code samples.

---

## VS Code

Create or edit `.vscode/mcp.json` in your workspace root:

```json
{
  "servers": {
    "igniteui": {
      "command": "npx",
      "args": ["-y", "igniteui-cli@next", "mcp"]
    }
  }
}
```

> **Requires:** Node.js 18+ and npm installed and available on `PATH`.

After saving, VS Code will detect the MCP server configuration and prompt you to enable it. Accept the prompt to activate the server.

---

## Cursor

Create or edit `.cursor/mcp.json` in your workspace root:

```json
{
  "mcpServers": {
    "igniteui": {
      "command": "npx",
      "args": ["-y", "igniteui-cli@next", "mcp"]
    }
  }
}
```

---

## Claude Desktop

Add to your Claude Desktop configuration file:

**macOS:** `~/Library/Application Support/Claude/claude_desktop_config.json`  
**Windows:** `%APPDATA%\Claude\claude_desktop_config.json`

```json
{
  "mcpServers": {
    "igniteui": {
      "command": "npx",
      "args": ["-y", "igniteui-cli@next", "mcp"]
    }
  }
}
```

---

## WebStorm / JetBrains IDEs

1. Open **Settings → Tools → AI Assistant → Model Context Protocol (MCP)**.
2. Click **Add (+)** and configure:
   - **Name:** `igniteui`
   - **Command:** `npx`
   - **Arguments:** `-y igniteui-cli@next mcp`
3. Apply and restart the AI assistant.

---

## Verification

After configuring, verify the MCP server is running:

1. Open the agent/chat panel in your IDE.
2. Ask: *"What Ignite UI components are available?"*
3. The agent should list available components with descriptions from the MCP server.

If the server doesn't start:
- Check that `node` and `npx` are available on your system `PATH`.
- Check the MCP server logs in your IDE for errors.
- Try running `npx -y igniteui-cli@next mcp` in a terminal manually to confirm it works.

---

## What the MCP server provides

| Capability | Description |
|---|---|
| Component docs | API references, parameter lists, and descriptions for all Ignite UI components |
| Code samples | Verified code snippets for common scenarios |
| Scaffolding | Generate component boilerplate for Angular, Blazor, React, and Web Components |
| Version info | Information about available package versions and compatibility |

The MCP server is platform-agnostic — the same `igniteui-cli` server works for Blazor, Angular, React, and Web Components projects.

---

## See Also

- [setup.md](setup.md) — Blazor project setup (NuGet, Program.cs, _Imports.razor)
