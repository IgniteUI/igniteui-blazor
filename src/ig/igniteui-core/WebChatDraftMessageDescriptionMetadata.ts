import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageAttachmentDescriptionMetadata } from "./WebChatMessageAttachmentDescriptionMetadata";
import { WebChatDraftMessageDescription } from "./WebChatDraftMessageDescription";

/**
 * @hidden 
 */
export class WebChatDraftMessageDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatDraftMessageDescriptionMetadata, 'WebChatDraftMessageDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatDraftMessageDescriptionMetadata._metadata == null) {
			WebChatDraftMessageDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatDraftMessageDescriptionMetadata.fillMetadata(WebChatDraftMessageDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatDraftMessageDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatDraftMessageDescriptionMetadata._metadata);
		WebChatMessageAttachmentDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatDraftMessage");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Text", "String");
		metadata.item("Attachments", "Array:WebChatMessageAttachmentDescription:ChatMessageAttachment");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatDraftMessageDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatDraftMessage", () => new WebChatDraftMessageDescription());
		context.register("WebChatDraftMessage", WebChatDraftMessageDescriptionMetadata._metadata);
	}
}


