import { Description } from "./Description";
import { WebComboSearchInputEventArgsDetailDescription } from "./WebComboSearchInputEventArgsDetailDescription";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebComboSearchInputEventArgsDescription extends Description {
	static $t: Type = markType(WebComboSearchInputEventArgsDescription, 'WebComboSearchInputEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboSearchInputEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboSearchInputEventArgs";
	constructor() {
		super();
	}
	private _detail: WebComboSearchInputEventArgsDetailDescription = null;
	get detail(): WebComboSearchInputEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebComboSearchInputEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


