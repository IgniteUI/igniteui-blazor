import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden
 */
export class WebVirtualScrollDescription extends Description {
	static $t: Type = markType(WebVirtualScrollDescription, 'WebVirtualScrollDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebVirtualScroll";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _dataRef: string = null;
	get dataRef(): string {
		return this._dataRef;
	}
	set dataRef(value: string) {
		this._dataRef = value;
		this.markDirty("DataRef");
	}
	private _orientation: string = null;
	get orientation(): string {
		return this._orientation;
	}
	set orientation(value: string) {
		this._orientation = value;
		this.markDirty("Orientation");
	}
	private _overScan: number = 0;
	get overScan(): number {
		return this._overScan;
	}
	set overScan(value: number) {
		this._overScan = value;
		this.markDirty("OverScan");
	}
	private _estimatedItemSize: number = 0;
	get estimatedItemSize(): number {
		return this._estimatedItemSize;
	}
	set estimatedItemSize(value: number) {
		this._estimatedItemSize = value;
		this.markDirty("EstimatedItemSize");
	}
	private _itemTemplate: string = null;
	get itemTemplateRef(): string {
		return this._itemTemplate;
	}
	set itemTemplateRef(value: string) {
		this._itemTemplate = value;
		this.markDirty("ItemTemplateRef");
	}
	private _stateChange: string = null;
	get stateChangeRef(): string {
		return this._stateChange;
	}
	set stateChangeRef(value: string) {
		this._stateChange = value;
		this.markDirty("StateChangeRef");
	}
	private _dataRequest: string = null;
	get dataRequestRef(): string {
		return this._dataRequest;
	}
	set dataRequestRef(value: string) {
		this._dataRequest = value;
		this.markDirty("DataRequestRef");
	}
}


