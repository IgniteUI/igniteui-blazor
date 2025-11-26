import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangePickerResourceStringsDescription extends Description {
	static $t: Type = markType(WebDateRangePickerResourceStringsDescription, 'WebDateRangePickerResourceStringsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDateRangePickerResourceStrings";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


