import { Description } from "./Description";
import { DatePartDeltasDescription } from "./DatePartDeltasDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebDateTimeInputBaseDescription extends Description {
	static $t: Type = markType(WebDateTimeInputBaseDescription, 'WebDateTimeInputBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDateTimeInputBase";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
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
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _inputFormat: string = null;
	get inputFormat(): string {
		return this._inputFormat;
	}
	set inputFormat(value: string) {
		this._inputFormat = value;
		this.markDirty("InputFormat");
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
	private _displayFormat: string = null;
	get displayFormat(): string {
		return this._displayFormat;
	}
	set displayFormat(value: string) {
		this._displayFormat = value;
		this.markDirty("DisplayFormat");
	}
	private _spinDelta: DatePartDeltasDescription = null;
	get spinDelta(): DatePartDeltasDescription {
		return this._spinDelta;
	}
	set spinDelta(value: DatePartDeltasDescription) {
		this._spinDelta = value;
		this.markDirty("SpinDelta");
	}
	private _spinLoop: boolean = false;
	get spinLoop(): boolean {
		return this._spinLoop;
	}
	set spinLoop(value: boolean) {
		this._spinLoop = value;
		this.markDirty("SpinLoop");
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
		this.markDirty("Locale");
	}
	private _readOnly: boolean = false;
	get readOnly(): boolean {
		return this._readOnly;
	}
	set readOnly(value: boolean) {
		this._readOnly = value;
		this.markDirty("ReadOnly");
	}
	private _mask: string = null;
	get mask(): string {
		return this._mask;
	}
	set mask(value: string) {
		this._mask = value;
		this.markDirty("Mask");
	}
	private _prompt: string = null;
	get prompt(): string {
		return this._prompt;
	}
	set prompt(value: string) {
		this._prompt = value;
		this.markDirty("Prompt");
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
}


