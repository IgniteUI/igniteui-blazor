import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class WebBaseComboBoxDescriptionMetadata extends Base {
	static $t: Type = markType(WebBaseComboBoxDescriptionMetadata, 'WebBaseComboBoxDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBaseComboBoxDescriptionMetadata._metadata == null) {
			WebBaseComboBoxDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBaseComboBoxDescriptionMetadata.fillMetadata(WebBaseComboBoxDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBaseComboBoxDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBaseComboBoxDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:BaseComboBox");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Open", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxDescriptionMetadata.ensureMetadata(context);
		context.register("WebBaseComboBox", WebBaseComboBoxDescriptionMetadata._metadata);
	}
}


