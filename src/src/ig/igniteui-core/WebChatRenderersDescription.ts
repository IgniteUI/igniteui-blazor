import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatRenderersDescription extends Description {
	static $t: Type = markType(WebChatRenderersDescription, 'WebChatRenderersDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatRenderers";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _attachmentRef: string = null;
	get attachmentRef(): string {
		return this._attachmentRef;
	}
	set attachmentRef(value: string) {
		this._attachmentRef = value;
		this.markDirty("AttachmentRef");
	}
	private _attachmentContentRef: string = null;
	get attachmentContentRef(): string {
		return this._attachmentContentRef;
	}
	set attachmentContentRef(value: string) {
		this._attachmentContentRef = value;
		this.markDirty("AttachmentContentRef");
	}
	private _attachmentHeaderRef: string = null;
	get attachmentHeaderRef(): string {
		return this._attachmentHeaderRef;
	}
	set attachmentHeaderRef(value: string) {
		this._attachmentHeaderRef = value;
		this.markDirty("AttachmentHeaderRef");
	}
	private _inputRef: string = null;
	get inputRef(): string {
		return this._inputRef;
	}
	set inputRef(value: string) {
		this._inputRef = value;
		this.markDirty("InputRef");
	}
	private _inputActionsRef: string = null;
	get inputActionsRef(): string {
		return this._inputActionsRef;
	}
	set inputActionsRef(value: string) {
		this._inputActionsRef = value;
		this.markDirty("InputActionsRef");
	}
	private _inputActionsEndRef: string = null;
	get inputActionsEndRef(): string {
		return this._inputActionsEndRef;
	}
	set inputActionsEndRef(value: string) {
		this._inputActionsEndRef = value;
		this.markDirty("InputActionsEndRef");
	}
	private _inputActionsStartRef: string = null;
	get inputActionsStartRef(): string {
		return this._inputActionsStartRef;
	}
	set inputActionsStartRef(value: string) {
		this._inputActionsStartRef = value;
		this.markDirty("InputActionsStartRef");
	}
	private _messageRef: string = null;
	get messageRef(): string {
		return this._messageRef;
	}
	set messageRef(value: string) {
		this._messageRef = value;
		this.markDirty("MessageRef");
	}
	private _messageActionsRef: string = null;
	get messageActionsRef(): string {
		return this._messageActionsRef;
	}
	set messageActionsRef(value: string) {
		this._messageActionsRef = value;
		this.markDirty("MessageActionsRef");
	}
	private _messageAttachmentsRef: string = null;
	get messageAttachmentsRef(): string {
		return this._messageAttachmentsRef;
	}
	set messageAttachmentsRef(value: string) {
		this._messageAttachmentsRef = value;
		this.markDirty("MessageAttachmentsRef");
	}
	private _messageContentRef: string = null;
	get messageContentRef(): string {
		return this._messageContentRef;
	}
	set messageContentRef(value: string) {
		this._messageContentRef = value;
		this.markDirty("MessageContentRef");
	}
	private _messageHeaderRef: string = null;
	get messageHeaderRef(): string {
		return this._messageHeaderRef;
	}
	set messageHeaderRef(value: string) {
		this._messageHeaderRef = value;
		this.markDirty("MessageHeaderRef");
	}
	private _sendButtonRef: string = null;
	get sendButtonRef(): string {
		return this._sendButtonRef;
	}
	set sendButtonRef(value: string) {
		this._sendButtonRef = value;
		this.markDirty("SendButtonRef");
	}
	private _suggestionPrefixRef: string = null;
	get suggestionPrefixRef(): string {
		return this._suggestionPrefixRef;
	}
	set suggestionPrefixRef(value: string) {
		this._suggestionPrefixRef = value;
		this.markDirty("SuggestionPrefixRef");
	}
}


