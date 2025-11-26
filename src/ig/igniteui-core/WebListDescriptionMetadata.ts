import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebListDescription } from "./WebListDescription";

/**
 * @hidden 
 */
export class WebListDescriptionMetadata extends Base {
	static $t: Type = markType(WebListDescriptionMetadata, 'WebListDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebListDescriptionMetadata._metadata == null) {
			WebListDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebListDescriptionMetadata.fillMetadata(WebListDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebListDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebListDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:List");
		metadata.item("__tagNameWC", "String:igc-list");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebListDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebList", () => new WebListDescription());
		context.register("WebList", WebListDescriptionMetadata._metadata);
	}
}


