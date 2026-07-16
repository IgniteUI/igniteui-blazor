import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebActiveStepChangingEventArgsDetailDescriptionMetadata } from "./WebActiveStepChangingEventArgsDetailDescriptionMetadata";
import { WebActiveStepChangingEventArgsDescription } from "./WebActiveStepChangingEventArgsDescription";

/**
 * @hidden 
 */
export class WebActiveStepChangingEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebActiveStepChangingEventArgsDescriptionMetadata, 'WebActiveStepChangingEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebActiveStepChangingEventArgsDescriptionMetadata._metadata == null) {
			WebActiveStepChangingEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebActiveStepChangingEventArgsDescriptionMetadata.fillMetadata(WebActiveStepChangingEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebActiveStepChangingEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebActiveStepChangingEventArgsDescriptionMetadata._metadata);
		WebActiveStepChangingEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ActiveStepChangingEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebActiveStepChangingEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebActiveStepChangingEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebActiveStepChangingEventArgs", () => new WebActiveStepChangingEventArgsDescription());
		context.register("WebActiveStepChangingEventArgs", WebActiveStepChangingEventArgsDescriptionMetadata._metadata);
	}
}


