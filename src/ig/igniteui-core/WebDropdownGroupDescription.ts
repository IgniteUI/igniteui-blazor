import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDropdownGroupDescription extends Description {
	static $t: Type = markType(WebDropdownGroupDescription, 'WebDropdownGroupDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDropdownGroup";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


