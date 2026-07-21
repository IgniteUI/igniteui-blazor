import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebListDescriptionMetadata } from "./WebListDescriptionMetadata";

/**
 * @hidden 
 */
export class WebListDescriptionModule extends Base {
	static $t: Type = markType(WebListDescriptionModule, 'WebListDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebListDescriptionMetadata.register(context);
	}
}


