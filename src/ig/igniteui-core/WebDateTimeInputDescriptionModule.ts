import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebMaskInputBaseDescriptionModule } from "./WebMaskInputBaseDescriptionModule";
import { WebDateTimeInputDescriptionMetadata } from "./WebDateTimeInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDateTimeInputDescriptionModule extends Base {
	static $t: Type = markType(WebDateTimeInputDescriptionModule, 'WebDateTimeInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebMaskInputBaseDescriptionModule.register(context);
		WebDateTimeInputDescriptionMetadata.register(context);
	}
}


