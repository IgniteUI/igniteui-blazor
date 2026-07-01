
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbChatMessageRenderContext: BaseRendererElement {
                                public override string Type { get { return "WebChatMessageRenderContext"; } }

	
	    public IgbChatMessageRenderContext(): base() {
	        OnCreatedIgbChatMessageRenderContext();
	
	        
	    }
	
	    partial void OnCreatedIgbChatMessageRenderContext();
	    
	private IgbChatMessage _message;
	
	partial void OnMessageChanging(ref IgbChatMessage newValue);
	/// <summary>
	/// The specific chat message being rendered.
	/// </summary>
	[Parameter]
	public IgbChatMessage Message 
	{
	get { return this._message; }
	set { 
	                        OnMessageChanging(ref value);
	                        MarkPropDirty("Message"); 
	                        if (this._message != null) {
	                            this.DetachChild(this._message);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._message = value; 
	                    }
	                    
	}
	
	    partial void FindByNameChatMessageRenderContext(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameChatMessageRenderContext(name, ref item);
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
	
	    partial void SerializeCoreIgbChatMessageRenderContext(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbChatMessageRenderContext(ser);
	
	if (IsPropDirty("Message")) { ser.AddSerializableProp("message", this._message); }
	
	    }
	
}
}
