import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebDropdownItemDescriptionModule } from "./WebDropdownItemDescriptionModule";
import { WebDropdownHeaderDescriptionModule } from "./WebDropdownHeaderDescriptionModule";
import { WebDropdownGroupDescriptionModule } from "./WebDropdownGroupDescriptionModule";
import { WebDropdownItemComponentEventArgsDescriptionMetadata } from "./WebDropdownItemComponentEventArgsDescriptionMetadata";
import { WebDropdownDescriptionMetadata } from "./WebDropdownDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDropdownDescriptionModule extends Base {
	static $t: Type = markType(WebDropdownDescriptionModule, 'WebDropdownDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebDropdownItemDescriptionModule.register(context);
		WebDropdownHeaderDescriptionModule.register(context);
		WebDropdownGroupDescriptionModule.register(context);
		WebDropdownItemComponentEventArgsDescriptionMetadata.register(context);
		WebDropdownDescriptionMetadata.register(context);
	}
}


