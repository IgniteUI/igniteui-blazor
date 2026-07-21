import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class BrushDescription extends Description {
	static $t: Type = markType(BrushDescription, 'BrushDescription', (<any>Description).$type);
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
	}
}


