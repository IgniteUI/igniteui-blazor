import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class VoidEventArgsDescription extends Description {
	static $t: Type = markType(VoidEventArgsDescription, 'VoidEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "VoidEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
}


