import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebListDescription extends Description {
	static $t: Type = markType(WebListDescription, 'WebListDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebList";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


