/**
 * Public JS API of Ignite UI for Blazor, served at `_content/IgniteUI.Blazor/api.js`.
 *
 * The interop bundle imports this module too, so the registry below is a single instance.
 */
import { html as litHtml } from 'lit-html';

// Explicit tag type keeps api.d.ts standalone (no lit-html type dependency for consumers).
export type TemplateTag = (strings: TemplateStringsArray, ...values: unknown[]) => unknown;

/** lit-html's template tag — use to build client html templates with */
export const html: TemplateTag = litHtml;

/** Registry record; only the bundle uses it. @internal */
export interface RegisteredScript {
  /** `true` = the function is a factory invoked for the value; `false` = the function *is* the value. */
  readonly shouldCall: boolean;
  readonly func: Function;
}

const scripts = new Map<string, RegisteredScript>();

/**
 * Registers a script usable from a `*Script` component parameter.
 *
 * The function is the script. With `shouldCall`, the function is instead called
 * when the component parameter is resolved and its return value is the script.
 */
export function registerScript(name: string, func: Function, shouldCall = false): void {
  scripts.set(name, { shouldCall, func });
}

/** Removes a previously registered script. */
export function removeScript(name: string): void {
  scripts.delete(name);
}

/** Gets a previously registered script, if any. @internal */
export function getRegisteredScript(name: string): RegisteredScript | undefined {
  return scripts.get(name);
}
