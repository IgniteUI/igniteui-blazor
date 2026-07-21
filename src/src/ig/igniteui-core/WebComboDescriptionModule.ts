import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebIconDescriptionModule } from "./WebIconDescriptionModule";
import { WebInputDescriptionModule } from "./WebInputDescriptionModule";
import { WebComboChangeEventArgsDescriptionMetadata } from "./WebComboChangeEventArgsDescriptionMetadata";
import { WebComboDescriptionMetadata } from "./WebComboDescriptionMetadata";

/**
 * @hidden 
 */
export class WebComboDescriptionModule extends Base {
	static $t: Type = markType(WebComboDescriptionModule, 'WebComboDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebIconDescriptionModule.register(context);
		WebInputDescriptionModule.register(context);
		WebComboChangeEventArgsDescriptionMetadata.register(context);
		WebComboDescriptionMetadata.register(context);
	}
}


