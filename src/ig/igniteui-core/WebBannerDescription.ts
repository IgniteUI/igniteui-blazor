import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebBannerDescription extends Description {
	static $t: Type = markType(WebBannerDescription, 'WebBannerDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBanner";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
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


