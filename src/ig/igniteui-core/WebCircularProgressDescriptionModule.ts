import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCircularProgressDescriptionMetadata } from "./WebCircularProgressDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCircularProgressDescriptionModule extends Base {
	static $t: Type = markType(WebCircularProgressDescriptionModule, 'WebCircularProgressDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCircularProgressDescriptionMetadata.register(context);
	}
}


