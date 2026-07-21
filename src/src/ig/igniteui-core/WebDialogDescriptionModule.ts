import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebButtonDescriptionModule } from "./WebButtonDescriptionModule";
import { WebDialogDescriptionMetadata } from "./WebDialogDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDialogDescriptionModule extends Base {
	static $t: Type = markType(WebDialogDescriptionModule, 'WebDialogDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebButtonDescriptionModule.register(context);
		WebDialogDescriptionMetadata.register(context);
	}
}


