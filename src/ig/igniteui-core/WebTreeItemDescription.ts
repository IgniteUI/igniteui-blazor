import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTreeItemDescription extends Description {
	static $t: Type = markType(WebTreeItemDescription, 'WebTreeItemDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTreeItem";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _parent: WebTreeItemDescription = null;
	get parent(): WebTreeItemDescription {
		return this._parent;
	}
	set parent(value: WebTreeItemDescription) {
		this._parent = value;
		this.markDirty("Parent");
	}
	private _level: number = 0;
	get level(): number {
		return this._level;
	}
	set level(value: number) {
		this._level = value;
		this.markDirty("Level");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _expanded: boolean = false;
	get expanded(): boolean {
		return this._expanded;
	}
	set expanded(value: boolean) {
		this._expanded = value;
		this.markDirty("Expanded");
	}
	private _active: boolean = false;
	get active(): boolean {
		return this._active;
	}
	set active(value: boolean) {
		this._active = value;
		this.markDirty("Active");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _selected: boolean = false;
	get selected(): boolean {
		return this._selected;
	}
	set selected(value: boolean) {
		this._selected = value;
		this.markDirty("Selected");
	}
	private _loading: boolean = false;
	get loading(): boolean {
		return this._loading;
	}
	set loading(value: boolean) {
		this._loading = value;
		this.markDirty("Loading");
	}
	private _value: any = null;
	get value(): any {
		return this._value;
	}
	set value(value: any) {
		this._value = value;
		this.markDirty("Value");
	}
}


