import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardMediaDescriptionMetadata } from "./WebCardMediaDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardMediaDescriptionModule extends Base {
	static $t: Type = markType(WebCardMediaDescriptionModule, 'WebCardMediaDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardMediaDescriptionMetadata.register(context);
	}
}


