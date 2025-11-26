import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebListHeaderDescriptionMetadata } from "./WebListHeaderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebListHeaderDescriptionModule extends Base {
	static $t: Type = markType(WebListHeaderDescriptionModule, 'WebListHeaderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebListHeaderDescriptionMetadata.register(context);
	}
}


