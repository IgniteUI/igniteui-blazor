import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { WebComponentDateValueChangedEventArgsDescriptionMetadata } from "./WebComponentDateValueChangedEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebDateTimeInputBaseDescriptionMetadata } from "./WebDateTimeInputBaseDescriptionMetadata";
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
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
		WebComponentDateValueChangedEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebDateTimeInputBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:DateTimeInput");
		metadata.item("__tagNameWC", "String:igc-date-time-input");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "Date");
		metadata.item("InputOcurredRef", "EventRef:ComponentValueChangedEventHandler:inputOcurred");
		metadata.item("InputOcurredRef@args", "ComponentValueChangedEventArgs");
		metadata.item("ChangeRef", "EventRef:ComponentDateValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentDateValueChangedEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateTimeInputBaseDescriptionMetadata.register(context);
		WebDateTimeInputDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateTimeInput", () => new WebDateTimeInputDescription());
		context.register("WebDateTimeInput", WebDateTimeInputDescriptionMetadata._metadata);
	}
}


