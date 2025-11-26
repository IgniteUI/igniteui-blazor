import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangePickerResourceStringsDescription } from "./WebDateRangePickerResourceStringsDescription";

/**
 * @hidden 
 */
export class WebDateRangePickerResourceStringsDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateRangePickerResourceStringsDescriptionMetadata, 'WebDateRangePickerResourceStringsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateRangePickerResourceStringsDescriptionMetadata._metadata == null) {
			WebDateRangePickerResourceStringsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateRangePickerResourceStringsDescriptionMetadata.fillMetadata(WebDateRangePickerResourceStringsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateRangePickerResourceStringsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateRangePickerResourceStringsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DateRangePickerResourceStrings");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateRangePickerResourceStringsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateRangePickerResourceStrings", () => new WebDateRangePickerResourceStringsDescription());
		context.register("WebDateRangePickerResourceStrings", WebDateRangePickerResourceStringsDescriptionMetadata._metadata);
	}
}


