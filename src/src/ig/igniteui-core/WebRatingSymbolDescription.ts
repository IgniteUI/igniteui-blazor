import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRatingSymbolDescription extends Description {
	static $t: Type = markType(WebRatingSymbolDescription, 'WebRatingSymbolDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRatingSymbol";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


