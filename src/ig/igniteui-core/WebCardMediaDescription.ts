import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCardMediaDescription extends Description {
	static $t: Type = markType(WebCardMediaDescription, 'WebCardMediaDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCardMedia";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


