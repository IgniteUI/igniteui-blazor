import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebLinearProgressDescriptionMetadata } from "./WebLinearProgressDescriptionMetadata";

/**
 * @hidden 
 */
export class WebLinearProgressDescriptionModule extends Base {
	static $t: Type = markType(WebLinearProgressDescriptionModule, 'WebLinearProgressDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebLinearProgressDescriptionMetadata.register(context);
	}
}


