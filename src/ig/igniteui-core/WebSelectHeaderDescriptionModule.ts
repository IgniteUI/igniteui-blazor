import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSelectHeaderDescriptionMetadata } from "./WebSelectHeaderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSelectHeaderDescriptionModule extends Base {
	static $t: Type = markType(WebSelectHeaderDescriptionModule, 'WebSelectHeaderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSelectHeaderDescriptionMetadata.register(context);
	}
}


