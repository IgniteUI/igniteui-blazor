import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRadioChangeEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebRadioChangeEventArgsDetailDescription, 'WebRadioChangeEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRadioChangeEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "RadioChangeEventArgsDetail";
	constructor() {
		super();
	}
	private _checked: boolean = false;
	get checked(): boolean {
		return this._checked;
	}
	set checked(value: boolean) {
		this._checked = value;
		this.markDirty("Checked");
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
}


