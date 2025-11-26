import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebNavDrawerHeaderItemDescriptionMetadata } from "./WebNavDrawerHeaderItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebNavDrawerHeaderItemDescriptionModule extends Base {
	static $t: Type = markType(WebNavDrawerHeaderItemDescriptionModule, 'WebNavDrawerHeaderItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerHeaderItemDescriptionMetadata.register(context);
	}
}


