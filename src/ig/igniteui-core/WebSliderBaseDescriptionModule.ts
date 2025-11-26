import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSliderBaseDescriptionMetadata } from "./WebSliderBaseDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSliderBaseDescriptionModule extends Base {
	static $t: Type = markType(WebSliderBaseDescriptionModule, 'WebSliderBaseDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionMetadata.register(context);
	}
}


