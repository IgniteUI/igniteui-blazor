import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebListItemDescriptionMetadata } from "./WebListItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebListItemDescriptionModule extends Base {
	static $t: Type = markType(WebListItemDescriptionModule, 'WebListItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebListItemDescriptionMetadata.register(context);
	}
}


