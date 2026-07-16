import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DatePartDeltasDescriptionMetadata } from "./DatePartDeltasDescriptionMetadata";

/**
 * @hidden 
 */
export class WebDateTimeInputBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateTimeInputBaseDescriptionMetadata, 'WebDateTimeInputBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateTimeInputBaseDescriptionMetadata._metadata == null) {
			WebDateTimeInputBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateTimeInputBaseDescriptionMetadata.fillMetadata(WebDateTimeInputBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateTimeInputBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateTimeInputBaseDescriptionMetadata._metadata);
		DatePartDeltasDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DateTimeInputBase");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Outlined", "Boolean");
		metadata.item("Placeholder", "String");
		metadata.item("Label", "String");
		metadata.item("InputFormat", "String");
		metadata.item("Min", "Date");
		metadata.item("Max", "Date");
		metadata.item("DisplayFormat", "String");
		metadata.item("SpinDelta", "ExportedType:DatePartDeltas");
		metadata.item("SpinLoop", "Boolean");
		metadata.item("Locale", "String");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("Mask", "String");
		metadata.item("Prompt", "String");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateTimeInputBaseDescriptionMetadata.ensureMetadata(context);
		context.register("WebDateTimeInputBase", WebDateTimeInputBaseDescriptionMetadata._metadata);
	}
}


