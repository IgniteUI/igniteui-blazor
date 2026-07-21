import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebStepDescriptionMetadata } from "./WebStepDescriptionMetadata";

/**
 * @hidden 
 */
export class WebStepDescriptionModule extends Base {
	static $t: Type = markType(WebStepDescriptionModule, 'WebStepDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebStepDescriptionMetadata.register(context);
	}
}


