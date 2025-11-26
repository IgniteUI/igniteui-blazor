import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSliderBaseDescriptionModule } from "./WebSliderBaseDescriptionModule";
import { WebRangeSliderValueEventArgsDescriptionMetadata } from "./WebRangeSliderValueEventArgsDescriptionMetadata";
import { WebRangeSliderDescriptionMetadata } from "./WebRangeSliderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRangeSliderDescriptionModule extends Base {
	static $t: Type = markType(WebRangeSliderDescriptionModule, 'WebRangeSliderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionModule.register(context);
		WebRangeSliderValueEventArgsDescriptionMetadata.register(context);
		WebRangeSliderDescriptionMetadata.register(context);
	}
}


