import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDividerDescription } from "./WebDividerDescription";

/**
 * @hidden 
 */
export class WebDividerDescriptionMetadata extends Base {
	static $t: Type = markType(WebDividerDescriptionMetadata, 'WebDividerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDividerDescriptionMetadata._metadata == null) {
			WebDividerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDividerDescriptionMetadata.fillMetadata(WebDividerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDividerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDividerDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Divider");
		metadata.item("__tagNameWC", "String:igc-divider");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Vertical", "Boolean");
		metadata.item("Middle", "Boolean");
		metadata.item("LineType", "(wc:Type)ExportedType:string:DividerType");
		metadata.item("LineType@stringUnion", "WebComponents;React");
		metadata.item("LineType@names", "Solid;Dashed");
	}
	static register(context: TypeDescriptionContext): void {
		WebDividerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDivider", () => new WebDividerDescription());
		context.register("WebDivider", WebDividerDescriptionMetadata._metadata);
	}
}


