import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class CalendarFormatOptionsDescription extends Description {
	static $t: Type = markType(CalendarFormatOptionsDescription, 'CalendarFormatOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "CalendarFormatOptions";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "CalendarFormatOptions";
	constructor() {
		super();
	}
	private _weekday: string = null;
	get weekday(): string {
		return this._weekday;
	}
	set weekday(value: string) {
		this._weekday = value;
		this.markDirty("Weekday");
	}
	private _month: string = null;
	get month(): string {
		return this._month;
	}
	set month(value: string) {
		this._month = value;
		this.markDirty("Month");
	}
}


