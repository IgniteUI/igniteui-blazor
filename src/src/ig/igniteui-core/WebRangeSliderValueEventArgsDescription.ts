import { Description } from "./Description";
import { WebRangeSliderValueDescription } from "./WebRangeSliderValueDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRangeSliderValueEventArgsDescription extends Description {
	static $t: Type = markType(WebRangeSliderValueEventArgsDescription, 'WebRangeSliderValueEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRangeSliderValueEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _detail: WebRangeSliderValueDescription = null;
	get detail(): WebRangeSliderValueDescription {
		return this._detail;
	}
	set detail(value: WebRangeSliderValueDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


