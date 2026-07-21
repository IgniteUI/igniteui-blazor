import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { CalendarFormatOptionsDescription } from "./CalendarFormatOptionsDescription";

/**
 * @hidden 
 */
export class CalendarFormatOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(CalendarFormatOptionsDescriptionMetadata, 'CalendarFormatOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (CalendarFormatOptionsDescriptionMetadata._metadata == null) {
			CalendarFormatOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			CalendarFormatOptionsDescriptionMetadata.fillMetadata(CalendarFormatOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(CalendarFormatOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(CalendarFormatOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Weekday", "String");
		metadata.item("Month", "String");
	}
	static register(context: TypeDescriptionContext): void {
		CalendarFormatOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("CalendarFormatOptions", () => new CalendarFormatOptionsDescription());
		context.register("CalendarFormatOptions", CalendarFormatOptionsDescriptionMetadata._metadata);
	}
}


