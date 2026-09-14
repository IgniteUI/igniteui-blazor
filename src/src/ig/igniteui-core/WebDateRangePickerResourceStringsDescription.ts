import { WebCalendarResourceStringsDescription } from "./WebCalendarResourceStringsDescription";
import { Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangePickerResourceStringsDescription extends WebCalendarResourceStringsDescription {
	static $t: Type = markType(WebDateRangePickerResourceStringsDescription, 'WebDateRangePickerResourceStringsDescription', (<any>WebCalendarResourceStringsDescription).$type);
	protected get_type(): string {
		return "WebDateRangePickerResourceStrings";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _separator: string = null!;
	get separator(): string {
		return this._separator;
	}
	set separator(value: string) {
		this._separator = value;
		this.markDirty("Separator");
	}
	private _doneButton: string = null!;
	get doneButton(): string {
		return this._doneButton;
	}
	set doneButton(value: string) {
		this._doneButton = value;
		this.markDirty("DoneButton");
	}
	private _cancelButton: string = null!;
	get cancelButton(): string {
		return this._cancelButton;
	}
	set cancelButton(value: string) {
		this._cancelButton = value;
		this.markDirty("CancelButton");
	}
	private _last7Days: string = null!;
	get last7Days(): string {
		return this._last7Days;
	}
	set last7Days(value: string) {
		this._last7Days = value;
		this.markDirty("Last7Days");
	}
	private _last30Days: string = null!;
	get last30Days(): string {
		return this._last30Days;
	}
	set last30Days(value: string) {
		this._last30Days = value;
		this.markDirty("Last30Days");
	}
	private _currentMonth: string = null!;
	get currentMonth(): string {
		return this._currentMonth;
	}
	set currentMonth(value: string) {
		this._currentMonth = value;
		this.markDirty("CurrentMonth");
	}
	private _yearToDate: string = null!;
	get yearToDate(): string {
		return this._yearToDate;
	}
	set yearToDate(value: string) {
		this._yearToDate = value;
		this.markDirty("YearToDate");
	}
}


