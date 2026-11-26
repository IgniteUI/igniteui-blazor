import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebIconDescriptionModule } from "./WebIconDescriptionModule";
import { WebInputDescriptionModule } from "./WebInputDescriptionModule";
import { WebComboChangeEventArgsDescriptionMetadata } from "./WebComboChangeEventArgsDescriptionMetadata";
import { WebComboClosingEventArgsDescriptionMetadata } from "./WebComboClosingEventArgsDescriptionMetadata";
import { WebComboSearchInputEventArgsDescriptionMetadata } from "./WebComboSearchInputEventArgsDescriptionMetadata";
import { WebVirtualScrollDataRequestEventArgsDescriptionMetadata } from "./WebVirtualScrollDataRequestEventArgsDescriptionMetadata";
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
		WebComboClosingEventArgsDescriptionMetadata.register(context);
		WebComboSearchInputEventArgsDescriptionMetadata.register(context);
		WebVirtualScrollDataRequestEventArgsDescriptionMetadata.register(context);
		WebComboDescriptionMetadata.register(context);
	}
}


