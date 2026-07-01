import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebHighlightDescriptionMetadata } from "./WebHighlightDescriptionMetadata";

/**
 * @hidden 
 */
export class WebHighlightDescriptionModule extends Base {
	static $t: Type = markType(WebHighlightDescriptionModule, 'WebHighlightDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebHighlightDescriptionMetadata.register(context);
	}
}


