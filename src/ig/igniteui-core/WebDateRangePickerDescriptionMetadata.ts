import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangeValueDescriptionMetadata } from "./WebDateRangeValueDescriptionMetadata";
import { WebCustomDateRangeDescriptionMetadata } from "./WebCustomDateRangeDescriptionMetadata";
import { WebDateRangePickerResourceStringsDescriptionMetadata } from "./WebDateRangePickerResourceStringsDescriptionMetadata";
import { DateRangeDescriptorDescriptionMetadata } from "./DateRangeDescriptorDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebDateRangeValueEventArgsDescriptionMetadata } from "./WebDateRangeValueEventArgsDescriptionMetadata";
import { WebBaseComboBoxLikeDescriptionMetadata } from "./WebBaseComboBoxLikeDescriptionMetadata";
import { WebDateRangePickerDescription } from "./WebDateRangePickerDescription";

/**
 * @hidden 
 */
export class WebDateRangePickerDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateRangePickerDescriptionMetadata, 'WebDateRangePickerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateRangePickerDescriptionMetadata._metadata == null) {
			WebDateRangePickerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateRangePickerDescriptionMetadata.fillMetadata(WebDateRangePickerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateRangePickerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateRangePickerDescriptionMetadata._metadata);
		WebDateRangeValueDescriptionMetadata.register(context);
		WebCustomDateRangeDescriptionMetadata.register(context);
		WebDateRangePickerResourceStringsDescriptionMetadata.register(context);
		DateRangeDescriptorDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
		WebDateRangeValueEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:DateRangePicker");
		metadata.item("__tagNameWC", "String:igc-date-range-picker");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "ExportedType:WebDateRangeValue");
		metadata.item("CustomRanges", "Array:WebCustomDateRangeDescription:CustomDateRange");
		metadata.item("Mode", "ExportedType:string:PickerMode");
		metadata.item("Mode@stringUnion", "WebComponents;React");
		metadata.item("Mode@names", "Dropdown;Dialog");
		metadata.item("UseTwoInputs", "Boolean");
		metadata.item("UsePredefinedRanges", "Boolean");
		metadata.item("Locale", "String");
		metadata.item("ResourceStrings", "ExportedType:WebDateRangePickerResourceStrings");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("NonEditable", "Boolean");
		metadata.item("Outlined", "Boolean");
		metadata.item("Label", "String");
		metadata.item("LabelStart", "String");
		metadata.item("LabelEnd", "String");
		metadata.item("Placeholder", "String");
		metadata.item("PlaceholderStart", "String");
		metadata.item("PlaceholderEnd", "String");
		metadata.item("Prompt", "String");
		metadata.item("DisplayFormat", "String");
		metadata.item("InputFormat", "String");
		metadata.item("Min", "Date");
		metadata.item("Max", "Date");
		metadata.item("DisabledDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
		metadata.item("VisibleMonths", "Number:double");
		metadata.item("HeaderOrientation", "ExportedType:string:ContentOrientation");
		metadata.item("HeaderOrientation@stringUnion", "WebComponents;React");
		metadata.item("HeaderOrientation@names", "Horizontal;Vertical");
		metadata.item("Orientation", "ExportedType:string:ContentOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("HideHeader", "Boolean");
		metadata.item("ActiveDate", "Date");
		metadata.item("ShowWeekNumbers", "Boolean");
		metadata.item("HideOutsideDays", "Boolean");
		metadata.item("SpecialDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
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
		metadata.item("ChangeRef", "EventRef:DateRangeValueEventHandler:change");
		metadata.item("ChangeRef@args", "DateRangeValueEventArgs");
		metadata.item("InputRef", "EventRef:DateRangeValueEventHandler:input");
		metadata.item("InputRef@args", "DateRangeValueEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionMetadata.register(context);
		WebDateRangePickerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateRangePicker", () => new WebDateRangePickerDescription());
		context.register("WebDateRangePicker", WebDateRangePickerDescriptionMetadata._metadata);
	}
}


