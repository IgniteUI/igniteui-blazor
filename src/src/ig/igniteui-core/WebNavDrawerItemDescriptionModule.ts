import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebNavDrawerItemDescriptionMetadata } from "./WebNavDrawerItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebNavDrawerItemDescriptionModule extends Base {
	static $t: Type = markType(WebNavDrawerItemDescriptionModule, 'WebNavDrawerItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerItemDescriptionMetadata.register(context);
	}
}


