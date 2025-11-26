import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangeValueDescriptionMetadata } from "./WebDateRangeValueDescriptionMetadata";
import { WebCustomDateRangeDescription } from "./WebCustomDateRangeDescription";

/**
 * @hidden 
 */
export class WebCustomDateRangeDescriptionMetadata extends Base {
	static $t: Type = markType(WebCustomDateRangeDescriptionMetadata, 'WebCustomDateRangeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCustomDateRangeDescriptionMetadata._metadata == null) {
			WebCustomDateRangeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCustomDateRangeDescriptionMetadata.fillMetadata(WebCustomDateRangeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCustomDateRangeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCustomDateRangeDescriptionMetadata._metadata);
		WebDateRangeValueDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CustomDateRange");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("Label", "String");
		metadata.item("DateRange", "ExportedType:WebDateRangeValue");
	}
	static register(context: TypeDescriptionContext): void {
		WebCustomDateRangeDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCustomDateRange", () => new WebCustomDateRangeDescription());
		context.register("WebCustomDateRange", WebCustomDateRangeDescriptionMetadata._metadata);
	}
}


