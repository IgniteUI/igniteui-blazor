import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DateRangeDescriptorDescriptionMetadata } from "./DateRangeDescriptorDescriptionMetadata";
import { WebCalendarBaseDescription } from "./WebCalendarBaseDescription";

/**
 * @hidden 
 */
export class WebCalendarBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebCalendarBaseDescriptionMetadata, 'WebCalendarBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCalendarBaseDescriptionMetadata._metadata == null) {
			WebCalendarBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCalendarBaseDescriptionMetadata.fillMetadata(WebCalendarBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCalendarBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCalendarBaseDescriptionMetadata._metadata);
		DateRangeDescriptorDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CalendarBase");
		metadata.item("__tagNameWC", "String:igc-calendar-base");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Selection", "ExportedType:string:CalendarSelection");
		metadata.item("Selection@stringUnion", "WebComponents;React");
		metadata.item("Selection@names", "Single;Multiple;Range");
		metadata.item("ShowWeekNumbers", "Boolean");
		metadata.item("WeekStart", "ExportedType:string:WeekDays");
		metadata.item("WeekStart@stringUnion", "WebComponents;React");
		metadata.item("WeekStart@names", "Sunday;Monday;Tuesday;Wednesday;Thursday;Friday;Saturday");
		metadata.item("Locale", "String");
		metadata.item("SpecialDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
		metadata.item("DisabledDates", "Array:DateRangeDescriptorDescription:IDateRangeDescriptor");
	}
	static register(context: TypeDescriptionContext): void {
		WebCalendarBaseDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCalendarBase", () => new WebCalendarBaseDescription());
		context.register("WebCalendarBase", WebCalendarBaseDescriptionMetadata._metadata);
	}
}


