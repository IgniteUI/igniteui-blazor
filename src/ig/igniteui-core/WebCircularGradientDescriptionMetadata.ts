import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCircularGradientDescription } from "./WebCircularGradientDescription";

/**
 * @hidden 
 */
export class WebCircularGradientDescriptionMetadata extends Base {
	static $t: Type = markType(WebCircularGradientDescriptionMetadata, 'WebCircularGradientDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCircularGradientDescriptionMetadata._metadata == null) {
			WebCircularGradientDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCircularGradientDescriptionMetadata.fillMetadata(WebCircularGradientDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCircularGradientDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCircularGradientDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CircularGradient");
		metadata.item("__tagNameWC", "String:igc-circular-gradient");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Offset", "String");
		metadata.item("Color", "String");
		metadata.item("Opacity", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebCircularGradientDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCircularGradient", () => new WebCircularGradientDescription());
		context.register("WebCircularGradient", WebCircularGradientDescriptionMetadata._metadata);
	}
}


