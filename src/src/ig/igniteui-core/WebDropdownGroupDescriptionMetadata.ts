import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDropdownGroupDescription } from "./WebDropdownGroupDescription";

/**
 * @hidden 
 */
export class WebDropdownGroupDescriptionMetadata extends Base {
	static $t: Type = markType(WebDropdownGroupDescriptionMetadata, 'WebDropdownGroupDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDropdownGroupDescriptionMetadata._metadata == null) {
			WebDropdownGroupDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDropdownGroupDescriptionMetadata.fillMetadata(WebDropdownGroupDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDropdownGroupDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDropdownGroupDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DropdownGroup");
		metadata.item("__tagNameWC", "String:igc-dropdown-group");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebDropdownGroupDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDropdownGroup", () => new WebDropdownGroupDescription());
		context.register("WebDropdownGroup", WebDropdownGroupDescriptionMetadata._metadata);
	}
}


