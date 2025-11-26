import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardActionsDescriptionMetadata } from "./WebCardActionsDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardActionsDescriptionModule extends Base {
	static $t: Type = markType(WebCardActionsDescriptionModule, 'WebCardActionsDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardActionsDescriptionMetadata.register(context);
	}
}


