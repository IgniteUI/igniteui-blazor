import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRadioDescription extends Description {
	static $t: Type = markType(WebRadioDescription, 'WebRadioDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRadio";
	}
	get type(): string {
		return this.get_type();
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
	private _checked: boolean = false;
	get checked(): boolean {
		return this._checked;
	}
	set checked(value: boolean) {
		this._checked = value;
		this.markDirty("Checked");
	}
	private _labelPosition: string = null;
	get labelPosition(): string {
		return this._labelPosition;
	}
	set labelPosition(value: string) {
		this._labelPosition = value;
		this.markDirty("LabelPosition");
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
	private _defaultChecked: boolean = false;
	get defaultChecked(): boolean {
		return this._defaultChecked;
	}
	set defaultChecked(value: boolean) {
		this._defaultChecked = value;
		this.markDirty("DefaultChecked");
	}
	private _invalid: boolean = false;
	get invalid(): boolean {
		return this._invalid;
	}
	set invalid(value: boolean) {
		this._invalid = value;
		this.markDirty("Invalid");
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


