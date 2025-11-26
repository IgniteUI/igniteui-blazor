import { WebBaseComboBoxLikeDescription } from "./WebBaseComboBoxLikeDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDropdownDescription extends WebBaseComboBoxLikeDescription {
	static $t: Type = markType(WebDropdownDescription, 'WebDropdownDescription', (<any>WebBaseComboBoxLikeDescription).$type);
	protected get_type(): string {
		return "WebDropdown";
	}
	constructor() {
		super();
	}
	private _placement: string = null;
	get placement(): string {
		return this._placement;
	}
	set placement(value: string) {
		this._placement = value;
		this.markDirty("Placement");
	}
	private _scrollStrategy: string = null;
	get scrollStrategy(): string {
		return this._scrollStrategy;
	}
	set scrollStrategy(value: string) {
		this._scrollStrategy = value;
		this.markDirty("ScrollStrategy");
	}
	private _flip: boolean = false;
	get flip(): boolean {
		return this._flip;
	}
	set flip(value: boolean) {
		this._flip = value;
		this.markDirty("Flip");
	}
	private _distance: number = 0;
	get distance(): number {
		return this._distance;
	}
	set distance(value: number) {
		this._distance = value;
		this.markDirty("Distance");
	}
	private _sameWidth: boolean = false;
	get sameWidth(): boolean {
		return this._sameWidth;
	}
	set sameWidth(value: boolean) {
		this._sameWidth = value;
		this.markDirty("SameWidth");
	}
	private _opening: string = null;
	get openingRef(): string {
		return this._opening;
	}
	set openingRef(value: string) {
		this._opening = value;
		this.markDirty("OpeningRef");
	}
	private _opened: string = null;
	get openedRef(): string {
		return this._opened;
	}
	set openedRef(value: string) {
		this._opened = value;
		this.markDirty("OpenedRef");
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
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


