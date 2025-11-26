import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBaseAlertLikeDescriptionModule } from "./WebBaseAlertLikeDescriptionModule";
import { WebToastDescriptionMetadata } from "./WebToastDescriptionMetadata";

/**
 * @hidden 
 */
export class WebToastDescriptionModule extends Base {
	static $t: Type = markType(WebToastDescriptionModule, 'WebToastDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBaseAlertLikeDescriptionModule.register(context);
		WebToastDescriptionMetadata.register(context);
	}
}


