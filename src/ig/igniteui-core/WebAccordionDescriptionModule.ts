import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebAccordionDescriptionMetadata } from "./WebAccordionDescriptionMetadata";

/**
 * @hidden 
 */
export class WebAccordionDescriptionModule extends Base {
	static $t: Type = markType(WebAccordionDescriptionModule, 'WebAccordionDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebAccordionDescriptionMetadata.register(context);
	}
}


