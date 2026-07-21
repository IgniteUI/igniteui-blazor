import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTabDescription } from "./WebTabDescription";

/**
 * @hidden 
 */
export class WebTabDescriptionMetadata extends Base {
	static $t: Type = markType(WebTabDescriptionMetadata, 'WebTabDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTabDescriptionMetadata._metadata == null) {
			WebTabDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTabDescriptionMetadata.fillMetadata(WebTabDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTabDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTabDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Tab");
		metadata.item("__tagNameWC", "String:igc-tab");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Label", "String");
		metadata.item("Selected", "Boolean");
		metadata.item("Disabled", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebTabDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTab", () => new WebTabDescription());
		context.register("WebTab", WebTabDescriptionMetadata._metadata);
	}
}


