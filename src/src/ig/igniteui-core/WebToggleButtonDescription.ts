import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebToggleButtonDescription extends Description {
	static $t: Type = markType(WebToggleButtonDescription, 'WebToggleButtonDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebToggleButton";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
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


