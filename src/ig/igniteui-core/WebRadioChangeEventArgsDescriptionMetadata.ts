import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRadioChangeEventArgsDetailDescriptionMetadata } from "./WebRadioChangeEventArgsDetailDescriptionMetadata";
import { WebRadioChangeEventArgsDescription } from "./WebRadioChangeEventArgsDescription";

/**
 * @hidden 
 */
export class WebRadioChangeEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebRadioChangeEventArgsDescriptionMetadata, 'WebRadioChangeEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRadioChangeEventArgsDescriptionMetadata._metadata == null) {
			WebRadioChangeEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRadioChangeEventArgsDescriptionMetadata.fillMetadata(WebRadioChangeEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRadioChangeEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRadioChangeEventArgsDescriptionMetadata._metadata);
		WebRadioChangeEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RadioChangeEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebRadioChangeEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebRadioChangeEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRadioChangeEventArgs", () => new WebRadioChangeEventArgsDescription());
		context.register("WebRadioChangeEventArgs", WebRadioChangeEventArgsDescriptionMetadata._metadata);
	}
}


