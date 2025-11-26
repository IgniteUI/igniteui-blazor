import { WebSliderBaseDescription } from "./WebSliderBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSliderDescription extends WebSliderBaseDescription {
	static $t: Type = markType(WebSliderDescription, 'WebSliderDescription', (<any>WebSliderBaseDescription).$type);
	protected get_type(): string {
		return "WebSlider";
	}
	constructor() {
		super();
	}
	private _value: number = 0;
	get value(): number {
		return this._value;
	}
	set value(value: number) {
		this._value = value;
		this.markDirty("Value");
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
}


