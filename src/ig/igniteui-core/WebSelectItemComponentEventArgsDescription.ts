import { Description } from "./Description";
import { WebSelectItemDescription } from "./WebSelectItemDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSelectItemComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebSelectItemComponentEventArgsDescription, 'WebSelectItemComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSelectItemComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "SelectItemComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebSelectItemDescription = null;
	get detail(): WebSelectItemDescription {
		return this._detail;
	}
	set detail(value: WebSelectItemDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


