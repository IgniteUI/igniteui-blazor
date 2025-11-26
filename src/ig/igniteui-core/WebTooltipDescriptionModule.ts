import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTooltipDescriptionMetadata } from "./WebTooltipDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTooltipDescriptionModule extends Base {
	static $t: Type = markType(WebTooltipDescriptionModule, 'WebTooltipDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTooltipDescriptionMetadata.register(context);
	}
}


