import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebRadioGroupDescriptionMetadata } from "./WebRadioGroupDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRadioGroupDescriptionModule extends Base {
	static $t: Type = markType(WebRadioGroupDescriptionModule, 'WebRadioGroupDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebRadioGroupDescriptionMetadata.register(context);
	}
}


