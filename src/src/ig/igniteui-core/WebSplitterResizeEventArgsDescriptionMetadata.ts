import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSplitterResizeEventArgsDetailDescriptionMetadata } from "./WebSplitterResizeEventArgsDetailDescriptionMetadata";
import { WebSplitterResizeEventArgsDescription } from "./WebSplitterResizeEventArgsDescription";

/**
 * @hidden 
 */
export class WebSplitterResizeEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebSplitterResizeEventArgsDescriptionMetadata, 'WebSplitterResizeEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSplitterResizeEventArgsDescriptionMetadata._metadata == null) {
			WebSplitterResizeEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSplitterResizeEventArgsDescriptionMetadata.fillMetadata(WebSplitterResizeEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSplitterResizeEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSplitterResizeEventArgsDescriptionMetadata._metadata);
		WebSplitterResizeEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SplitterResizeEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebSplitterResizeEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebSplitterResizeEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSplitterResizeEventArgs", () => new WebSplitterResizeEventArgsDescription());
		context.register("WebSplitterResizeEventArgs", WebSplitterResizeEventArgsDescriptionMetadata._metadata);
	}
}


