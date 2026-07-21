import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebNavbarDescriptionMetadata } from "./WebNavbarDescriptionMetadata";

/**
 * @hidden 
 */
export class WebNavbarDescriptionModule extends Base {
	static $t: Type = markType(WebNavbarDescriptionModule, 'WebNavbarDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebNavbarDescriptionMetadata.register(context);
	}
}


