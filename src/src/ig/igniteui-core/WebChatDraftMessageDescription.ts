import { Description } from "./Description";
import { WebChatMessageAttachmentDescription } from "./WebChatMessageAttachmentDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatDraftMessageDescription extends Description {
	static $t: Type = markType(WebChatDraftMessageDescription, 'WebChatDraftMessageDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatDraftMessage";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatDraftMessage";
	constructor() {
		super();
	}
	private _text: string = null;
	get text(): string {
		return this._text;
	}
	set text(value: string) {
		this._text = value;
		this.markDirty("Text");
	}
	private _attachments: WebChatMessageAttachmentDescription[] = null;
	get attachments(): WebChatMessageAttachmentDescription[] {
		return this._attachments;
	}
	set attachments(value: WebChatMessageAttachmentDescription[]) {
		this._attachments = value;
		this.markDirty("Attachments");
	}
}


