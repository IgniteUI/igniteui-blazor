import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebThemeProviderDescriptionMetadata } from "./WebThemeProviderDescriptionMetadata";

/**
 * @hidden 
 */
export class WebThemeProviderDescriptionModule extends Base {
	static $t: Type = markType(WebThemeProviderDescriptionModule, 'WebThemeProviderDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebThemeProviderDescriptionMetadata.register(context);
	}
}


