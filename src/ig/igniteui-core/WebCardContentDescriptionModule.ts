import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardContentDescriptionMetadata } from "./WebCardContentDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardContentDescriptionModule extends Base {
	static $t: Type = markType(WebCardContentDescriptionModule, 'WebCardContentDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardContentDescriptionMetadata.register(context);
	}
}


