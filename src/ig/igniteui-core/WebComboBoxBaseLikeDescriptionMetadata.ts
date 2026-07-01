import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBaseComboBoxDescriptionMetadata } from "./WebBaseComboBoxDescriptionMetadata";

/**
 * @hidden 
 */
export class WebComboBoxBaseLikeDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboBoxBaseLikeDescriptionMetadata, 'WebComboBoxBaseLikeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboBoxBaseLikeDescriptionMetadata._metadata == null) {
			WebComboBoxBaseLikeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboBoxBaseLikeDescriptionMetadata.fillMetadata(WebComboBoxBaseLikeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboBoxBaseLikeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboBoxBaseLikeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:ComboBoxBaseLike");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("KeepOpenOnSelect", "Boolean");
		metadata.item("KeepOpenOnOutsideClick", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxDescriptionMetadata.register(context);
		WebComboBoxBaseLikeDescriptionMetadata.ensureMetadata(context);
		context.register("WebComboBoxBaseLike", WebComboBoxBaseLikeDescriptionMetadata._metadata);
	}
}


