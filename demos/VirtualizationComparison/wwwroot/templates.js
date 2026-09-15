// Client-side item templates for IgbVirtualScroll. The inline styles must match the
// Razor-side row markup in Home.razor so both lists render identical items.
export function registerComparisonTemplates() {
  const html = window.igTemplating.html;

  window.igRegisterScript(
    'ComparisonItemTemplate',
    (ctx) => {
      return html`
        <div
          style="padding: 0.5rem 1rem; border-bottom: 1px solid #e4e4e7; box-sizing: border-box; line-height: 1.5; font-size: 0.9rem;"
        >
          <span style="display: inline-block; min-width: 4.5rem; font-weight: 600; color: #6d28d9;"
            >#${ctx.value.Id}</span
          >
          ${ctx.value.Text}
        </div>
      `;
    },
    false,
  );

  window.igRegisterScript(
    'ComparisonCardTemplate',
    (ctx) => {
      return html`
        <div
          style="width: 140px; height: 100%; display: flex; align-items: center; justify-content: center; border-right: 1px solid #e4e4e7; box-sizing: border-box; font-size: 0.9rem;"
        >
          Card ${ctx.value.Id}
        </div>
      `;
    },
    false,
  );
}
