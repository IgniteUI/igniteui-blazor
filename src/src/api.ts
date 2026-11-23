/**
 * Public JS API of Ignite UI for Blazor, served at `_content/IgniteUI.Blazor/api.js`.
 *
 * The interop bundle imports this module too, so the registry below is a single instance.
 */
import {
  registerIcon as igcRegisterIcon,
  registerIconFromText as igcRegisterIconFromText,
  setIconRef as igcSetIconRef,
} from 'igniteui-webcomponents/components/icon/icon.registry.js';
import { html as litHtml } from 'lit-html';

// Explicit tag type keeps api.d.ts standalone (no lit-html type dependency for consumers).
export type TemplateTag = (strings: TemplateStringsArray, ...values: unknown[]) => unknown;

/** lit-html's template tag — use to build client html templates with */
export const html: TemplateTag = litHtml;

// The icon types and signatures below mirror igniteui-webcomponents', declared here to keep api.d.ts standalone.
// The functions are its own: the icon registry is global (a single instance on globalThis, even across bundles).

/** Identifies a registered icon by its name and the collection it belongs to. */
export interface IconMeta {
  /** The name identifier of the icon in the collection. */
  name: string;
  /** The name of the collection the icon is registered in. */
  collection: string;
}

/** Options for registering an SVG icon. */
export interface RegisterIconOptions {
  /** The collection to register the icon in. Defaults to `'default'`. */
  collection?: string;
  /**
   * Whether to strip SVG meta elements (`<title>` and `<desc>`) from the icon before storing it.
   * This prevents the native browser tooltip on hover; the title text stays available as the
   * `aria-label` of the host icon element. Defaults to `false`.
   */
  stripMeta?: boolean;
}

export interface RegisterIcon {
  /**
   * Registers an icon by fetching it from a URL.
   * @param collection - The collection to register the icon in (default: `'default'`)
   */
  (name: string, url: string, collection?: string): Promise<void>;
  /**
   * Registers an icon by fetching it from a URL.
   * @param options - Registration options: target collection and/or `stripMeta`
   */
  (name: string, url: string, options?: RegisterIconOptions): Promise<void>;
}

export interface RegisterIconFromText {
  /**
   * Registers an icon from SVG text content.
   * @param collection - The collection to register the icon in (default: `'default'`)
   */
  (name: string, iconText: string, collection?: string): void;
  /**
   * Registers an icon from SVG text content.
   * @param options - Registration options: target collection and/or `stripMeta`
   */
  (name: string, iconText: string, options?: RegisterIconOptions): void;
}

/** Registers an icon by fetching it from a URL, for every icon on the page. */
export const registerIcon: RegisterIcon = igcRegisterIcon;

/** Registers an icon from SVG text content, for every icon on the page. */
export const registerIconFromText: RegisterIconFromText = igcRegisterIconFromText;

/** Sets an icon reference (an alias) that points to another icon, for every icon on the page. */
export const setIconRef: (name: string, collection: string, icon: IconMeta) => void = igcSetIconRef;

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
