import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DateRangeDescriptorDescription } from "./DateRangeDescriptorDescription";

/**
 * @hidden 
 */
export class DateRangeDescriptorDescriptionMetadata extends Base {
	static $t: Type = markType(DateRangeDescriptorDescriptionMetadata, 'DateRangeDescriptorDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (DateRangeDescriptorDescriptionMetadata._metadata == null) {
			DateRangeDescriptorDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			DateRangeDescriptorDescriptionMetadata.fillMetadata(DateRangeDescriptorDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(DateRangeDescriptorDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(DateRangeDescriptorDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("RangeType", "(wc:Type)ExportedType:string:DateRangeType");
		metadata.item("RangeType@names", "After;Before;Between;Specific;Weekdays;Weekends");
		metadata.item("RangeType@constantValues", "0;1;2;3;4;5");
		metadata.item("DateRange", "Unknown");
	}
	static register(context: TypeDescriptionContext): void {
		DateRangeDescriptorDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("DateRangeDescriptor", () => new DateRangeDescriptorDescription());
		context.register("DateRangeDescriptor", DateRangeDescriptorDescriptionMetadata._metadata);
	}
}


