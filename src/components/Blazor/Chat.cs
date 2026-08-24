using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A chat UI component for displaying messages, attachments, and input interaction.
    /// </summary>
    public partial class IgbChat : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChat"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbChatModule.IsLoadRequested(IgBlazor))
            {
                IgbChatModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Queued; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="IgbChat"/>.
        /// </summary>
        public IgbChat() : base()
        {
            // Ensure that Options setter is called to apply the default options and disable input attachments.
            this.Options = new IgbChatOptions();
        }

        private IgbChatMessage[]? _messages;

        /// <summary>
        /// The list of chat messages currently displayed.
        /// Use this property to set or update the message history.
        /// </summary>
        [Parameter]
        public IgbChatMessage[]? Messages
        {
            get { return this._messages; }
            set
            {
                if (this._messages != value || !IsPropDirty("Messages"))
                {
                    MarkPropDirty("Messages");
                }
                this._messages = value;

            }
        }
        private IgbChatDraftMessage? _draftMessage;

        /// <summary>
        /// The chat message currently being composed but not yet sent.
        /// Includes the draft text and any attachments.
        /// </summary>
        [Parameter]
        public IgbChatDraftMessage? DraftMessage
        {
            get { return this._draftMessage; }
            set
            {
                MarkPropDirty("DraftMessage");
                if (this._draftMessage != null)
                {
                    this.DetachChild(this._draftMessage);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._draftMessage = value;
            }

        }
        private IgbChatOptions? _options;

        /// <summary>
        /// Controls the chat behavior and appearance through a configuration object.
        /// Use this to toggle UI options, provide suggestions, templates, etc.
        /// </summary>
        [Parameter]
        public IgbChatOptions? Options
        {
            get { return this._options; }
            set
            {
                // Never store a null options object, and input attachments are not supported yet.
                value ??= new IgbChatOptions();
                value.DisableInputAttachments = true;
                MarkPropDirty("Options");
                if (this._options != null)
                {
                    this.DetachChild(this._options);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._options = value;
            }

        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Scrolls the view to a specific message by id.
        /// </summary>
        public async Task ScrollToMessageAsync(String messageId)
        {
            await InvokeMethod("scrollToMessage", new object?[] { StringToString(messageId) }, new string[] { "String" });
        }

        /// <summary>
        /// Scrolls the view to a specific message by id.
        /// </summary>
        public void ScrollToMessage(String messageId)
        {
            InvokeMethodSync("scrollToMessage", new object?[] { StringToString(messageId) }, new string[] { "String" });
        }

        private string? _messageCreatedRef = null;
        private string? _messageCreatedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="MessageCreated"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? MessageCreatedScript
        {

            set
            {
                if (value != this._messageCreatedScript)
                {
                    this._messageCreatedScript = value;
                    this.OnRefChanged("MessageCreated", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._messageCreatedRef = refName;
                        this.MarkPropDirty("MessageCreatedRef");
                    });
                }
            }
            get
            {
                return this._messageCreatedScript;
            }
        }

        private EventCallback<IgbChatMessageEventArgs>? _messageCreated = null;

        /// <summary>
        /// Dispatched when a new chat message is created (sent).
        /// </summary>
        [Parameter]
        public EventCallback<IgbChatMessageEventArgs> MessageCreated
        {
            get
            {
                return this._messageCreated != null ? this._messageCreated.Value : EventCallback<IgbChatMessageEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_messageCreated))
                    {
                        _messageCreated = value;
                        this.SetHandler<IgbChatMessageEventArgs>(this.Name, "MessageCreated", value);
                        this.OnRefChanged("MessageCreated", null, "event:::MessageCreated", true, false, (refName, oldValue, newValue) =>
                        {
                            this._messageCreatedRef = refName;
                            this.MarkPropDirty("MessageCreatedRef");
                        });
                    }
                }
                else
                {
                    _messageCreated = null;
                    this.SetHandler<IgbChatMessageEventArgs>(this.Name, "MessageCreated", null);
                    this.OnRefChanged("MessageCreated", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._messageCreatedRef = null;
                        this.MarkPropDirty("MessageCreatedRef");
                    });
                }
            }
        }

        private string? _messageReactRef = null;
        private string? _messageReactScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="MessageReact"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? MessageReactScript
        {

            set
            {
                if (value != this._messageReactScript)
                {
                    this._messageReactScript = value;
                    this.OnRefChanged("MessageReact", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._messageReactRef = refName;
                        this.MarkPropDirty("MessageReactRef");
                    });
                }
            }
            get
            {
                return this._messageReactScript;
            }
        }

        private EventCallback<IgbChatMessageReactionEventArgs>? _messageReact = null;

        /// <summary>
        /// Dispatched when a message is reacted to.
        /// </summary>
        [Parameter]
        public EventCallback<IgbChatMessageReactionEventArgs> MessageReact
        {
            get
            {
                return this._messageReact != null ? this._messageReact.Value : EventCallback<IgbChatMessageReactionEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_messageReact))
                    {
                        _messageReact = value;
                        this.SetHandler<IgbChatMessageReactionEventArgs>(this.Name, "MessageReact", value);
                        this.OnRefChanged("MessageReact", null, "event:::MessageReact", true, false, (refName, oldValue, newValue) =>
                        {
                            this._messageReactRef = refName;
                            this.MarkPropDirty("MessageReactRef");
                        });
                    }
                }
                else
                {
                    _messageReact = null;
                    this.SetHandler<IgbChatMessageReactionEventArgs>(this.Name, "MessageReact", null);
                    this.OnRefChanged("MessageReact", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._messageReactRef = null;
                        this.MarkPropDirty("MessageReactRef");
                    });
                }
            }
        }

        private string? _attachmentClickRef = null;
        private string? _attachmentClickScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="AttachmentClick"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? AttachmentClickScript
        {

            set
            {
                if (value != this._attachmentClickScript)
                {
                    this._attachmentClickScript = value;
                    this.OnRefChanged("AttachmentClick", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._attachmentClickRef = refName;
                        this.MarkPropDirty("AttachmentClickRef");
                    });
                }
            }
            get
            {
                return this._attachmentClickScript;
            }
        }

        private EventCallback<IgbChatMessageAttachmentEventArgs>? _attachmentClick = null;

        /// <summary>
        /// Dispatched when a chat message attachment is clicked.
        /// </summary>
        [Parameter]
        public EventCallback<IgbChatMessageAttachmentEventArgs> AttachmentClick
        {
            get
            {
                return this._attachmentClick != null ? this._attachmentClick.Value : EventCallback<IgbChatMessageAttachmentEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_attachmentClick))
                    {
                        _attachmentClick = value;
                        this.SetHandler<IgbChatMessageAttachmentEventArgs>(this.Name, "AttachmentClick", value);
                        this.OnRefChanged("AttachmentClick", null, "event:::AttachmentClick", true, false, (refName, oldValue, newValue) =>
                        {
                            this._attachmentClickRef = refName;
                            this.MarkPropDirty("AttachmentClickRef");
                        });
                    }
                }
                else
                {
                    _attachmentClick = null;
                    this.SetHandler<IgbChatMessageAttachmentEventArgs>(this.Name, "AttachmentClick", null);
                    this.OnRefChanged("AttachmentClick", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._attachmentClickRef = null;
                        this.MarkPropDirty("AttachmentClickRef");
                    });
                }
            }
        }

        private string? _typingChangeRef = null;
        private string? _typingChangeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TypingChange"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? TypingChangeScript
        {

            set
            {
                if (value != this._typingChangeScript)
                {
                    this._typingChangeScript = value;
                    this.OnRefChanged("TypingChange", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._typingChangeRef = refName;
                        this.MarkPropDirty("TypingChangeRef");
                    });
                }
            }
            get
            {
                return this._typingChangeScript;
            }
        }

        private EventCallback<IgbComponentBoolValueChangedEventArgs>? _typingChange = null;

        /// <summary>
        /// Dispatched when the typing status changes (e.g. user starts or stops typing).
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentBoolValueChangedEventArgs> TypingChange
        {
            get
            {
                return this._typingChange != null ? this._typingChange.Value : EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_typingChange))
                    {
                        _typingChange = value;
                        this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "TypingChange", value);
                        this.OnRefChanged("TypingChange", null, "event:::TypingChange", true, false, (refName, oldValue, newValue) =>
                        {
                            this._typingChangeRef = refName;
                            this.MarkPropDirty("TypingChangeRef");
                        });
                    }
                }
                else
                {
                    _typingChange = null;
                    this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "TypingChange", null);
                    this.OnRefChanged("TypingChange", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._typingChangeRef = null;
                        this.MarkPropDirty("TypingChangeRef");
                    });
                }
            }
        }

        private string? _inputFocusRef = null;
        private string? _inputFocusScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="InputFocus"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? InputFocusScript
        {

            set
            {
                if (value != this._inputFocusScript)
                {
                    this._inputFocusScript = value;
                    this.OnRefChanged("InputFocus", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputFocusRef = refName;
                        this.MarkPropDirty("InputFocusRef");
                    });
                }
            }
            get
            {
                return this._inputFocusScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _inputFocus = null;

        /// <summary>
        /// Dispatched when the chat input field gains focus.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> InputFocus
        {
            get
            {
                return this._inputFocus != null ? this._inputFocus.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_inputFocus))
                    {
                        _inputFocus = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "InputFocus", value);
                        this.OnRefChanged("InputFocus", null, "event:::InputFocus", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputFocusRef = refName;
                            this.MarkPropDirty("InputFocusRef");
                        });
                    }
                }
                else
                {
                    _inputFocus = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "InputFocus", null);
                    this.OnRefChanged("InputFocus", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputFocusRef = null;
                        this.MarkPropDirty("InputFocusRef");
                    });
                }
            }
        }

        private string? _inputBlurRef = null;
        private string? _inputBlurScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="InputBlur"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? InputBlurScript
        {

            set
            {
                if (value != this._inputBlurScript)
                {
                    this._inputBlurScript = value;
                    this.OnRefChanged("InputBlur", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputBlurRef = refName;
                        this.MarkPropDirty("InputBlurRef");
                    });
                }
            }
            get
            {
                return this._inputBlurScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _inputBlur = null;

        /// <summary>
        /// Dispatched when the chat input field loses focus.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> InputBlur
        {
            get
            {
                return this._inputBlur != null ? this._inputBlur.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_inputBlur))
                    {
                        _inputBlur = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "InputBlur", value);
                        this.OnRefChanged("InputBlur", null, "event:::InputBlur", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputBlurRef = refName;
                            this.MarkPropDirty("InputBlurRef");
                        });
                    }
                }
                else
                {
                    _inputBlur = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "InputBlur", null);
                    this.OnRefChanged("InputBlur", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputBlurRef = null;
                        this.MarkPropDirty("InputBlurRef");
                    });
                }
            }
        }

        private string? _inputChangeRef = null;
        private string? _inputChangeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="InputChange"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? InputChangeScript
        {

            set
            {
                if (value != this._inputChangeScript)
                {
                    this._inputChangeScript = value;
                    this.OnRefChanged("InputChange", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputChangeRef = refName;
                        this.MarkPropDirty("InputChangeRef");
                    });
                }
            }
            get
            {
                return this._inputChangeScript;
            }
        }

        private EventCallback<IgbComponentValueChangedEventArgs>? _inputChange = null;

        /// <summary>
        /// Dispatched when the content of the chat input changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> InputChange
        {
            get
            {
                return this._inputChange != null ? this._inputChange.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_inputChange))
                    {
                        _inputChange = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputChange", value);
                        this.OnRefChanged("InputChange", null, "event:::InputChange", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputChangeRef = refName;
                            this.MarkPropDirty("InputChangeRef");
                        });
                    }
                }
                else
                {
                    _inputChange = null;
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputChange", null);
                    this.OnRefChanged("InputChange", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputChangeRef = null;
                        this.MarkPropDirty("InputChangeRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Messages"))
            { ser.AddSerializableArrayProp("messages", this._messages); }
            if (IsPropDirty("DraftMessage"))
            { ser.AddSerializableProp("draftMessage", this._draftMessage); }
            if (IsPropDirty("Options"))
            { ser.AddSerializableProp("options", this._options); }
            if (IsPropDirty("MessageCreatedRef"))
            { ser.AddStringProp("messageCreatedRef", this._messageCreatedRef); }
            if (IsPropDirty("MessageReactRef"))
            { ser.AddStringProp("messageReactRef", this._messageReactRef); }
            if (IsPropDirty("AttachmentClickRef"))
            { ser.AddStringProp("attachmentClickRef", this._attachmentClickRef); }
            if (IsPropDirty("TypingChangeRef"))
            { ser.AddStringProp("typingChangeRef", this._typingChangeRef); }
            if (IsPropDirty("InputFocusRef"))
            { ser.AddStringProp("inputFocusRef", this._inputFocusRef); }
            if (IsPropDirty("InputBlurRef"))
            { ser.AddStringProp("inputBlurRef", this._inputBlurRef); }
            if (IsPropDirty("InputChangeRef"))
            { ser.AddStringProp("inputChangeRef", this._inputChangeRef); }

        }

    }
}
