import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSplitterDescription extends Description {
	static $t: Type = markType(WebSplitterDescription, 'WebSplitterDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSplitter";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _orientation: string = null;
	get orientation(): string {
		return this._orientation;
	}
	set orientation(value: string) {
		this._orientation = value;
		this.markDirty("Orientation");
	}
	private _disableCollapse: boolean = false;
	get disableCollapse(): boolean {
		return this._disableCollapse;
	}
	set disableCollapse(value: boolean) {
		this._disableCollapse = value;
		this.markDirty("DisableCollapse");
	}
	private _disableResize: boolean = false;
	get disableResize(): boolean {
		return this._disableResize;
	}
	set disableResize(value: boolean) {
		this._disableResize = value;
		this.markDirty("DisableResize");
	}
	private _hideCollapseButtons: boolean = false;
	get hideCollapseButtons(): boolean {
		return this._hideCollapseButtons;
	}
	set hideCollapseButtons(value: boolean) {
		this._hideCollapseButtons = value;
		this.markDirty("HideCollapseButtons");
	}
	private _hideDragHandle: boolean = false;
	get hideDragHandle(): boolean {
		return this._hideDragHandle;
	}
	set hideDragHandle(value: boolean) {
		this._hideDragHandle = value;
		this.markDirty("HideDragHandle");
	}
	private _startMinSize: string = null;
	get startMinSize(): string {
		return this._startMinSize;
	}
	set startMinSize(value: string) {
		this._startMinSize = value;
		this.markDirty("StartMinSize");
	}
	private _endMinSize: string = null;
	get endMinSize(): string {
		return this._endMinSize;
	}
	set endMinSize(value: string) {
		this._endMinSize = value;
		this.markDirty("EndMinSize");
	}
	private _startMaxSize: string = null;
	get startMaxSize(): string {
		return this._startMaxSize;
	}
	set startMaxSize(value: string) {
		this._startMaxSize = value;
		this.markDirty("StartMaxSize");
	}
	private _endMaxSize: string = null;
	get endMaxSize(): string {
		return this._endMaxSize;
	}
	set endMaxSize(value: string) {
		this._endMaxSize = value;
		this.markDirty("EndMaxSize");
	}
	private _startSize: string = null;
	get startSize(): string {
		return this._startSize;
	}
	set startSize(value: string) {
		this._startSize = value;
		this.markDirty("StartSize");
	}
	private _endSize: string = null;
	get endSize(): string {
		return this._endSize;
	}
	set endSize(value: string) {
		this._endSize = value;
		this.markDirty("EndSize");
	}
	private _resizeStart: string = null;
	get resizeStartRef(): string {
		return this._resizeStart;
	}
	set resizeStartRef(value: string) {
		this._resizeStart = value;
		this.markDirty("ResizeStartRef");
	}
	private _resizing: string = null;
	get resizingRef(): string {
		return this._resizing;
	}
	set resizingRef(value: string) {
		this._resizing = value;
		this.markDirty("ResizingRef");
	}
	private _resizeEnd: string = null;
	get resizeEndRef(): string {
		return this._resizeEnd;
	}
	set resizeEndRef(value: string) {
		this._resizeEnd = value;
		this.markDirty("ResizeEndRef");
	}
}


