import { WebBaseComboBoxLikeDescription } from "./WebBaseComboBoxLikeDescription";
import { Description } from "./Description";
import { DateRangeDescriptorDescription } from "./DateRangeDescriptorDescription";
import { WebCalendarResourceStringsDescription } from "./WebCalendarResourceStringsDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDatePickerDescription extends WebBaseComboBoxLikeDescription {
	static $t: Type = markType(WebDatePickerDescription, 'WebDatePickerDescription', (<any>WebBaseComboBoxLikeDescription).$type);
	protected get_type(): string {
		return "WebDatePicker";
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
	private _mode: string = null;
	get mode(): string {
		return this._mode;
	}
	set mode(value: string) {
		this._mode = value;
		this.markDirty("Mode");
	}
	private _nonEditable: boolean = false;
	get nonEditable(): boolean {
		return this._nonEditable;
	}
	set nonEditable(value: boolean) {
		this._nonEditable = value;
		this.markDirty("NonEditable");
	}
	private _readOnly: boolean = false;
	get readOnly(): boolean {
		return this._readOnly;
	}
	set readOnly(value: boolean) {
		this._readOnly = value;
		this.markDirty("ReadOnly");
	}
	private _value: Date = new Date();
	get value(): Date {
		return this._value;
	}
	set value(value: Date) {
		this._value = value;
		this.markDirty("Value");
	}
	private _activeDate: Date = new Date();
	get activeDate(): Date {
		return this._activeDate;
	}
	set activeDate(value: Date) {
		this._activeDate = value;
		this.markDirty("ActiveDate");
	}
	private _min: Date = new Date();
	get min(): Date {
		return this._min;
	}
	set min(value: Date) {
		this._min = value;
		this.markDirty("Min");
	}
	private _max: Date = new Date();
	get max(): Date {
		return this._max;
	}
	set max(value: Date) {
		this._max = value;
		this.markDirty("Max");
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
	private _hideHeader: boolean = false;
	get hideHeader(): boolean {
		return this._hideHeader;
	}
	set hideHeader(value: boolean) {
		this._hideHeader = value;
		this.markDirty("HideHeader");
	}
	private _hideOutsideDays: boolean = false;
	get hideOutsideDays(): boolean {
		return this._hideOutsideDays;
	}
	set hideOutsideDays(value: boolean) {
		this._hideOutsideDays = value;
		this.markDirty("HideOutsideDays");
	}
	private _disabledDates: DateRangeDescriptorDescription[] = null;
	get disabledDates(): DateRangeDescriptorDescription[] {
		return this._disabledDates;
	}
	set disabledDates(value: DateRangeDescriptorDescription[]) {
		this._disabledDates = value;
		this.markDirty("DisabledDates");
	}
	private _specialDates: DateRangeDescriptorDescription[] = null;
	get specialDates(): DateRangeDescriptorDescription[] {
		return this._specialDates;
	}
	set specialDates(value: DateRangeDescriptorDescription[]) {
		this._specialDates = value;
		this.markDirty("SpecialDates");
	}
	private _outlined: boolean = false;
	get outlined(): boolean {
		return this._outlined;
	}
	set outlined(value: boolean) {
		this._outlined = value;
		this.markDirty("Outlined");
	}
	private _placeholder: string = null;
	get placeholder(): string {
		return this._placeholder;
	}
	set placeholder(value: string) {
		this._placeholder = value;
		this.markDirty("Placeholder");
	}
	private _visibleMonths: number = 0;
	get visibleMonths(): number {
		return this._visibleMonths;
	}
	set visibleMonths(value: number) {
		this._visibleMonths = value;
		this.markDirty("VisibleMonths");
	}
	private _showWeekNumbers: boolean = false;
	get showWeekNumbers(): boolean {
		return this._showWeekNumbers;
	}
	set showWeekNumbers(value: boolean) {
		this._showWeekNumbers = value;
		this.markDirty("ShowWeekNumbers");
	}
	private _displayFormat: string = null;
	get displayFormat(): string {
		return this._displayFormat;
	}
	set displayFormat(value: string) {
		this._displayFormat = value;
		this.markDirty("DisplayFormat");
	}
	private _inputFormat: string = null;
	get inputFormat(): string {
		return this._inputFormat;
	}
	set inputFormat(value: string) {
		this._inputFormat = value;
		this.markDirty("InputFormat");
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
		this.markDirty("Locale");
	}
	private _prompt: string = null;
	get prompt(): string {
		return this._prompt;
	}
	set prompt(value: string) {
		this._prompt = value;
		this.markDirty("Prompt");
	}
	private _resourceStrings: WebCalendarResourceStringsDescription = null;
	get resourceStrings(): WebCalendarResourceStringsDescription {
		return this._resourceStrings;
	}
	set resourceStrings(value: WebCalendarResourceStringsDescription) {
		this._resourceStrings = value;
		this.markDirty("ResourceStrings");
	}
	private _weekStart: string = null;
	get weekStart(): string {
		return this._weekStart;
	}
	set weekStart(value: string) {
		this._weekStart = value;
		this.markDirty("WeekStart");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _required: boolean = false;
	get required(): boolean {
		return this._required;
	}
	set required(value: boolean) {
		this._required = value;
		this.markDirty("Required");
	}
	private _defaultValue: any = null;
	get defaultValue(): any {
		return this._defaultValue;
	}
	set defaultValue(value: any) {
		this._defaultValue = value;
		this.markDirty("DefaultValue");
	}
	private _invalid: boolean = false;
	get invalid(): boolean {
		return this._invalid;
	}
	set invalid(value: boolean) {
		this._invalid = value;
		this.markDirty("Invalid");
	}
	private _opening: string = null;
	get openingRef(): string {
		return this._opening;
	}
	set openingRef(value: string) {
		this._opening = value;
		this.markDirty("OpeningRef");
	}
	private _opened: string = null;
	get openedRef(): string {
		return this._opened;
	}
	set openedRef(value: string) {
		this._opened = value;
		this.markDirty("OpenedRef");
	}
	private _closing: string = null;
	get closingRef(): string {
		return this._closing;
	}
	set closingRef(value: string) {
		this._closing = value;
		this.markDirty("ClosingRef");
	}
	private _closed: string = null;
	get closedRef(): string {
		return this._closed;
	}
	set closedRef(value: string) {
		this._closed = value;
		this.markDirty("ClosedRef");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
	private _input: string = null;
	get inputRef(): string {
		return this._input;
	}
	set inputRef(value: string) {
		this._input = value;
		this.markDirty("InputRef");
	}
}


