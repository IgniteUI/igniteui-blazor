import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTabDescriptionMetadata } from "./WebTabDescriptionMetadata";
import { WebTabComponentEventArgsDescription } from "./WebTabComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebTabComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTabComponentEventArgsDescriptionMetadata, 'WebTabComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTabComponentEventArgsDescriptionMetadata._metadata == null) {
			WebTabComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTabComponentEventArgsDescriptionMetadata.fillMetadata(WebTabComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTabComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTabComponentEventArgsDescriptionMetadata._metadata);
		WebTabDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TabComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebTab");
	}
	static register(context: TypeDescriptionContext): void {
		WebTabComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTabComponentEventArgs", () => new WebTabComponentEventArgsDescription());
		context.register("WebTabComponentEventArgs", WebTabComponentEventArgsDescriptionMetadata._metadata);
	}
}


