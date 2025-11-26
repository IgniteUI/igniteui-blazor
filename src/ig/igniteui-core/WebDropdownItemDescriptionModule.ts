import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBaseOptionLikeDescriptionModule } from "./WebBaseOptionLikeDescriptionModule";
import { WebDropdownItemDescriptionMetadata } from "./WebDropdownItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDropdownItemDescriptionModule extends Base {
	static $t: Type = markType(WebDropdownItemDescriptionModule, 'WebDropdownItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBaseOptionLikeDescriptionModule.register(context);
		WebDropdownItemDescriptionMetadata.register(context);
	}
}


