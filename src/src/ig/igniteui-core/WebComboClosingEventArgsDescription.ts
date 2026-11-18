import { Description } from "./Description";
import { WebComboClosingEventArgsDetailDescription } from "./WebComboClosingEventArgsDetailDescription";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebComboClosingEventArgsDescription extends Description {
	static $t: Type = markType(WebComboClosingEventArgsDescription, 'WebComboClosingEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboClosingEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboClosingEventArgs";
	constructor() {
		super();
	}
	private _detail: WebComboClosingEventArgsDetailDescription = null;
	get detail(): WebComboClosingEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebComboClosingEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


