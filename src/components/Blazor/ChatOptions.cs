
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatOptions : BaseRendererElement
    {
        public override string Type { get { return "WebChatOptions"; } }

        public IgbChatOptions() : base()
        {
            OnCreatedIgbChatOptions();

        }

        partial void OnCreatedIgbChatOptions();

        private string _currentUserId;

        partial void OnCurrentUserIdChanging(ref string newValue);
        /// <summary>
        /// The ID of the current user. Used to differentiate between incoming and outgoing messages.
        /// </summary>
        [Parameter]
        public string CurrentUserId
        {
            get { return this._currentUserId; }
            set
            {
                if (this._currentUserId != value || !IsPropDirty("CurrentUserId"))
                {
                    MarkPropDirty("CurrentUserId");
                }
                this._currentUserId = value;

            }
        }
        private bool _disableAutoScroll = false;

        partial void OnDisableAutoScrollChanging(ref bool newValue);
        /// <summary>
        /// If `true`, prevents the chat from automatically scrolling to the latest message.
        /// </summary>
        [Parameter]
        public bool DisableAutoScroll
        {
            get { return this._disableAutoScroll; }
            set
            {
                if (this._disableAutoScroll != value || !IsPropDirty("DisableAutoScroll"))
                {
                    MarkPropDirty("DisableAutoScroll");
                }
                this._disableAutoScroll = value;

            }
        }
        private bool _disableInputAttachments = false;

        partial void OnDisableInputAttachmentsChanging(ref bool newValue);
        /// <summary>
        /// If `true`, disables the ability to upload and send attachments.
        /// Defaults to `false`.
        /// </summary>
        [Parameter]
        public bool DisableInputAttachments
        {
            get { return this._disableInputAttachments; }
            set
            {
                if (this._disableInputAttachments != value || !IsPropDirty("DisableInputAttachments"))
                {
                    MarkPropDirty("DisableInputAttachments");
                }
                this._disableInputAttachments = value;

            }
        }
        private bool _isTyping = false;

        partial void OnIsTypingChanging(ref bool newValue);
        /// <summary>
        /// Indicates whether the other user is currently typing a message.
        /// </summary>
        [Parameter]
        public bool IsTyping
        {
            get { return this._isTyping; }
            set
            {
                if (this._isTyping != value || !IsPropDirty("IsTyping"))
                {
                    MarkPropDirty("IsTyping");
                }
                this._isTyping = value;

            }
        }
        private string _headerText;

        partial void OnHeaderTextChanging(ref string newValue);
        /// <summary>
        /// Optional header text to display at the top of the chat component.
        /// </summary>
        [Parameter]
        public string HeaderText
        {
            get { return this._headerText; }
            set
            {
                if (this._headerText != value || !IsPropDirty("HeaderText"))
                {
                    MarkPropDirty("HeaderText");
                }
                this._headerText = value;

            }
        }
        private string _inputPlaceholder;

        partial void OnInputPlaceholderChanging(ref string newValue);
        /// <summary>
        /// Optional placeholder text for the chat input area.
        /// Provides a hint to the user about what they can type (e.g. "Type a message...").
        /// </summary>
        [Parameter]
        public string InputPlaceholder
        {
            get { return this._inputPlaceholder; }
            set
            {
                if (this._inputPlaceholder != value || !IsPropDirty("InputPlaceholder"))
                {
                    MarkPropDirty("InputPlaceholder");
                }
                this._inputPlaceholder = value;

            }
        }
        private string[] _suggestions;

        partial void OnSuggestionsChanging(ref string[] newValue);
        /// <summary>
        /// Suggested text snippets or quick replies that can be shown as user-selectable options.
        /// </summary>
        [Parameter]
        public string[] Suggestions
        {
            get { return this._suggestions; }
            set
            {
                if (this._suggestions != value || !IsPropDirty("Suggestions"))
                {
                    MarkPropDirty("Suggestions");
                }
                this._suggestions = value;

            }
        }
        private ChatSuggestionsPosition _suggestionsPosition = ChatSuggestionsPosition.BelowInput;

        partial void OnSuggestionsPositionChanging(ref ChatSuggestionsPosition newValue);
        /// <summary>
        /// Controls the position of the chat suggestions within the component layout.
        /// - `"below-input"`: Renders suggestions below the chat input area.
        /// - `"below-messages"`: Renders suggestions below the chat messages area.
        /// Default is `"below-messages"`.
        /// </summary>
        [Parameter]
        public ChatSuggestionsPosition SuggestionsPosition
        {
            get { return this._suggestionsPosition; }
            set
            {
                if (this._suggestionsPosition != value || !IsPropDirty("SuggestionsPosition"))
                {
                    MarkPropDirty("SuggestionsPosition");
                }
                this._suggestionsPosition = value;

            }
        }
        private double _stopTypingDelay = 0;

        partial void OnStopTypingDelayChanging(ref double newValue);
        /// <summary>
        /// Time in milliseconds to wait before dispatching a stop typing event.
        /// Default is `3000`.
        /// </summary>
        [Parameter]
        public double StopTypingDelay
        {
            get { return this._stopTypingDelay; }
            set
            {
                if (this._stopTypingDelay != value || !IsPropDirty("StopTypingDelay"))
                {
                    MarkPropDirty("StopTypingDelay");
                }
                this._stopTypingDelay = value;

            }
        }
        private bool _adoptRootStyles = false;

        partial void OnAdoptRootStylesChanging(ref bool newValue);
        [Parameter]
        public bool AdoptRootStyles
        {
            get { return this._adoptRootStyles; }
            set
            {
                if (this._adoptRootStyles != value || !IsPropDirty("AdoptRootStyles"))
                {
                    MarkPropDirty("AdoptRootStyles");
                }
                this._adoptRootStyles = value;

            }
        }
        private IgbChatRenderers _renderers;

        partial void OnRenderersChanging(ref IgbChatRenderers newValue);
        /// <summary>
        /// An object containing a collection of custom renderers for different parts of the chat UI.
        /// </summary>
        [Parameter]
        public IgbChatRenderers Renderers
        {
            get { return this._renderers; }
            set
            {
                OnRenderersChanging(ref value);
                MarkPropDirty("Renderers");
                if (this._renderers != null)
                {
                    this.DetachChild(this._renderers);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._renderers = value;
            }

        }

        partial void FindByNameChatOptions(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatOptions(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        partial void SerializeCoreIgbChatOptions(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatOptions(ser);

            if (IsPropDirty("CurrentUserId"))
            { ser.AddStringProp("currentUserId", this._currentUserId); }
            if (IsPropDirty("DisableAutoScroll"))
            { ser.AddBooleanProp("disableAutoScroll", this._disableAutoScroll); }
            if (IsPropDirty("DisableInputAttachments"))
            { ser.AddBooleanProp("disableInputAttachments", this._disableInputAttachments); }
            if (IsPropDirty("IsTyping"))
            { ser.AddBooleanProp("isTyping", this._isTyping); }
            if (IsPropDirty("HeaderText"))
            { ser.AddStringProp("headerText", this._headerText); }
            if (IsPropDirty("InputPlaceholder"))
            { ser.AddStringProp("inputPlaceholder", this._inputPlaceholder); }
            if (IsPropDirty("Suggestions"))
            { ser.AddArrayProp("suggestions", this._suggestions); }
            if (IsPropDirty("SuggestionsPosition"))
            { ser.AddEnumProp("suggestionsPosition", this._suggestionsPosition); }
            if (IsPropDirty("StopTypingDelay"))
            { ser.AddNumberProp("stopTypingDelay", this._stopTypingDelay); }
            if (IsPropDirty("AdoptRootStyles"))
            { ser.AddBooleanProp("adoptRootStyles", this._adoptRootStyles); }
            if (IsPropDirty("Renderers"))
            { ser.AddSerializableProp("renderers", this._renderers); }

        }

    }
}
