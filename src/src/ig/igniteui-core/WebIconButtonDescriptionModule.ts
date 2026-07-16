import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebIconButtonDescriptionMetadata } from "./WebIconButtonDescriptionMetadata";

/**
 * @hidden 
 */
export class WebIconButtonDescriptionModule extends Base {
	static $t: Type = markType(WebIconButtonDescriptionModule, 'WebIconButtonDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebIconButtonDescriptionMetadata.register(context);
	}
}


