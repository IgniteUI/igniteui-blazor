import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSliderLabelDescription extends Description {
	static $t: Type = markType(WebSliderLabelDescription, 'WebSliderLabelDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSliderLabel";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


