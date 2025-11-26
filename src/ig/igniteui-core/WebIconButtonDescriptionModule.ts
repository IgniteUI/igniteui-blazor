import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebButtonBaseDescriptionModule } from "./WebButtonBaseDescriptionModule";
import { WebIconButtonDescriptionMetadata } from "./WebIconButtonDescriptionMetadata";

/**
 * @hidden 
 */
export class WebIconButtonDescriptionModule extends Base {
	static $t: Type = markType(WebIconButtonDescriptionModule, 'WebIconButtonDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebButtonBaseDescriptionModule.register(context);
		WebIconButtonDescriptionMetadata.register(context);
	}
}


