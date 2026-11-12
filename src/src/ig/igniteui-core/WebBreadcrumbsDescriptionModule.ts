import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBreadcrumbDescriptionModule } from "./WebBreadcrumbDescriptionModule";
import { WebBreadcrumbsDescriptionMetadata } from "./WebBreadcrumbsDescriptionMetadata";

/**
 * @hidden 
 */
export class WebBreadcrumbsDescriptionModule extends Base {
	static $t: Type = markType(WebBreadcrumbsDescriptionModule, 'WebBreadcrumbsDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBreadcrumbDescriptionModule.register(context);
		WebBreadcrumbsDescriptionMetadata.register(context);
	}
}


