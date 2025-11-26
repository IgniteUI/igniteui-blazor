import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCheckboxChangeEventArgsDetailDescriptionMetadata } from "./WebCheckboxChangeEventArgsDetailDescriptionMetadata";
import { WebCheckboxChangeEventArgsDescription } from "./WebCheckboxChangeEventArgsDescription";

/**
 * @hidden 
 */
export class WebCheckboxChangeEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebCheckboxChangeEventArgsDescriptionMetadata, 'WebCheckboxChangeEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCheckboxChangeEventArgsDescriptionMetadata._metadata == null) {
			WebCheckboxChangeEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCheckboxChangeEventArgsDescriptionMetadata.fillMetadata(WebCheckboxChangeEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCheckboxChangeEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCheckboxChangeEventArgsDescriptionMetadata._metadata);
		WebCheckboxChangeEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CheckboxChangeEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebCheckboxChangeEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebCheckboxChangeEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCheckboxChangeEventArgs", () => new WebCheckboxChangeEventArgsDescription());
		context.register("WebCheckboxChangeEventArgs", WebCheckboxChangeEventArgsDescriptionMetadata._metadata);
	}
}


