import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSnackbarDescriptionMetadata } from "./WebSnackbarDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSnackbarDescriptionModule extends Base {
	static $t: Type = markType(WebSnackbarDescriptionModule, 'WebSnackbarDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSnackbarDescriptionMetadata.register(context);
	}
}


