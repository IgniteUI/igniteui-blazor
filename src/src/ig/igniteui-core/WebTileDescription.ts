import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTileDescription extends Description {
	static $t: Type = markType(WebTileDescription, 'WebTileDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTile";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _colSpan: number = 0;
	get colSpan(): number {
		return this._colSpan;
	}
	set colSpan(value: number) {
		this._colSpan = value;
		this.markDirty("ColSpan");
	}
	private _rowSpan: number = 0;
	get rowSpan(): number {
		return this._rowSpan;
	}
	set rowSpan(value: number) {
		this._rowSpan = value;
		this.markDirty("RowSpan");
	}
	private _colStart: number = 0;
	get colStart(): number {
		return this._colStart;
	}
	set colStart(value: number) {
		this._colStart = value;
		this.markDirty("ColStart");
	}
	private _rowStart: number = 0;
	get rowStart(): number {
		return this._rowStart;
	}
	set rowStart(value: number) {
		this._rowStart = value;
		this.markDirty("RowStart");
	}
	private _maximized: boolean = false;
	get maximized(): boolean {
		return this._maximized;
	}
	set maximized(value: boolean) {
		this._maximized = value;
		this.markDirty("Maximized");
	}
	private _disableResize: boolean = false;
	get disableResize(): boolean {
		return this._disableResize;
	}
	set disableResize(value: boolean) {
		this._disableResize = value;
		this.markDirty("DisableResize");
	}
	private _disableFullscreen: boolean = false;
	get disableFullscreen(): boolean {
		return this._disableFullscreen;
	}
	set disableFullscreen(value: boolean) {
		this._disableFullscreen = value;
		this.markDirty("DisableFullscreen");
	}
	private _disableMaximize: boolean = false;
	get disableMaximize(): boolean {
		return this._disableMaximize;
	}
	set disableMaximize(value: boolean) {
		this._disableMaximize = value;
		this.markDirty("DisableMaximize");
	}
	private _position: number = 0;
	get position(): number {
		return this._position;
	}
	set position(value: number) {
		this._position = value;
		this.markDirty("Position");
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


