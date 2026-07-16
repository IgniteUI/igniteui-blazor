import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebProgressBaseDescriptionMetadata } from "./WebProgressBaseDescriptionMetadata";
import { WebCircularProgressDescription } from "./WebCircularProgressDescription";

/**
 * @hidden 
 */
export class WebCircularProgressDescriptionMetadata extends Base {
	static $t: Type = markType(WebCircularProgressDescriptionMetadata, 'WebCircularProgressDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCircularProgressDescriptionMetadata._metadata == null) {
			WebCircularProgressDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCircularProgressDescriptionMetadata.fillMetadata(WebCircularProgressDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCircularProgressDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCircularProgressDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebProgressBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:CircularProgress");
		metadata.item("__tagNameWC", "String:igc-circular-progress");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebProgressBaseDescriptionMetadata.register(context);
		WebCircularProgressDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCircularProgress", () => new WebCircularProgressDescription());
		context.register("WebCircularProgress", WebCircularProgressDescriptionMetadata._metadata);
	}
}


