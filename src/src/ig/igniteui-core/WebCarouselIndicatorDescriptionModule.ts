import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCarouselIndicatorDescriptionMetadata } from "./WebCarouselIndicatorDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCarouselIndicatorDescriptionModule extends Base {
	static $t: Type = markType(WebCarouselIndicatorDescriptionModule, 'WebCarouselIndicatorDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCarouselIndicatorDescriptionMetadata.register(context);
	}
}


