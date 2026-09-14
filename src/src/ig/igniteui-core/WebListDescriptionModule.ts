import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebListHeaderDescriptionModule } from "./WebListHeaderDescriptionModule";
import { WebListItemDescriptionModule } from "./WebListItemDescriptionModule";
import { WebListDescriptionMetadata } from "./WebListDescriptionMetadata";

/**
 * @hidden 
 */
export class WebListDescriptionModule extends Base {
	static $t: Type = markType(WebListDescriptionModule, 'WebListDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebListHeaderDescriptionModule.register(context);
		WebListItemDescriptionModule.register(context);
		WebListDescriptionMetadata.register(context);
	}
}


