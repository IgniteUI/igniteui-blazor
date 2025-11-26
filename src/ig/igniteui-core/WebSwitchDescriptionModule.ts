import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCheckboxBaseDescriptionModule } from "./WebCheckboxBaseDescriptionModule";
import { WebSwitchDescriptionMetadata } from "./WebSwitchDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSwitchDescriptionModule extends Base {
	static $t: Type = markType(WebSwitchDescriptionModule, 'WebSwitchDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCheckboxBaseDescriptionModule.register(context);
		WebSwitchDescriptionMetadata.register(context);
	}
}


