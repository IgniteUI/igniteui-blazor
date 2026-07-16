import { WebDateTimeInputBaseDescription } from "./WebDateTimeInputBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateTimeInputDescription extends WebDateTimeInputBaseDescription {
	static $t: Type = markType(WebDateTimeInputDescription, 'WebDateTimeInputDescription', (<any>WebDateTimeInputBaseDescription).$type);
	protected get_type(): string {
		return "WebDateTimeInput";
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
	private _inputOcurred: string = null;
	get inputOcurredRef(): string {
		return this._inputOcurred;
	}
	set inputOcurredRef(value: string) {
		this._inputOcurred = value;
		this.markDirty("InputOcurredRef");
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


