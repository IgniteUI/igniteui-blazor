import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebTileDescriptionModule } from "./WebTileDescriptionModule";
import { WebTileManagerDescriptionMetadata } from "./WebTileManagerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebTileManagerDescriptionModule extends Base {
	static $t: Type = markType(WebTileManagerDescriptionModule, 'WebTileManagerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebTileDescriptionModule.register(context);
		WebTileManagerDescriptionMetadata.register(context);
	}
}


