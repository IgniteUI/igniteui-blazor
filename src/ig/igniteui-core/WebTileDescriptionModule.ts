import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTileChangeStateEventArgsDescriptionMetadata } from "./WebTileChangeStateEventArgsDescriptionMetadata";
import { WebTileComponentEventArgsDescriptionMetadata } from "./WebTileComponentEventArgsDescriptionMetadata";
import { WebTileDescriptionMetadata } from "./WebTileDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTileDescriptionModule extends Base {
	static $t: Type = markType(WebTileDescriptionModule, 'WebTileDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTileChangeStateEventArgsDescriptionMetadata.register(context);
		WebTileComponentEventArgsDescriptionMetadata.register(context);
		WebTileDescriptionMetadata.register(context);
	}
}


