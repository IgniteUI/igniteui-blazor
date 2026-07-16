import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNavDrawerDescription extends Description {
	static $t: Type = markType(WebNavDrawerDescription, 'WebNavDrawerDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNavDrawer";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _position: string = null;
	get position(): string {
		return this._position;
	}
	set position(value: string) {
		this._position = value;
		this.markDirty("Position");
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
	private _keepOpenOnEscape: boolean = false;
	get keepOpenOnEscape(): boolean {
		return this._keepOpenOnEscape;
	}
	set keepOpenOnEscape(value: boolean) {
		this._keepOpenOnEscape = value;
		this.markDirty("KeepOpenOnEscape");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
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


