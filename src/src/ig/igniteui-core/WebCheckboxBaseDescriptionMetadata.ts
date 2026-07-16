import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCheckboxChangeEventArgsDescriptionMetadata } from "./WebCheckboxChangeEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebCheckboxBaseDescription } from "./WebCheckboxBaseDescription";

/**
 * @hidden 
 */
export class WebCheckboxBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebCheckboxBaseDescriptionMetadata, 'WebCheckboxBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCheckboxBaseDescriptionMetadata._metadata == null) {
			WebCheckboxBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCheckboxBaseDescriptionMetadata.fillMetadata(WebCheckboxBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCheckboxBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCheckboxBaseDescriptionMetadata._metadata);
		WebCheckboxChangeEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CheckboxBase");
		metadata.item("__tagNameWC", "String:igc-checkbox-base");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("Checked", "Boolean");
		metadata.item("LabelPosition", "ExportedType:string:ToggleLabelPosition");
		metadata.item("LabelPosition@stringUnion", "WebComponents;React");
		metadata.item("LabelPosition@names", "After;Before");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultChecked", "Boolean");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("ChangeRef", "EventRef:CheckboxChangeEventHandler:change");
		metadata.item("ChangeRef@args", "CheckboxChangeEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebCheckboxBaseDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCheckboxBase", () => new WebCheckboxBaseDescription());
		context.register("WebCheckboxBase", WebCheckboxBaseDescriptionMetadata._metadata);
	}
}


