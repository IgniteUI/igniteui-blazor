import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSelectHeaderDescription } from "./WebSelectHeaderDescription";

/**
 * @hidden 
 */
export class WebSelectHeaderDescriptionMetadata extends Base {
	static $t: Type = markType(WebSelectHeaderDescriptionMetadata, 'WebSelectHeaderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSelectHeaderDescriptionMetadata._metadata == null) {
			WebSelectHeaderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSelectHeaderDescriptionMetadata.fillMetadata(WebSelectHeaderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSelectHeaderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSelectHeaderDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SelectHeader");
		metadata.item("__tagNameWC", "String:igc-select-header");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebSelectHeaderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSelectHeader", () => new WebSelectHeaderDescription());
		context.register("WebSelectHeader", WebSelectHeaderDescriptionMetadata._metadata);
	}
}


