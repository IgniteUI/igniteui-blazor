import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { CalendarFormatOptionsDescriptionMetadata } from "./CalendarFormatOptionsDescriptionMetadata";
import { WebCalendarResourceStringsDescriptionMetadata } from "./WebCalendarResourceStringsDescriptionMetadata";
import { WebComponentDataValueChangedEventArgsDescriptionMetadata } from "./WebComponentDataValueChangedEventArgsDescriptionMetadata";
import { WebCalendarBaseDescriptionMetadata } from "./WebCalendarBaseDescriptionMetadata";
import { WebCalendarDescription } from "./WebCalendarDescription";

/**
 * @hidden 
 */
export class WebCalendarDescriptionMetadata extends Base {
	static $t: Type = markType(WebCalendarDescriptionMetadata, 'WebCalendarDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCalendarDescriptionMetadata._metadata == null) {
			WebCalendarDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCalendarDescriptionMetadata.fillMetadata(WebCalendarDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCalendarDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCalendarDescriptionMetadata._metadata);
		CalendarFormatOptionsDescriptionMetadata.register(context);
		WebCalendarResourceStringsDescriptionMetadata.register(context);
		WebComponentDataValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebCalendarBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Calendar");
		metadata.item("__tagNameWC", "String:igc-calendar");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "Date");
		metadata.item("Values", "Array:Date");
		metadata.item("ActiveDate", "Date");
		metadata.item("HideOutsideDays", "Boolean");
		metadata.item("HideHeader", "Boolean");
		metadata.item("HeaderOrientation", "ExportedType:string:CalendarHeaderOrientation");
		metadata.item("HeaderOrientation@stringUnion", "WebComponents;React");
		metadata.item("HeaderOrientation@names", "Horizontal;Vertical");
		metadata.item("Orientation", "ExportedType:string:ContentOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("VisibleMonths", "Number:double");
		metadata.item("ActiveView", "ExportedType:string:CalendarActiveView");
		metadata.item("ActiveView@stringUnion", "WebComponents;React");
		metadata.item("ActiveView@names", "Days;Months;Years");
		metadata.item("FormatOptions", "ExportedType:CalendarFormatOptions");
		metadata.item("ResourceStrings", "ExportedType:WebCalendarResourceStrings");
		metadata.item("ChangeRef", "EventRef:ComponentDataValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentDataValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebCalendarBaseDescriptionMetadata.register(context);
		WebCalendarDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCalendar", () => new WebCalendarDescription());
		context.register("WebCalendar", WebCalendarDescriptionMetadata._metadata);
	}
}


