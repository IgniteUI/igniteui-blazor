import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTreeDescription extends Description {
	static $t: Type = markType(WebTreeDescription, 'WebTreeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTree";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _singleBranchExpand: boolean = false;
	get singleBranchExpand(): boolean {
		return this._singleBranchExpand;
	}
	set singleBranchExpand(value: boolean) {
		this._singleBranchExpand = value;
		this.markDirty("SingleBranchExpand");
	}
	private _toggleNodeOnClick: boolean = false;
	get toggleNodeOnClick(): boolean {
		return this._toggleNodeOnClick;
	}
	set toggleNodeOnClick(value: boolean) {
		this._toggleNodeOnClick = value;
		this.markDirty("ToggleNodeOnClick");
	}
	private _selection: string = null;
	get selection(): string {
		return this._selection;
	}
	set selection(value: string) {
		this._selection = value;
		this.markDirty("Selection");
	}
	private _selectionChanged: string = null;
	get selectionChangedRef(): string {
		return this._selectionChanged;
	}
	set selectionChangedRef(value: string) {
		this._selectionChanged = value;
		this.markDirty("SelectionChangedRef");
	}
	private _itemExpanding: string = null;
	get itemExpandingRef(): string {
		return this._itemExpanding;
	}
	set itemExpandingRef(value: string) {
		this._itemExpanding = value;
		this.markDirty("ItemExpandingRef");
	}
	private _itemExpanded: string = null;
	get itemExpandedRef(): string {
		return this._itemExpanded;
	}
	set itemExpandedRef(value: string) {
		this._itemExpanded = value;
		this.markDirty("ItemExpandedRef");
	}
	private _itemCollapsing: string = null;
	get itemCollapsingRef(): string {
		return this._itemCollapsing;
	}
	set itemCollapsingRef(value: string) {
		this._itemCollapsing = value;
		this.markDirty("ItemCollapsingRef");
	}
	private _itemCollapsed: string = null;
	get itemCollapsedRef(): string {
		return this._itemCollapsed;
	}
	set itemCollapsedRef(value: string) {
		this._itemCollapsed = value;
		this.markDirty("ItemCollapsedRef");
	}
	private _activeItem: string = null;
	get activeItemRef(): string {
		return this._activeItem;
	}
	set activeItemRef(value: string) {
		this._activeItem = value;
		this.markDirty("ActiveItemRef");
	}
}


