import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebExpansionPanelComponentEventArgsDescriptionMetadata } from "./WebExpansionPanelComponentEventArgsDescriptionMetadata";
import { WebExpansionPanelDescriptionMetadata } from "./WebExpansionPanelDescriptionMetadata";

/**
 * @hidden 
 */
export class WebExpansionPanelDescriptionModule extends Base {
	static $t: Type = markType(WebExpansionPanelDescriptionModule, 'WebExpansionPanelDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebExpansionPanelComponentEventArgsDescriptionMetadata.register(context);
		WebExpansionPanelDescriptionMetadata.register(context);
	}
}


