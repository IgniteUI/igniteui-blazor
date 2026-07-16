import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangeValueDescription } from "./WebDateRangeValueDescription";

/**
 * @hidden 
 */
export class WebDateRangeValueDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateRangeValueDescriptionMetadata, 'WebDateRangeValueDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateRangeValueDescriptionMetadata._metadata == null) {
			WebDateRangeValueDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateRangeValueDescriptionMetadata.fillMetadata(WebDateRangeValueDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateRangeValueDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateRangeValueDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DateRangeValue");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("Start", "Date");
		metadata.item("End", "Date");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateRangeValueDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateRangeValue", () => new WebDateRangeValueDescription());
		context.register("WebDateRangeValue", WebDateRangeValueDescriptionMetadata._metadata);
	}
}


