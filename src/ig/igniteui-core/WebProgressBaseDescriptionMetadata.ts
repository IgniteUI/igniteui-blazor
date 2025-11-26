import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class WebProgressBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebProgressBaseDescriptionMetadata, 'WebProgressBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebProgressBaseDescriptionMetadata._metadata == null) {
			WebProgressBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebProgressBaseDescriptionMetadata.fillMetadata(WebProgressBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebProgressBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebProgressBaseDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ProgressBase");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Max", "Number:double");
		metadata.item("Value", "Number:double");
		metadata.item("Variant", "ExportedType:string:StyleVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Primary;Info;Success;Warning;Danger");
		metadata.item("AnimationDuration", "Number:double");
		metadata.item("Indeterminate", "Boolean");
		metadata.item("HideLabel", "Boolean");
		metadata.item("LabelFormat", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebProgressBaseDescriptionMetadata.ensureMetadata(context);
		context.register("WebProgressBase", WebProgressBaseDescriptionMetadata._metadata);
	}
}


