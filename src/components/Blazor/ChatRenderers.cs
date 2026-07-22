using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatRenderers : BaseRendererElement
    {
        public override string Type { get { return "WebChatRenderers"; } }

        public IgbChatRenderers() : base()
        {
            OnCreatedIgbChatRenderers();

        }

        partial void OnCreatedIgbChatRenderers();

        private string _attachmentRef;
        private RenderFragment<IgbChatAttachmentRenderContext> _attachment;

        partial void OnAttachmentChanging(ref RenderFragment<IgbChatAttachmentRenderContext> newValue);
        /// <summary>
        /// Custom renderer for a single chat message attachment.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatAttachmentRenderContext> Attachment
        {
            get { return this._attachment; }

            set
            {
                var oldValue = this._attachment;
                OnAttachmentChanging(ref value);
                if (oldValue != value || !IsPropDirty("Attachment"))
                {
                    MarkPropDirty("Attachment");
                    this._attachment = value;
                    this._attachmentTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._attachmentTemplateId, this._attachment, typeof(IgbChatAttachmentRenderContext));
                    this.OnRefChanged("Attachment", null, "template:::" + this._attachmentTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentRef = refName;
                        this.MarkPropDirty("AttachmentRef");
                    });
                }
            }
        }

        private string _attachmentTemplateId;
        private string _attachmentScript;

        ///<summary>Provides a means of setting Attachment in the JavaScript environment.</summary>
        [Parameter]
        public string AttachmentScript
        {
            get { return _attachmentScript; }

            set
            {
                var oldValue = this._attachmentScript;
                if (oldValue != value || !IsPropDirty("Attachment"))
                {
                    this._attachmentScript = value;
                    MarkPropDirty("Attachment");
                    this.OnRefChanged("Attachment", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentRef = refName;
                        this.MarkPropDirty("AttachmentRef");
                    });
                }
            }
        }
        private string _attachmentContentRef;
        private RenderFragment<IgbChatAttachmentRenderContext> _attachmentContent;

        partial void OnAttachmentContentChanging(ref RenderFragment<IgbChatAttachmentRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the content of an attachment.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatAttachmentRenderContext> AttachmentContent
        {
            get { return this._attachmentContent; }

            set
            {
                var oldValue = this._attachmentContent;
                OnAttachmentContentChanging(ref value);
                if (oldValue != value || !IsPropDirty("AttachmentContent"))
                {
                    MarkPropDirty("AttachmentContent");
                    this._attachmentContent = value;
                    this._attachmentContentTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._attachmentContentTemplateId, this._attachmentContent, typeof(IgbChatAttachmentRenderContext));
                    this.OnRefChanged("AttachmentContent", null, "template:::" + this._attachmentContentTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentContentRef = refName;
                        this.MarkPropDirty("AttachmentContentRef");
                    });
                }
            }
        }

        private string _attachmentContentTemplateId;
        private string _attachmentContentScript;

        ///<summary>Provides a means of setting AttachmentContent in the JavaScript environment.</summary>
        [Parameter]
        public string AttachmentContentScript
        {
            get { return _attachmentContentScript; }

            set
            {
                var oldValue = this._attachmentContentScript;
                if (oldValue != value || !IsPropDirty("AttachmentContent"))
                {
                    this._attachmentContentScript = value;
                    MarkPropDirty("AttachmentContent");
                    this.OnRefChanged("AttachmentContent", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentContentRef = refName;
                        this.MarkPropDirty("AttachmentContentRef");
                    });
                }
            }
        }
        private string _attachmentHeaderRef;
        private RenderFragment<IgbChatAttachmentRenderContext> _attachmentHeader;

        partial void OnAttachmentHeaderChanging(ref RenderFragment<IgbChatAttachmentRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the header of an attachment.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatAttachmentRenderContext> AttachmentHeader
        {
            get { return this._attachmentHeader; }

            set
            {
                var oldValue = this._attachmentHeader;
                OnAttachmentHeaderChanging(ref value);
                if (oldValue != value || !IsPropDirty("AttachmentHeader"))
                {
                    MarkPropDirty("AttachmentHeader");
                    this._attachmentHeader = value;
                    this._attachmentHeaderTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._attachmentHeaderTemplateId, this._attachmentHeader, typeof(IgbChatAttachmentRenderContext));
                    this.OnRefChanged("AttachmentHeader", null, "template:::" + this._attachmentHeaderTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentHeaderRef = refName;
                        this.MarkPropDirty("AttachmentHeaderRef");
                    });
                }
            }
        }

        private string _attachmentHeaderTemplateId;
        private string _attachmentHeaderScript;

        ///<summary>Provides a means of setting AttachmentHeader in the JavaScript environment.</summary>
        [Parameter]
        public string AttachmentHeaderScript
        {
            get { return _attachmentHeaderScript; }

            set
            {
                var oldValue = this._attachmentHeaderScript;
                if (oldValue != value || !IsPropDirty("AttachmentHeader"))
                {
                    this._attachmentHeaderScript = value;
                    MarkPropDirty("AttachmentHeader");
                    this.OnRefChanged("AttachmentHeader", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._attachmentHeaderRef = refName;
                        this.MarkPropDirty("AttachmentHeaderRef");
                    });
                }
            }
        }
        private string _inputRef;
        private RenderFragment<IgbChatInputRenderContext> _input;

        partial void OnInputChanging(ref RenderFragment<IgbChatInputRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the main chat input field.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatInputRenderContext> Input
        {
            get { return this._input; }

            set
            {
                var oldValue = this._input;
                OnInputChanging(ref value);
                if (oldValue != value || !IsPropDirty("Input"))
                {
                    MarkPropDirty("Input");
                    this._input = value;
                    this._inputTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._inputTemplateId, this._input, typeof(IgbChatInputRenderContext));
                    this.OnRefChanged("Input", null, "template:::" + this._inputTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputRef = refName;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
        }

        private string _inputTemplateId;
        private string _inputScript;

        ///<summary>Provides a means of setting Input in the JavaScript environment.</summary>
        [Parameter]
        public string InputScript
        {
            get { return _inputScript; }

            set
            {
                var oldValue = this._inputScript;
                if (oldValue != value || !IsPropDirty("Input"))
                {
                    this._inputScript = value;
                    MarkPropDirty("Input");
                    this.OnRefChanged("Input", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputRef = refName;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
        }
        private string _inputActionsRef;
        private RenderFragment<IgbChatRenderContext> _inputActions;

        partial void OnInputActionsChanging(ref RenderFragment<IgbChatRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the actions container within the input area.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatRenderContext> InputActions
        {
            get { return this._inputActions; }

            set
            {
                var oldValue = this._inputActions;
                OnInputActionsChanging(ref value);
                if (oldValue != value || !IsPropDirty("InputActions"))
                {
                    MarkPropDirty("InputActions");
                    this._inputActions = value;
                    this._inputActionsTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._inputActionsTemplateId, this._inputActions, typeof(IgbChatRenderContext));
                    this.OnRefChanged("InputActions", null, "template:::" + this._inputActionsTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsRef = refName;
                        this.MarkPropDirty("InputActionsRef");
                    });
                }
            }
        }

        private string _inputActionsTemplateId;
        private string _inputActionsScript;

        ///<summary>Provides a means of setting InputActions in the JavaScript environment.</summary>
        [Parameter]
        public string InputActionsScript
        {
            get { return _inputActionsScript; }

            set
            {
                var oldValue = this._inputActionsScript;
                if (oldValue != value || !IsPropDirty("InputActions"))
                {
                    this._inputActionsScript = value;
                    MarkPropDirty("InputActions");
                    this.OnRefChanged("InputActions", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsRef = refName;
                        this.MarkPropDirty("InputActionsRef");
                    });
                }
            }
        }
        private string _inputActionsEndRef;
        private RenderFragment<IgbChatRenderContext> _inputActionsEnd;

        partial void OnInputActionsEndChanging(ref RenderFragment<IgbChatRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the actions at the end of the input area.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatRenderContext> InputActionsEnd
        {
            get { return this._inputActionsEnd; }

            set
            {
                var oldValue = this._inputActionsEnd;
                OnInputActionsEndChanging(ref value);
                if (oldValue != value || !IsPropDirty("InputActionsEnd"))
                {
                    MarkPropDirty("InputActionsEnd");
                    this._inputActionsEnd = value;
                    this._inputActionsEndTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._inputActionsEndTemplateId, this._inputActionsEnd, typeof(IgbChatRenderContext));
                    this.OnRefChanged("InputActionsEnd", null, "template:::" + this._inputActionsEndTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsEndRef = refName;
                        this.MarkPropDirty("InputActionsEndRef");
                    });
                }
            }
        }

        private string _inputActionsEndTemplateId;
        private string _inputActionsEndScript;

        ///<summary>Provides a means of setting InputActionsEnd in the JavaScript environment.</summary>
        [Parameter]
        public string InputActionsEndScript
        {
            get { return _inputActionsEndScript; }

            set
            {
                var oldValue = this._inputActionsEndScript;
                if (oldValue != value || !IsPropDirty("InputActionsEnd"))
                {
                    this._inputActionsEndScript = value;
                    MarkPropDirty("InputActionsEnd");
                    this.OnRefChanged("InputActionsEnd", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsEndRef = refName;
                        this.MarkPropDirty("InputActionsEndRef");
                    });
                }
            }
        }
        private string _inputActionsStartRef;
        private RenderFragment<IgbChatRenderContext> _inputActionsStart;

        partial void OnInputActionsStartChanging(ref RenderFragment<IgbChatRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the actions at the start of the input area.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatRenderContext> InputActionsStart
        {
            get { return this._inputActionsStart; }

            set
            {
                var oldValue = this._inputActionsStart;
                OnInputActionsStartChanging(ref value);
                if (oldValue != value || !IsPropDirty("InputActionsStart"))
                {
                    MarkPropDirty("InputActionsStart");
                    this._inputActionsStart = value;
                    this._inputActionsStartTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._inputActionsStartTemplateId, this._inputActionsStart, typeof(IgbChatRenderContext));
                    this.OnRefChanged("InputActionsStart", null, "template:::" + this._inputActionsStartTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsStartRef = refName;
                        this.MarkPropDirty("InputActionsStartRef");
                    });
                }
            }
        }

        private string _inputActionsStartTemplateId;
        private string _inputActionsStartScript;

        ///<summary>Provides a means of setting InputActionsStart in the JavaScript environment.</summary>
        [Parameter]
        public string InputActionsStartScript
        {
            get { return _inputActionsStartScript; }

            set
            {
                var oldValue = this._inputActionsStartScript;
                if (oldValue != value || !IsPropDirty("InputActionsStart"))
                {
                    this._inputActionsStartScript = value;
                    MarkPropDirty("InputActionsStart");
                    this.OnRefChanged("InputActionsStart", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._inputActionsStartRef = refName;
                        this.MarkPropDirty("InputActionsStartRef");
                    });
                }
            }
        }
        private string _messageRef;
        private RenderFragment<IgbChatMessageRenderContext> _message;

        partial void OnMessageChanging(ref RenderFragment<IgbChatMessageRenderContext> newValue);
        /// <summary>
        /// Custom renderer for an entire chat message bubble.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatMessageRenderContext> Message
        {
            get { return this._message; }

            set
            {
                var oldValue = this._message;
                OnMessageChanging(ref value);
                if (oldValue != value || !IsPropDirty("Message"))
                {
                    MarkPropDirty("Message");
                    this._message = value;
                    this._messageTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._messageTemplateId, this._message, typeof(IgbChatMessageRenderContext));
                    this.OnRefChanged("Message", null, "template:::" + this._messageTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageRef = refName;
                        this.MarkPropDirty("MessageRef");
                    });
                }
            }
        }

        private string _messageTemplateId;
        private string _messageScript;

        ///<summary>Provides a means of setting Message in the JavaScript environment.</summary>
        [Parameter]
        public string MessageScript
        {
            get { return _messageScript; }

            set
            {
                var oldValue = this._messageScript;
                if (oldValue != value || !IsPropDirty("Message"))
                {
                    this._messageScript = value;
                    MarkPropDirty("Message");
                    this.OnRefChanged("Message", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageRef = refName;
                        this.MarkPropDirty("MessageRef");
                    });
                }
            }
        }
        private string _messageActionsRef;
        private RenderFragment<IgbChatMessageRenderContext> _messageActions;

        partial void OnMessageActionsChanging(ref RenderFragment<IgbChatMessageRenderContext> newValue);
        /// <summary>
        /// Custom renderer for message-specific actions (e.g., reply or delete buttons).
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatMessageRenderContext> MessageActions
        {
            get { return this._messageActions; }

            set
            {
                var oldValue = this._messageActions;
                OnMessageActionsChanging(ref value);
                if (oldValue != value || !IsPropDirty("MessageActions"))
                {
                    MarkPropDirty("MessageActions");
                    this._messageActions = value;
                    this._messageActionsTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._messageActionsTemplateId, this._messageActions, typeof(IgbChatMessageRenderContext));
                    this.OnRefChanged("MessageActions", null, "template:::" + this._messageActionsTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageActionsRef = refName;
                        this.MarkPropDirty("MessageActionsRef");
                    });
                }
            }
        }

        private string _messageActionsTemplateId;
        private string _messageActionsScript;

        ///<summary>Provides a means of setting MessageActions in the JavaScript environment.</summary>
        [Parameter]
        public string MessageActionsScript
        {
            get { return _messageActionsScript; }

            set
            {
                var oldValue = this._messageActionsScript;
                if (oldValue != value || !IsPropDirty("MessageActions"))
                {
                    this._messageActionsScript = value;
                    MarkPropDirty("MessageActions");
                    this.OnRefChanged("MessageActions", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageActionsRef = refName;
                        this.MarkPropDirty("MessageActionsRef");
                    });
                }
            }
        }
        private string _messageAttachmentsRef;
        private RenderFragment<IgbChatMessageRenderContext> _messageAttachments;

        partial void OnMessageAttachmentsChanging(ref RenderFragment<IgbChatMessageRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the attachments associated with a message.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatMessageRenderContext> MessageAttachments
        {
            get { return this._messageAttachments; }

            set
            {
                var oldValue = this._messageAttachments;
                OnMessageAttachmentsChanging(ref value);
                if (oldValue != value || !IsPropDirty("MessageAttachments"))
                {
                    MarkPropDirty("MessageAttachments");
                    this._messageAttachments = value;
                    this._messageAttachmentsTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._messageAttachmentsTemplateId, this._messageAttachments, typeof(IgbChatMessageRenderContext));
                    this.OnRefChanged("MessageAttachments", null, "template:::" + this._messageAttachmentsTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageAttachmentsRef = refName;
                        this.MarkPropDirty("MessageAttachmentsRef");
                    });
                }
            }
        }

        private string _messageAttachmentsTemplateId;
        private string _messageAttachmentsScript;

        ///<summary>Provides a means of setting MessageAttachments in the JavaScript environment.</summary>
        [Parameter]
        public string MessageAttachmentsScript
        {
            get { return _messageAttachmentsScript; }

            set
            {
                var oldValue = this._messageAttachmentsScript;
                if (oldValue != value || !IsPropDirty("MessageAttachments"))
                {
                    this._messageAttachmentsScript = value;
                    MarkPropDirty("MessageAttachments");
                    this.OnRefChanged("MessageAttachments", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageAttachmentsRef = refName;
                        this.MarkPropDirty("MessageAttachmentsRef");
                    });
                }
            }
        }
        private string _messageContentRef;
        private RenderFragment<IgbChatMessageRenderContext> _messageContent;

        partial void OnMessageContentChanging(ref RenderFragment<IgbChatMessageRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the main text and content of a message.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatMessageRenderContext> MessageContent
        {
            get { return this._messageContent; }

            set
            {
                var oldValue = this._messageContent;
                OnMessageContentChanging(ref value);
                if (oldValue != value || !IsPropDirty("MessageContent"))
                {
                    MarkPropDirty("MessageContent");
                    this._messageContent = value;
                    this._messageContentTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._messageContentTemplateId, this._messageContent, typeof(IgbChatMessageRenderContext));
                    this.OnRefChanged("MessageContent", null, "template:::" + this._messageContentTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageContentRef = refName;
                        this.MarkPropDirty("MessageContentRef");
                    });
                }
            }
        }

        private string _messageContentTemplateId;
        private string _messageContentScript;

        ///<summary>Provides a means of setting MessageContent in the JavaScript environment.</summary>
        [Parameter]
        public string MessageContentScript
        {
            get { return _messageContentScript; }

            set
            {
                var oldValue = this._messageContentScript;
                if (oldValue != value || !IsPropDirty("MessageContent"))
                {
                    this._messageContentScript = value;
                    MarkPropDirty("MessageContent");
                    this.OnRefChanged("MessageContent", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageContentRef = refName;
                        this.MarkPropDirty("MessageContentRef");
                    });
                }
            }
        }
        private string _messageHeaderRef;
        private RenderFragment<IgbChatMessageRenderContext> _messageHeader;

        partial void OnMessageHeaderChanging(ref RenderFragment<IgbChatMessageRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the header of a message, including sender and timestamp.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatMessageRenderContext> MessageHeader
        {
            get { return this._messageHeader; }

            set
            {
                var oldValue = this._messageHeader;
                OnMessageHeaderChanging(ref value);
                if (oldValue != value || !IsPropDirty("MessageHeader"))
                {
                    MarkPropDirty("MessageHeader");
                    this._messageHeader = value;
                    this._messageHeaderTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._messageHeaderTemplateId, this._messageHeader, typeof(IgbChatMessageRenderContext));
                    this.OnRefChanged("MessageHeader", null, "template:::" + this._messageHeaderTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageHeaderRef = refName;
                        this.MarkPropDirty("MessageHeaderRef");
                    });
                }
            }
        }

        private string _messageHeaderTemplateId;
        private string _messageHeaderScript;

        ///<summary>Provides a means of setting MessageHeader in the JavaScript environment.</summary>
        [Parameter]
        public string MessageHeaderScript
        {
            get { return _messageHeaderScript; }

            set
            {
                var oldValue = this._messageHeaderScript;
                if (oldValue != value || !IsPropDirty("MessageHeader"))
                {
                    this._messageHeaderScript = value;
                    MarkPropDirty("MessageHeader");
                    this.OnRefChanged("MessageHeader", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._messageHeaderRef = refName;
                        this.MarkPropDirty("MessageHeaderRef");
                    });
                }
            }
        }
        private string _sendButtonRef;
        private RenderFragment<IgbChatRenderContext> _sendButton;

        partial void OnSendButtonChanging(ref RenderFragment<IgbChatRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the message send button.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatRenderContext> SendButton
        {
            get { return this._sendButton; }

            set
            {
                var oldValue = this._sendButton;
                OnSendButtonChanging(ref value);
                if (oldValue != value || !IsPropDirty("SendButton"))
                {
                    MarkPropDirty("SendButton");
                    this._sendButton = value;
                    this._sendButtonTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._sendButtonTemplateId, this._sendButton, typeof(IgbChatRenderContext));
                    this.OnRefChanged("SendButton", null, "template:::" + this._sendButtonTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._sendButtonRef = refName;
                        this.MarkPropDirty("SendButtonRef");
                    });
                }
            }
        }

        private string _sendButtonTemplateId;
        private string _sendButtonScript;

        ///<summary>Provides a means of setting SendButton in the JavaScript environment.</summary>
        [Parameter]
        public string SendButtonScript
        {
            get { return _sendButtonScript; }

            set
            {
                var oldValue = this._sendButtonScript;
                if (oldValue != value || !IsPropDirty("SendButton"))
                {
                    this._sendButtonScript = value;
                    MarkPropDirty("SendButton");
                    this.OnRefChanged("SendButton", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._sendButtonRef = refName;
                        this.MarkPropDirty("SendButtonRef");
                    });
                }
            }
        }
        private string _suggestionPrefixRef;
        private RenderFragment<IgbChatRenderContext> _suggestionPrefix;

        partial void OnSuggestionPrefixChanging(ref RenderFragment<IgbChatRenderContext> newValue);
        /// <summary>
        /// Custom renderer for the prefix text shown before suggestions.
        /// </summary>
        [Parameter]
        public RenderFragment<IgbChatRenderContext> SuggestionPrefix
        {
            get { return this._suggestionPrefix; }

            set
            {
                var oldValue = this._suggestionPrefix;
                OnSuggestionPrefixChanging(ref value);
                if (oldValue != value || !IsPropDirty("SuggestionPrefix"))
                {
                    MarkPropDirty("SuggestionPrefix");
                    this._suggestionPrefix = value;
                    this._suggestionPrefixTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._suggestionPrefixTemplateId, this._suggestionPrefix, typeof(IgbChatRenderContext));
                    this.OnRefChanged("SuggestionPrefix", null, "template:::" + this._suggestionPrefixTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._suggestionPrefixRef = refName;
                        this.MarkPropDirty("SuggestionPrefixRef");
                    });
                }
            }
        }

        private string _suggestionPrefixTemplateId;
        private string _suggestionPrefixScript;

        ///<summary>Provides a means of setting SuggestionPrefix in the JavaScript environment.</summary>
        [Parameter]
        public string SuggestionPrefixScript
        {
            get { return _suggestionPrefixScript; }

            set
            {
                var oldValue = this._suggestionPrefixScript;
                if (oldValue != value || !IsPropDirty("SuggestionPrefix"))
                {
                    this._suggestionPrefixScript = value;
                    MarkPropDirty("SuggestionPrefix");
                    this.OnRefChanged("SuggestionPrefix", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._suggestionPrefixRef = refName;
                        this.MarkPropDirty("SuggestionPrefixRef");
                    });
                }
            }
        }

        partial void FindByNameChatRenderers(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatRenderers(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbChatRenderers(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatRenderers(ser);

            if (IsPropDirty("AttachmentRef"))
            { ser.AddStringProp("attachmentRef", this._attachmentRef); }
            if (IsPropDirty("AttachmentContentRef"))
            { ser.AddStringProp("attachmentContentRef", this._attachmentContentRef); }
            if (IsPropDirty("AttachmentHeaderRef"))
            { ser.AddStringProp("attachmentHeaderRef", this._attachmentHeaderRef); }
            if (IsPropDirty("InputRef"))
            { ser.AddStringProp("inputRef", this._inputRef); }
            if (IsPropDirty("InputActionsRef"))
            { ser.AddStringProp("inputActionsRef", this._inputActionsRef); }
            if (IsPropDirty("InputActionsEndRef"))
            { ser.AddStringProp("inputActionsEndRef", this._inputActionsEndRef); }
            if (IsPropDirty("InputActionsStartRef"))
            { ser.AddStringProp("inputActionsStartRef", this._inputActionsStartRef); }
            if (IsPropDirty("MessageRef"))
            { ser.AddStringProp("messageRef", this._messageRef); }
            if (IsPropDirty("MessageActionsRef"))
            { ser.AddStringProp("messageActionsRef", this._messageActionsRef); }
            if (IsPropDirty("MessageAttachmentsRef"))
            { ser.AddStringProp("messageAttachmentsRef", this._messageAttachmentsRef); }
            if (IsPropDirty("MessageContentRef"))
            { ser.AddStringProp("messageContentRef", this._messageContentRef); }
            if (IsPropDirty("MessageHeaderRef"))
            { ser.AddStringProp("messageHeaderRef", this._messageHeaderRef); }
            if (IsPropDirty("SendButtonRef"))
            { ser.AddStringProp("sendButtonRef", this._sendButtonRef); }
            if (IsPropDirty("SuggestionPrefixRef"))
            { ser.AddStringProp("suggestionPrefixRef", this._suggestionPrefixRef); }

        }

    }
}
