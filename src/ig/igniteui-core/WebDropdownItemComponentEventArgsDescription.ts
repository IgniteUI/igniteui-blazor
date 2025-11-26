import { Description } from "./Description";
import { WebDropdownItemDescription } from "./WebDropdownItemDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDropdownItemComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebDropdownItemComponentEventArgsDescription, 'WebDropdownItemComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDropdownItemComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "DropdownItemComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebDropdownItemDescription = null;
	get detail(): WebDropdownItemDescription {
		return this._detail;
	}
	set detail(value: WebDropdownItemDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


