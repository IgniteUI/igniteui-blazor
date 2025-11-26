import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebActiveStepChangingEventArgsDetailDescription } from "./WebActiveStepChangingEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebActiveStepChangingEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebActiveStepChangingEventArgsDetailDescriptionMetadata, 'WebActiveStepChangingEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata == null) {
			WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebActiveStepChangingEventArgsDetailDescriptionMetadata.fillMetadata(WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ActiveStepChangingEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("OldIndex", "Number:double");
		metadata.item("NewIndex", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebActiveStepChangingEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebActiveStepChangingEventArgsDetail", () => new WebActiveStepChangingEventArgsDetailDescription());
		context.register("WebActiveStepChangingEventArgsDetail", WebActiveStepChangingEventArgsDetailDescriptionMetadata._metadata);
	}
}


