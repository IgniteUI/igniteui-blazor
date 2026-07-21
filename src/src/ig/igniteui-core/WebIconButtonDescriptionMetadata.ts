import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebButtonBaseDescriptionMetadata } from "./WebButtonBaseDescriptionMetadata";
import { WebIconButtonDescription } from "./WebIconButtonDescription";

/**
 * @hidden 
 */
export class WebIconButtonDescriptionMetadata extends Base {
	static $t: Type = markType(WebIconButtonDescriptionMetadata, 'WebIconButtonDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebIconButtonDescriptionMetadata._metadata == null) {
			WebIconButtonDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebIconButtonDescriptionMetadata.fillMetadata(WebIconButtonDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebIconButtonDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebIconButtonDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebButtonBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:IconButton");
		metadata.item("__tagNameWC", "String:igc-icon-button");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("IconName", "(wc:Name)String");
		metadata.item("Collection", "String");
		metadata.item("Mirrored", "Boolean");
		metadata.item("Variant", "ExportedType:string:IconButtonVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Contained;Flat;Outlined");
	}
	static register(context: TypeDescriptionContext): void {
		WebButtonBaseDescriptionMetadata.register(context);
		WebIconButtonDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebIconButton", () => new WebIconButtonDescription());
		context.register("WebIconButton", WebIconButtonDescriptionMetadata._metadata);
	}
}


