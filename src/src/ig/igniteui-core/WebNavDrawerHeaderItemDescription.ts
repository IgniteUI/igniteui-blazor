import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNavDrawerHeaderItemDescription extends Description {
	static $t: Type = markType(WebNavDrawerHeaderItemDescription, 'WebNavDrawerHeaderItemDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNavDrawerHeaderItem";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


