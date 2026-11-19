// Client-side item templates for the VirtualScroll stories, loaded through stories.js.
import { registerScript, html } from '../_content/IgniteUI.Blazor/api.js';

function registerVirtualScrollTemplates() {
  registerScript('VirtualScrollStoryItemTemplate', (ctx) => {
    return html`
      <div style="padding: 0.5rem 1rem; border-bottom: 1px solid var(--ig-gray-200, #eee); box-sizing: border-box;">
        <strong>#${ctx.value.Id}</strong> — ${ctx.value.Text}
      </div>
    `;
  });

  registerScript('VirtualScrollStoryCardTemplate', (ctx) => {
    return html`
      <div
        style="width: 140px; height: 100%; display: flex; align-items: center; justify-content: center; border-right: 1px solid var(--ig-gray-200, #eee); box-sizing: border-box;"
      >
        Card ${ctx.value.Id}
      </div>
    `;
  });
}
registerVirtualScrollTemplates();
