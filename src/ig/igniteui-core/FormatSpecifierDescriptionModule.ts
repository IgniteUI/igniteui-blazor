import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { FormatSpecifierDescriptionMetadata } from "./FormatSpecifierDescriptionMetadata";

/**
 * @hidden 
 */
export class FormatSpecifierDescriptionModule extends Base {
	static $t: Type = markType(FormatSpecifierDescriptionModule, 'FormatSpecifierDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		FormatSpecifierDescriptionMetadata.register(context);
	}
}


