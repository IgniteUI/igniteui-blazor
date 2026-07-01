import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageAttachmentDescriptionMetadata } from "./WebChatMessageAttachmentDescriptionMetadata";
import { WebChatMessageAttachmentEventArgsDescription } from "./WebChatMessageAttachmentEventArgsDescription";

/**
 * @hidden 
 */
export class WebChatMessageAttachmentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageAttachmentEventArgsDescriptionMetadata, 'WebChatMessageAttachmentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata == null) {
			WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageAttachmentEventArgsDescriptionMetadata.fillMetadata(WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata);
		WebChatMessageAttachmentDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessageAttachmentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebChatMessageAttachment");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageAttachmentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessageAttachmentEventArgs", () => new WebChatMessageAttachmentEventArgsDescription());
		context.register("WebChatMessageAttachmentEventArgs", WebChatMessageAttachmentEventArgsDescriptionMetadata._metadata);
	}
}


