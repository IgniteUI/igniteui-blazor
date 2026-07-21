import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComponentDateValueChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebComponentDateValueChangedEventArgsDescription, 'WebComponentDateValueChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComponentDateValueChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComponentDateValueChangedEventArgs";
	constructor() {
		super();
	}
	private _detail: Date = new Date();
	get detail(): Date {
		return this._detail;
	}
	set detail(value: Date) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


