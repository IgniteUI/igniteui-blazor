import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebFocusOptionsDescription } from "./WebFocusOptionsDescription";

/**
 * @hidden
 */
export class WebFocusOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebFocusOptionsDescriptionMetadata, 'WebFocusOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebFocusOptionsDescriptionMetadata._metadata == null) {
			WebFocusOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebFocusOptionsDescriptionMetadata.fillMetadata(WebFocusOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebFocusOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebFocusOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:FocusOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("__isTSPlainInterface", "Boolean");
		metadata.item("PreventScroll", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebFocusOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebFocusOptions", () => new WebFocusOptionsDescription());
		context.register("WebFocusOptions", WebFocusOptionsDescriptionMetadata._metadata);
	}
}


