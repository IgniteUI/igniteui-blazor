import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBaseOptionLikeDescriptionMetadata } from "./WebBaseOptionLikeDescriptionMetadata";
import { WebDropdownItemDescription } from "./WebDropdownItemDescription";

/**
 * @hidden 
 */
export class WebDropdownItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebDropdownItemDescriptionMetadata, 'WebDropdownItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDropdownItemDescriptionMetadata._metadata == null) {
			WebDropdownItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDropdownItemDescriptionMetadata.fillMetadata(WebDropdownItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDropdownItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDropdownItemDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseOptionLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:DropdownItem");
		metadata.item("__tagNameWC", "String:igc-dropdown-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseOptionLikeDescriptionMetadata.register(context);
		WebDropdownItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDropdownItem", () => new WebDropdownItemDescription());
		context.register("WebDropdownItem", WebDropdownItemDescriptionMetadata._metadata);
	}
}


