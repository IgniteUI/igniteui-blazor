import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangeValueDescription extends Description {
	static $t: Type = markType(WebDateRangeValueDescription, 'WebDateRangeValueDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDateRangeValue";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _start: Date = new Date();
	get start(): Date {
		return this._start;
	}
	set start(value: Date) {
		this._start = value;
		this.markDirty("Start");
	}
	private _end: Date = new Date();
	get end(): Date {
		return this._end;
	}
	set end(value: Date) {
		this._end = value;
		this.markDirty("End");
	}
}


