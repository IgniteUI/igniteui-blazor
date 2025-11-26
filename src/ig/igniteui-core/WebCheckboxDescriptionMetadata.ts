import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCheckboxBaseDescriptionMetadata } from "./WebCheckboxBaseDescriptionMetadata";
import { WebCheckboxDescription } from "./WebCheckboxDescription";

/**
 * @hidden 
 */
export class WebCheckboxDescriptionMetadata extends Base {
	static $t: Type = markType(WebCheckboxDescriptionMetadata, 'WebCheckboxDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCheckboxDescriptionMetadata._metadata == null) {
			WebCheckboxDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCheckboxDescriptionMetadata.fillMetadata(WebCheckboxDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCheckboxDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCheckboxDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebCheckboxBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Checkbox");
		metadata.item("__tagNameWC", "String:igc-checkbox");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Indeterminate", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebCheckboxBaseDescriptionMetadata.register(context);
		WebCheckboxDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCheckbox", () => new WebCheckboxDescription());
		context.register("WebCheckbox", WebCheckboxDescriptionMetadata._metadata);
	}
}


