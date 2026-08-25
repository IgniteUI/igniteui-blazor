import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebCardActionsDescriptionModule } from "./WebCardActionsDescriptionModule";
import { WebCardContentDescriptionModule } from "./WebCardContentDescriptionModule";
import { WebCardHeaderDescriptionModule } from "./WebCardHeaderDescriptionModule";
import { WebCardMediaDescriptionModule } from "./WebCardMediaDescriptionModule";
import { WebCardDescriptionMetadata } from "./WebCardDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCardDescriptionModule extends Base {
	static $t: Type = markType(WebCardDescriptionModule, 'WebCardDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebCardActionsDescriptionModule.register(context);
		WebCardContentDescriptionModule.register(context);
		WebCardHeaderDescriptionModule.register(context);
		WebCardMediaDescriptionModule.register(context);
		WebCardDescriptionMetadata.register(context);
	}
}


