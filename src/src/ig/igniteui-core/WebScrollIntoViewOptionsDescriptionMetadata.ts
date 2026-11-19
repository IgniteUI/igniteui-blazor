import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebScrollIntoViewOptionsDescription } from "./WebScrollIntoViewOptionsDescription";

/**
 * @hidden
 */
export class WebScrollIntoViewOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebScrollIntoViewOptionsDescriptionMetadata, 'WebScrollIntoViewOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebScrollIntoViewOptionsDescriptionMetadata._metadata == null) {
			WebScrollIntoViewOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebScrollIntoViewOptionsDescriptionMetadata.fillMetadata(WebScrollIntoViewOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebScrollIntoViewOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebScrollIntoViewOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ScrollIntoViewOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("__isTSPlainInterface", "Boolean");
		metadata.item("Behavior", "String");
		metadata.item("Block", "String");
		metadata.item("Inline", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebScrollIntoViewOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebScrollIntoViewOptions", () => new WebScrollIntoViewOptionsDescription());
		context.register("WebScrollIntoViewOptions", WebScrollIntoViewOptionsDescriptionMetadata._metadata);
	}
}


