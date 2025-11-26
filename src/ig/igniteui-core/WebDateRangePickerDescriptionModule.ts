import { Base, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { WebBaseComboBoxLikeDescriptionModule } from "./WebBaseComboBoxLikeDescriptionModule";
import { WebCalendarDescriptionModule } from "./WebCalendarDescriptionModule";
import { WebDateTimeInputDescriptionModule } from "./WebDateTimeInputDescriptionModule";
import { WebDialogDescriptionModule } from "./WebDialogDescriptionModule";
import { WebIconDescriptionModule } from "./WebIconDescriptionModule";
import { WebChipDescriptionModule } from "./WebChipDescriptionModule";
import { WebInputDescriptionModule } from "./WebInputDescriptionModule";
import { WebDateRangeValueEventArgsDescriptionMetadata } from "./WebDateRangeValueEventArgsDescriptionMetadata";
import { WebDateRangePickerDescriptionMetadata } from "./WebDateRangePickerDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDateRangePickerDescriptionModule extends Base {
	static $t: Type = markType(WebDateRangePickerDescriptionModule, 'WebDateRangePickerDescriptionModule');
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionModule.register(context);
		WebCalendarDescriptionModule.register(context);
		WebDateTimeInputDescriptionModule.register(context);
		WebDialogDescriptionModule.register(context);
		WebIconDescriptionModule.register(context);
		WebChipDescriptionModule.register(context);
		WebInputDescriptionModule.register(context);
		WebDateRangeValueEventArgsDescriptionMetadata.register(context);
		WebDateRangePickerDescriptionMetadata.register(context);
	}
}


