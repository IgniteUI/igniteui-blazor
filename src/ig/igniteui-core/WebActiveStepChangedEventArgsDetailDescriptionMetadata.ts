import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebActiveStepChangedEventArgsDetailDescription } from "./WebActiveStepChangedEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebActiveStepChangedEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebActiveStepChangedEventArgsDetailDescriptionMetadata, 'WebActiveStepChangedEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata == null) {
			WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebActiveStepChangedEventArgsDetailDescriptionMetadata.fillMetadata(WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ActiveStepChangedEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Index", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebActiveStepChangedEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebActiveStepChangedEventArgsDetail", () => new WebActiveStepChangedEventArgsDetailDescription());
		context.register("WebActiveStepChangedEventArgsDetail", WebActiveStepChangedEventArgsDetailDescriptionMetadata._metadata);
	}
}


