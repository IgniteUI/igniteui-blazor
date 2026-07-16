import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSelectHeaderDescription extends Description {
	static $t: Type = markType(WebSelectHeaderDescription, 'WebSelectHeaderDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSelectHeader";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


