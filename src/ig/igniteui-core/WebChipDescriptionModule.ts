import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebChipDescriptionMetadata } from "./WebChipDescriptionMetadata";

/**
 * @hidden 
 */
export class WebChipDescriptionModule extends Base {
	static $t: Type = markType(WebChipDescriptionModule, 'WebChipDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebChipDescriptionMetadata.register(context);
	}
}


