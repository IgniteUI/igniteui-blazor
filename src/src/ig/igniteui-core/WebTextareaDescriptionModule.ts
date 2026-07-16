import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTextareaDescriptionMetadata } from "./WebTextareaDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTextareaDescriptionModule extends Base {
	static $t: Type = markType(WebTextareaDescriptionModule, 'WebTextareaDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTextareaDescriptionMetadata.register(context);
	}
}


