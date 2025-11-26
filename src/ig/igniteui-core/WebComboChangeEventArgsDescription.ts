import { Description } from "./Description";
import { WebComboChangeEventArgsDetailDescription } from "./WebComboChangeEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComboChangeEventArgsDescription extends Description {
	static $t: Type = markType(WebComboChangeEventArgsDescription, 'WebComboChangeEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboChangeEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboChangeEventArgs";
	constructor() {
		super();
	}
	private _detail: WebComboChangeEventArgsDetailDescription = null;
	get detail(): WebComboChangeEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebComboChangeEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


