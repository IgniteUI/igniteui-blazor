import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCalendarResourceStringsDescription } from "./WebCalendarResourceStringsDescription";

/**
 * @hidden 
 */
export class WebCalendarResourceStringsDescriptionMetadata extends Base {
	static $t: Type = markType(WebCalendarResourceStringsDescriptionMetadata, 'WebCalendarResourceStringsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCalendarResourceStringsDescriptionMetadata._metadata == null) {
			WebCalendarResourceStringsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCalendarResourceStringsDescriptionMetadata.fillMetadata(WebCalendarResourceStringsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCalendarResourceStringsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCalendarResourceStringsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CalendarResourceStrings");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("SelectMonth", "String");
		metadata.item("SelectYear", "String");
		metadata.item("SelectDate", "String");
		metadata.item("SelectRange", "String");
		metadata.item("SelectedDate", "String");
		metadata.item("StartDate", "String");
		metadata.item("EndDate", "String");
		metadata.item("PreviousMonth", "String");
		metadata.item("NextMonth", "String");
		metadata.item("PreviousYear", "String");
		metadata.item("NextYear", "String");
		metadata.item("PreviousYears", "String");
		metadata.item("NextYears", "String");
		metadata.item("WeekLabel", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebCalendarResourceStringsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCalendarResourceStrings", () => new WebCalendarResourceStringsDescription());
		context.register("WebCalendarResourceStrings", WebCalendarResourceStringsDescriptionMetadata._metadata);
	}
}


