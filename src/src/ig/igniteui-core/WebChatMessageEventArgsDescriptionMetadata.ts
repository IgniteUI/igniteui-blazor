import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageDescriptionMetadata } from "./WebChatMessageDescriptionMetadata";
import { WebChatMessageEventArgsDescription } from "./WebChatMessageEventArgsDescription";

/**
 * @hidden 
 */
export class WebChatMessageEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageEventArgsDescriptionMetadata, 'WebChatMessageEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageEventArgsDescriptionMetadata._metadata == null) {
			WebChatMessageEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageEventArgsDescriptionMetadata.fillMetadata(WebChatMessageEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageEventArgsDescriptionMetadata._metadata);
		WebChatMessageDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessageEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebChatMessage");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessageEventArgs", () => new WebChatMessageEventArgsDescription());
		context.register("WebChatMessageEventArgs", WebChatMessageEventArgsDescriptionMetadata._metadata);
	}
}


