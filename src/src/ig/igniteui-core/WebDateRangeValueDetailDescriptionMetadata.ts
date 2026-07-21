import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangeValueDetailDescription } from "./WebDateRangeValueDetailDescription";

/**
 * @hidden 
 */
export class WebDateRangeValueDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateRangeValueDetailDescriptionMetadata, 'WebDateRangeValueDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateRangeValueDetailDescriptionMetadata._metadata == null) {
			WebDateRangeValueDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateRangeValueDetailDescriptionMetadata.fillMetadata(WebDateRangeValueDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateRangeValueDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateRangeValueDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DateRangeValueDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Start", "Date");
		metadata.item("End", "Date");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateRangeValueDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateRangeValueDetail", () => new WebDateRangeValueDetailDescription());
		context.register("WebDateRangeValueDetail", WebDateRangeValueDetailDescriptionMetadata._metadata);
	}
}


