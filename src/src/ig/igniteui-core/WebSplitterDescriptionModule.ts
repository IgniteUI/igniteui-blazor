import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebSplitterResizeEventArgsDescriptionMetadata } from "./WebSplitterResizeEventArgsDescriptionMetadata";
import { WebSplitterLayoutChangedEventArgsDescriptionMetadata } from "./WebSplitterLayoutChangedEventArgsDescriptionMetadata";
import { WebSplitterDescriptionMetadata } from "./WebSplitterDescriptionMetadata";

/**
 * @hidden
 */
export class WebSplitterDescriptionModule extends Base {
	static $t: Type = markType(WebSplitterDescriptionModule, 'WebSplitterDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebSplitterResizeEventArgsDescriptionMetadata.register(context);
		WebSplitterLayoutChangedEventArgsDescriptionMetadata.register(context);
		WebSplitterDescriptionMetadata.register(context);
	}
}


