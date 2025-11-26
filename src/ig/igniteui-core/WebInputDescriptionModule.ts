import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebInputBaseDescriptionModule } from "./WebInputBaseDescriptionModule";
import { WebInputDescriptionMetadata } from "./WebInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebInputDescriptionModule extends Base {
	static $t: Type = markType(WebInputDescriptionModule, 'WebInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionModule.register(context);
		WebInputDescriptionMetadata.register(context);
	}
}


