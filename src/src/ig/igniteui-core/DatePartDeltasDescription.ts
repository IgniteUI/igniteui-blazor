import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DatePartDeltasDescription extends Description {
	static $t: Type = markType(DatePartDeltasDescription, 'DatePartDeltasDescription', (<any>Description).$type);
	protected get_type(): string {
		return "DatePartDeltas";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _date: number = 0;
	get date(): number {
		return this._date;
	}
	set date(value: number) {
		this._date = value;
		this.markDirty("Date");
	}
	private _month: number = 0;
	get month(): number {
		return this._month;
	}
	set month(value: number) {
		this._month = value;
		this.markDirty("Month");
	}
	private _year: number = 0;
	get year(): number {
		return this._year;
	}
	set year(value: number) {
		this._year = value;
		this.markDirty("Year");
	}
	private _hours: number = 0;
	get hours(): number {
		return this._hours;
	}
	set hours(value: number) {
		this._hours = value;
		this.markDirty("Hours");
	}
	private _minutes: number = 0;
	get minutes(): number {
		return this._minutes;
	}
	set minutes(value: number) {
		this._minutes = value;
		this.markDirty("Minutes");
	}
	private _seconds: number = 0;
	get seconds(): number {
		return this._seconds;
	}
	set seconds(value: number) {
		this._seconds = value;
		this.markDirty("Seconds");
	}
}


