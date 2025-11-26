import { Description } from "./Description";
import { WebDateRangeValueDetailDescription } from "./WebDateRangeValueDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangeValueEventArgsDescription extends Description {
	static $t: Type = markType(WebDateRangeValueEventArgsDescription, 'WebDateRangeValueEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDateRangeValueEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "DateRangeValueEventArgs";
	constructor() {
		super();
	}
	private _detail: WebDateRangeValueDetailDescription = null;
	get detail(): WebDateRangeValueDetailDescription {
		return this._detail;
	}
	set detail(value: WebDateRangeValueDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


