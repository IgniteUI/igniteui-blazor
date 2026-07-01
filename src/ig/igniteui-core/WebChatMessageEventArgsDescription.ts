import { Description } from "./Description";
import { WebChatMessageDescription } from "./WebChatMessageDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageEventArgsDescription extends Description {
	static $t: Type = markType(WebChatMessageEventArgsDescription, 'WebChatMessageEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessageEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessageEventArgs";
	constructor() {
		super();
	}
	private _detail: WebChatMessageDescription = null;
	get detail(): WebChatMessageDescription {
		return this._detail;
	}
	set detail(value: WebChatMessageDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


