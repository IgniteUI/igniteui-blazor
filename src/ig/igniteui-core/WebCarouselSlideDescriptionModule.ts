import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCarouselSlideDescriptionMetadata } from "./WebCarouselSlideDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCarouselSlideDescriptionModule extends Base {
	static $t: Type = markType(WebCarouselSlideDescriptionModule, 'WebCarouselSlideDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCarouselSlideDescriptionMetadata.register(context);
	}
}


