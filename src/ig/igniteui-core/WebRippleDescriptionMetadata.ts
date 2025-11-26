import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRippleDescription } from "./WebRippleDescription";

/**
 * @hidden 
 */
export class WebRippleDescriptionMetadata extends Base {
	static $t: Type = markType(WebRippleDescriptionMetadata, 'WebRippleDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRippleDescriptionMetadata._metadata == null) {
			WebRippleDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRippleDescriptionMetadata.fillMetadata(WebRippleDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRippleDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRippleDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Ripple");
		metadata.item("__tagNameWC", "String:igc-ripple");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebRippleDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRipple", () => new WebRippleDescription());
		context.register("WebRipple", WebRippleDescriptionMetadata._metadata);
	}
}


