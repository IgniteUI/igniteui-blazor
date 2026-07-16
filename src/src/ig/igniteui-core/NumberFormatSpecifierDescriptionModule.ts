import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { FormatSpecifierDescriptionModule } from "./FormatSpecifierDescriptionModule";
import { NumberFormatSpecifierDescriptionMetadata } from "./NumberFormatSpecifierDescriptionMetadata";

/**
 * @hidden 
 */
export class NumberFormatSpecifierDescriptionModule extends Base {
	static $t: Type = markType(NumberFormatSpecifierDescriptionModule, 'NumberFormatSpecifierDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		FormatSpecifierDescriptionModule.register(context);
		NumberFormatSpecifierDescriptionMetadata.register(context);
	}
}


