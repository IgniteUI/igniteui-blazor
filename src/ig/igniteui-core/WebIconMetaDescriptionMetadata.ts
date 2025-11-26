import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebIconMetaDescription } from "./WebIconMetaDescription";

/**
 * @hidden 
 */
export class WebIconMetaDescriptionMetadata extends Base {
	static $t: Type = markType(WebIconMetaDescriptionMetadata, 'WebIconMetaDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebIconMetaDescriptionMetadata._metadata == null) {
			WebIconMetaDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebIconMetaDescriptionMetadata.fillMetadata(WebIconMetaDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebIconMetaDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebIconMetaDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:IconMeta");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("__isTSPlainInterface", "Boolean");
		metadata.item("Name", "String");
		metadata.item("Collection", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebIconMetaDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebIconMeta", () => new WebIconMetaDescription());
		context.register("WebIconMeta", WebIconMetaDescriptionMetadata._metadata);
	}
}


