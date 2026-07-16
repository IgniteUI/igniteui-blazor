import { WebCheckboxBaseDescription } from "./WebCheckboxBaseDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSwitchDescription extends WebCheckboxBaseDescription {
	static $t: Type = markType(WebSwitchDescription, 'WebSwitchDescription', (<any>WebCheckboxBaseDescription).$type);
	protected get_type(): string {
		return "WebSwitch";
	}
	constructor() {
		super();
	}
}


