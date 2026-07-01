import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSplitterResizeEventArgsDetailDescription } from "./WebSplitterResizeEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebSplitterResizeEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebSplitterResizeEventArgsDetailDescriptionMetadata, 'WebSplitterResizeEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata == null) {
			WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSplitterResizeEventArgsDetailDescriptionMetadata.fillMetadata(WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SplitterResizeEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("StartPanelSize", "Number:double");
		metadata.item("EndPanelSize", "Number:double");
		metadata.item("Delta", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebSplitterResizeEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSplitterResizeEventArgsDetail", () => new WebSplitterResizeEventArgsDetailDescription());
		context.register("WebSplitterResizeEventArgsDetail", WebSplitterResizeEventArgsDetailDescriptionMetadata._metadata);
	}
}


