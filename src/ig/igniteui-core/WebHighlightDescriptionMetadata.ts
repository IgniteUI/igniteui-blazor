import { Base, String_$type, Type, markType } from "./type";
import { Dictionary$2 } from "./Dictionary$2";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebHighlightNavigationDescriptionMetadata } from "./WebHighlightNavigationDescriptionMetadata";
import { WebHighlightDescription } from "./WebHighlightDescription";

/**
 * @hidden 
 */
export class WebHighlightDescriptionMetadata extends Base {
	static $t: Type = markType(WebHighlightDescriptionMetadata, 'WebHighlightDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebHighlightDescriptionMetadata._metadata == null) {
			WebHighlightDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebHighlightDescriptionMetadata.fillMetadata(WebHighlightDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebHighlightDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebHighlightDescriptionMetadata._metadata);
		WebHighlightDescriptionMetadata.registerOtherTypes(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Highlight");
		metadata.item("__tagNameWC", "String:igc-highlight");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("CaseSensitive", "Boolean");
		metadata.item("SearchText", "String");
		WebHighlightDescriptionMetadata.registerOtherMetadata(metadata);
	}
	static register(context: TypeDescriptionContext): void {
		WebHighlightDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebHighlight", () => new WebHighlightDescription());
		context.register("WebHighlight", WebHighlightDescriptionMetadata._metadata);
	}
	private static registerOtherMetadata(metadata: Dictionary$2<string, string>): void {
	}
	private static registerOtherTypes(context: TypeDescriptionContext): void {
		WebHighlightNavigationDescriptionMetadata.register(context);
	}
}


