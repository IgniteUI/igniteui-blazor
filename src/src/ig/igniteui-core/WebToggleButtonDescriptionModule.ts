import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebToggleButtonDescriptionMetadata } from "./WebToggleButtonDescriptionMetadata";

/**
 * @hidden 
 */
export class WebToggleButtonDescriptionModule extends Base {
	static $t: Type = markType(WebToggleButtonDescriptionModule, 'WebToggleButtonDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebToggleButtonDescriptionMetadata.register(context);
	}
}


