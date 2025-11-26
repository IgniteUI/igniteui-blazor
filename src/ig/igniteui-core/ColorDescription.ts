import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class ColorDescription extends Description {
	static $t: Type = markType(ColorDescription, 'ColorDescription', (<any>Description).$type);
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
	}
}


