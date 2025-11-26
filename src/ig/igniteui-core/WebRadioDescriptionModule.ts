import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebRadioChangeEventArgsDescriptionMetadata } from "./WebRadioChangeEventArgsDescriptionMetadata";
import { WebRadioDescriptionMetadata } from "./WebRadioDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRadioDescriptionModule extends Base {
	static $t: Type = markType(WebRadioDescriptionModule, 'WebRadioDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebRadioChangeEventArgsDescriptionMetadata.register(context);
		WebRadioDescriptionMetadata.register(context);
	}
}


