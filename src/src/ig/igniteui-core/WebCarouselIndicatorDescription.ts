import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCarouselIndicatorDescription extends Description {
	static $t: Type = markType(WebCarouselIndicatorDescription, 'WebCarouselIndicatorDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCarouselIndicator";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


