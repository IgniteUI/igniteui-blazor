import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTabDescriptionMetadata } from "./WebTabDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTabDescriptionModule extends Base {
	static $t: Type = markType(WebTabDescriptionModule, 'WebTabDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTabDescriptionMetadata.register(context);
	}
}


