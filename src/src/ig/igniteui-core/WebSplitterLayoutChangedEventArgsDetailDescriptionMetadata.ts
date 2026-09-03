import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSplitterLayoutChangedEventArgsDetailDescription } from "./WebSplitterLayoutChangedEventArgsDetailDescription";

/**
 * @hidden
 */
export class WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata, 'WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata == null) {
			WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata.fillMetadata(WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SplitterLayoutChangedEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("StartSize", "String");
		metadata.item("EndSize", "String");
		metadata.item("StartCollapsed", "Boolean");
		metadata.item("EndCollapsed", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSplitterLayoutChangedEventArgsDetail", () => new WebSplitterLayoutChangedEventArgsDetailDescription());
		context.register("WebSplitterLayoutChangedEventArgsDetail", WebSplitterLayoutChangedEventArgsDetailDescriptionMetadata._metadata);
	}
}


