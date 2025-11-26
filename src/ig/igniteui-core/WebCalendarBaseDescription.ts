import { Description } from "./Description";
import { DateRangeDescriptorDescription } from "./DateRangeDescriptorDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCalendarBaseDescription extends Description {
	static $t: Type = markType(WebCalendarBaseDescription, 'WebCalendarBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCalendarBase";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _selection: string = null;
	get selection(): string {
		return this._selection;
	}
	set selection(value: string) {
		this._selection = value;
		this.markDirty("Selection");
	}
	private _showWeekNumbers: boolean = false;
	get showWeekNumbers(): boolean {
		return this._showWeekNumbers;
	}
	set showWeekNumbers(value: boolean) {
		this._showWeekNumbers = value;
		this.markDirty("ShowWeekNumbers");
	}
	private _weekStart: string = null;
	get weekStart(): string {
		return this._weekStart;
	}
	set weekStart(value: string) {
		this._weekStart = value;
		this.markDirty("WeekStart");
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
		this.markDirty("Locale");
	}
	private _specialDates: DateRangeDescriptorDescription[] = null;
	get specialDates(): DateRangeDescriptorDescription[] {
		return this._specialDates;
	}
	set specialDates(value: DateRangeDescriptorDescription[]) {
		this._specialDates = value;
		this.markDirty("SpecialDates");
	}
	private _disabledDates: DateRangeDescriptorDescription[] = null;
	get disabledDates(): DateRangeDescriptorDescription[] {
		return this._disabledDates;
	}
	set disabledDates(value: DateRangeDescriptorDescription[]) {
		this._disabledDates = value;
		this.markDirty("DisabledDates");
	}
}


