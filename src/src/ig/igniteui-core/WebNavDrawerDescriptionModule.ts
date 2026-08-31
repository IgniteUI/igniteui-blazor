import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebNavDrawerHeaderItemDescriptionModule } from "./WebNavDrawerHeaderItemDescriptionModule";
import { WebNavDrawerItemDescriptionModule } from "./WebNavDrawerItemDescriptionModule";
import { WebNavDrawerDescriptionMetadata } from "./WebNavDrawerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebNavDrawerDescriptionModule extends Base {
	static $t: Type = markType(WebNavDrawerDescriptionModule, 'WebNavDrawerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerHeaderItemDescriptionModule.register(context);
		WebNavDrawerItemDescriptionModule.register(context);
		WebNavDrawerDescriptionMetadata.register(context);
	}
}


