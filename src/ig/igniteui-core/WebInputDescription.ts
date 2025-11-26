import { WebInputBaseDescription } from "./WebInputBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebInputDescription extends WebInputBaseDescription {
	static $t: Type = markType(WebInputDescription, 'WebInputDescription', (<any>WebInputBaseDescription).$type);
	protected get_type(): string {
		return "WebInput";
	}
	constructor() {
		super();
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _displayType: string = null;
	get displayType(): string {
		return this._displayType;
	}
	set displayType(value: string) {
		this._displayType = value;
		this.markDirty("DisplayType");
	}
	private _inputMode: string = null;
	get inputMode(): string {
		return this._inputMode;
	}
	set inputMode(value: string) {
		this._inputMode = value;
		this.markDirty("InputMode");
	}
	private _pattern: string = null;
	get pattern(): string {
		return this._pattern;
	}
	set pattern(value: string) {
		this._pattern = value;
		this.markDirty("Pattern");
	}
	private _minLength: number = 0;
	get minLength(): number {
		return this._minLength;
	}
	set minLength(value: number) {
		this._minLength = value;
		this.markDirty("MinLength");
	}
	private _maxLength: number = 0;
	get maxLength(): number {
		return this._maxLength;
	}
	set maxLength(value: number) {
		this._maxLength = value;
		this.markDirty("MaxLength");
	}
	private _min: number = 0;
	get min(): number {
		return this._min;
	}
	set min(value: number) {
		this._min = value;
		this.markDirty("Min");
	}
	private _max: number = 0;
	get max(): number {
		return this._max;
	}
	set max(value: number) {
		this._max = value;
		this.markDirty("Max");
	}
	private _step: number = 0;
	get step(): number {
		return this._step;
	}
	set step(value: number) {
		this._step = value;
		this.markDirty("Step");
	}
	private _autofocus: boolean = false;
	get autofocus(): boolean {
		return this._autofocus;
	}
	set autofocus(value: boolean) {
		this._autofocus = value;
		this.markDirty("Autofocus");
	}
	private _autocomplete: string = null;
	get autocomplete(): string {
		return this._autocomplete;
	}
	set autocomplete(value: string) {
		this._autocomplete = value;
		this.markDirty("Autocomplete");
	}
	private _validateOnly: boolean = false;
	get validateOnly(): boolean {
		return this._validateOnly;
	}
	set validateOnly(value: boolean) {
		this._validateOnly = value;
		this.markDirty("ValidateOnly");
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


