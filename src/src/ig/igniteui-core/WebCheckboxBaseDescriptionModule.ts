import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCheckboxChangeEventArgsDescriptionMetadata } from "./WebCheckboxChangeEventArgsDescriptionMetadata";
import { WebCheckboxBaseDescriptionMetadata } from "./WebCheckboxBaseDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCheckboxBaseDescriptionModule extends Base {
	static $t: Type = markType(WebCheckboxBaseDescriptionModule, 'WebCheckboxBaseDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCheckboxChangeEventArgsDescriptionMetadata.register(context);
		WebCheckboxBaseDescriptionMetadata.register(context);
	}
}


