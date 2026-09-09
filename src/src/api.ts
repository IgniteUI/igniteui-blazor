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

/** Registry record; only the bundle and the legacy queue plumbing use it. @internal */
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

const warned = new Set<string>();

function deprecated(api: string, replacement: string): void {
  if (warned.has(api)) {
    return;
  }
  warned.add(api);
  console.warn(`${api} is deprecated — import { ${replacement} } from './_content/IgniteUI.Blazor/api.js' instead.`);
}

function registerScriptShim(name: string, fn: Function, shouldCall: boolean = true): void {
  deprecated('igRegisterScript', 'registerScript');
  registerScript(name, fn, shouldCall);
}

function removeScriptShim(name: string): void {
  deprecated('igRemoveScript', 'removeScript');
  removeScript(name);
}

const templatingShim = {
  get html() {
    deprecated('igTemplating.html', 'html');
    return html;
  },
};

/** Type for the `app.bundle.js` stub buffered calls. @internal */
export type LegacyRegisterQueue = (RegisteredScript & { name: string })[];
/** @internal */
export type LegacyRemoveQueue = string[];
/** Deprecated global API, including the stub's queue internals. @internal */
export type LegacyWindow = {
  igRegisterScript?: Window['igRegisterScript'] & { __igQueue?: LegacyRegisterQueue };
  igRemoveScript?: Window['igRemoveScript'] & { __igQueue?: LegacyRemoveQueue };
  igTemplating?: Window['igTemplating'];
};

const w = window as unknown as LegacyWindow;
const stubQueue = w.igRegisterScript?.__igQueue;

if (stubQueue) {
  // Replay through the shims so queued legacy calls get the deprecation notice too.
  for (const { name, func, shouldCall } of stubQueue) {
    registerScriptShim(name, func, shouldCall);
  }
  for (const name of w.igRemoveScript?.__igQueue ?? []) {
    removeScriptShim(name);
  }
  // The stub's shims only queue — they have to be replaced, not preserved.
  w.igRegisterScript = registerScriptShim;
  w.igRemoveScript = removeScriptShim;
  w.igTemplating = templatingShim;
} else {
  w.igRegisterScript ??= registerScriptShim;
  w.igRemoveScript ??= removeScriptShim;
  w.igTemplating ??= templatingShim;
}

declare global {
  interface Window {
    /** @deprecated use `import { registerScript } from './_content/IgniteUI.Blazor/api.js'` */
    igRegisterScript(name: string, fn: Function, shouldCall?: boolean): void;
    /** @deprecated use `import { removeScript } from './_content/IgniteUI.Blazor/api.js'` */
    igRemoveScript(name: string): void;
    /** @deprecated use `import { html } from './_content/IgniteUI.Blazor/api.js'` */
    igTemplating: { readonly html: TemplateTag };
  }
}
