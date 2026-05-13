---
allowed-tools: Glob Grep Read Write
compatibility: Designed for Blazing Story projects (a Blazor-based Storybook clone for .NET). Requires a .Stories project referencing BlazingStory NuGet packages.
description: Implement a Blazing Story story file (.stories.razor) for a Blazor UI component. Use when the user says "create a story for component X", "add stories for X", or similar requests in a Blazing Story (.NET / Blazor / Storybook) project.
license: Unlicense
metadata:
    author: jsakamoto
    github-path: skills/blazing-story-story
    github-ref: refs/tags/v1.0.2
    github-repo: https://github.com/BlazingStory/agent-skills
    github-tree-sha: 36d13a0943547a646a3d91de27c9d8870b951cbd
name: blazing-story-story
---
# Blazing Story — Story Implementation

Create a `.stories.razor` file for a Blazor component in the currently open Blazing Story project.

## Investigation policy

The main goal of this policy is to free the developer from the hassle of approving "may I run this command?" prompts one by one. Many of those prompts come from operations that poke around outside the project — and most of the knowledge needed to write a story file is already available without them.

Implement the story relying primarily on:

- The guidance in this skill file
- Your own knowledge of C#, .NET, Blazor, and general web/UI development
- Other relevant skills available in this environment
- Already-configured MCP servers and tools
- Read-only exploration of the current project (`ls`, `Glob`, `Grep`, `Read`)

**Avoid** operations that inspect the NuGet package cache folder, decompile Blazing Story DLLs, or otherwise probe the installed package contents. These are slow, require the developer's per-command approval, and disrupt the flow of work.

If implementation details that are not covered above become necessary, consult the published source code on GitHub at https://github.com/jsakamoto/BlazingStory **instead of** digging into the local NuGet cache or decompiling DLLs.

This policy may be relaxed only when strictly unavoidable.

## Step 1: Identify the target component

From `$ARGUMENTS` or the user's message, determine the component name (e.g., `Button`, `Rating`).

## Step 2: Locate the component file

Search the workspace for a `.razor` file matching the component name (e.g., `Button.razor`). Read it to understand:

- All `[Parameter]` properties and their types
- Any `RenderFragment` parameters (e.g., `ChildContent`)
- Enum types used by parameters

## Step 3: Locate the stories project

Find the stories project directory — it is typically a separate project named `*.Stories` or containing a `Stories/` subfolder. Look for existing `.stories.razor` files to confirm the correct location and the `@using` conventions used.

## Step 4: Determine the story file path

Place the new file inside the `Stories/` folder of the stories project, mirroring the category structure if one already exists. Name the file `ComponentName.stories.razor`.

Example: `MyApp.Stories/Stories/Components/Button.stories.razor`

## Step 5: Write the story file

Use the following structure:

```razor
@attribute [Stories("Category/ComponentName")]

<Stories TComponent="ComponentName" Layout="typeof(Centered)">

    <ArgType For="_ => _.EnumParam" Control="ControlType.Radio" />
    <ArgType For="_ => _.ColorParam" Control="ControlType.Color" />

    <Story Name="Default">
        <Arguments>
            <Arg For="_ => _.SomeParam" Value="someValue" />
        </Arguments>
        <Template>
            <ComponentName @attributes="context.Args" />
        </Template>
    </Story>

</Stories>

@code {
    private RenderFragment _content = @<text>Label</text>;
}
```

### Rules

**File naming**
- Must end in `.stories.razor` to enable the "Show code" feature in Blazing Story.

**`[Stories("...")]` path**
- Use `/` as separator. The path becomes the sidebar navigation tree.
- Mirror the folder path under `Stories/` (e.g., file at `Stories/Components/Button.stories.razor` → `[Stories("Components/Button")]`).

**`<Stories TComponent="...">`**
- `TComponent` is the Blazor component type.
- `Layout` is optional. Common choices:
  - `Centered` — centers the component horizontally and vertically (good default for most UI components).
  - `FullScreen` — fills the entire canvas (good for page-level components).
  - `MarginedFrame` — adds a margin around the component.
  - Omit `Layout` entirely if you are unsure or if the project has no custom layouts.

**`<ArgType>`**
- Controls how a parameter appears in the Controls panel.
- Available `ControlType` values:
  - `ControlType.Default` — auto-detected from the parameter type (no need to specify explicitly).
  - `ControlType.Radio` — radio buttons; good for enums with 2–4 values.
  - `ControlType.Select` — dropdown; good for enums with 5+ values.
  - `ControlType.Color` — color picker; use for `string` or `Color` parameters representing a color.

**`<Story Name="...">`**
- Each `<Story>` represents one variant shown in the sidebar.
- Always include a `"Default"` story as the baseline.
- Add further stories for meaningful parameter combinations (e.g., `"Large"`, `"Disabled"`, `"With Icon"`).

**`<Arguments>` and `<Arg>`**
- Use `<Arg For="_ => _.ParamName" Value="..." />` to set initial parameter values for a story.
- For `RenderFragment` parameters, define the value in the `@code` block and reference it via `<Arg>`:
  ```razor
  <Arg For="_ => _.ChildContent" Value="_content" />

  @code {
      private RenderFragment _content = @<text>Click me</text>;
  }
  ```
- Do **not** hardcode `RenderFragment` content directly in the `<Template>` markup — this prevents runtime modification via the Controls panel.

**`<Template>`**
- Always add `@attributes="context.Args"` to the component tag to wire up the Controls panel.
- Pass only parameters that cannot be handled via `@attributes` (e.g., event callbacks, non-parameter child content) directly in the markup.

**Null-forgiving operator**
- When the component type is nullable, use `_=>_!.PropertyName` in `For` lambdas:
  ```razor
  <ArgType For="_=>_!.Color" Control="ControlType.Color" />
  ```

**`@using` directives**
- Check whether the component namespace is already imported globally (e.g., via `_Imports.razor`). Add `@using` only if needed.

## Step 6: Verify

After writing the file, briefly summarize:
- The file path created
- The stories added and which parameter variants they cover
- Any `ArgType` customizations applied
- Any assumptions made (e.g., chosen `Layout`, omitted optional parameters)
