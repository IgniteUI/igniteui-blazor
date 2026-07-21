import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSelectItemDescriptionMetadata } from "./WebSelectItemDescriptionMetadata";
import { WebSelectItemComponentEventArgsDescription } from "./WebSelectItemComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebSelectItemComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebSelectItemComponentEventArgsDescriptionMetadata, 'WebSelectItemComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSelectItemComponentEventArgsDescriptionMetadata._metadata == null) {
			WebSelectItemComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSelectItemComponentEventArgsDescriptionMetadata.fillMetadata(WebSelectItemComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSelectItemComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSelectItemComponentEventArgsDescriptionMetadata._metadata);
		WebSelectItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SelectItemComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebSelectItem");
	}
	static register(context: TypeDescriptionContext): void {
		WebSelectItemComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSelectItemComponentEventArgs", () => new WebSelectItemComponentEventArgsDescription());
		context.register("WebSelectItemComponentEventArgs", WebSelectItemComponentEventArgsDescriptionMetadata._metadata);
	}
}


