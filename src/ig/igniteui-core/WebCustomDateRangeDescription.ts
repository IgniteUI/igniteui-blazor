import { Description } from "./Description";
import { WebDateRangeValueDescription } from "./WebDateRangeValueDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCustomDateRangeDescription extends Description {
	static $t: Type = markType(WebCustomDateRangeDescription, 'WebCustomDateRangeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCustomDateRange";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _dateRange: WebDateRangeValueDescription = null;
	get dateRange(): WebDateRangeValueDescription {
		return this._dateRange;
	}
	set dateRange(value: WebDateRangeValueDescription) {
		this._dateRange = value;
		this.markDirty("DateRange");
	}
}


