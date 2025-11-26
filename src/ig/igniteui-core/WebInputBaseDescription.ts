import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebInputBaseDescription extends Description {
	static $t: Type = markType(WebInputBaseDescription, 'WebInputBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebInputBase";
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
	private _readOnly: boolean = false;
	get readOnly(): boolean {
		return this._readOnly;
	}
	set readOnly(value: boolean) {
		this._readOnly = value;
		this.markDirty("ReadOnly");
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
	private _inputOcurred: string = null;
	get inputOcurredRef(): string {
		return this._inputOcurred;
	}
	set inputOcurredRef(value: string) {
		this._inputOcurred = value;
		this.markDirty("InputOcurredRef");
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


