import { Description } from "./Description";
import { WebChatMessageDescription } from "./WebChatMessageDescription";
import { WebChatDraftMessageDescription } from "./WebChatDraftMessageDescription";
import { WebChatOptionsDescription } from "./WebChatOptionsDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatDescription extends Description {
	static $t: Type = markType(WebChatDescription, 'WebChatDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChat";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _messages: WebChatMessageDescription[] = null;
	get messages(): WebChatMessageDescription[] {
		return this._messages;
	}
	set messages(value: WebChatMessageDescription[]) {
		this._messages = value;
		this.markDirty("Messages");
	}
	private _draftMessage: WebChatDraftMessageDescription = null;
	get draftMessage(): WebChatDraftMessageDescription {
		return this._draftMessage;
	}
	set draftMessage(value: WebChatDraftMessageDescription) {
		this._draftMessage = value;
		this.markDirty("DraftMessage");
	}
	private _options: WebChatOptionsDescription = null;
	get options(): WebChatOptionsDescription {
		return this._options;
	}
	set options(value: WebChatOptionsDescription) {
		this._options = value;
		this.markDirty("Options");
	}
	private _messageCreated: string = null;
	get messageCreatedRef(): string {
		return this._messageCreated;
	}
	set messageCreatedRef(value: string) {
		this._messageCreated = value;
		this.markDirty("MessageCreatedRef");
	}
	private _messageReact: string = null;
	get messageReactRef(): string {
		return this._messageReact;
	}
	set messageReactRef(value: string) {
		this._messageReact = value;
		this.markDirty("MessageReactRef");
	}
	private _attachmentClick: string = null;
	get attachmentClickRef(): string {
		return this._attachmentClick;
	}
	set attachmentClickRef(value: string) {
		this._attachmentClick = value;
		this.markDirty("AttachmentClickRef");
	}
	private _typingChange: string = null;
	get typingChangeRef(): string {
		return this._typingChange;
	}
	set typingChangeRef(value: string) {
		this._typingChange = value;
		this.markDirty("TypingChangeRef");
	}
	private _inputFocus: string = null;
	get inputFocusRef(): string {
		return this._inputFocus;
	}
	set inputFocusRef(value: string) {
		this._inputFocus = value;
		this.markDirty("InputFocusRef");
	}
	private _inputBlur: string = null;
	get inputBlurRef(): string {
		return this._inputBlur;
	}
	set inputBlurRef(value: string) {
		this._inputBlur = value;
		this.markDirty("InputBlurRef");
	}
	private _inputChange: string = null;
	get inputChangeRef(): string {
		return this._inputChange;
	}
	set inputChangeRef(value: string) {
		this._inputChange = value;
		this.markDirty("InputChangeRef");
	}
}


