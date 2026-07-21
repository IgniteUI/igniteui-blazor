import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DateRangeDescriptorDescription extends Description {
	static $t: Type = markType(DateRangeDescriptorDescription, 'DateRangeDescriptorDescription', (<any>Description).$type);
	protected get_type(): string {
		return "DateRangeDescriptor";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _rangeType: string = null;
	get rangeType(): string {
		return this._rangeType;
	}
	set rangeType(value: string) {
		this._rangeType = value;
		this.markDirty("RangeType");
	}
	private _dateRange: any = null;
	get dateRange(): any {
		return this._dateRange;
	}
	set dateRange(value: any) {
		this._dateRange = value;
		this.markDirty("DateRange");
	}
}


