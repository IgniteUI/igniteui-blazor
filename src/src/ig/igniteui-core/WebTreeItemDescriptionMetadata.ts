import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTreeItemDescription } from "./WebTreeItemDescription";

/**
 * @hidden 
 */
export class WebTreeItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebTreeItemDescriptionMetadata, 'WebTreeItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTreeItemDescriptionMetadata._metadata == null) {
			WebTreeItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTreeItemDescriptionMetadata.fillMetadata(WebTreeItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTreeItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTreeItemDescriptionMetadata._metadata);
		WebTreeItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TreeItem");
		metadata.item("__tagNameWC", "String:igc-tree-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Parent", "ExportedType:WebTreeItem");
		metadata.item("Level", "Number:double");
		metadata.item("Label", "String");
		metadata.item("Expanded", "Boolean");
		metadata.item("Active", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("Selected", "Boolean");
		metadata.item("Loading", "Boolean");
		metadata.item("Value", "Unknown");
	}
	static register(context: TypeDescriptionContext): void {
		WebTreeItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTreeItem", () => new WebTreeItemDescription());
		context.register("WebTreeItem", WebTreeItemDescriptionMetadata._metadata);
	}
}


