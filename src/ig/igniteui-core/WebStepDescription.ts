import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebStepDescription extends Description {
	static $t: Type = markType(WebStepDescription, 'WebStepDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebStep";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _invalid: boolean = false;
	get invalid(): boolean {
		return this._invalid;
	}
	set invalid(value: boolean) {
		this._invalid = value;
		this.markDirty("Invalid");
	}
	private _active: boolean = false;
	get active(): boolean {
		return this._active;
	}
	set active(value: boolean) {
		this._active = value;
		this.markDirty("Active");
	}
	private _optional: boolean = false;
	get optional(): boolean {
		return this._optional;
	}
	set optional(value: boolean) {
		this._optional = value;
		this.markDirty("Optional");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _complete: boolean = false;
	get complete(): boolean {
		return this._complete;
	}
	set complete(value: boolean) {
		this._complete = value;
		this.markDirty("Complete");
	}
}


