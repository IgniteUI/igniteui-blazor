
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbChatDraftMessage: BaseRendererElement {
                                public override string Type { get { return "WebChatDraftMessage"; } }

	
	                    private static bool _marshalByValue = true;
	
	    public IgbChatDraftMessage(): base() {
	        OnCreatedIgbChatDraftMessage();
	
	        
	    }
	
	    partial void OnCreatedIgbChatDraftMessage();
	    
	private string _text;
	
	partial void OnTextChanging(ref string newValue);
	/// <summary>
	/// The textual content of the draft message.
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
	private IgbChatMessageAttachment[] _attachments;
	
	partial void OnAttachmentsChanging(ref IgbChatMessageAttachment[] newValue);
	/// <summary>
	/// An array of attachments associated with the draft message.
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
	
	    partial void FindByNameChatDraftMessage(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameChatDraftMessage(name, ref item);
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
	
	    partial void SerializeCoreIgbChatDraftMessage(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbChatDraftMessage(ser);
	
	if (IsPropDirty("Text")) { ser.AddStringProp("text", this._text); }
	if (IsPropDirty("Attachments")) { ser.AddSerializableArrayProp("attachments", this._attachments); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Text")) { args["text"] = this._text; }
	if (IsPropDirty("Attachments")) { args["attachments"] = ObjectArrayToParam(this._attachments); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("text")) { this.Text = ReturnToString(args["text"]); }
	if (args.ContainsKey("attachments")) { this.Attachments = ReturnToObjectArray<IgbChatMessageAttachment>(args["attachments"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
