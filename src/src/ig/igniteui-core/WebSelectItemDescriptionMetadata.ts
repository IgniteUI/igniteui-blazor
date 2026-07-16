import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBaseOptionLikeDescriptionMetadata } from "./WebBaseOptionLikeDescriptionMetadata";
import { WebSelectItemDescription } from "./WebSelectItemDescription";

/**
 * @hidden 
 */
export class WebSelectItemDescriptionMetadata extends Base {
	static $t: Type = markType(WebSelectItemDescriptionMetadata, 'WebSelectItemDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSelectItemDescriptionMetadata._metadata == null) {
			WebSelectItemDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSelectItemDescriptionMetadata.fillMetadata(WebSelectItemDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSelectItemDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSelectItemDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseOptionLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:SelectItem");
		metadata.item("__tagNameWC", "String:igc-select-item");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseOptionLikeDescriptionMetadata.register(context);
		WebSelectItemDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSelectItem", () => new WebSelectItemDescription());
		context.register("WebSelectItem", WebSelectItemDescriptionMetadata._metadata);
	}
}


