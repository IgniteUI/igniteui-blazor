import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebComboClosingEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebComboClosingEventArgsDetailDescription, 'WebComboClosingEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboClosingEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboClosingEventArgsDetail";
	constructor() {
		super();
	}
	private _trigger: string = null;
	get trigger(): string {
		return this._trigger;
	}
	set trigger(value: string) {
		this._trigger = value;
		this.markDirty("Trigger");
	}
}


