import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRegisterIconOptionsDescription } from "./WebRegisterIconOptionsDescription";

/**
 * @hidden
 */
export class WebRegisterIconOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebRegisterIconOptionsDescriptionMetadata, 'WebRegisterIconOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRegisterIconOptionsDescriptionMetadata._metadata == null) {
			WebRegisterIconOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRegisterIconOptionsDescriptionMetadata.fillMetadata(WebRegisterIconOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRegisterIconOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRegisterIconOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RegisterIconOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("__isTSPlainInterface", "Boolean");
		metadata.item("Collection", "String");
		metadata.item("StripMeta", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebRegisterIconOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRegisterIconOptions", () => new WebRegisterIconOptionsDescription());
		context.register("WebRegisterIconOptions", WebRegisterIconOptionsDescriptionMetadata._metadata);
	}
}


