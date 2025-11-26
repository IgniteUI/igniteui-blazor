import { Description } from "./Description";
import { WebRadioChangeEventArgsDetailDescription } from "./WebRadioChangeEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRadioChangeEventArgsDescription extends Description {
	static $t: Type = markType(WebRadioChangeEventArgsDescription, 'WebRadioChangeEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRadioChangeEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "RadioChangeEventArgs";
	constructor() {
		super();
	}
	private _detail: WebRadioChangeEventArgsDetailDescription = null;
	get detail(): WebRadioChangeEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebRadioChangeEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


