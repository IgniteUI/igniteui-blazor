import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRadioChangeEventArgsDetailDescription } from "./WebRadioChangeEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebRadioChangeEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebRadioChangeEventArgsDetailDescriptionMetadata, 'WebRadioChangeEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRadioChangeEventArgsDetailDescriptionMetadata._metadata == null) {
			WebRadioChangeEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRadioChangeEventArgsDetailDescriptionMetadata.fillMetadata(WebRadioChangeEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRadioChangeEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRadioChangeEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RadioChangeEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Checked", "Boolean");
		metadata.item("Value", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebRadioChangeEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRadioChangeEventArgsDetail", () => new WebRadioChangeEventArgsDetailDescription());
		context.register("WebRadioChangeEventArgsDetail", WebRadioChangeEventArgsDetailDescriptionMetadata._metadata);
	}
}


