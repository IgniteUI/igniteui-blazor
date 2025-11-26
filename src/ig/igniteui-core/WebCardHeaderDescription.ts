import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCardHeaderDescription extends Description {
	static $t: Type = markType(WebCardHeaderDescription, 'WebCardHeaderDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCardHeader";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


