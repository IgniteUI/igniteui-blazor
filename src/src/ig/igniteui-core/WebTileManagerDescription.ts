import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTileManagerDescription extends Description {
	static $t: Type = markType(WebTileManagerDescription, 'WebTileManagerDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTileManager";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _resizeMode: string = null;
	get resizeMode(): string {
		return this._resizeMode;
	}
	set resizeMode(value: string) {
		this._resizeMode = value;
		this.markDirty("ResizeMode");
	}
	private _dragMode: string = null;
	get dragMode(): string {
		return this._dragMode;
	}
	set dragMode(value: string) {
		this._dragMode = value;
		this.markDirty("DragMode");
	}
	private _columnCount: number = 0;
	get columnCount(): number {
		return this._columnCount;
	}
	set columnCount(value: number) {
		this._columnCount = value;
		this.markDirty("ColumnCount");
	}
	private _minColumnWidth: string = null;
	get minColumnWidth(): string {
		return this._minColumnWidth;
	}
	set minColumnWidth(value: string) {
		this._minColumnWidth = value;
		this.markDirty("MinColumnWidth");
	}
	private _minRowHeight: string = null;
	get minRowHeight(): string {
		return this._minRowHeight;
	}
	set minRowHeight(value: string) {
		this._minRowHeight = value;
		this.markDirty("MinRowHeight");
	}
	private _gap: string = null;
	get gap(): string {
		return this._gap;
	}
	set gap(value: string) {
		this._gap = value;
		this.markDirty("Gap");
	}
	private _tileFullscreen: string = null;
	get tileFullscreenRef(): string {
		return this._tileFullscreen;
	}
	set tileFullscreenRef(value: string) {
		this._tileFullscreen = value;
		this.markDirty("TileFullscreenRef");
	}
	private _tileMaximize: string = null;
	get tileMaximizeRef(): string {
		return this._tileMaximize;
	}
	set tileMaximizeRef(value: string) {
		this._tileMaximize = value;
		this.markDirty("TileMaximizeRef");
	}
	private _tileDragStart: string = null;
	get tileDragStartRef(): string {
		return this._tileDragStart;
	}
	set tileDragStartRef(value: string) {
		this._tileDragStart = value;
		this.markDirty("TileDragStartRef");
	}
	private _tileDragEnd: string = null;
	get tileDragEndRef(): string {
		return this._tileDragEnd;
	}
	set tileDragEndRef(value: string) {
		this._tileDragEnd = value;
		this.markDirty("TileDragEndRef");
	}
	private _tileDragCancel: string = null;
	get tileDragCancelRef(): string {
		return this._tileDragCancel;
	}
	set tileDragCancelRef(value: string) {
		this._tileDragCancel = value;
		this.markDirty("TileDragCancelRef");
	}
	private _tileResizeStart: string = null;
	get tileResizeStartRef(): string {
		return this._tileResizeStart;
	}
	set tileResizeStartRef(value: string) {
		this._tileResizeStart = value;
		this.markDirty("TileResizeStartRef");
	}
	private _tileResizeEnd: string = null;
	get tileResizeEndRef(): string {
		return this._tileResizeEnd;
	}
	set tileResizeEndRef(value: string) {
		this._tileResizeEnd = value;
		this.markDirty("TileResizeEndRef");
	}
	private _tileResizeCancel: string = null;
	get tileResizeCancelRef(): string {
		return this._tileResizeCancel;
	}
	set tileResizeCancelRef(value: string) {
		this._tileResizeCancel = value;
		this.markDirty("TileResizeCancelRef");
	}
}


