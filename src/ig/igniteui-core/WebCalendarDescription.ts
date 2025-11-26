import { WebCalendarBaseDescription } from "./WebCalendarBaseDescription";
import { Description } from "./Description";
import { CalendarFormatOptionsDescription } from "./CalendarFormatOptionsDescription";
import { WebCalendarResourceStringsDescription } from "./WebCalendarResourceStringsDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCalendarDescription extends WebCalendarBaseDescription {
	static $t: Type = markType(WebCalendarDescription, 'WebCalendarDescription', (<any>WebCalendarBaseDescription).$type);
	protected get_type(): string {
		return "WebCalendar";
	}
	constructor() {
		super();
	}
	private _value: Date = new Date();
	get value(): Date {
		return this._value;
	}
	set value(value: Date) {
		this._value = value;
		this.markDirty("Value");
	}
	private _values: Date[] = null;
	get values(): Date[] {
		return this._values;
	}
	set values(value: Date[]) {
		this._values = value;
		this.markDirty("Values");
	}
	private _activeDate: Date = new Date();
	get activeDate(): Date {
		return this._activeDate;
	}
	set activeDate(value: Date) {
		this._activeDate = value;
		this.markDirty("ActiveDate");
	}
	private _hideOutsideDays: boolean = false;
	get hideOutsideDays(): boolean {
		return this._hideOutsideDays;
	}
	set hideOutsideDays(value: boolean) {
		this._hideOutsideDays = value;
		this.markDirty("HideOutsideDays");
	}
	private _hideHeader: boolean = false;
	get hideHeader(): boolean {
		return this._hideHeader;
	}
	set hideHeader(value: boolean) {
		this._hideHeader = value;
		this.markDirty("HideHeader");
	}
	private _headerOrientation: string = null;
	get headerOrientation(): string {
		return this._headerOrientation;
	}
	set headerOrientation(value: string) {
		this._headerOrientation = value;
		this.markDirty("HeaderOrientation");
	}
	private _orientation: string = null;
	get orientation(): string {
		return this._orientation;
	}
	set orientation(value: string) {
		this._orientation = value;
		this.markDirty("Orientation");
	}
	private _visibleMonths: number = 0;
	get visibleMonths(): number {
		return this._visibleMonths;
	}
	set visibleMonths(value: number) {
		this._visibleMonths = value;
		this.markDirty("VisibleMonths");
	}
	private _activeView: string = null;
	get activeView(): string {
		return this._activeView;
	}
	set activeView(value: string) {
		this._activeView = value;
		this.markDirty("ActiveView");
	}
	private _formatOptions: CalendarFormatOptionsDescription = null;
	get formatOptions(): CalendarFormatOptionsDescription {
		return this._formatOptions;
	}
	set formatOptions(value: CalendarFormatOptionsDescription) {
		this._formatOptions = value;
		this.markDirty("FormatOptions");
	}
	private _resourceStrings: WebCalendarResourceStringsDescription = null;
	get resourceStrings(): WebCalendarResourceStringsDescription {
		return this._resourceStrings;
	}
	set resourceStrings(value: WebCalendarResourceStringsDescription) {
		this._resourceStrings = value;
		this.markDirty("ResourceStrings");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


