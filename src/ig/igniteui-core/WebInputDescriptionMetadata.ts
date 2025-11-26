import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { WebInputBaseDescriptionMetadata } from "./WebInputBaseDescriptionMetadata";
import { WebInputDescription } from "./WebInputDescription";

/**
 * @hidden 
 */
export class WebInputDescriptionMetadata extends Base {
	static $t: Type = markType(WebInputDescriptionMetadata, 'WebInputDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebInputDescriptionMetadata._metadata == null) {
			WebInputDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebInputDescriptionMetadata.fillMetadata(WebInputDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebInputDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebInputDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebInputBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Input");
		metadata.item("__tagNameWC", "String:igc-input");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("DisplayType", "(wc:Type)ExportedType:string:InputType");
		metadata.item("DisplayType@stringUnion", "WebComponents;React");
		metadata.item("DisplayType@names", "Text;Email;Number;Password;Search;Tel;Url");
		metadata.item("InputMode", "String");
		metadata.item("Pattern", "String");
		metadata.item("MinLength", "Number:double");
		metadata.item("MaxLength", "Number:double");
		metadata.item("Min", "Number:double");
		metadata.item("Max", "Number:double");
		metadata.item("Step", "Number:double");
		metadata.item("Autofocus", "Boolean");
		metadata.item("Autocomplete", "String");
		metadata.item("ValidateOnly", "Boolean");
		metadata.item("ChangeRef", "EventRef:ComponentValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionMetadata.register(context);
		WebInputDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebInput", () => new WebInputDescription());
		context.register("WebInput", WebInputDescriptionMetadata._metadata);
	}
}


