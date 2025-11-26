import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebBaseOptionLikeDescription extends Description {
	static $t: Type = markType(WebBaseOptionLikeDescription, 'WebBaseOptionLikeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBaseOptionLike";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
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
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
}


