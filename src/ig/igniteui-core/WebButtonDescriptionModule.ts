import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebButtonBaseDescriptionModule } from "./WebButtonBaseDescriptionModule";
import { WebButtonDescriptionMetadata } from "./WebButtonDescriptionMetadata";

/**
 * @hidden 
 */
export class WebButtonDescriptionModule extends Base {
	static $t: Type = markType(WebButtonDescriptionModule, 'WebButtonDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebButtonBaseDescriptionModule.register(context);
		WebButtonDescriptionMetadata.register(context);
	}
}


