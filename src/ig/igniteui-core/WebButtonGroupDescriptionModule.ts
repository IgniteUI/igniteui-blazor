import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebToggleButtonDescriptionModule } from "./WebToggleButtonDescriptionModule";
import { WebButtonGroupDescriptionMetadata } from "./WebButtonGroupDescriptionMetadata";

/**
 * @hidden 
 */
export class WebButtonGroupDescriptionModule extends Base {
	static $t: Type = markType(WebButtonGroupDescriptionModule, 'WebButtonGroupDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebToggleButtonDescriptionModule.register(context);
		WebButtonGroupDescriptionMetadata.register(context);
	}
}


