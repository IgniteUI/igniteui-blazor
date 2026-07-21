import { WebProgressBaseDescription } from "./WebProgressBaseDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCircularProgressDescription extends WebProgressBaseDescription {
	static $t: Type = markType(WebCircularProgressDescription, 'WebCircularProgressDescription', (<any>WebProgressBaseDescription).$type);
	protected get_type(): string {
		return "WebCircularProgress";
	}
	constructor() {
		super();
	}
}


