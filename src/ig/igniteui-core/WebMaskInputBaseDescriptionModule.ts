import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebInputBaseDescriptionModule } from "./WebInputBaseDescriptionModule";

/**
 * @hidden 
 */
export class WebMaskInputBaseDescriptionModule extends Base {
	static $t: Type = markType(WebMaskInputBaseDescriptionModule, 'WebMaskInputBaseDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionModule.register(context);
	}
}


