import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebListItemDescription } from "./WebListItemDescription";

/**
 * @hidden 
 */
export class WebListItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebListItemDescriptionMetadata, 'WebListItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebListItemDescriptionMetadata._metadata == null) {
			WebListItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebListItemDescriptionMetadata.fillMetadata(WebListItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebListItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebListItemDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ListItem");
		metadata.item("__tagNameWC", "String:igc-list-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Selected", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebListItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebListItem", () => new WebListItemDescription());
		context.register("WebListItem", WebListItemDescriptionMetadata._metadata);
	}
}


