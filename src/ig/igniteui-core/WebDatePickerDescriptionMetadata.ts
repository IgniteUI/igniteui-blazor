import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DateRangeDescriptorDescriptionMetadata } from "./DateRangeDescriptorDescriptionMetadata";
import { WebCalendarResourceStringsDescriptionMetadata } from "./WebCalendarResourceStringsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebComponentDateValueChangedEventArgsDescriptionMetadata } from "./WebComponentDateValueChangedEventArgsDescriptionMetadata";
import { WebBaseComboBoxLikeDescriptionMetadata } from "./WebBaseComboBoxLikeDescriptionMetadata";
import { WebDatePickerDescription } from "./WebDatePickerDescription";

/**
 * @hidden 
 */
export class WebDatePickerDescriptionMetadata extends Base {
	static $t: Type = markType(WebDatePickerDescriptionMetadata, 'WebDatePickerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDatePickerDescriptionMetadata._metadata == null) {
			WebDatePickerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDatePickerDescriptionMetadata.fillMetadata(WebDatePickerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDatePickerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDatePickerDescriptionMetadata._metadata);
		DateRangeDescriptorDescriptionMetadata.register(context);
		WebCalendarResourceStringsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
		WebComponentDateValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:DatePicker");
		metadata.item("__tagNameWC", "String:igc-date-picker");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Label", "String");
		metadata.item("Mode", "ExportedType:string:PickerMode");
		metadata.item("Mode@stringUnion", "WebComponents;React");
		metadata.item("Mode@names", "Dropdown;Dialog");
		metadata.item("NonEditable", "Boolean");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("Value", "Date");
		metadata.item("ActiveDate", "Date");
		metadata.item("Min", "Date");
		metadata.item("Max", "Date");
		metadata.item("HeaderOrientation", "ExportedType:string:CalendarHeaderOrientation");
		metadata.item("HeaderOrientation@stringUnion", "WebComponents;React");
		metadata.item("HeaderOrientation@names", "Horizontal;Vertical");
		metadata.item("Orientation", "ExportedType:string:ContentOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("HideHeader", "Boolean");
		metadata.item("HideOutsideDays", "Boolean");
		metadata.item("DisabledDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
		metadata.item("SpecialDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
		metadata.item("Outlined", "Boolean");
		metadata.item("Placeholder", "String");
		metadata.item("VisibleMonths", "Number:double");
		metadata.item("ShowWeekNumbers", "Boolean");
		metadata.item("DisplayFormat", "String");
		metadata.item("InputFormat", "String");
		metadata.item("Locale", "String");
		metadata.item("Prompt", "String");
		metadata.item("ResourceStrings", "ExportedType:WebCalendarResourceStrings");
		metadata.item("WeekStart", "ExportedType:string:WeekDays");
		metadata.item("WeekStart@stringUnion", "WebComponents;React");
		metadata.item("WeekStart@names", "Sunday;Monday;Tuesday;Wednesday;Thursday;Friday;Saturday");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("OpeningRef", "EventRef:VoidHandler:opening");
		metadata.item("OpeningRef@args", "VoidEventArgs");
		metadata.item("OpenedRef", "EventRef:VoidHandler:opened");
		metadata.item("OpenedRef@args", "VoidEventArgs");
		metadata.item("ClosingRef", "EventRef:VoidHandler:closing");
		metadata.item("ClosingRef@args", "VoidEventArgs");
		metadata.item("ClosedRef", "EventRef:VoidHandler:closed");
		metadata.item("ClosedRef@args", "VoidEventArgs");
		metadata.item("ChangeRef", "EventRef:ComponentDateValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentDateValueChangedEventArgs");
		metadata.item("InputRef", "EventRef:ComponentDateValueChangedEventHandler:input");
		metadata.item("InputRef@args", "ComponentDateValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionMetadata.register(context);
		WebDatePickerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDatePicker", () => new WebDatePickerDescription());
		context.register("WebDatePicker", WebDatePickerDescriptionMetadata._metadata);
	}
}


