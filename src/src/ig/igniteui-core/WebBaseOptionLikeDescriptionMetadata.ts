import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class WebBaseOptionLikeDescriptionMetadata extends Base {
	static $t: Type = markType(WebBaseOptionLikeDescriptionMetadata, 'WebBaseOptionLikeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBaseOptionLikeDescriptionMetadata._metadata == null) {
			WebBaseOptionLikeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBaseOptionLikeDescriptionMetadata.fillMetadata(WebBaseOptionLikeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBaseOptionLikeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBaseOptionLikeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:BaseOptionLike");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Active", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("Selected", "Boolean");
		metadata.item("Value", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseOptionLikeDescriptionMetadata.ensureMetadata(context);
		context.register("WebBaseOptionLike", WebBaseOptionLikeDescriptionMetadata._metadata);
	}
}


