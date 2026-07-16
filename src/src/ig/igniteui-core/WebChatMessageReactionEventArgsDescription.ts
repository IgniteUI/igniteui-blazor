import { Description } from "./Description";
import { WebChatMessageReactionDescription } from "./WebChatMessageReactionDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageReactionEventArgsDescription extends Description {
	static $t: Type = markType(WebChatMessageReactionEventArgsDescription, 'WebChatMessageReactionEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessageReactionEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessageReactionEventArgs";
	constructor() {
		super();
	}
	private _detail: WebChatMessageReactionDescription = null;
	get detail(): WebChatMessageReactionDescription {
		return this._detail;
	}
	set detail(value: WebChatMessageReactionDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


