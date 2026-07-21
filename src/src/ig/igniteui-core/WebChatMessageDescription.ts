import { Description } from "./Description";
import { WebChatMessageAttachmentDescription } from "./WebChatMessageAttachmentDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageDescription extends Description {
	static $t: Type = markType(WebChatMessageDescription, 'WebChatMessageDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessage";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessage";
	constructor() {
		super();
	}
	private _id: string = null;
	get id(): string {
		return this._id;
	}
	set id(value: string) {
		this._id = value;
		this.markDirty("Id");
	}
	private _text: string = null;
	get text(): string {
		return this._text;
	}
	set text(value: string) {
		this._text = value;
		this.markDirty("Text");
	}
	private _sender: string = null;
	get sender(): string {
		return this._sender;
	}
	set sender(value: string) {
		this._sender = value;
		this.markDirty("Sender");
	}
	private _timestamp: string = null;
	get timestamp(): string {
		return this._timestamp;
	}
	set timestamp(value: string) {
		this._timestamp = value;
		this.markDirty("Timestamp");
	}
	private _attachments: WebChatMessageAttachmentDescription[] = null;
	get attachments(): WebChatMessageAttachmentDescription[] {
		return this._attachments;
	}
	set attachments(value: WebChatMessageAttachmentDescription[]) {
		this._attachments = value;
		this.markDirty("Attachments");
	}
	private _reactions: string[] = null;
	get reactions(): string[] {
		return this._reactions;
	}
	set reactions(value: string[]) {
		this._reactions = value;
		this.markDirty("Reactions");
	}
}


