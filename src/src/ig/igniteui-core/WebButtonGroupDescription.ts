import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebButtonGroupDescription extends Description {
	static $t: Type = markType(WebButtonGroupDescription, 'WebButtonGroupDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebButtonGroup";
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
	private _alignment: string = null;
	get alignment(): string {
		return this._alignment;
	}
	set alignment(value: string) {
		this._alignment = value;
		this.markDirty("Alignment");
	}
	private _selection: string = null;
	get selection(): string {
		return this._selection;
	}
	set selection(value: string) {
		this._selection = value;
		this.markDirty("Selection");
	}
	private _selectedItems: string[] = null;
	get selectedItems(): string[] {
		return this._selectedItems;
	}
	set selectedItems(value: string[]) {
		this._selectedItems = value;
		this.markDirty("SelectedItems");
	}
	private _select: string = null;
	get selectRef(): string {
		return this._select;
	}
	set selectRef(value: string) {
		this._select = value;
		this.markDirty("SelectRef");
	}
	private _deselect: string = null;
	get deselectRef(): string {
		return this._deselect;
	}
	set deselectRef(value: string) {
		this._deselect = value;
		this.markDirty("DeselectRef");
	}
}


