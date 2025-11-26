import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCalendarBaseDescriptionModule } from "./WebCalendarBaseDescriptionModule";
import { WebComponentDataValueChangedEventArgsDescriptionMetadata } from "./WebComponentDataValueChangedEventArgsDescriptionMetadata";
import { WebCalendarDescriptionMetadata } from "./WebCalendarDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCalendarDescriptionModule extends Base {
	static $t: Type = markType(WebCalendarDescriptionModule, 'WebCalendarDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCalendarBaseDescriptionModule.register(context);
		WebComponentDataValueChangedEventArgsDescriptionMetadata.register(context);
		WebCalendarDescriptionMetadata.register(context);
	}
}


