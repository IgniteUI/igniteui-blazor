import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebActiveStepChangingEventArgsDescriptionMetadata } from "./WebActiveStepChangingEventArgsDescriptionMetadata";
import { WebActiveStepChangedEventArgsDescriptionMetadata } from "./WebActiveStepChangedEventArgsDescriptionMetadata";
import { WebStepDescriptionModule } from "./WebStepDescriptionModule";
import { WebStepperDescriptionMetadata } from "./WebStepperDescriptionMetadata";

/**
 * @hidden 
 */
export class WebStepperDescriptionModule extends Base {
	static $t: Type = markType(WebStepperDescriptionModule, 'WebStepperDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebActiveStepChangingEventArgsDescriptionMetadata.register(context);
		WebActiveStepChangedEventArgsDescriptionMetadata.register(context);
		WebStepDescriptionModule.register(context);
		WebStepperDescriptionMetadata.register(context);
	}
}


