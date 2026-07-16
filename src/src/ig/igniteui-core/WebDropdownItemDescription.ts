import { WebBaseOptionLikeDescription } from "./WebBaseOptionLikeDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDropdownItemDescription extends WebBaseOptionLikeDescription {
	static $t: Type = markType(WebDropdownItemDescription, 'WebDropdownItemDescription', (<any>WebBaseOptionLikeDescription).$type);
	protected get_type(): string {
		return "WebDropdownItem";
	}
	constructor() {
		super();
	}
}


