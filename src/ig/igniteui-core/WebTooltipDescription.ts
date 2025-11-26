import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTooltipDescription extends Description {
	static $t: Type = markType(WebTooltipDescription, 'WebTooltipDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTooltip";
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
	private _disableArrow: boolean = false;
	get disableArrow(): boolean {
		return this._disableArrow;
	}
	set disableArrow(value: boolean) {
		this._disableArrow = value;
		this.markDirty("DisableArrow");
	}
	private _withArrow: boolean = false;
	get withArrow(): boolean {
		return this._withArrow;
	}
	set withArrow(value: boolean) {
		this._withArrow = value;
		this.markDirty("WithArrow");
	}
	private _offset: number = 0;
	get offset(): number {
		return this._offset;
	}
	set offset(value: number) {
		this._offset = value;
		this.markDirty("Offset");
	}
	private _placement: string = null;
	get placement(): string {
		return this._placement;
	}
	set placement(value: string) {
		this._placement = value;
		this.markDirty("Placement");
	}
	private _anchor: string = null;
	get anchor(): string {
		return this._anchor;
	}
	set anchor(value: string) {
		this._anchor = value;
		this.markDirty("Anchor");
	}
	private _showTriggers: string = null;
	get showTriggers(): string {
		return this._showTriggers;
	}
	set showTriggers(value: string) {
		this._showTriggers = value;
		this.markDirty("ShowTriggers");
	}
	private _hideTriggers: string = null;
	get hideTriggers(): string {
		return this._hideTriggers;
	}
	set hideTriggers(value: string) {
		this._hideTriggers = value;
		this.markDirty("HideTriggers");
	}
	private _showDelay: number = 0;
	get showDelay(): number {
		return this._showDelay;
	}
	set showDelay(value: number) {
		this._showDelay = value;
		this.markDirty("ShowDelay");
	}
	private _hideDelay: number = 0;
	get hideDelay(): number {
		return this._hideDelay;
	}
	set hideDelay(value: number) {
		this._hideDelay = value;
		this.markDirty("HideDelay");
	}
	private _message: string = null;
	get message(): string {
		return this._message;
	}
	set message(value: string) {
		this._message = value;
		this.markDirty("Message");
	}
	private _sticky: boolean = false;
	get sticky(): boolean {
		return this._sticky;
	}
	set sticky(value: boolean) {
		this._sticky = value;
		this.markDirty("Sticky");
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
}


