import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRadioChangeEventArgsDescriptionMetadata } from "./WebRadioChangeEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebRadioDescription } from "./WebRadioDescription";

/**
 * @hidden 
 */
export class WebRadioDescriptionMetadata extends Base {
	static $t: Type = markType(WebRadioDescriptionMetadata, 'WebRadioDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRadioDescriptionMetadata._metadata == null) {
			WebRadioDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRadioDescriptionMetadata.fillMetadata(WebRadioDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRadioDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRadioDescriptionMetadata._metadata);
		WebRadioChangeEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Radio");
		metadata.item("__tagNameWC", "String:igc-radio");
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
		metadata.item("ChangeRef", "EventRef:RadioChangeEventHandler:change");
		metadata.item("ChangeRef@args", "RadioChangeEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebRadioDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRadio", () => new WebRadioDescription());
		context.register("WebRadio", WebRadioDescriptionMetadata._metadata);
	}
}


