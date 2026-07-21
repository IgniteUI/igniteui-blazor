import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebRatingDescriptionMetadata } from "./WebRatingDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRatingDescriptionModule extends Base {
	static $t: Type = markType(WebRatingDescriptionModule, 'WebRatingDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebRatingDescriptionMetadata.register(context);
	}
}


