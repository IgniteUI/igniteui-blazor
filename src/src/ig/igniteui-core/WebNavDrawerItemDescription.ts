import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNavDrawerItemDescription extends Description {
	static $t: Type = markType(WebNavDrawerItemDescription, 'WebNavDrawerItemDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNavDrawerItem";
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
	private _active: boolean = false;
	get active(): boolean {
		return this._active;
	}
	set active(value: boolean) {
		this._active = value;
		this.markDirty("Active");
	}
}


