import { Description } from "./Description";
import { WebChatMessageDescription } from "./WebChatMessageDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageReactionDescription extends Description {
	static $t: Type = markType(WebChatMessageReactionDescription, 'WebChatMessageReactionDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessageReaction";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessageReaction";
	constructor() {
		super();
	}
	private _message: WebChatMessageDescription = null;
	get message(): WebChatMessageDescription {
		return this._message;
	}
	set message(value: WebChatMessageDescription) {
		this._message = value;
		this.markDirty("Message");
	}
	private _reaction: string = null;
	get reaction(): string {
		return this._reaction;
	}
	set reaction(value: string) {
		this._reaction = value;
		this.markDirty("Reaction");
	}
}


