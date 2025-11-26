import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardDescriptionMetadata } from "./WebCardDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardDescriptionModule extends Base {
	static $t: Type = markType(WebCardDescriptionModule, 'WebCardDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardDescriptionMetadata.register(context);
	}
}


