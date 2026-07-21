import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCircularGradientDescriptionMetadata } from "./WebCircularGradientDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCircularGradientDescriptionModule extends Base {
	static $t: Type = markType(WebCircularGradientDescriptionModule, 'WebCircularGradientDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCircularGradientDescriptionMetadata.register(context);
	}
}


