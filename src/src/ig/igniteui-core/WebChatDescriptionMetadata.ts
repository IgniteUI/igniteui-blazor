import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageDescriptionMetadata } from "./WebChatMessageDescriptionMetadata";
import { WebChatDraftMessageDescriptionMetadata } from "./WebChatDraftMessageDescriptionMetadata";
import { WebChatOptionsDescriptionMetadata } from "./WebChatOptionsDescriptionMetadata";
import { WebChatMessageEventArgsDescriptionMetadata } from "./WebChatMessageEventArgsDescriptionMetadata";
import { WebChatMessageReactionEventArgsDescriptionMetadata } from "./WebChatMessageReactionEventArgsDescriptionMetadata";
import { WebChatMessageAttachmentEventArgsDescriptionMetadata } from "./WebChatMessageAttachmentEventArgsDescriptionMetadata";
import { WebComponentBoolValueChangedEventArgsDescriptionMetadata } from "./WebComponentBoolValueChangedEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { WebChatDescription } from "./WebChatDescription";

/**
 * @hidden 
 */
export class WebChatDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatDescriptionMetadata, 'WebChatDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatDescriptionMetadata._metadata == null) {
			WebChatDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatDescriptionMetadata.fillMetadata(WebChatDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatDescriptionMetadata._metadata);
		WebChatMessageDescriptionMetadata.register(context);
		WebChatDraftMessageDescriptionMetadata.register(context);
		WebChatOptionsDescriptionMetadata.register(context);
		WebChatMessageEventArgsDescriptionMetadata.register(context);
		WebChatMessageReactionEventArgsDescriptionMetadata.register(context);
		WebChatMessageAttachmentEventArgsDescriptionMetadata.register(context);
		WebComponentBoolValueChangedEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Chat");
		metadata.item("__tagNameWC", "String:igc-chat");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Messages", "Array:WebChatMessageDescription:ChatMessage");
		metadata.item("DraftMessage", "ExportedType:WebChatDraftMessage");
		metadata.item("Options", "ExportedType:WebChatOptions");
		metadata.item("MessageCreatedRef", "EventRef:ChatMessageEventHandler:messageCreated");
		metadata.item("MessageCreatedRef@args", "ChatMessageEventArgs");
		metadata.item("MessageReactRef", "EventRef:ChatMessageReactionEventHandler:messageReact");
		metadata.item("MessageReactRef@args", "ChatMessageReactionEventArgs");
		metadata.item("AttachmentClickRef", "EventRef:ChatMessageAttachmentEventHandler:attachmentClick");
		metadata.item("AttachmentClickRef@args", "ChatMessageAttachmentEventArgs");
		metadata.item("TypingChangeRef", "EventRef:ComponentBoolValueChangedEventHandler:typingChange");
		metadata.item("TypingChangeRef@args", "ComponentBoolValueChangedEventArgs");
		metadata.item("InputFocusRef", "EventRef:VoidHandler:inputFocus");
		metadata.item("InputFocusRef@args", "VoidEventArgs");
		metadata.item("InputBlurRef", "EventRef:VoidHandler:inputBlur");
		metadata.item("InputBlurRef@args", "VoidEventArgs");
		metadata.item("InputChangeRef", "EventRef:ComponentValueChangedEventHandler:inputChange");
		metadata.item("InputChangeRef@args", "ComponentValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChat", () => new WebChatDescription());
		context.register("WebChat", WebChatDescriptionMetadata._metadata);
	}
}


