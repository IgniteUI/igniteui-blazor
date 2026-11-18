import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboSearchInputEventArgsDetailDescriptionMetadata } from "./WebComboSearchInputEventArgsDetailDescriptionMetadata";
import { WebComboSearchInputEventArgsDescription } from "./WebComboSearchInputEventArgsDescription";

/**
 * @hidden
 */
export class WebComboSearchInputEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboSearchInputEventArgsDescriptionMetadata, 'WebComboSearchInputEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboSearchInputEventArgsDescriptionMetadata._metadata == null) {
			WebComboSearchInputEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboSearchInputEventArgsDescriptionMetadata.fillMetadata(WebComboSearchInputEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboSearchInputEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboSearchInputEventArgsDescriptionMetadata._metadata);
		WebComboSearchInputEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboSearchInputEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebComboSearchInputEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboSearchInputEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboSearchInputEventArgs", () => new WebComboSearchInputEventArgsDescription());
		context.register("WebComboSearchInputEventArgs", WebComboSearchInputEventArgsDescriptionMetadata._metadata);
	}
}


