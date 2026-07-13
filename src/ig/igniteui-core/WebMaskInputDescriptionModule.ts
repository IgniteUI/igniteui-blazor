import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebMaskInputDescriptionMetadata } from "./WebMaskInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebMaskInputDescriptionModule extends Base {
	static $t: Type = markType(WebMaskInputDescriptionModule, 'WebMaskInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebMaskInputDescriptionMetadata.register(context);
	}
}


