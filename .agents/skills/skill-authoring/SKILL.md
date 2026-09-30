---
license: MIT
name: skill-authoring
description: "Provides rules for writing or updating a SKILL.md in this repository: frontmatter validation for license, name and description, the WHEN TO USE and WHEN NOT TO USE description format, the split between public and contributor skills, and the 500-line body budget with progressive disclosure into reference files. WHEN TO USE: creating a new skill under .agents/skills/ or skills/, or editing an existing skill's frontmatter, scope, or length. WHEN NOT TO USE: writing library code or tests (use write-library-tests or maintain-trim-compatibility), writing Blazing Story stories (use blazing-story-story), changing the coding rules themselves (edit .github/copilot-instructions.md), or editing skills/AGENTS.md, which is an instruction file, not a skill."
user-invocable: true
---

# Ignite UI for Blazor — Skill Authoring

Quick-reference for writing a `SKILL.md` that agents can discover and load reliably.

## Location

- Internal, contributor-facing skills: `.agents/skills/<name>/SKILL.md`
- Public skills that ship with the product: `skills/<name>/SKILL.md`
- The folder name must match the `name` field.

Public skills teach app developers to *use* Ignite UI for Blazor. They are copied into other projects, so they must not link to or depend on anything outside `skills/` (no `src/`, `tests/`, or `.agents/` paths); link to the public docs instead. Contributor skills may link to anything in the repository.

## Frontmatter

| Field | Rules |
|---|---|
| `license` | Required. Must specify the license under which the skill is released. Default is MIT. |
| `name` | Required. Max 64 characters. Lowercase letters, numbers, and hyphens only. No XML tags. No reserved words (`anthropic`, `claude`). Public skills use the `igniteui-blazor-` prefix; internal skills use a plain kebab-case name without that prefix. |
| `description` | Required. Non-empty. Max 1,024 characters. No XML tags. |

Optional keys: `user-invocable`, `argument-hint`, `compatibility`, `disable-model-invocation`, `metadata`.

Write the description in the third person: say what the skill covers, then add both markers:

- `WHEN TO USE:` the tasks or triggers that should load the skill.
- `WHEN NOT TO USE:` nearby tasks it does not cover, naming the skill to use instead.

Agents see only `name` and `description` until they load the skill, so the description decides whether it is ever used. Wrap it in double quotes, since it contains colons.

## Token Budget

- Keep the `SKILL.md` body under 500 lines.
- If it grows past that, use progressive disclosure: keep the overview and core rules in `SKILL.md` and move detail into `references/<topic>.md` files.
- Link each reference file directly from `SKILL.md` (one level deep) and say when to read it, so agents load it only when needed.

## Vendored Skills

A skill installed from another repository (its `metadata` carries `github-repo` and `github-ref`, as in `blazing-story-story`) is kept byte-for-byte as upstream publishes it, so that it can be updated by reinstalling. Do not edit it to pass these rules; propose changes upstream instead. It is still listed in the README.

## Checklist

1. Frontmatter passes the rules above.
2. The description includes `WHEN TO USE:` and `WHEN NOT TO USE:`.
3. The body is under 500 lines, and every reference file is linked from `SKILL.md`.
4. Every relative link resolves; public skills link only inside `skills/`.
5. The skill is listed where its readers find it:
   - Internal skills: the Skills table of [.agents/skills/README.md](../README.md) and the Workflow section of [.agents/context/project.md](../../context/project.md).
   - Public skills: the Skills table of [skills/README.md](../../../skills/README.md) and the Copilot Skills section of [.github/copilot-instructions.md](../../../.github/copilot-instructions.md).
6. When a skill is renamed, search the repository for the old name (`CONTRIBUTING.md`, test READMEs, other skills) and update every reference.

## Related Skills

- [`write-library-tests`](../write-library-tests/SKILL.md) — Unit and integration tests for the library
- [`maintain-trim-compatibility`](../maintain-trim-compatibility/SKILL.md) — Trim-safe library code and IL2xxx errors
- [`blazing-story-story`](../blazing-story-story/SKILL.md) — Blazing Story stories for a component
