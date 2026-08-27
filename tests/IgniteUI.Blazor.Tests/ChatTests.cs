using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ChatTests : ComponentWithContractTestBase<IgbChat>
{
    protected override ComponentContract<IgbChat> InteropContract { get; } = new ComponentContract<IgbChat>()
        .Method(c => c.ScrollToMessageAsync("message-42"), c => c.ScrollToMessage("message-42"), "scrollToMessage",
            args: ["message-42"], types: ["String"])
        .Getter(c => c.GetCurrentDraftMessageAsync(), c => c.GetCurrentDraftMessage(), "DraftMessage",
            arrange: _ => { },
            returns: FromRender.Of((interop, cut) => InteropReturn.Object("", """{"text": "wip draft"}""")),
            assert: (cut, result) => Assert.Equal("wip draft", result!.Text))
        .Event(c => c.TypingChange,
            argsJson: """{"detail": true}""",
            assert: args => Assert.True(args.Detail))
        .Event(c => c.InputChange,
            argsJson: """{"detail": "draft text"}""",
            assert: args => Assert.Equal("draft text", args.Detail))
        .Event(c => c.InputFocus)
        .Event(c => c.InputBlur)
        .Event(c => c.MessageCreated,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"id": "m-1", "text": "hello", "sender": "user-1"}}}""",
            assert: args =>
            {
                Assert.Equal("m-1", args.Detail!.Id);
                Assert.Equal("hello", args.Detail.Text);
                Assert.Equal("user-1", args.Detail.Sender);
            })
        .Event(c => c.AttachmentClick,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"id": "a-1", "name": "photo.png", "url": "https://host/photo.png"}}}""",
            assert: args =>
            {
                Assert.Equal("a-1", args.Detail!.Id);
                Assert.Equal("photo.png", args.Detail.Name);
                Assert.Equal("https://host/photo.png", args.Detail.Url);
            })
        .Event(c => c.MessageReact,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"message": {"retType": "object", "type": "", "value": {"id": "m-1", "text": "hello"}}, "reaction": "like"}}}""",
            assert: args =>
            {
                // The reaction's message currently decoded by value
                // (it is NOT restored by reference to an instance in Messages on the current stack).
                Assert.Equal("like", args.Detail!.Reaction);
                Assert.Equal("m-1", args.Detail!.Message!.Id);
                Assert.Equal("hello", args.Detail.Message.Text);
            })
        .Prop(c => c.Options,
            new IgbChatOptions
            {
                HeaderText = "Support",
                SuggestionsPosition = ChatSuggestionsPosition.BelowMessages,
                DisableAutoScroll = true,
                Suggestions = ["How do I install?", "Show me theming"],
            },
            wire: new JsonSubset("""{"headerText": "Support", "suggestionsPosition": "below-messages", "disableAutoScroll": true, "suggestions": ["How do I install?", "Show me theming"]}"""))
        .Prop(c => c.Messages,
            [
                new IgbChatMessage { Id = "m-1", Text = "hello", Sender = "user-1" },
                new IgbChatMessage { Id = "m-2", Text = "hi there", Sender = "agent" },
            ],
            wire: new JsonSubset("""[{"id": "m-1", "text": "hello", "sender": "user-1"}, {"id": "m-2", "text": "hi there", "sender": "agent"}]"""))
        .Prop(c => c.DraftMessage,
            new IgbChatDraftMessage
            {
                Text = "draft text 2",
                Attachments = [new IgbChatMessageAttachment { Id = "a-2", Url = "https://host/file.pdf", AttachmentType = "application/pdf", Thumbnail = "thumb.png" }],
            },
            wire: new JsonSubset("""{"text": "draft text 2", "attachments": [{"id": "a-2", "url": "https://host/file.pdf", "attachmentType": "application/pdf", "thumbnail": "thumb.png"}]}"""));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void Chat_RendersCorrectElement()
    {
        var cut = Render<IgbChat>();
        Assert.NotNull(cut.Find("igc-chat"));
    }

    [Fact]
    public void Chat_DefaultOptions_AreInitializedAndAttachmentsDisabled()
    {
        var chat = new IgbChat();

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options.DisableInputAttachments);
    }

    [Fact]
    public void Chat_Options_SetToNull_ReplacedWithDefaultAndAttachmentsDisabled()
    {
        var chat = new IgbChat();

        chat.Options = null;

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options.DisableInputAttachments);
    }

    [Fact]
    public void Chat_Options_Assigned_AlwaysDisableInputAttachments()
    {
        var chat = new IgbChat();
        var options = new IgbChatOptions { DisableInputAttachments = false };

        chat.Options = options;

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options.DisableInputAttachments);
    }
}
