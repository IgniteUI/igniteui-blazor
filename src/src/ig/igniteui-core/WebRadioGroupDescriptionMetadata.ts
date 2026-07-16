import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRadioChangeEventArgsDescriptionMetadata } from "./WebRadioChangeEventArgsDescriptionMetadata";
import { WebRadioGroupDescription } from "./WebRadioGroupDescription";

/**
 * @hidden 
 */
export class WebRadioGroupDescriptionMetadata extends Base {
	static $t: Type = markType(WebRadioGroupDescriptionMetadata, 'WebRadioGroupDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRadioGroupDescriptionMetadata._metadata == null) {
			WebRadioGroupDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRadioGroupDescriptionMetadata.fillMetadata(WebRadioGroupDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRadioGroupDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRadioGroupDescriptionMetadata._metadata);
		WebRadioChangeEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RadioGroup");
		metadata.item("__tagNameWC", "String:igc-radio-group");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Alignment", "ExportedType:string:ContentOrientation");
		metadata.item("Alignment@stringUnion", "WebComponents;React");
		metadata.item("Alignment@names", "Horizontal;Vertical");
		metadata.item("DefaultValue", "String");
		metadata.item("Name", "String");
		metadata.item("Value", "String");
		metadata.item("ChangeRef", "EventRef:RadioChangeEventHandler:change");
		metadata.item("ChangeRef@args", "RadioChangeEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebRadioGroupDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRadioGroup", () => new WebRadioGroupDescription());
		context.register("WebRadioGroup", WebRadioGroupDescriptionMetadata._metadata);
	}
}


