import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageAttachmentDescription } from "./WebChatMessageAttachmentDescription";

/**
 * @hidden 
 */
export class WebChatMessageAttachmentDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageAttachmentDescriptionMetadata, 'WebChatMessageAttachmentDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageAttachmentDescriptionMetadata._metadata == null) {
			WebChatMessageAttachmentDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageAttachmentDescriptionMetadata.fillMetadata(WebChatMessageAttachmentDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageAttachmentDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageAttachmentDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessageAttachment");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Id", "String");
		metadata.item("Name", "String");
		metadata.item("Url", "String");
		metadata.item("AttachmentType", "(wc:Type)String");
		metadata.item("Thumbnail", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageAttachmentDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessageAttachment", () => new WebChatMessageAttachmentDescription());
		context.register("WebChatMessageAttachment", WebChatMessageAttachmentDescriptionMetadata._metadata);
	}
}


