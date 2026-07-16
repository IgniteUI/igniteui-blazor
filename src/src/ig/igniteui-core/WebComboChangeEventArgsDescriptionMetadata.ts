import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboChangeEventArgsDetailDescriptionMetadata } from "./WebComboChangeEventArgsDetailDescriptionMetadata";
import { WebComboChangeEventArgsDescription } from "./WebComboChangeEventArgsDescription";

/**
 * @hidden 
 */
export class WebComboChangeEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboChangeEventArgsDescriptionMetadata, 'WebComboChangeEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboChangeEventArgsDescriptionMetadata._metadata == null) {
			WebComboChangeEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboChangeEventArgsDescriptionMetadata.fillMetadata(WebComboChangeEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboChangeEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboChangeEventArgsDescriptionMetadata._metadata);
		WebComboChangeEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboChangeEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebComboChangeEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboChangeEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboChangeEventArgs", () => new WebComboChangeEventArgsDescription());
		context.register("WebComboChangeEventArgs", WebComboChangeEventArgsDescriptionMetadata._metadata);
	}
}


