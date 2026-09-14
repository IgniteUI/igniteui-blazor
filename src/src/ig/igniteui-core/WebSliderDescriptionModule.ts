import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSliderLabelDescriptionModule } from "./WebSliderLabelDescriptionModule";
import { WebSliderBaseDescriptionModule } from "./WebSliderBaseDescriptionModule";
import { WebSliderDescriptionMetadata } from "./WebSliderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSliderDescriptionModule extends Base {
	static $t: Type = markType(WebSliderDescriptionModule, 'WebSliderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSliderLabelDescriptionModule.register(context);
		WebSliderBaseDescriptionModule.register(context);
		WebSliderDescriptionMetadata.register(context);
	}
}


