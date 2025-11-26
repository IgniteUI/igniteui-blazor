import { Description } from "./Description";
import { WebCheckboxChangeEventArgsDetailDescription } from "./WebCheckboxChangeEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCheckboxChangeEventArgsDescription extends Description {
	static $t: Type = markType(WebCheckboxChangeEventArgsDescription, 'WebCheckboxChangeEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCheckboxChangeEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "CheckboxChangeEventArgs";
	constructor() {
		super();
	}
	private _detail: WebCheckboxChangeEventArgsDetailDescription = null;
	get detail(): WebCheckboxChangeEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebCheckboxChangeEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


