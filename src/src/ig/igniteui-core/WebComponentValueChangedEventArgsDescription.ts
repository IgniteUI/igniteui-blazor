import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComponentValueChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebComponentValueChangedEventArgsDescription, 'WebComponentValueChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComponentValueChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComponentValueChangedEventArgs";
	constructor() {
		super();
	}
	private _detail: string = null;
	get detail(): string {
		return this._detail;
	}
	set detail(value: string) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


