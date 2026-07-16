import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentDateValueChangedEventArgsDescription } from "./WebComponentDateValueChangedEventArgsDescription";

/**
 * @hidden 
 */
export class WebComponentDateValueChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComponentDateValueChangedEventArgsDescriptionMetadata, 'WebComponentDateValueChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata == null) {
			WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComponentDateValueChangedEventArgsDescriptionMetadata.fillMetadata(WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComponentDateValueChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "Date");
	}
	static register(context: TypeDescriptionContext): void {
		WebComponentDateValueChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComponentDateValueChangedEventArgs", () => new WebComponentDateValueChangedEventArgsDescription());
		context.register("WebComponentDateValueChangedEventArgs", WebComponentDateValueChangedEventArgsDescriptionMetadata._metadata);
	}
}


