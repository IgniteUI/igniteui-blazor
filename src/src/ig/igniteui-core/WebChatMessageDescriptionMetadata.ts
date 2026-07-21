import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageAttachmentDescriptionMetadata } from "./WebChatMessageAttachmentDescriptionMetadata";
import { WebChatMessageDescription } from "./WebChatMessageDescription";

/**
 * @hidden 
 */
export class WebChatMessageDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageDescriptionMetadata, 'WebChatMessageDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageDescriptionMetadata._metadata == null) {
			WebChatMessageDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageDescriptionMetadata.fillMetadata(WebChatMessageDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageDescriptionMetadata._metadata);
		WebChatMessageAttachmentDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessage");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Id", "String");
		metadata.item("Text", "String");
		metadata.item("Sender", "String");
		metadata.item("Timestamp", "String");
		metadata.item("Attachments", "Array:WebChatMessageAttachmentDescription:ChatMessageAttachment");
		metadata.item("Reactions", "Array:string");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessage", () => new WebChatMessageDescription());
		context.register("WebChatMessage", WebChatMessageDescriptionMetadata._metadata);
	}
}


