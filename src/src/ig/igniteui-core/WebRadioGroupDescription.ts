import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRadioGroupDescription extends Description {
	static $t: Type = markType(WebRadioGroupDescription, 'WebRadioGroupDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRadioGroup";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _alignment: string = null;
	get alignment(): string {
		return this._alignment;
	}
	set alignment(value: string) {
		this._alignment = value;
		this.markDirty("Alignment");
	}
	private _defaultValue: string = null;
	get defaultValue(): string {
		return this._defaultValue;
	}
	set defaultValue(value: string) {
		this._defaultValue = value;
		this.markDirty("DefaultValue");
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


