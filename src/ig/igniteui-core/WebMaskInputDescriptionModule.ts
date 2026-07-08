import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebInputBaseDescriptionModule } from "./WebInputBaseDescriptionModule";
import { WebMaskInputDescriptionMetadata } from "./WebMaskInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebMaskInputDescriptionModule extends Base {
	static $t: Type = markType(WebMaskInputDescriptionModule, 'WebMaskInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionModule.register(context);
		WebMaskInputDescriptionMetadata.register(context);
	}
}


