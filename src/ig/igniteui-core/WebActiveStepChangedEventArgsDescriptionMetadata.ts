import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebActiveStepChangedEventArgsDetailDescriptionMetadata } from "./WebActiveStepChangedEventArgsDetailDescriptionMetadata";
import { WebActiveStepChangedEventArgsDescription } from "./WebActiveStepChangedEventArgsDescription";

/**
 * @hidden 
 */
export class WebActiveStepChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebActiveStepChangedEventArgsDescriptionMetadata, 'WebActiveStepChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebActiveStepChangedEventArgsDescriptionMetadata._metadata == null) {
			WebActiveStepChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebActiveStepChangedEventArgsDescriptionMetadata.fillMetadata(WebActiveStepChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebActiveStepChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebActiveStepChangedEventArgsDescriptionMetadata._metadata);
		WebActiveStepChangedEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ActiveStepChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebActiveStepChangedEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebActiveStepChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebActiveStepChangedEventArgs", () => new WebActiveStepChangedEventArgsDescription());
		context.register("WebActiveStepChangedEventArgs", WebActiveStepChangedEventArgsDescriptionMetadata._metadata);
	}
}


