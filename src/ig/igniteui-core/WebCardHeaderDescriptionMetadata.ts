import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCardHeaderDescription } from "./WebCardHeaderDescription";

/**
 * @hidden 
 */
export class WebCardHeaderDescriptionMetadata extends Base {
	static $t: Type = markType(WebCardHeaderDescriptionMetadata, 'WebCardHeaderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCardHeaderDescriptionMetadata._metadata == null) {
			WebCardHeaderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCardHeaderDescriptionMetadata.fillMetadata(WebCardHeaderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCardHeaderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCardHeaderDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CardHeader");
		metadata.item("__tagNameWC", "String:igc-card-header");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebCardHeaderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCardHeader", () => new WebCardHeaderDescription());
		context.register("WebCardHeader", WebCardHeaderDescriptionMetadata._metadata);
	}
}


