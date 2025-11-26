import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCheckboxChangeEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebCheckboxChangeEventArgsDetailDescription, 'WebCheckboxChangeEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCheckboxChangeEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "CheckboxChangeEventArgsDetail";
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


