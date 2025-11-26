import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCardMediaDescription } from "./WebCardMediaDescription";

/**
 * @hidden 
 */
export class WebCardMediaDescriptionMetadata extends Base {
	static $t: Type = markType(WebCardMediaDescriptionMetadata, 'WebCardMediaDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCardMediaDescriptionMetadata._metadata == null) {
			WebCardMediaDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCardMediaDescriptionMetadata.fillMetadata(WebCardMediaDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCardMediaDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCardMediaDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CardMedia");
		metadata.item("__tagNameWC", "String:igc-card-media");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebCardMediaDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCardMedia", () => new WebCardMediaDescription());
		context.register("WebCardMedia", WebCardMediaDescriptionMetadata._metadata);
	}
}


