import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBannerDescriptionMetadata } from "./WebBannerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebBannerDescriptionModule extends Base {
	static $t: Type = markType(WebBannerDescriptionModule, 'WebBannerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBannerDescriptionMetadata.register(context);
	}
}


