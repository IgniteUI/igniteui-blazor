import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBaseComboBoxLikeDescriptionModule } from "./WebBaseComboBoxLikeDescriptionModule";
import { WebCalendarDescriptionModule } from "./WebCalendarDescriptionModule";
import { WebDateTimeInputDescriptionModule } from "./WebDateTimeInputDescriptionModule";
import { WebDialogDescriptionModule } from "./WebDialogDescriptionModule";
import { WebIconDescriptionModule } from "./WebIconDescriptionModule";
import { WebDatePickerDescriptionMetadata } from "./WebDatePickerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDatePickerDescriptionModule extends Base {
	static $t: Type = markType(WebDatePickerDescriptionModule, 'WebDatePickerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionModule.register(context);
		WebCalendarDescriptionModule.register(context);
		WebDateTimeInputDescriptionModule.register(context);
		WebDialogDescriptionModule.register(context);
		WebIconDescriptionModule.register(context);
		WebDatePickerDescriptionMetadata.register(context);
	}
}


