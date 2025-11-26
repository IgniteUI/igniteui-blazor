import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebDividerDescriptionMetadata } from "./WebDividerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDividerDescriptionModule extends Base {
	static $t: Type = markType(WebDividerDescriptionModule, 'WebDividerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebDividerDescriptionMetadata.register(context);
	}
}


