import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCalendarResourceStringsDescription extends Description {
	static $t: Type = markType(WebCalendarResourceStringsDescription, 'WebCalendarResourceStringsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCalendarResourceStrings";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _selectMonth: string = null;
	get selectMonth(): string {
		return this._selectMonth;
	}
	set selectMonth(value: string) {
		this._selectMonth = value;
		this.markDirty("SelectMonth");
	}
	private _selectYear: string = null;
	get selectYear(): string {
		return this._selectYear;
	}
	set selectYear(value: string) {
		this._selectYear = value;
		this.markDirty("SelectYear");
	}
	private _selectDate: string = null;
	get selectDate(): string {
		return this._selectDate;
	}
	set selectDate(value: string) {
		this._selectDate = value;
		this.markDirty("SelectDate");
	}
	private _selectRange: string = null;
	get selectRange(): string {
		return this._selectRange;
	}
	set selectRange(value: string) {
		this._selectRange = value;
		this.markDirty("SelectRange");
	}
	private _selectedDate: string = null;
	get selectedDate(): string {
		return this._selectedDate;
	}
	set selectedDate(value: string) {
		this._selectedDate = value;
		this.markDirty("SelectedDate");
	}
	private _startDate: string = null;
	get startDate(): string {
		return this._startDate;
	}
	set startDate(value: string) {
		this._startDate = value;
		this.markDirty("StartDate");
	}
	private _endDate: string = null;
	get endDate(): string {
		return this._endDate;
	}
	set endDate(value: string) {
		this._endDate = value;
		this.markDirty("EndDate");
	}
	private _previousMonth: string = null;
	get previousMonth(): string {
		return this._previousMonth;
	}
	set previousMonth(value: string) {
		this._previousMonth = value;
		this.markDirty("PreviousMonth");
	}
	private _nextMonth: string = null;
	get nextMonth(): string {
		return this._nextMonth;
	}
	set nextMonth(value: string) {
		this._nextMonth = value;
		this.markDirty("NextMonth");
	}
	private _previousYear: string = null;
	get previousYear(): string {
		return this._previousYear;
	}
	set previousYear(value: string) {
		this._previousYear = value;
		this.markDirty("PreviousYear");
	}
	private _nextYear: string = null;
	get nextYear(): string {
		return this._nextYear;
	}
	set nextYear(value: string) {
		this._nextYear = value;
		this.markDirty("NextYear");
	}
	private _previousYears: string = null;
	get previousYears(): string {
		return this._previousYears;
	}
	set previousYears(value: string) {
		this._previousYears = value;
		this.markDirty("PreviousYears");
	}
	private _nextYears: string = null;
	get nextYears(): string {
		return this._nextYears;
	}
	set nextYears(value: string) {
		this._nextYears = value;
		this.markDirty("NextYears");
	}
	private _weekLabel: string = null;
	get weekLabel(): string {
		return this._weekLabel;
	}
	set weekLabel(value: string) {
		this._weekLabel = value;
		this.markDirty("WeekLabel");
	}
}


