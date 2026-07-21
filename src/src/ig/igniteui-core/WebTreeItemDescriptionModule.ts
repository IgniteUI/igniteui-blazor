import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTreeItemDescriptionMetadata } from "./WebTreeItemDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTreeItemDescriptionModule extends Base {
	static $t: Type = markType(WebTreeItemDescriptionModule, 'WebTreeItemDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTreeItemDescriptionMetadata.register(context);
	}
}


