import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCarouselIndicatorDescriptionModule } from "./WebCarouselIndicatorDescriptionModule";
import { WebCarouselSlideDescriptionModule } from "./WebCarouselSlideDescriptionModule";
import { WebCarouselDescriptionMetadata } from "./WebCarouselDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCarouselDescriptionModule extends Base {
	static $t: Type = markType(WebCarouselDescriptionModule, 'WebCarouselDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCarouselIndicatorDescriptionModule.register(context);
		WebCarouselSlideDescriptionModule.register(context);
		WebCarouselDescriptionMetadata.register(context);
	}
}


