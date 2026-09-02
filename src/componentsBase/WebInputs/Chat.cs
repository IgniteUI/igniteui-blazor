namespace IgniteUI.Blazor.Controls
{
    /// <remarks>
    /// This component is in preview and under active development.
    /// Some features are not yet implemented, and APIs may evolve in upcoming releases.
    /// </remarks>
    public partial class IgbChat
    {
        public IgbChatDraftMessage GetCurrentDraftMessage()
        {
            var iv = InvokeMethodSync("p:DraftMessage", new object?[] { }, new string[] { });
            var result = ReturnToObject<IgbChatDraftMessage>(iv, "ChatDraftMessage");
            return result ?? new IgbChatDraftMessage();
        }

        public async Task<IgbChatDraftMessage> GetCurrentDraftMessageAsync()
        {
            var iv = await InvokeMethod("p:DraftMessage", new object?[] { }, new string[] { });
            var result = ReturnToObject<IgbChatDraftMessage>(iv, "ChatDraftMessage");
            return result ?? new IgbChatDraftMessage();
        }
    }
}
