import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSliderBaseDescriptionModule } from "./WebSliderBaseDescriptionModule";
import { WebSliderDescriptionMetadata } from "./WebSliderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSliderDescriptionModule extends Base {
	static $t: Type = markType(WebSliderDescriptionModule, 'WebSliderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionModule.register(context);
		WebSliderDescriptionMetadata.register(context);
	}
}


