import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";

/**
 * @hidden 
 */
export class WebButtonBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebButtonBaseDescriptionMetadata, 'WebButtonBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebButtonBaseDescriptionMetadata._metadata == null) {
			WebButtonBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebButtonBaseDescriptionMetadata.fillMetadata(WebButtonBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebButtonBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebButtonBaseDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ButtonBase");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("DisplayType", "(wc:Type)ExportedType:string:ButtonBaseType");
		metadata.item("DisplayType@stringUnion", "WebComponents;React");
		metadata.item("DisplayType@names", "Button;Reset;Submit");
		metadata.item("Href", "String");
		metadata.item("Download", "String");
		metadata.item("Target", "ExportedType:string:ButtonBaseTarget");
		metadata.item("Target@stringUnion", "WebComponents;React");
		metadata.item("Target@names", "_blank;_parent;_self;_top");
		metadata.item("Rel", "String");
		metadata.item("Disabled", "Boolean");
		metadata.item("ClickedRef", "EventRef:VoidHandler:clicked");
		metadata.item("ClickedRef@args", "VoidEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebButtonBaseDescriptionMetadata.ensureMetadata(context);
		context.register("WebButtonBase", WebButtonBaseDescriptionMetadata._metadata);
	}
}


