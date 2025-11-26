import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebProgressBaseDescriptionModule } from "./WebProgressBaseDescriptionModule";
import { WebCircularProgressDescriptionMetadata } from "./WebCircularProgressDescriptionMetadata";

/**
 * @hidden 
 */
export class WebCircularProgressDescriptionModule extends Base {
	static $t: Type = markType(WebCircularProgressDescriptionModule, 'WebCircularProgressDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebProgressBaseDescriptionModule.register(context);
		WebCircularProgressDescriptionMetadata.register(context);
	}
}


