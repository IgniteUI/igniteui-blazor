import { WebBaseOptionLikeDescription } from "./WebBaseOptionLikeDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSelectItemDescription extends WebBaseOptionLikeDescription {
	static $t: Type = markType(WebSelectItemDescription, 'WebSelectItemDescription', (<any>WebBaseOptionLikeDescription).$type);
	protected get_type(): string {
		return "WebSelectItem";
	}
	constructor() {
		super();
	}
}


