import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTabDescriptionModule } from "./WebTabDescriptionModule";
import { WebTabComponentEventArgsDescriptionMetadata } from "./WebTabComponentEventArgsDescriptionMetadata";
import { WebTabsDescriptionMetadata } from "./WebTabsDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTabsDescriptionModule extends Base {
	static $t: Type = markType(WebTabsDescriptionModule, 'WebTabsDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTabDescriptionModule.register(context);
		WebTabComponentEventArgsDescriptionMetadata.register(context);
		WebTabsDescriptionMetadata.register(context);
	}
}


