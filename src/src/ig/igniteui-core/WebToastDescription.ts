import { WebBaseAlertLikeDescription } from "./WebBaseAlertLikeDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebToastDescription extends WebBaseAlertLikeDescription {
	static $t: Type = markType(WebToastDescription, 'WebToastDescription', (<any>WebBaseAlertLikeDescription).$type);
	protected get_type(): string {
		return "WebToast";
	}
	constructor() {
		super();
	}
}


