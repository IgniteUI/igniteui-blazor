import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebVirtualScrollStateChangeEventArgsDescriptionMetadata } from "./WebVirtualScrollStateChangeEventArgsDescriptionMetadata";
import { WebVirtualScrollDataRequestEventArgsDescriptionMetadata } from "./WebVirtualScrollDataRequestEventArgsDescriptionMetadata";
import { WebVirtualScrollDescriptionMetadata } from "./WebVirtualScrollDescriptionMetadata";

/**
 * @hidden
 */
export class WebVirtualScrollDescriptionModule extends Base {
	static $t: Type = markType(WebVirtualScrollDescriptionModule, 'WebVirtualScrollDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollStateChangeEventArgsDescriptionMetadata.register(context);
		WebVirtualScrollDataRequestEventArgsDescriptionMetadata.register(context);
		WebVirtualScrollDescriptionMetadata.register(context);
	}
}


