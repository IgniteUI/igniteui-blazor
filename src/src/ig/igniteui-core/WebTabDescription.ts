import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTabDescription extends Description {
	static $t: Type = markType(WebTabDescription, 'WebTabDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTab";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _selected: boolean = false;
	get selected(): boolean {
		return this._selected;
	}
	set selected(value: boolean) {
		this._selected = value;
		this.markDirty("Selected");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
}


