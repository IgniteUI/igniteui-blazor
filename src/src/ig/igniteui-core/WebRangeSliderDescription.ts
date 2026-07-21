import { WebSliderBaseDescription } from "./WebSliderBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRangeSliderDescription extends WebSliderBaseDescription {
	static $t: Type = markType(WebRangeSliderDescription, 'WebRangeSliderDescription', (<any>WebSliderBaseDescription).$type);
	protected get_type(): string {
		return "WebRangeSlider";
	}
	constructor() {
		super();
	}
	private _lower: number = 0;
	get lower(): number {
		return this._lower;
	}
	set lower(value: number) {
		this._lower = value;
		this.markDirty("Lower");
	}
	private _upper: number = 0;
	get upper(): number {
		return this._upper;
	}
	set upper(value: number) {
		this._upper = value;
		this.markDirty("Upper");
	}
	private _thumbLabelLower: string = null;
	get thumbLabelLower(): string {
		return this._thumbLabelLower;
	}
	set thumbLabelLower(value: string) {
		this._thumbLabelLower = value;
		this.markDirty("ThumbLabelLower");
	}
	private _thumbLabelUpper: string = null;
	get thumbLabelUpper(): string {
		return this._thumbLabelUpper;
	}
	set thumbLabelUpper(value: string) {
		this._thumbLabelUpper = value;
		this.markDirty("ThumbLabelUpper");
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


