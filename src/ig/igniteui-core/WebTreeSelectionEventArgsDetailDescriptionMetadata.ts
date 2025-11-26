import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTreeItemDescriptionMetadata } from "./WebTreeItemDescriptionMetadata";
import { WebTreeSelectionEventArgsDetailDescription } from "./WebTreeSelectionEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebTreeSelectionEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebTreeSelectionEventArgsDetailDescriptionMetadata, 'WebTreeSelectionEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata == null) {
			WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTreeSelectionEventArgsDetailDescriptionMetadata.fillMetadata(WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata);
		WebTreeItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TreeSelectionEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("NewSelection", "Array:WebTreeItemDescription:TreeItem");
	}
	static register(context: TypeDescriptionContext): void {
		WebTreeSelectionEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTreeSelectionEventArgsDetail", () => new WebTreeSelectionEventArgsDetailDescription());
		context.register("WebTreeSelectionEventArgsDetail", WebTreeSelectionEventArgsDetailDescriptionMetadata._metadata);
	}
}


