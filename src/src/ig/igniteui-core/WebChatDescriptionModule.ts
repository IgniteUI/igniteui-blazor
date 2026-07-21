import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebChatMessageEventArgsDescriptionMetadata } from "./WebChatMessageEventArgsDescriptionMetadata";
import { WebChatMessageReactionEventArgsDescriptionMetadata } from "./WebChatMessageReactionEventArgsDescriptionMetadata";
import { WebChatMessageAttachmentEventArgsDescriptionMetadata } from "./WebChatMessageAttachmentEventArgsDescriptionMetadata";
import { WebChatDescriptionMetadata } from "./WebChatDescriptionMetadata";

/**
 * @hidden 
 */
export class WebChatDescriptionModule extends Base {
	static $t: Type = markType(WebChatDescriptionModule, 'WebChatDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebChatMessageEventArgsDescriptionMetadata.register(context);
		WebChatMessageReactionEventArgsDescriptionMetadata.register(context);
		WebChatMessageAttachmentEventArgsDescriptionMetadata.register(context);
		WebChatDescriptionMetadata.register(context);
	}
}


