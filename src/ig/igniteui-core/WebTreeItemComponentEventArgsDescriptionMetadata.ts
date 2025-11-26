import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTreeItemDescriptionMetadata } from "./WebTreeItemDescriptionMetadata";
import { WebTreeItemComponentEventArgsDescription } from "./WebTreeItemComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebTreeItemComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTreeItemComponentEventArgsDescriptionMetadata, 'WebTreeItemComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTreeItemComponentEventArgsDescriptionMetadata._metadata == null) {
			WebTreeItemComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTreeItemComponentEventArgsDescriptionMetadata.fillMetadata(WebTreeItemComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTreeItemComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTreeItemComponentEventArgsDescriptionMetadata._metadata);
		WebTreeItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TreeItemComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebTreeItem");
	}
	static register(context: TypeDescriptionContext): void {
		WebTreeItemComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTreeItemComponentEventArgs", () => new WebTreeItemComponentEventArgsDescription());
		context.register("WebTreeItemComponentEventArgs", WebTreeItemComponentEventArgsDescriptionMetadata._metadata);
	}
}


