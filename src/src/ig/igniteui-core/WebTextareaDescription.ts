import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTextareaDescription extends Description {
	static $t: Type = markType(WebTextareaDescription, 'WebTextareaDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTextarea";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _autocomplete: string = null;
	get autocomplete(): string {
		return this._autocomplete;
	}
	set autocomplete(value: string) {
		this._autocomplete = value;
		this.markDirty("Autocomplete");
	}
	private _autocapitalize: string = null;
	get autocapitalize(): string {
		return this._autocapitalize;
	}
	set autocapitalize(value: string) {
		this._autocapitalize = value;
		this.markDirty("Autocapitalize");
	}
	private _inputMode: string = null;
	get inputMode(): string {
		return this._inputMode;
	}
	set inputMode(value: string) {
		this._inputMode = value;
		this.markDirty("InputMode");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _maxLength: number = 0;
	get maxLength(): number {
		return this._maxLength;
	}
	set maxLength(value: number) {
		this._maxLength = value;
		this.markDirty("MaxLength");
	}
	private _minLength: number = 0;
	get minLength(): number {
		return this._minLength;
	}
	set minLength(value: number) {
		this._minLength = value;
		this.markDirty("MinLength");
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
	private _readOnly: boolean = false;
	get readOnly(): boolean {
		return this._readOnly;
	}
	set readOnly(value: boolean) {
		this._readOnly = value;
		this.markDirty("ReadOnly");
	}
	private _resize: string = null;
	get resize(): string {
		return this._resize;
	}
	set resize(value: string) {
		this._resize = value;
		this.markDirty("Resize");
	}
	private _rows: number = 0;
	get rows(): number {
		return this._rows;
	}
	set rows(value: number) {
		this._rows = value;
		this.markDirty("Rows");
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _spellcheck: boolean = false;
	get spellcheck(): boolean {
		return this._spellcheck;
	}
	set spellcheck(value: boolean) {
		this._spellcheck = value;
		this.markDirty("Spellcheck");
	}
	private _wrap: string = null;
	get wrap(): string {
		return this._wrap;
	}
	set wrap(value: string) {
		this._wrap = value;
		this.markDirty("Wrap");
	}
	private _validateOnly: boolean = false;
	get validateOnly(): boolean {
		return this._validateOnly;
	}
	set validateOnly(value: boolean) {
		this._validateOnly = value;
		this.markDirty("ValidateOnly");
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
	private _input: string = null;
	get inputRef(): string {
		return this._input;
	}
	set inputRef(value: string) {
		this._input = value;
		this.markDirty("InputRef");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
	private _focus: string = null;
	get focusRef(): string {
		return this._focus;
	}
	set focusRef(value: string) {
		this._focus = value;
		this.markDirty("FocusRef");
	}
	private _blur: string = null;
	get blurRef(): string {
		return this._blur;
	}
	set blurRef(value: string) {
		this._blur = value;
		this.markDirty("BlurRef");
	}
}


