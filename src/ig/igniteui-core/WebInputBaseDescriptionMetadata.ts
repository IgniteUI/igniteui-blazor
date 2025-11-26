import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";

/**
 * @hidden 
 */
export class WebInputBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebInputBaseDescriptionMetadata, 'WebInputBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebInputBaseDescriptionMetadata._metadata == null) {
			WebInputBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebInputBaseDescriptionMetadata.fillMetadata(WebInputBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebInputBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebInputBaseDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:InputBase");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Outlined", "Boolean");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("Placeholder", "String");
		metadata.item("Label", "String");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("InputOcurredRef", "EventRef:ComponentValueChangedEventHandler:inputOcurred");
		metadata.item("InputOcurredRef@args", "ComponentValueChangedEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionMetadata.ensureMetadata(context);
		context.register("WebInputBase", WebInputBaseDescriptionMetadata._metadata);
	}
}


