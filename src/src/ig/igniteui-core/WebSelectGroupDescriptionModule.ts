import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSelectItemDescriptionModule } from "./WebSelectItemDescriptionModule";
import { WebSelectGroupDescriptionMetadata } from "./WebSelectGroupDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSelectGroupDescriptionModule extends Base {
	static $t: Type = markType(WebSelectGroupDescriptionModule, 'WebSelectGroupDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSelectItemDescriptionModule.register(context);
		WebSelectGroupDescriptionMetadata.register(context);
	}
}


