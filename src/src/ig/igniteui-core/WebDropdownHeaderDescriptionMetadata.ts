import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDropdownHeaderDescription } from "./WebDropdownHeaderDescription";

/**
 * @hidden 
 */
export class WebDropdownHeaderDescriptionMetadata extends Base {
	static $t: Type = markType(WebDropdownHeaderDescriptionMetadata, 'WebDropdownHeaderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDropdownHeaderDescriptionMetadata._metadata == null) {
			WebDropdownHeaderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDropdownHeaderDescriptionMetadata.fillMetadata(WebDropdownHeaderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDropdownHeaderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDropdownHeaderDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DropdownHeader");
		metadata.item("__tagNameWC", "String:igc-dropdown-header");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebDropdownHeaderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDropdownHeader", () => new WebDropdownHeaderDescription());
		context.register("WebDropdownHeader", WebDropdownHeaderDescriptionMetadata._metadata);
	}
}


