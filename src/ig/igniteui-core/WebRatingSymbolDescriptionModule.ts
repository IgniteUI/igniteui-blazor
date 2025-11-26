import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebRatingSymbolDescriptionMetadata } from "./WebRatingSymbolDescriptionMetadata";

/**
 * @hidden 
 */
export class WebRatingSymbolDescriptionModule extends Base {
	static $t: Type = markType(WebRatingSymbolDescriptionModule, 'WebRatingSymbolDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebRatingSymbolDescriptionMetadata.register(context);
	}
}


