import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class FormatSpecifierDescription extends Description {
	static $t: Type = markType(FormatSpecifierDescription, 'FormatSpecifierDescription', (<any>Description).$type);
	protected get_type(): string {
		return "FormatSpecifier";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "FormatSpecifier";
	constructor() {
		super();
	}
}


