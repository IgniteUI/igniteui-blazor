import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardHeaderDescriptionMetadata } from "./WebCardHeaderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardHeaderDescriptionModule extends Base {
	static $t: Type = markType(WebCardHeaderDescriptionModule, 'WebCardHeaderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardHeaderDescriptionMetadata.register(context);
	}
}


