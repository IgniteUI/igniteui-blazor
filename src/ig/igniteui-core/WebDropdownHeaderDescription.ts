import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDropdownHeaderDescription extends Description {
	static $t: Type = markType(WebDropdownHeaderDescription, 'WebDropdownHeaderDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDropdownHeader";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


