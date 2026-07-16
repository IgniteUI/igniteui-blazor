import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCheckboxBaseDescriptionModule } from "./WebCheckboxBaseDescriptionModule";
import { WebCheckboxDescriptionMetadata } from "./WebCheckboxDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCheckboxDescriptionModule extends Base {
	static $t: Type = markType(WebCheckboxDescriptionModule, 'WebCheckboxDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCheckboxBaseDescriptionModule.register(context);
		WebCheckboxDescriptionMetadata.register(context);
	}
}


