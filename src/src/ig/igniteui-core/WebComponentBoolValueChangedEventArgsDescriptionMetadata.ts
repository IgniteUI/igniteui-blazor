import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentBoolValueChangedEventArgsDescription } from "./WebComponentBoolValueChangedEventArgsDescription";

/**
 * @hidden 
 */
export class WebComponentBoolValueChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComponentBoolValueChangedEventArgsDescriptionMetadata, 'WebComponentBoolValueChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata == null) {
			WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComponentBoolValueChangedEventArgsDescriptionMetadata.fillMetadata(WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComponentBoolValueChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebComponentBoolValueChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComponentBoolValueChangedEventArgs", () => new WebComponentBoolValueChangedEventArgsDescription());
		context.register("WebComponentBoolValueChangedEventArgs", WebComponentBoolValueChangedEventArgsDescriptionMetadata._metadata);
	}
}


