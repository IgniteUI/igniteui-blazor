import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCardDescription } from "./WebCardDescription";

/**
 * @hidden 
 */
export class WebCardDescriptionMetadata extends Base {
	static $t: Type = markType(WebCardDescriptionMetadata, 'WebCardDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCardDescriptionMetadata._metadata == null) {
			WebCardDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCardDescriptionMetadata.fillMetadata(WebCardDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCardDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCardDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Card");
		metadata.item("__tagNameWC", "String:igc-card");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Elevated", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebCardDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCard", () => new WebCardDescription());
		context.register("WebCard", WebCardDescriptionMetadata._metadata);
	}
}


