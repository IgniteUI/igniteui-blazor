# LLM Agent Skills

Workflows for contributors to this repository. End-user skills live in [`skills/`](../../skills/).

- The rules live in [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md) and
  [`.github/CONTRIBUTING.md`](../../.github/CONTRIBUTING.md). A skill says in what order to apply
  them. When a skill and those files disagree, they win and the skill needs a fix.
- Project context for agents lives in [`.agents/context/project.md`](../context/project.md).

## Available Skills

| Skill                                                         | Use when                                                           |
| ------------------------------------------------------------- | ------------------------------------------------------------------ |
| [write-library-tests](./write-library-tests/)                 | Adding or changing unit (bUnit) or integration (Playwright) tests  |
| [maintain-trim-compatibility](./maintain-trim-compatibility/) | Touching reflection or serialization, or fixing IL2xxx errors      |
| [blazing-story-story](./blazing-story-story/)                 | Writing a Blazing Story story for a component (vendored, upstream) |
| [skill-authoring](./skill-authoring/)                         | Writing or updating a skill                                        |

Reference a skill by name: "Follow the write-library-tests skill to cover the new rating
component."

## Adding a Skill

Skills use the
[VS Code agent skills format](https://code.visualstudio.com/docs/copilot/customization/agent-skills).
Follow the [skill-authoring](./skill-authoring/SKILL.md) skill for the frontmatter rules, the
description format and the size budget. In short:

1. Create `.agents/skills/[skill-name]/SKILL.md`. The directory name is kebab-case and
   matches `name`.
2. Add frontmatter with `license`, `name` and `description`. The description has
   `WHEN TO USE:` and `WHEN NOT TO USE:` markers. Optional keys: `user-invocable`,
   `argument-hint`, `compatibility`, `disable-model-invocation`, `metadata`.
3. Link to the guidelines for rules. Do not copy them.
4. Add the skill to the table above and to the Workflow section of
   [`.agents/context/project.md`](../context/project.md).
