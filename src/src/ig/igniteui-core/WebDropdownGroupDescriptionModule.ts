import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebDropdownGroupDescriptionMetadata } from "./WebDropdownGroupDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDropdownGroupDescriptionModule extends Base {
	static $t: Type = markType(WebDropdownGroupDescriptionModule, 'WebDropdownGroupDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebDropdownGroupDescriptionMetadata.register(context);
	}
}


