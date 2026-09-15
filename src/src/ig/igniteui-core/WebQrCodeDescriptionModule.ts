import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebQrCodeDescriptionMetadata } from "./WebQrCodeDescriptionMetadata";

/**
 * @hidden
 */
export class WebQrCodeDescriptionModule extends Base {
	static $t: Type = markType(WebQrCodeDescriptionModule, 'WebQrCodeDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebQrCodeDescriptionMetadata.register(context);
	}
}


