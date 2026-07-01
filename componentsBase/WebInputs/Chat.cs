using System.Threading.Tasks;

namespace IgniteUI.Blazor.Controls
{
    /// <remarks>
    /// This component is in preview and under active development.
    /// Some features are not yet implemented, and APIs may evolve in upcoming releases.
    /// </remarks>
    public partial class IgbChat
    {
        partial void OnCreatedIgbChat()
        {
            // Ensure that Options setter is called to apply the default options and disable input attachments.
            this.Options = new IgbChatOptions();
        }
        partial void OnOptionsChanging(ref IgbChatOptions? newValue)
        {
            newValue ??= new IgbChatOptions();
            newValue.DisableInputAttachments = true;
        }


        public IgbChatDraftMessage GetCurrentDraftMessage()
        {
            var iv = InvokeMethodSync("p:DraftMessage", new object[] { }, new string[] { });
            return ReturnToObject<IgbChatDraftMessage>(iv, "ChatDraftMessage");
        }

        public async Task<IgbChatDraftMessage> GetCurrentDraftMessageAsync()
        {
            var iv = await InvokeMethod("p:DraftMessage", new object[] { }, new string[] { });
            return ReturnToObject<IgbChatDraftMessage>(iv, "ChatDraftMessage");
        }
    }
}
