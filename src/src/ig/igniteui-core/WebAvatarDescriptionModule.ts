import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebAvatarDescriptionMetadata } from "./WebAvatarDescriptionMetadata";

/**
 * @hidden 
 */
export class WebAvatarDescriptionModule extends Base {
	static $t: Type = markType(WebAvatarDescriptionModule, 'WebAvatarDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebAvatarDescriptionMetadata.register(context);
	}
}


