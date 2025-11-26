import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebListHeaderDescription } from "./WebListHeaderDescription";

/**
 * @hidden 
 */
export class WebListHeaderDescriptionMetadata extends Base {
	static $t: Type = markType(WebListHeaderDescriptionMetadata, 'WebListHeaderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebListHeaderDescriptionMetadata._metadata == null) {
			WebListHeaderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebListHeaderDescriptionMetadata.fillMetadata(WebListHeaderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebListHeaderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebListHeaderDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ListHeader");
		metadata.item("__tagNameWC", "String:igc-list-header");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebListHeaderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebListHeader", () => new WebListHeaderDescription());
		context.register("WebListHeader", WebListHeaderDescriptionMetadata._metadata);
	}
}


