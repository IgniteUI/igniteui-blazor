import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata } from "./WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata";
import { WebSplitterLayoutChangedEventArgsDescription } from "./WebSplitterLayoutChangedEventArgsDescription";

/**
 * @hidden
 */
export class WebSplitterLayoutChangedEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebSplitterLayoutChangedEventArgsDescriptionMetadata, 'WebSplitterLayoutChangedEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata == null) {
			WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSplitterLayoutChangedEventArgsDescriptionMetadata.fillMetadata(WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata);
		WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SplitterLayoutChangedEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebSplitterLayoutChangedEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebSplitterLayoutChangedEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSplitterLayoutChangedEventArgs", () => new WebSplitterLayoutChangedEventArgsDescription());
		context.register("WebSplitterLayoutChangedEventArgs", WebSplitterLayoutChangedEventArgsDescriptionMetadata._metadata);
	}
}


