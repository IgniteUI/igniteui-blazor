import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebFilteringOptionsDescription } from "./WebFilteringOptionsDescription";

/**
 * @hidden 
 */
export class WebFilteringOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebFilteringOptionsDescriptionMetadata, 'WebFilteringOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebFilteringOptionsDescriptionMetadata._metadata == null) {
			WebFilteringOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebFilteringOptionsDescriptionMetadata.fillMetadata(WebFilteringOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebFilteringOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebFilteringOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:FilteringOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebFilteringOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebFilteringOptions", () => new WebFilteringOptionsDescription());
		context.register("WebFilteringOptions", WebFilteringOptionsDescriptionMetadata._metadata);
	}
}


