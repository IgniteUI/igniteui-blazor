import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebListHeaderDescription extends Description {
	static $t: Type = markType(WebListHeaderDescription, 'WebListHeaderDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebListHeader";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


