import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSelectItemDescriptionMetadata } from "./WebSelectItemDescriptionMetadata";
import { WebSelectGroupDescription } from "./WebSelectGroupDescription";

/**
 * @hidden 
 */
export class WebSelectGroupDescriptionMetadata extends Base {
	static $t: Type = markType(WebSelectGroupDescriptionMetadata, 'WebSelectGroupDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSelectGroupDescriptionMetadata._metadata == null) {
			WebSelectGroupDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSelectGroupDescriptionMetadata.fillMetadata(WebSelectGroupDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSelectGroupDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSelectGroupDescriptionMetadata._metadata);
		WebSelectItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SelectGroup");
		metadata.item("__tagNameWC", "String:igc-select-group");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Items", "Array:WebSelectItemDescription:SelectItem");
		metadata.item("Disabled", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebSelectGroupDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSelectGroup", () => new WebSelectGroupDescription());
		context.register("WebSelectGroup", WebSelectGroupDescriptionMetadata._metadata);
	}
}


