import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata } from "./WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata";
import { WebVirtualScrollStateChangeEventArgsDescription } from "./WebVirtualScrollStateChangeEventArgsDescription";

/**
 * @hidden
 */
export class WebVirtualScrollStateChangeEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebVirtualScrollStateChangeEventArgsDescriptionMetadata, 'WebVirtualScrollStateChangeEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata == null) {
			WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebVirtualScrollStateChangeEventArgsDescriptionMetadata.fillMetadata(WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata);
		WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:VirtualScrollStateChangeEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebVirtualScrollStateChangeEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollStateChangeEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebVirtualScrollStateChangeEventArgs", () => new WebVirtualScrollStateChangeEventArgsDescription());
		context.register("WebVirtualScrollStateChangeEventArgs", WebVirtualScrollStateChangeEventArgsDescriptionMetadata._metadata);
	}
}


