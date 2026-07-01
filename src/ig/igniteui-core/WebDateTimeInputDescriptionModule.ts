import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebDateTimeInputBaseDescriptionModule } from "./WebDateTimeInputBaseDescriptionModule";
import { WebDateTimeInputDescriptionMetadata } from "./WebDateTimeInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDateTimeInputDescriptionModule extends Base {
	static $t: Type = markType(WebDateTimeInputDescriptionModule, 'WebDateTimeInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebDateTimeInputBaseDescriptionModule.register(context);
		WebDateTimeInputDescriptionMetadata.register(context);
	}
}


