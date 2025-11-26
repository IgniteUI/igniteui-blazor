import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTreeItemDescriptionModule } from "./WebTreeItemDescriptionModule";
import { WebTreeSelectionEventArgsDescriptionMetadata } from "./WebTreeSelectionEventArgsDescriptionMetadata";
import { WebTreeItemComponentEventArgsDescriptionMetadata } from "./WebTreeItemComponentEventArgsDescriptionMetadata";
import { WebTreeDescriptionMetadata } from "./WebTreeDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTreeDescriptionModule extends Base {
	static $t: Type = markType(WebTreeDescriptionModule, 'WebTreeDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTreeItemDescriptionModule.register(context);
		WebTreeSelectionEventArgsDescriptionMetadata.register(context);
		WebTreeItemComponentEventArgsDescriptionMetadata.register(context);
		WebTreeDescriptionMetadata.register(context);
	}
}


