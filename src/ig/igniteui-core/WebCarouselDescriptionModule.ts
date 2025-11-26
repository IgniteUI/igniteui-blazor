import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCarouselDescriptionMetadata } from "./WebCarouselDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCarouselDescriptionModule extends Base {
	static $t: Type = markType(WebCarouselDescriptionModule, 'WebCarouselDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCarouselDescriptionMetadata.register(context);
	}
}


