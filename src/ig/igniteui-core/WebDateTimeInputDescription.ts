import { WebMaskInputBaseDescription } from "./WebMaskInputBaseDescription";
import { Description } from "./Description";
import { DatePartDeltasDescription } from "./DatePartDeltasDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateTimeInputDescription extends WebMaskInputBaseDescription {
	static $t: Type = markType(WebDateTimeInputDescription, 'WebDateTimeInputDescription', (<any>WebMaskInputBaseDescription).$type);
	protected get_type(): string {
		return "WebDateTimeInput";
	}
	constructor() {
		super();
	}
	private _inputFormat: string = null;
	get inputFormat(): string {
		return this._inputFormat;
	}
	set inputFormat(value: string) {
		this._inputFormat = value;
		this.markDirty("InputFormat");
	}
	private _value: Date = new Date();
	get value(): Date {
		return this._value;
	}
	set value(value: Date) {
		this._value = value;
		this.markDirty("Value");
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
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


