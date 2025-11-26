import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebRippleDescriptionMetadata } from "./WebRippleDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRippleDescriptionModule extends Base {
	static $t: Type = markType(WebRippleDescriptionModule, 'WebRippleDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebRippleDescriptionMetadata.register(context);
	}
}


