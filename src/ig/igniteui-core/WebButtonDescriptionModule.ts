import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebButtonDescriptionMetadata } from "./WebButtonDescriptionMetadata";

/**
 * @hidden 
 */
export class WebButtonDescriptionModule extends Base {
	static $t: Type = markType(WebButtonDescriptionModule, 'WebButtonDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebButtonDescriptionMetadata.register(context);
	}
}


