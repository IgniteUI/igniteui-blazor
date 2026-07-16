import { List$1 } from "./List$1";
import { Base, Number_$type, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DoubleCollection extends List$1<number> {
	static $t: Type = markType(DoubleCollection, 'DoubleCollection', (<any>List$1).$type.specialize(Number_$type));
	constructor() {
		super(Number_$type, 0);
	}
}


