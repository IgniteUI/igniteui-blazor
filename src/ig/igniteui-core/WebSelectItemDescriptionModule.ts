import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSelectItemDescriptionMetadata } from "./WebSelectItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSelectItemDescriptionModule extends Base {
	static $t: Type = markType(WebSelectItemDescriptionModule, 'WebSelectItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSelectItemDescriptionMetadata.register(context);
	}
}


