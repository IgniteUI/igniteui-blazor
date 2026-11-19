import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboClosingEventArgsDetailDescriptionMetadata } from "./WebComboClosingEventArgsDetailDescriptionMetadata";
import { WebComboClosingEventArgsDescription } from "./WebComboClosingEventArgsDescription";

/**
 * @hidden
 */
export class WebComboClosingEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboClosingEventArgsDescriptionMetadata, 'WebComboClosingEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboClosingEventArgsDescriptionMetadata._metadata == null) {
			WebComboClosingEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboClosingEventArgsDescriptionMetadata.fillMetadata(WebComboClosingEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboClosingEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboClosingEventArgsDescriptionMetadata._metadata);
		WebComboClosingEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboClosingEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebComboClosingEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboClosingEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboClosingEventArgs", () => new WebComboClosingEventArgsDescription());
		context.register("WebComboClosingEventArgs", WebComboClosingEventArgsDescriptionMetadata._metadata);
	}
}


