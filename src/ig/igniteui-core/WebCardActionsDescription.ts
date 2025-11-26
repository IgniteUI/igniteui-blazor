import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCardActionsDescription extends Description {
	static $t: Type = markType(WebCardActionsDescription, 'WebCardActionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCardActions";
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
}


