import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { DatePartDeltasDescriptionMetadata } from "./DatePartDeltasDescriptionMetadata";
import { WebComponentDateValueChangedEventArgsDescriptionMetadata } from "./WebComponentDateValueChangedEventArgsDescriptionMetadata";
import { WebMaskInputBaseDescriptionMetadata } from "./WebMaskInputBaseDescriptionMetadata";
import { WebDateTimeInputDescription } from "./WebDateTimeInputDescription";

/**
 * @hidden 
 */
export class WebDateTimeInputDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateTimeInputDescriptionMetadata, 'WebDateTimeInputDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateTimeInputDescriptionMetadata._metadata == null) {
			WebDateTimeInputDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateTimeInputDescriptionMetadata.fillMetadata(WebDateTimeInputDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateTimeInputDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateTimeInputDescriptionMetadata._metadata);
		DatePartDeltasDescriptionMetadata.register(context);
		WebComponentDateValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebMaskInputBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:DateTimeInput");
		metadata.item("__tagNameWC", "String:igc-date-time-input");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("InputFormat", "String");
		metadata.item("Value", "Date");
		metadata.item("Min", "Date");
		metadata.item("Max", "Date");
		metadata.item("DisplayFormat", "String");
		metadata.item("SpinDelta", "ExportedType:DatePartDeltas");
		metadata.item("SpinLoop", "Boolean");
		metadata.item("Locale", "String");
		metadata.item("ChangeRef", "EventRef:ComponentDateValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentDateValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebMaskInputBaseDescriptionMetadata.register(context);
		WebDateTimeInputDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateTimeInput", () => new WebDateTimeInputDescription());
		context.register("WebDateTimeInput", WebDateTimeInputDescriptionMetadata._metadata);
	}
}


