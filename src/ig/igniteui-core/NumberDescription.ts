import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class NumberDescription extends Description {
	static $t: Type = markType(NumberDescription, 'NumberDescription', (<any>Description).$type);
	private _value: number = 0;
	get value(): number {
		return this._value;
	}
	set value(value: number) {
		this._value = value;
	}
}


