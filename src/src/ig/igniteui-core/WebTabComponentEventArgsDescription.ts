import { Description } from "./Description";
import { WebTabDescription } from "./WebTabDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTabComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebTabComponentEventArgsDescription, 'WebTabComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTabComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TabComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebTabDescription = null;
	get detail(): WebTabDescription {
		return this._detail;
	}
	set detail(value: WebTabDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


