import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebNavDrawerDescriptionMetadata } from "./WebNavDrawerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebNavDrawerDescriptionModule extends Base {
	static $t: Type = markType(WebNavDrawerDescriptionModule, 'WebNavDrawerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerDescriptionMetadata.register(context);
	}
}


