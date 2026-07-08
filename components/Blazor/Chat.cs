
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A chat UI component for displaying messages, attachments, and input interaction.
/// </summary>
public partial class IgbChat: BaseRendererControl {
                                public override string Type { get { return "WebChat"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbChatModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbChatModule.Register(IgBlazor);
                                    }
                                }

                            protected override string ResolveDisplay()
                        {
                        return "inline-block";
                        }

                            protected override bool SupportsVisualChildren
                        {
                                get 
                                {
                            return true;
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Queued; }
                            }
	
	    public IgbChat(): base() {
	        OnCreatedIgbChat();
	
	        
	    }
	
	    partial void OnCreatedIgbChat();
	    
	private IgbChatMessage[] _messages;
	
	partial void OnMessagesChanging(ref IgbChatMessage[] newValue);
	/// <summary>
	/// The list of chat messages currently displayed.
	/// Use this property to set or update the message history.
	/// </summary>
	[Parameter]
	public IgbChatMessage[] Messages 
	{
	get { return this._messages; }
	set { 
	                if (this._messages != value || !IsPropDirty("Messages")) {
	                        MarkPropDirty("Messages");
	                } 
	                this._messages = value;
	                 
	                }
	}
	private IgbChatDraftMessage _draftMessage;
	
	partial void OnDraftMessageChanging(ref IgbChatDraftMessage newValue);
	/// <summary>
	/// The chat message currently being composed but not yet sent.
	/// Includes the draft text and any attachments.
	/// </summary>
	[Parameter]
	public IgbChatDraftMessage DraftMessage 
	{
	get { return this._draftMessage; }
	set { 
	                        OnDraftMessageChanging(ref value);
	                        MarkPropDirty("DraftMessage"); 
	                        if (this._draftMessage != null) {
	                            this.DetachChild(this._draftMessage);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._draftMessage = value; 
	                    }
	                    
	}
	private IgbChatOptions? _options;
	
	partial void OnOptionsChanging(ref IgbChatOptions? newValue);
	/// <summary>
	/// Controls the chat behavior and appearance through a configuration object.
	/// Use this to toggle UI options, provide suggestions, templates, etc.
	/// </summary>
	[Parameter]
	public IgbChatOptions? Options 
	{
	get { return this._options; }
	set { 
	                        OnOptionsChanging(ref value);
	                        MarkPropDirty("Options"); 
	                        if (this._options != null) {
	                            this.DetachChild(this._options);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._options = value; 
	                    }
	                    
	}
	
	    partial void FindByNameChat(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameChat(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task SetNativeElementAsync(Object element) 
	                    {
		await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	                    public  void SetNativeElement(Object element) 
	                    {
		InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	/// <summary>
	/// Scrolls the view to a specific message by id.
	/// </summary>
	public async  Task ScrollToMessageAsync(String messageId) 
	                    {
		await InvokeMethod("scrollToMessage", new object[] { StringToString(messageId) }, new string[] { "String" });
	}
	                    public  void ScrollToMessage(String messageId) 
	                    {
		InvokeMethodSync("scrollToMessage", new object[] { StringToString(messageId) }, new string[] { "String" });
	}
	
	    private string _messageCreatedRef = null;
	    private string _messageCreatedScript = null;
	    [Parameter]
	    public string MessageCreatedScript { 
	    
	        set 
	        {
	            if (value != this._messageCreatedScript)
	            {
	                this._messageCreatedScript = value;
	                this.OnRefChanged("MessageCreated", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingMessageCreated(IgbChatMessageEventArgs args);
	    private EventCallback<IgbChatMessageEventArgs>? _messageCreated = null;
	    [Parameter]
	    public EventCallback<IgbChatMessageEventArgs> MessageCreated
	    {
	        get 
	        {
	            return this._messageCreated != null ? this._messageCreated.Value : EventCallback<IgbChatMessageEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbChatMessageEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _messageCreated, ref eventCallbacksCache))
	                {
	                    _messageCreated = value;
	                    this.SetHandler<IgbChatMessageEventArgs>(this.Name, "MessageCreated", value, (args) => {
	                        OnHandlingMessageCreated(args);
	                        
	                    });
	        this.OnRefChanged("MessageCreated", null, "event:::MessageCreated", true, false, (refName, oldValue, newValue) => {
	                        this._messageCreatedRef = refName;
	                        this.MarkPropDirty("MessageCreatedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _messageCreated = null;
	                this.SetHandler<IgbChatMessageEventArgs>(this.Name, "MessageCreated", null);
	    this.OnRefChanged("MessageCreated", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._messageCreatedRef = null;
	                    this.MarkPropDirty("MessageCreatedRef");	
	            });
	    }
	    }
	    }
	
	    private string _messageReactRef = null;
	    private string _messageReactScript = null;
	    [Parameter]
	    public string MessageReactScript { 
	    
	        set 
	        {
	            if (value != this._messageReactScript)
	            {
	                this._messageReactScript = value;
	                this.OnRefChanged("MessageReact", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingMessageReact(IgbChatMessageReactionEventArgs args);
	    private EventCallback<IgbChatMessageReactionEventArgs>? _messageReact = null;
	    [Parameter]
	    public EventCallback<IgbChatMessageReactionEventArgs> MessageReact
	    {
	        get 
	        {
	            return this._messageReact != null ? this._messageReact.Value : EventCallback<IgbChatMessageReactionEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbChatMessageReactionEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _messageReact, ref eventCallbacksCache))
	                {
	                    _messageReact = value;
	                    this.SetHandler<IgbChatMessageReactionEventArgs>(this.Name, "MessageReact", value, (args) => {
	                        OnHandlingMessageReact(args);
	                        
	                    });
	        this.OnRefChanged("MessageReact", null, "event:::MessageReact", true, false, (refName, oldValue, newValue) => {
	                        this._messageReactRef = refName;
	                        this.MarkPropDirty("MessageReactRef");	
	                });
	                }
	    }
	        else 
	            {
	                _messageReact = null;
	                this.SetHandler<IgbChatMessageReactionEventArgs>(this.Name, "MessageReact", null);
	    this.OnRefChanged("MessageReact", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._messageReactRef = null;
	                    this.MarkPropDirty("MessageReactRef");	
	            });
	    }
	    }
	    }
	
	    private string _attachmentClickRef = null;
	    private string _attachmentClickScript = null;
	    [Parameter]
	    public string AttachmentClickScript { 
	    
	        set 
	        {
	            if (value != this._attachmentClickScript)
	            {
	                this._attachmentClickScript = value;
	                this.OnRefChanged("AttachmentClick", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingAttachmentClick(IgbChatMessageAttachmentEventArgs args);
	    private EventCallback<IgbChatMessageAttachmentEventArgs>? _attachmentClick = null;
	    [Parameter]
	    public EventCallback<IgbChatMessageAttachmentEventArgs> AttachmentClick
	    {
	        get 
	        {
	            return this._attachmentClick != null ? this._attachmentClick.Value : EventCallback<IgbChatMessageAttachmentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbChatMessageAttachmentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _attachmentClick, ref eventCallbacksCache))
	                {
	                    _attachmentClick = value;
	                    this.SetHandler<IgbChatMessageAttachmentEventArgs>(this.Name, "AttachmentClick", value, (args) => {
	                        OnHandlingAttachmentClick(args);
	                        
	                    });
	        this.OnRefChanged("AttachmentClick", null, "event:::AttachmentClick", true, false, (refName, oldValue, newValue) => {
	                        this._attachmentClickRef = refName;
	                        this.MarkPropDirty("AttachmentClickRef");	
	                });
	                }
	    }
	        else 
	            {
	                _attachmentClick = null;
	                this.SetHandler<IgbChatMessageAttachmentEventArgs>(this.Name, "AttachmentClick", null);
	    this.OnRefChanged("AttachmentClick", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._attachmentClickRef = null;
	                    this.MarkPropDirty("AttachmentClickRef");	
	            });
	    }
	    }
	    }
	
	    private string _typingChangeRef = null;
	    private string _typingChangeScript = null;
	    [Parameter]
	    public string TypingChangeScript { 
	    
	        set 
	        {
	            if (value != this._typingChangeScript)
	            {
	                this._typingChangeScript = value;
	                this.OnRefChanged("TypingChange", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingTypingChange(IgbComponentBoolValueChangedEventArgs args);
	    private EventCallback<IgbComponentBoolValueChangedEventArgs>? _typingChange = null;
	    [Parameter]
	    public EventCallback<IgbComponentBoolValueChangedEventArgs> TypingChange
	    {
	        get 
	        {
	            return this._typingChange != null ? this._typingChange.Value : EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _typingChange, ref eventCallbacksCache))
	                {
	                    _typingChange = value;
	                    this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "TypingChange", value, (args) => {
	                        OnHandlingTypingChange(args);
	                        
	                    });
	        this.OnRefChanged("TypingChange", null, "event:::TypingChange", true, false, (refName, oldValue, newValue) => {
	                        this._typingChangeRef = refName;
	                        this.MarkPropDirty("TypingChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _typingChange = null;
	                this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "TypingChange", null);
	    this.OnRefChanged("TypingChange", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._typingChangeRef = null;
	                    this.MarkPropDirty("TypingChangeRef");	
	            });
	    }
	    }
	    }
	
	    private string _inputFocusRef = null;
	    private string _inputFocusScript = null;
	    [Parameter]
	    public string InputFocusScript { 
	    
	        set 
	        {
	            if (value != this._inputFocusScript)
	            {
	                this._inputFocusScript = value;
	                this.OnRefChanged("InputFocus", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingInputFocus(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _inputFocus = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> InputFocus
	    {
	        get 
	        {
	            return this._inputFocus != null ? this._inputFocus.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _inputFocus, ref eventCallbacksCache))
	                {
	                    _inputFocus = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "InputFocus", value, (args) => {
	                        OnHandlingInputFocus(args);
	                        
	                    });
	        this.OnRefChanged("InputFocus", null, "event:::InputFocus", true, false, (refName, oldValue, newValue) => {
	                        this._inputFocusRef = refName;
	                        this.MarkPropDirty("InputFocusRef");	
	                });
	                }
	    }
	        else 
	            {
	                _inputFocus = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "InputFocus", null);
	    this.OnRefChanged("InputFocus", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputFocusRef = null;
	                    this.MarkPropDirty("InputFocusRef");	
	            });
	    }
	    }
	    }
	
	    private string _inputBlurRef = null;
	    private string _inputBlurScript = null;
	    [Parameter]
	    public string InputBlurScript { 
	    
	        set 
	        {
	            if (value != this._inputBlurScript)
	            {
	                this._inputBlurScript = value;
	                this.OnRefChanged("InputBlur", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingInputBlur(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _inputBlur = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> InputBlur
	    {
	        get 
	        {
	            return this._inputBlur != null ? this._inputBlur.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _inputBlur, ref eventCallbacksCache))
	                {
	                    _inputBlur = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "InputBlur", value, (args) => {
	                        OnHandlingInputBlur(args);
	                        
	                    });
	        this.OnRefChanged("InputBlur", null, "event:::InputBlur", true, false, (refName, oldValue, newValue) => {
	                        this._inputBlurRef = refName;
	                        this.MarkPropDirty("InputBlurRef");	
	                });
	                }
	    }
	        else 
	            {
	                _inputBlur = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "InputBlur", null);
	    this.OnRefChanged("InputBlur", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputBlurRef = null;
	                    this.MarkPropDirty("InputBlurRef");	
	            });
	    }
	    }
	    }
	
	    private string _inputChangeRef = null;
	    private string _inputChangeScript = null;
	    [Parameter]
	    public string InputChangeScript { 
	    
	        set 
	        {
	            if (value != this._inputChangeScript)
	            {
	                this._inputChangeScript = value;
	                this.OnRefChanged("InputChange", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	    partial void OnHandlingInputChange(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _inputChange = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> InputChange
	    {
	        get 
	        {
	            return this._inputChange != null ? this._inputChange.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _inputChange, ref eventCallbacksCache))
	                {
	                    _inputChange = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputChange", value, (args) => {
	                        OnHandlingInputChange(args);
	                        
	                    });
	        this.OnRefChanged("InputChange", null, "event:::InputChange", true, false, (refName, oldValue, newValue) => {
	                        this._inputChangeRef = refName;
	                        this.MarkPropDirty("InputChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _inputChange = null;
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputChange", null);
	    this.OnRefChanged("InputChange", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputChangeRef = null;
	                    this.MarkPropDirty("InputChangeRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbChat(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbChat(ser);
	
	if (IsPropDirty("Messages")) { ser.AddSerializableArrayProp("messages", this._messages); }
	if (IsPropDirty("DraftMessage")) { ser.AddSerializableProp("draftMessage", this._draftMessage); }
	if (IsPropDirty("Options")) { ser.AddSerializableProp("options", this._options); }
	if (IsPropDirty("MessageCreatedRef")) { ser.AddStringProp("messageCreatedRef", this._messageCreatedRef); }
	if (IsPropDirty("MessageReactRef")) { ser.AddStringProp("messageReactRef", this._messageReactRef); }
	if (IsPropDirty("AttachmentClickRef")) { ser.AddStringProp("attachmentClickRef", this._attachmentClickRef); }
	if (IsPropDirty("TypingChangeRef")) { ser.AddStringProp("typingChangeRef", this._typingChangeRef); }
	if (IsPropDirty("InputFocusRef")) { ser.AddStringProp("inputFocusRef", this._inputFocusRef); }
	if (IsPropDirty("InputBlurRef")) { ser.AddStringProp("inputBlurRef", this._inputBlurRef); }
	if (IsPropDirty("InputChangeRef")) { ser.AddStringProp("inputChangeRef", this._inputChangeRef); }
	
	    }
	
}
}
