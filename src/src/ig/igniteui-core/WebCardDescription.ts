import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCardDescription extends Description {
	static $t: Type = markType(WebCardDescription, 'WebCardDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCard";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _elevated: boolean = false;
	get elevated(): boolean {
		return this._elevated;
	}
	set elevated(value: boolean) {
		this._elevated = value;
		this.markDirty("Elevated");
	}
}


