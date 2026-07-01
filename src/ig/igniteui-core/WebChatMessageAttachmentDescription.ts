import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatMessageAttachmentDescription extends Description {
	static $t: Type = markType(WebChatMessageAttachmentDescription, 'WebChatMessageAttachmentDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatMessageAttachment";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ChatMessageAttachment";
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
	private _url: string = null;
	get url(): string {
		return this._url;
	}
	set url(value: string) {
		this._url = value;
		this.markDirty("Url");
	}
	private _attachmentType: string = null;
	get attachmentType(): string {
		return this._attachmentType;
	}
	set attachmentType(value: string) {
		this._attachmentType = value;
		this.markDirty("AttachmentType");
	}
	private _thumbnail: string = null;
	get thumbnail(): string {
		return this._thumbnail;
	}
	set thumbnail(value: string) {
		this._thumbnail = value;
		this.markDirty("Thumbnail");
	}
}


