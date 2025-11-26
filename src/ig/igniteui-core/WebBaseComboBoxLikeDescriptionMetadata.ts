import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class WebBaseComboBoxLikeDescriptionMetadata extends Base {
	static $t: Type = markType(WebBaseComboBoxLikeDescriptionMetadata, 'WebBaseComboBoxLikeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBaseComboBoxLikeDescriptionMetadata._metadata == null) {
			WebBaseComboBoxLikeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBaseComboBoxLikeDescriptionMetadata.fillMetadata(WebBaseComboBoxLikeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBaseComboBoxLikeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBaseComboBoxLikeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:BaseComboBoxLike");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("EmitEvent", "Unknown");
		metadata.item("KeepOpenOnSelect", "Boolean");
		metadata.item("KeepOpenOnOutsideClick", "Boolean");
		metadata.item("Open", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionMetadata.ensureMetadata(context);
		context.register("WebBaseComboBoxLike", WebBaseComboBoxLikeDescriptionMetadata._metadata);
	}
}


