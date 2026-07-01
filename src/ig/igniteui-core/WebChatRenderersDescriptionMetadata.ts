import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatRenderersDescription } from "./WebChatRenderersDescription";

/**
 * @hidden 
 */
export class WebChatRenderersDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatRenderersDescriptionMetadata, 'WebChatRenderersDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatRenderersDescriptionMetadata._metadata == null) {
			WebChatRenderersDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatRenderersDescriptionMetadata.fillMetadata(WebChatRenderersDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatRenderersDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatRenderersDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatRenderers");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("AttachmentRef", "(w:Attachment,p:Attachment)TemplateRef::object");
		metadata.item("AttachmentContentRef", "(w:AttachmentContent,p:AttachmentContent)TemplateRef::object");
		metadata.item("AttachmentHeaderRef", "(w:AttachmentHeader,p:AttachmentHeader)TemplateRef::object");
		metadata.item("InputRef", "(w:Input,p:Input)TemplateRef::object");
		metadata.item("InputActionsRef", "(w:InputActions,p:InputActions)TemplateRef::object");
		metadata.item("InputActionsEndRef", "(w:InputActionsEnd,p:InputActionsEnd)TemplateRef::object");
		metadata.item("InputActionsStartRef", "(w:InputActionsStart,p:InputActionsStart)TemplateRef::object");
		metadata.item("MessageRef", "(w:Message,p:Message)TemplateRef::object");
		metadata.item("MessageActionsRef", "(w:MessageActions,p:MessageActions)TemplateRef::object");
		metadata.item("MessageAttachmentsRef", "(w:MessageAttachments,p:MessageAttachments)TemplateRef::object");
		metadata.item("MessageContentRef", "(w:MessageContent,p:MessageContent)TemplateRef::object");
		metadata.item("MessageHeaderRef", "(w:MessageHeader,p:MessageHeader)TemplateRef::object");
		metadata.item("SendButtonRef", "(w:SendButton,p:SendButton)TemplateRef::object");
		metadata.item("SuggestionPrefixRef", "(w:SuggestionPrefix,p:SuggestionPrefix)TemplateRef::object");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatRenderersDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatRenderers", () => new WebChatRenderersDescription());
		context.register("WebChatRenderers", WebChatRenderersDescriptionMetadata._metadata);
	}
}


