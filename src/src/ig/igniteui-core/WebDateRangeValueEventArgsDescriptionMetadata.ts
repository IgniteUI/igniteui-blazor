import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDateRangeValueDetailDescriptionMetadata } from "./WebDateRangeValueDetailDescriptionMetadata";
import { WebDateRangeValueEventArgsDescription } from "./WebDateRangeValueEventArgsDescription";

/**
 * @hidden 
 */
export class WebDateRangeValueEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebDateRangeValueEventArgsDescriptionMetadata, 'WebDateRangeValueEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDateRangeValueEventArgsDescriptionMetadata._metadata == null) {
			WebDateRangeValueEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDateRangeValueEventArgsDescriptionMetadata.fillMetadata(WebDateRangeValueEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDateRangeValueEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDateRangeValueEventArgsDescriptionMetadata._metadata);
		WebDateRangeValueDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DateRangeValueEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebDateRangeValueDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebDateRangeValueEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDateRangeValueEventArgs", () => new WebDateRangeValueEventArgsDescription());
		context.register("WebDateRangeValueEventArgs", WebDateRangeValueEventArgsDescriptionMetadata._metadata);
	}
}


