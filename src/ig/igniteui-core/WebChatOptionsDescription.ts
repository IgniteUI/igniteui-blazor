import { Description } from "./Description";
import { WebChatRenderersDescription } from "./WebChatRenderersDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChatOptionsDescription extends Description {
	static $t: Type = markType(WebChatOptionsDescription, 'WebChatOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChatOptions";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _currentUserId: string = null;
	get currentUserId(): string {
		return this._currentUserId;
	}
	set currentUserId(value: string) {
		this._currentUserId = value;
		this.markDirty("CurrentUserId");
	}
	private _disableAutoScroll: boolean = false;
	get disableAutoScroll(): boolean {
		return this._disableAutoScroll;
	}
	set disableAutoScroll(value: boolean) {
		this._disableAutoScroll = value;
		this.markDirty("DisableAutoScroll");
	}
	private _disableInputAttachments: boolean = false;
	get disableInputAttachments(): boolean {
		return this._disableInputAttachments;
	}
	set disableInputAttachments(value: boolean) {
		this._disableInputAttachments = value;
		this.markDirty("DisableInputAttachments");
	}
	private _isTyping: boolean = false;
	get isTyping(): boolean {
		return this._isTyping;
	}
	set isTyping(value: boolean) {
		this._isTyping = value;
		this.markDirty("IsTyping");
	}
	private _headerText: string = null;
	get headerText(): string {
		return this._headerText;
	}
	set headerText(value: string) {
		this._headerText = value;
		this.markDirty("HeaderText");
	}
	private _inputPlaceholder: string = null;
	get inputPlaceholder(): string {
		return this._inputPlaceholder;
	}
	set inputPlaceholder(value: string) {
		this._inputPlaceholder = value;
		this.markDirty("InputPlaceholder");
	}
	private _suggestions: string[] = null;
	get suggestions(): string[] {
		return this._suggestions;
	}
	set suggestions(value: string[]) {
		this._suggestions = value;
		this.markDirty("Suggestions");
	}
	private _suggestionsPosition: string = null;
	get suggestionsPosition(): string {
		return this._suggestionsPosition;
	}
	set suggestionsPosition(value: string) {
		this._suggestionsPosition = value;
		this.markDirty("SuggestionsPosition");
	}
	private _stopTypingDelay: number = 0;
	get stopTypingDelay(): number {
		return this._stopTypingDelay;
	}
	set stopTypingDelay(value: number) {
		this._stopTypingDelay = value;
		this.markDirty("StopTypingDelay");
	}
	private _adoptRootStyles: boolean = false;
	get adoptRootStyles(): boolean {
		return this._adoptRootStyles;
	}
	set adoptRootStyles(value: boolean) {
		this._adoptRootStyles = value;
		this.markDirty("AdoptRootStyles");
	}
	private _renderers: WebChatRenderersDescription = null;
	get renderers(): WebChatRenderersDescription {
		return this._renderers;
	}
	set renderers(value: WebChatRenderersDescription) {
		this._renderers = value;
		this.markDirty("Renderers");
	}
}


