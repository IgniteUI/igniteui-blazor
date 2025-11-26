import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebFilteringOptionsDescription extends Description {
	static $t: Type = markType(WebFilteringOptionsDescription, 'WebFilteringOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebFilteringOptions";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


