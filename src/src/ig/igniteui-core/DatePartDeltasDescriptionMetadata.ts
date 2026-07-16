import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DatePartDeltasDescription } from "./DatePartDeltasDescription";

/**
 * @hidden 
 */
export class DatePartDeltasDescriptionMetadata extends Base {
	static $t: Type = markType(DatePartDeltasDescriptionMetadata, 'DatePartDeltasDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (DatePartDeltasDescriptionMetadata._metadata == null) {
			DatePartDeltasDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			DatePartDeltasDescriptionMetadata.fillMetadata(DatePartDeltasDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(DatePartDeltasDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(DatePartDeltasDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Date", "Number:double");
		metadata.item("Month", "Number:double");
		metadata.item("Year", "Number:double");
		metadata.item("Hours", "Number:double");
		metadata.item("Minutes", "Number:double");
		metadata.item("Seconds", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		DatePartDeltasDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("DatePartDeltas", () => new DatePartDeltasDescription());
		context.register("DatePartDeltas", DatePartDeltasDescriptionMetadata._metadata);
	}
}


