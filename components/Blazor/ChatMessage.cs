
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbChatMessage: BaseRendererElement {
                                public override string Type { get { return "WebChatMessage"; } }

	
	                    private static bool _marshalByValue = true;
	
	    public IgbChatMessage(): base() {
	        OnCreatedIgbChatMessage();
	
	        
	    }
	
	    partial void OnCreatedIgbChatMessage();
	    
	private string _id;
	
	partial void OnIdChanging(ref string newValue);
	/// <summary>
	/// A unique identifier for the message.
	/// </summary>
	[Parameter]
	public string Id 
	{
	get { return this._id; }
	set { 
	                if (this._id != value || !IsPropDirty("Id")) {
	                        MarkPropDirty("Id");
	                } 
	                this._id = value;
	                 
	                }
	}
	private string _text;
	
	partial void OnTextChanging(ref string newValue);
	/// <summary>
	/// The textual content of the message.
	/// </summary>
	[Parameter]
	public string Text 
	{
	get { return this._text; }
	set { 
	                if (this._text != value || !IsPropDirty("Text")) {
	                        MarkPropDirty("Text");
	                } 
	                this._text = value;
	                 
	                }
	}
	private string _sender;
	
	partial void OnSenderChanging(ref string newValue);
	/// <summary>
	/// The identifier or name of the sender of the message.
	/// </summary>
	[Parameter]
	public string Sender 
	{
	get { return this._sender; }
	set { 
	                if (this._sender != value || !IsPropDirty("Sender")) {
	                        MarkPropDirty("Sender");
	                } 
	                this._sender = value;
	                 
	                }
	}
	private string _timestamp;
	
	partial void OnTimestampChanging(ref string newValue);
	/// <summary>
	/// The timestamp indicating when the message was sent.
	/// </summary>
	[Parameter]
	public string Timestamp 
	{
	get { return this._timestamp; }
	set { 
	                if (this._timestamp != value || !IsPropDirty("Timestamp")) {
	                        MarkPropDirty("Timestamp");
	                } 
	                this._timestamp = value;
	                 
	                }
	}
	private IgbChatMessageAttachment[] _attachments;
	
	partial void OnAttachmentsChanging(ref IgbChatMessageAttachment[] newValue);
	/// <summary>
	/// Optional list of attachments associated with the message,
	/// such as images, files, or links.
	/// </summary>
	[Parameter]
	public IgbChatMessageAttachment[] Attachments 
	{
	get { return this._attachments; }
	set { 
	                if (this._attachments != value || !IsPropDirty("Attachments")) {
	                        MarkPropDirty("Attachments");
	                } 
	                this._attachments = value;
	                 
	                }
	}
	private string[] _reactions;
	
	partial void OnReactionsChanging(ref string[] newValue);
	/// <summary>
	/// Optional list of reactions associated with the message.
	/// </summary>
	[Parameter]
	public string[] Reactions 
	{
	get { return this._reactions; }
	set { 
	                if (this._reactions != value || !IsPropDirty("Reactions")) {
	                        MarkPropDirty("Reactions");
	                } 
	                this._reactions = value;
	                 
	                }
	}
	
	    partial void FindByNameChatMessage(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameChatMessage(name, ref item);
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
	
	    partial void SerializeCoreIgbChatMessage(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbChatMessage(ser);
	
	if (IsPropDirty("Id")) { ser.AddStringProp("id", this._id); }
	if (IsPropDirty("Text")) { ser.AddStringProp("text", this._text); }
	if (IsPropDirty("Sender")) { ser.AddStringProp("sender", this._sender); }
	if (IsPropDirty("Timestamp")) { ser.AddStringProp("timestamp", this._timestamp); }
	if (IsPropDirty("Attachments")) { ser.AddSerializableArrayProp("attachments", this._attachments); }
	if (IsPropDirty("Reactions")) { ser.AddArrayProp("reactions", this._reactions); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Id")) { args["id"] = this._id; }
	if (IsPropDirty("Text")) { args["text"] = this._text; }
	if (IsPropDirty("Sender")) { args["sender"] = this._sender; }
	if (IsPropDirty("Timestamp")) { args["timestamp"] = this._timestamp; }
	if (IsPropDirty("Attachments")) { args["attachments"] = ObjectArrayToParam(this._attachments); }
	if (IsPropDirty("Reactions")) { args["reactions"] = StringArrayToString(this._reactions); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("id")) { this.Id = ReturnToString(args["id"]); }
	if (args.ContainsKey("text")) { this.Text = ReturnToString(args["text"]); }
	if (args.ContainsKey("sender")) { this.Sender = ReturnToString(args["sender"]); }
	if (args.ContainsKey("timestamp")) { this.Timestamp = ReturnToString(args["timestamp"]); }
	if (args.ContainsKey("attachments")) { this.Attachments = ReturnToObjectArray<IgbChatMessageAttachment>(args["attachments"]); }
	if (args.ContainsKey("reactions")) { this.Reactions = ReturnToStringArray(args["reactions"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
