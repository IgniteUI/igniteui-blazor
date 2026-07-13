import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebInputDescriptionMetadata } from "./WebInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebInputDescriptionModule extends Base {
	static $t: Type = markType(WebInputDescriptionModule, 'WebInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebInputDescriptionMetadata.register(context);
	}
}


