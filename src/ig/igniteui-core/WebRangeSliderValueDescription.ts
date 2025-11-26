import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRangeSliderValueDescription extends Description {
	static $t: Type = markType(WebRangeSliderValueDescription, 'WebRangeSliderValueDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRangeSliderValue";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "RangeSliderValue";
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
}


