import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBadgeDescription } from "./WebBadgeDescription";

/**
 * @hidden 
 */
export class WebBadgeDescriptionMetadata extends Base {
	static $t: Type = markType(WebBadgeDescriptionMetadata, 'WebBadgeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBadgeDescriptionMetadata._metadata == null) {
			WebBadgeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBadgeDescriptionMetadata.fillMetadata(WebBadgeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBadgeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBadgeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Badge");
		metadata.item("__tagNameWC", "String:igc-badge");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Variant", "ExportedType:string:StyleVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Primary;Info;Success;Warning;Danger");
		metadata.item("Outlined", "Boolean");
		metadata.item("Shape", "ExportedType:string:BadgeShape");
		metadata.item("Shape@stringUnion", "WebComponents;React");
		metadata.item("Shape@names", "Rounded;Square");
	}
	static register(context: TypeDescriptionContext): void {
		WebBadgeDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebBadge", () => new WebBadgeDescription());
		context.register("WebBadge", WebBadgeDescriptionMetadata._metadata);
	}
}


