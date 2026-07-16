import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNavDrawerItemDescription } from "./WebNavDrawerItemDescription";

/**
 * @hidden 
 */
export class WebNavDrawerItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebNavDrawerItemDescriptionMetadata, 'WebNavDrawerItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebNavDrawerItemDescriptionMetadata._metadata == null) {
			WebNavDrawerItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebNavDrawerItemDescriptionMetadata.fillMetadata(WebNavDrawerItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebNavDrawerItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebNavDrawerItemDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:NavDrawerItem");
		metadata.item("__tagNameWC", "String:igc-nav-drawer-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Disabled", "Boolean");
		metadata.item("Active", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebNavDrawerItem", () => new WebNavDrawerItemDescription());
		context.register("WebNavDrawerItem", WebNavDrawerItemDescriptionMetadata._metadata);
	}
}


