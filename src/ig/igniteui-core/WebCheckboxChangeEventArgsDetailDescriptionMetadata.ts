import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCheckboxChangeEventArgsDetailDescription } from "./WebCheckboxChangeEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebCheckboxChangeEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebCheckboxChangeEventArgsDetailDescriptionMetadata, 'WebCheckboxChangeEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata == null) {
			WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCheckboxChangeEventArgsDetailDescriptionMetadata.fillMetadata(WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CheckboxChangeEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Checked", "Boolean");
		metadata.item("Value", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebCheckboxChangeEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCheckboxChangeEventArgsDetail", () => new WebCheckboxChangeEventArgsDetailDescription());
		context.register("WebCheckboxChangeEventArgsDetail", WebCheckboxChangeEventArgsDetailDescriptionMetadata._metadata);
	}
}


