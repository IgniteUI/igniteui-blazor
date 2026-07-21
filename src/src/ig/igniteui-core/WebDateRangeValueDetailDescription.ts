import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangeValueDetailDescription extends Description {
	static $t: Type = markType(WebDateRangeValueDetailDescription, 'WebDateRangeValueDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDateRangeValueDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "DateRangeValueDetail";
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


