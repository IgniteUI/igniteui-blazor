import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebDropdownHeaderDescriptionMetadata } from "./WebDropdownHeaderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDropdownHeaderDescriptionModule extends Base {
	static $t: Type = markType(WebDropdownHeaderDescriptionModule, 'WebDropdownHeaderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebDropdownHeaderDescriptionMetadata.register(context);
	}
}


