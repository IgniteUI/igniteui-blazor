import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNumberEventArgsDescription } from "./WebNumberEventArgsDescription";

/**
 * @hidden 
 */
export class WebNumberEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebNumberEventArgsDescriptionMetadata, 'WebNumberEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebNumberEventArgsDescriptionMetadata._metadata == null) {
			WebNumberEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebNumberEventArgsDescriptionMetadata.fillMetadata(WebNumberEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebNumberEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebNumberEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:NumberEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebNumberEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebNumberEventArgs", () => new WebNumberEventArgsDescription());
		context.register("WebNumberEventArgs", WebNumberEventArgsDescriptionMetadata._metadata);
	}
}


