import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebHighlightNavigationDescription } from "./WebHighlightNavigationDescription";

/**
 * @hidden 
 */
export class WebHighlightNavigationDescriptionMetadata extends Base {
	static $t: Type = markType(WebHighlightNavigationDescriptionMetadata, 'WebHighlightNavigationDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebHighlightNavigationDescriptionMetadata._metadata == null) {
			WebHighlightNavigationDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebHighlightNavigationDescriptionMetadata.fillMetadata(WebHighlightNavigationDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebHighlightNavigationDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebHighlightNavigationDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:HighlightNavigation");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("PreventScroll", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebHighlightNavigationDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebHighlightNavigation", () => new WebHighlightNavigationDescription());
		context.register("WebHighlightNavigation", WebHighlightNavigationDescriptionMetadata._metadata);
	}
}


