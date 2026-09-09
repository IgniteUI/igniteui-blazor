// collocated Chat.stories.razor.js with a component doesn't auto-load
import { registerScript, html } from '../_content/IgniteUI.Blazor/api.js';

/*export*/ function registerChatTemplates(params) {
  registerScript('ChatStoryMessageHeaderScript', (ctx) => {
    if (ctx?.message?.sender === 'user') {
      return html``;
    }

    return html`
      <div style="display: flex; align-items: center; gap: 8px;">
        <igc-avatar
          shape="circle"
          alt="support agent avatar"
          src="https://www.infragistics.com/angular-demos/assets/images/men/1.jpg"
        ></igc-avatar>
        <span style="font-weight: 600;">Support Agent</span>
      </div>
    `;
  });

  registerScript('ChatStoryMessageActionsScript', (ctx) => {
    if (ctx?.message?.sender === 'user' || !ctx?.message?.text) {
      return html``;
    }

    return html`
      <igc-icon-button
        variant="flat"
        aria-label="Mark as helpful"
        @click=${() => {
          /* invoke mark as helpful */
        }}
      >
        👍
      </igc-icon-button>
    `;
  });

  registerScript('ChatStoryInputActionsStartScript', () => {
    return html`<igc-icon-button variant="flat" aria-label="Voice input">🎙️</igc-icon-button>`;
  });

  registerScript('ChatStorySuggestionPrefixScript', () => {
    return html`<span>💬</span>`;
  });
}
registerChatTemplates();
