import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCalendarBaseDescriptionMetadata } from "./WebCalendarBaseDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCalendarBaseDescriptionModule extends Base {
	static $t: Type = markType(WebCalendarBaseDescriptionModule, 'WebCalendarBaseDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCalendarBaseDescriptionMetadata.register(context);
	}
}


