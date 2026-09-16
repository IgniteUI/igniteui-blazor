// Legacy compatibility only — the library self-loads through its JS initializer
// (IgniteUI.Blazor.Lite.lib.module.js), so this file loads nothing.
//
// It exists for apps that still carry the <script src=".../app.bundle.js"> tag and call the
// deprecated window API while the page parses. ESM can never load synchronously, so the shims
// below only queue; api.js replays the queue and replaces them when it loads.
//
// Emitted as the classic script `app.bundle.js` — the type-only import below is erased.
import type { LegacyRegisterQueue, LegacyRemoveQueue, LegacyWindow, TemplateTag } from './api.js';

(function () {
  const w = window as unknown as LegacyWindow;
  if (w.igRegisterScript) {
    // api.js got here first and installed the real API.
    return;
  }

  const registerQueue: LegacyRegisterQueue = [];
  w.igRegisterScript = Object.assign(
    (name: string, func: Function, shouldCall?: boolean): void => {
      registerQueue.push({ name, func, shouldCall: shouldCall ?? true });
    },
    { __igQueue: registerQueue },
  );

  const removeQueue: LegacyRemoveQueue = [];
  w.igRemoveScript = Object.assign(
    (name: string): void => {
      removeQueue.push(name);
    },
    { __igQueue: removeQueue },
  );

  // `var html = window.igTemplating.html` at parse time captures a delegator that resolves the
  // real tag at call time — by then api.js has replaced window.igTemplating.
  const stubTemplating = {
    get html(): TemplateTag {
      return (strings, ...values) => {
        const current = (window as unknown as LegacyWindow).igTemplating;
        if (!current || current === stubTemplating) {
          throw new Error(
            '[Ignite UI] the templating API is not loaded yet. Import { html } from ' +
              "'./_content/IgniteUI.Blazor/api.js', or call the template only after the components render.",
          );
        }
        return current.html(strings, ...values);
      };
    },
  };
  w.igTemplating = stubTemplating;
})();
