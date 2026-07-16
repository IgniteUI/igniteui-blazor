import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSliderLabelDescriptionMetadata } from "./WebSliderLabelDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSliderLabelDescriptionModule extends Base {
	static $t: Type = markType(WebSliderLabelDescriptionModule, 'WebSliderLabelDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSliderLabelDescriptionMetadata.register(context);
	}
}


