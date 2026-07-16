import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentDataValueChangedEventArgsDescription } from "./WebComponentDataValueChangedEventArgsDescription";

/**
 * @hidden 
 */
export class WebComponentDataValueChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebComponentDataValueChangedEventArgsDescriptionMetadata, 'WebComponentDataValueChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata == null) {
			WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComponentDataValueChangedEventArgsDescriptionMetadata.fillMetadata(WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComponentDataValueChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Detail", "Unknown");
	}
	static register(context: TypeDescriptionContext): void {
		WebComponentDataValueChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComponentDataValueChangedEventArgs", () => new WebComponentDataValueChangedEventArgsDescription());
		context.register("WebComponentDataValueChangedEventArgs", WebComponentDataValueChangedEventArgsDescriptionMetadata._metadata);
	}
}


