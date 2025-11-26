import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRippleDescription extends Description {
	static $t: Type = markType(WebRippleDescription, 'WebRippleDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRipple";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


