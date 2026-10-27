import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBreadcrumbDescriptionMetadata } from "./WebBreadcrumbDescriptionMetadata";

/**
 * @hidden 
 */
export class WebBreadcrumbDescriptionModule extends Base {
	static $t: Type = markType(WebBreadcrumbDescriptionModule, 'WebBreadcrumbDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBreadcrumbDescriptionMetadata.register(context);
	}
}


