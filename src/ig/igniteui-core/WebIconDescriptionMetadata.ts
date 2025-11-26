import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebIconMetaDescriptionMetadata } from "./WebIconMetaDescriptionMetadata";
import { Dictionary$2 } from "./Dictionary$2";
import { WebIconDescription } from "./WebIconDescription";

/**
 * @hidden 
 */
export class WebIconDescriptionMetadata extends Base {
	static $t: Type = markType(WebIconDescriptionMetadata, 'WebIconDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebIconDescriptionMetadata._metadata == null) {
			WebIconDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebIconDescriptionMetadata.fillMetadata(WebIconDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebIconDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebIconDescriptionMetadata._metadata);
		WebIconDescriptionMetadata.registerOtherTypes(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Icon");
		metadata.item("__tagNameWC", "String:igc-icon");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("IconName", "(wc:Name)String");
		metadata.item("Collection", "String");
		metadata.item("Mirrored", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebIconDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebIcon", () => new WebIconDescription());
		context.register("WebIcon", WebIconDescriptionMetadata._metadata);
	}
	private static registerOtherTypes(context: TypeDescriptionContext): void {
		WebIconMetaDescriptionMetadata.register(context);
	}
}


