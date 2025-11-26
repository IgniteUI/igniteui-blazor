import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCardContentDescription } from "./WebCardContentDescription";

/**
 * @hidden 
 */
export class WebCardContentDescriptionMetadata extends Base {
	static $t: Type = markType(WebCardContentDescriptionMetadata, 'WebCardContentDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCardContentDescriptionMetadata._metadata == null) {
			WebCardContentDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCardContentDescriptionMetadata.fillMetadata(WebCardContentDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCardContentDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCardContentDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CardContent");
		metadata.item("__tagNameWC", "String:igc-card-content");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebCardContentDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCardContent", () => new WebCardContentDescription());
		context.register("WebCardContent", WebCardContentDescriptionMetadata._metadata);
	}
}


