import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageReactionDescriptionMetadata } from "./WebChatMessageReactionDescriptionMetadata";
import { WebChatMessageReactionEventArgsDescription } from "./WebChatMessageReactionEventArgsDescription";

/**
 * @hidden 
 */
export class WebChatMessageReactionEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageReactionEventArgsDescriptionMetadata, 'WebChatMessageReactionEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageReactionEventArgsDescriptionMetadata._metadata == null) {
			WebChatMessageReactionEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageReactionEventArgsDescriptionMetadata.fillMetadata(WebChatMessageReactionEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageReactionEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageReactionEventArgsDescriptionMetadata._metadata);
		WebChatMessageReactionDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessageReactionEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebChatMessageReaction");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageReactionEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessageReactionEventArgs", () => new WebChatMessageReactionEventArgsDescription());
		context.register("WebChatMessageReactionEventArgs", WebChatMessageReactionEventArgsDescriptionMetadata._metadata);
	}
}


