/**
 * Public JS API of Ignite UI for Blazor, served at `_content/IgniteUI.Blazor/api.js`.
 *
 * The interop bundle imports this module too, so the registry below is a single instance.
 */
import { configureTheme as igcConfigureTheme } from 'igniteui-webcomponents/theming/config.js';
import { html as litHtml } from 'lit-html';

// Explicit tag type keeps api.d.ts standalone (no lit-html type dependency for consumers).
export type TemplateTag = (strings: TemplateStringsArray, ...values: unknown[]) => unknown;

/** lit-html's template tag — use to build client html templates with */
export const html: TemplateTag = litHtml;

// The theme types mirror igniteui-webcomponents', declared here to keep api.d.ts standalone.

/** Design system whose styles the components use. */
export type Theme = 'material' | 'bootstrap' | 'indigo' | 'fluent';

/** Color scheme of a theme. */
export type ThemeVariant = 'light' | 'dark';

/**
 * Sets the theme and variant whose structural styles every component on the page uses; the
 * `igniteui-webcomponents` function itself. It does not swap the theme stylesheet, which holds the palette.
 * Components inside a theme provider follow the provider instead.
 * @param variant - Defaults to `'light'`.
 */
export const configureTheme: (theme: Theme, variant?: ThemeVariant) => void = igcConfigureTheme;

/** Registry record; only the bundle uses it. @internal */
export interface RegisteredScript {
  /** `true` = the function is a factory invoked for the value; `false` = the function *is* the value. */
  readonly shouldCall: boolean;
  readonly func: Function;
}

const scripts = new Map<string, RegisteredScript>();
/** Dispatches `registered` (detail: the script name) after each `registerScript` call. Underscore-prefixed like every export the bundle uses but the typings hide. @internal */
export const _scriptRegistryEvents = new EventTarget();

/**
 * Registers a script usable from a `*Script` component parameter.
 *
 * The function is the script. With `shouldCall`, the function is instead called
 * when the component parameter is resolved and its return value is the script.
 */
export function registerScript(name: string, func: Function, shouldCall = false): void {
  scripts.set(name, { shouldCall, func });
  _scriptRegistryEvents.dispatchEvent(new CustomEvent('registered', { detail: name }));
}

/** Removes a previously registered script. */
export function removeScript(name: string): void {
  scripts.delete(name);
}

/** Gets a previously registered script, if any. @internal */
export function _getRegisteredScript(name: string): RegisteredScript | undefined {
  return scripts.get(name);
}
