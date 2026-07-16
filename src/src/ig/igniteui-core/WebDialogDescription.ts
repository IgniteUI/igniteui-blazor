import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDialogDescription extends Description {
	static $t: Type = markType(WebDialogDescription, 'WebDialogDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDialog";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _keepOpenOnEscape: boolean = false;
	get keepOpenOnEscape(): boolean {
		return this._keepOpenOnEscape;
	}
	set keepOpenOnEscape(value: boolean) {
		this._keepOpenOnEscape = value;
		this.markDirty("KeepOpenOnEscape");
	}
	private _closeOnOutsideClick: boolean = false;
	get closeOnOutsideClick(): boolean {
		return this._closeOnOutsideClick;
	}
	set closeOnOutsideClick(value: boolean) {
		this._closeOnOutsideClick = value;
		this.markDirty("CloseOnOutsideClick");
	}
	private _hideDefaultAction: boolean = false;
	get hideDefaultAction(): boolean {
		return this._hideDefaultAction;
	}
	set hideDefaultAction(value: boolean) {
		this._hideDefaultAction = value;
		this.markDirty("HideDefaultAction");
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
	private _title: string = null;
	get title(): string {
		return this._title;
	}
	set title(value: string) {
		this._title = value;
		this.markDirty("Title");
	}
	private _returnValue: string = null;
	get returnValue(): string {
		return this._returnValue;
	}
	set returnValue(value: string) {
		this._returnValue = value;
		this.markDirty("ReturnValue");
	}
	private _closing: string = null;
	get closingRef(): string {
		return this._closing;
	}
	set closingRef(value: string) {
		this._closing = value;
		this.markDirty("ClosingRef");
	}
	private _closed: string = null;
	get closedRef(): string {
		return this._closed;
	}
	set closedRef(value: string) {
		this._closed = value;
		this.markDirty("ClosedRef");
	}
}


