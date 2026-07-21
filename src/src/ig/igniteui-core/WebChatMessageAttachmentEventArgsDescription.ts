import { Description } from "./Description";
import { WebChatMessageAttachmentDescription } from "./WebChatMessageAttachmentDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageAttachmentEventArgsDescription extends Description {
	static $t: Type = markType(WebChatMessageAttachmentEventArgsDescription, 'WebChatMessageAttachmentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessageAttachmentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessageAttachmentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebChatMessageAttachmentDescription = null;
	get detail(): WebChatMessageAttachmentDescription {
		return this._detail;
	}
	set detail(value: WebChatMessageAttachmentDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


