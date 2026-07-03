using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ChatTests : BlazorComponentTestBase
{
    [Fact]
    public void Chat_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbChat>();
        Assert.NotNull(cut.Find("igc-chat"));
    }

    [Fact]
    public void Chat_DefaultOptions_AreInitializedAndAttachmentsDisabled()
    {
        var chat = new IgbChat();

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options!.DisableInputAttachments);
    }

    [Fact]
    public void Chat_Options_SetToNull_ReplacedWithDefaultAndAttachmentsDisabled()
    {
        var chat = new IgbChat();

        chat.Options = null;

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options!.DisableInputAttachments);
    }

    [Fact]
    public void Chat_Options_Assigned_AlwaysDisableInputAttachments()
    {
        var chat = new IgbChat();
        var options = new IgbChatOptions { DisableInputAttachments = false };

        chat.Options = options;

        Assert.NotNull(chat.Options);
        Assert.True(chat.Options!.DisableInputAttachments);
    }

    [Fact]
    public void Chat_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbChat>(parameters =>
            parameters.AddChildContent("Chat Header Content"));

        Assert.Contains("Chat Header Content", cut.Find("igc-chat").InnerHtml);
    }
}
