import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTreeSelectionEventArgsDetailDescriptionMetadata } from "./WebTreeSelectionEventArgsDetailDescriptionMetadata";
import { WebTreeSelectionEventArgsDescription } from "./WebTreeSelectionEventArgsDescription";

/**
 * @hidden 
 */
export class WebTreeSelectionEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTreeSelectionEventArgsDescriptionMetadata, 'WebTreeSelectionEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTreeSelectionEventArgsDescriptionMetadata._metadata == null) {
			WebTreeSelectionEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTreeSelectionEventArgsDescriptionMetadata.fillMetadata(WebTreeSelectionEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTreeSelectionEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTreeSelectionEventArgsDescriptionMetadata._metadata);
		WebTreeSelectionEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TreeSelectionEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebTreeSelectionEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebTreeSelectionEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTreeSelectionEventArgs", () => new WebTreeSelectionEventArgsDescription());
		context.register("WebTreeSelectionEventArgs", WebTreeSelectionEventArgsDescriptionMetadata._metadata);
	}
}


