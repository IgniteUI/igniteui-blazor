// Client-side item template for the IgbVirtualScrollComponent page.
// Registered from the page before the virtual scroll first renders.
export function registerItemTemplate() {
  const html = window.igTemplating.html;

  window.igRegisterScript(
    'IgbVirtualScrollComponentItemTemplate',
    (ctx) => {
      return html`
        <div
          style="padding: 0.5rem 1rem; border-bottom: 1px solid var(--ig-gray-200, #eee); box-sizing: border-box;"
        >
          <strong>#${ctx.value.Id}</strong> — ${ctx.value.Text}
        </div>
      `;
    },
    false,
  );
}
