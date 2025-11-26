import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebMaskInputBaseDescriptionModule } from "./WebMaskInputBaseDescriptionModule";
import { WebMaskInputDescriptionMetadata } from "./WebMaskInputDescriptionMetadata";

/**
 * @hidden 
 */
export class WebMaskInputDescriptionModule extends Base {
	static $t: Type = markType(WebMaskInputDescriptionModule, 'WebMaskInputDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebMaskInputBaseDescriptionModule.register(context);
		WebMaskInputDescriptionMetadata.register(context);
	}
}


