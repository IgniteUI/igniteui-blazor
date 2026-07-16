import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebChipDescription extends Description {
	static $t: Type = markType(WebChipDescription, 'WebChipDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebChip";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _removable: boolean = false;
	get removable(): boolean {
		return this._removable;
	}
	set removable(value: boolean) {
		this._removable = value;
		this.markDirty("Removable");
	}
	private _selectable: boolean = false;
	get selectable(): boolean {
		return this._selectable;
	}
	set selectable(value: boolean) {
		this._selectable = value;
		this.markDirty("Selectable");
	}
	private _selected: boolean = false;
	get selected(): boolean {
		return this._selected;
	}
	set selected(value: boolean) {
		this._selected = value;
		this.markDirty("Selected");
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
	private _remove: string = null;
	get removeRef(): string {
		return this._remove;
	}
	set removeRef(value: string) {
		this._remove = value;
		this.markDirty("RemoveRef");
	}
	private _select: string = null;
	get selectRef(): string {
		return this._select;
	}
	set selectRef(value: string) {
		this._select = value;
		this.markDirty("SelectRef");
	}
}


