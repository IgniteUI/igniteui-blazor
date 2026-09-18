import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebColorPickerDescriptionMetadata } from "./WebColorPickerDescriptionMetadata";

/**
 * @hidden
 */
export class WebColorPickerDescriptionModule extends Base {
	static $t: Type = markType(WebColorPickerDescriptionModule, 'WebColorPickerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebColorPickerDescriptionMetadata.register(context);
	}
}


