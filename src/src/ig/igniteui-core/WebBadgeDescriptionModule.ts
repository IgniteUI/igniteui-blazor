import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBadgeDescriptionMetadata } from "./WebBadgeDescriptionMetadata";

/**
 * @hidden 
 */
export class WebBadgeDescriptionModule extends Base {
	static $t: Type = markType(WebBadgeDescriptionModule, 'WebBadgeDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBadgeDescriptionMetadata.register(context);
	}
}


