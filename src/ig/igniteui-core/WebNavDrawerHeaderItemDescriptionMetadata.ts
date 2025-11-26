import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNavDrawerHeaderItemDescription } from "./WebNavDrawerHeaderItemDescription";

/**
 * @hidden 
 */
export class WebNavDrawerHeaderItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebNavDrawerHeaderItemDescriptionMetadata, 'WebNavDrawerHeaderItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebNavDrawerHeaderItemDescriptionMetadata._metadata == null) {
			WebNavDrawerHeaderItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebNavDrawerHeaderItemDescriptionMetadata.fillMetadata(WebNavDrawerHeaderItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebNavDrawerHeaderItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebNavDrawerHeaderItemDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:NavDrawerHeaderItem");
		metadata.item("__tagNameWC", "String:igc-nav-drawer-header-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerHeaderItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebNavDrawerHeaderItem", () => new WebNavDrawerHeaderItemDescription());
		context.register("WebNavDrawerHeaderItem", WebNavDrawerHeaderItemDescriptionMetadata._metadata);
	}
}


