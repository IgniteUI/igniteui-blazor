import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboSearchInputEventArgsDetailDescription } from "./WebComboSearchInputEventArgsDetailDescription";

/**
 * @hidden
 */
export class WebComboSearchInputEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboSearchInputEventArgsDetailDescriptionMetadata, 'WebComboSearchInputEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata == null) {
			WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboSearchInputEventArgsDetailDescriptionMetadata.fillMetadata(WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboSearchInputEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("SearchText", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboSearchInputEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboSearchInputEventArgsDetail", () => new WebComboSearchInputEventArgsDetailDescription());
		context.register("WebComboSearchInputEventArgsDetail", WebComboSearchInputEventArgsDetailDescriptionMetadata._metadata);
	}
}


