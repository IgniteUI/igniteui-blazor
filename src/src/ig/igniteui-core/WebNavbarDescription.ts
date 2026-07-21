import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNavbarDescription extends Description {
	static $t: Type = markType(WebNavbarDescription, 'WebNavbarDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNavbar";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


