import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCardContentDescription extends Description {
	static $t: Type = markType(WebCardContentDescription, 'WebCardContentDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCardContent";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


