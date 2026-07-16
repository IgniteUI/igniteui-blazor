import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatMessageDescriptionMetadata } from "./WebChatMessageDescriptionMetadata";
import { WebChatMessageReactionDescription } from "./WebChatMessageReactionDescription";

/**
 * @hidden 
 */
export class WebChatMessageReactionDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatMessageReactionDescriptionMetadata, 'WebChatMessageReactionDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatMessageReactionDescriptionMetadata._metadata == null) {
			WebChatMessageReactionDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatMessageReactionDescriptionMetadata.fillMetadata(WebChatMessageReactionDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatMessageReactionDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatMessageReactionDescriptionMetadata._metadata);
		WebChatMessageDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatMessageReaction");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Message", "ExportedType:WebChatMessage");
		metadata.item("Reaction", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatMessageReactionDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatMessageReaction", () => new WebChatMessageReactionDescription());
		context.register("WebChatMessageReaction", WebChatMessageReactionDescriptionMetadata._metadata);
	}
}


