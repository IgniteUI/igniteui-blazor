import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebIconDescriptionMetadata } from "./WebIconDescriptionMetadata";

/**
 * @hidden 
 */
export class WebIconDescriptionModule extends Base {
	static $t: Type = markType(WebIconDescriptionModule, 'WebIconDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebIconDescriptionMetadata.register(context);
	}
}


