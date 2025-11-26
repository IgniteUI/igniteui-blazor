import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBaseComboBoxLikeDescriptionModule } from "./WebBaseComboBoxLikeDescriptionModule";
import { WebIconDescriptionModule } from "./WebIconDescriptionModule";
import { WebInputDescriptionModule } from "./WebInputDescriptionModule";
import { WebSelectGroupDescriptionModule } from "./WebSelectGroupDescriptionModule";
import { WebSelectHeaderDescriptionModule } from "./WebSelectHeaderDescriptionModule";
import { WebSelectItemDescriptionModule } from "./WebSelectItemDescriptionModule";
import { WebSelectItemComponentEventArgsDescriptionMetadata } from "./WebSelectItemComponentEventArgsDescriptionMetadata";
import { WebSelectDescriptionMetadata } from "./WebSelectDescriptionMetadata";

/**
 * @hidden 
 */
export class WebSelectDescriptionModule extends Base {
	static $t: Type = markType(WebSelectDescriptionModule, 'WebSelectDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionModule.register(context);
		WebIconDescriptionModule.register(context);
		WebInputDescriptionModule.register(context);
		WebSelectGroupDescriptionModule.register(context);
		WebSelectHeaderDescriptionModule.register(context);
		WebSelectItemDescriptionModule.register(context);
		WebSelectItemComponentEventArgsDescriptionMetadata.register(context);
		WebSelectDescriptionMetadata.register(context);
	}
}


