import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescription } from "./WebComponentValueChangedEventArgsDescription";

/**
 * @hidden 
 */
export class WebComponentValueChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComponentValueChangedEventArgsDescriptionMetadata, 'WebComponentValueChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComponentValueChangedEventArgsDescriptionMetadata._metadata == null) {
			WebComponentValueChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComponentValueChangedEventArgsDescriptionMetadata.fillMetadata(WebComponentValueChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComponentValueChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComponentValueChangedEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComponentValueChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebComponentValueChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComponentValueChangedEventArgs", () => new WebComponentValueChangedEventArgsDescription());
		context.register("WebComponentValueChangedEventArgs", WebComponentValueChangedEventArgsDescriptionMetadata._metadata);
	}
}


