import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebTextareaDescription } from "./WebTextareaDescription";

/**
 * @hidden 
 */
export class WebTextareaDescriptionMetadata extends Base {
	static $t: Type = markType(WebTextareaDescriptionMetadata, 'WebTextareaDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTextareaDescriptionMetadata._metadata == null) {
			WebTextareaDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTextareaDescriptionMetadata.fillMetadata(WebTextareaDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTextareaDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTextareaDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Textarea");
		metadata.item("__tagNameWC", "String:igc-textarea");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Autocomplete", "String");
		metadata.item("Autocapitalize", "String");
		metadata.item("InputMode", "String");
		metadata.item("Label", "String");
		metadata.item("MaxLength", "Number:double");
		metadata.item("MinLength", "Number:double");
		metadata.item("Outlined", "Boolean");
		metadata.item("Placeholder", "String");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("Resize", "ExportedType:string:TextareaResize");
		metadata.item("Resize@stringUnion", "WebComponents;React");
		metadata.item("Resize@names", "Vertical;Auto;None");
		metadata.item("Rows", "Number:double");
		metadata.item("Value", "String");
		metadata.item("Spellcheck", "Boolean");
		metadata.item("Wrap", "ExportedType:string:TextareaWrap");
		metadata.item("Wrap@stringUnion", "WebComponents;React");
		metadata.item("Wrap@names", "Soft;Hard;Off");
		metadata.item("ValidateOnly", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("InputRef", "EventRef:ComponentValueChangedEventHandler:input");
		metadata.item("InputRef@args", "ComponentValueChangedEventArgs");
		metadata.item("ChangeRef", "EventRef:ComponentValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentValueChangedEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebTextareaDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTextarea", () => new WebTextareaDescription());
		context.register("WebTextarea", WebTextareaDescriptionMetadata._metadata);
	}
}


