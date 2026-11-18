import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata } from "./WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata";
import { WebVirtualScrollDataRequestEventArgsDescription } from "./WebVirtualScrollDataRequestEventArgsDescription";

/**
 * @hidden
 */
export class WebVirtualScrollDataRequestEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebVirtualScrollDataRequestEventArgsDescriptionMetadata, 'WebVirtualScrollDataRequestEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata == null) {
			WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebVirtualScrollDataRequestEventArgsDescriptionMetadata.fillMetadata(WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata);
		WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:VirtualScrollDataRequestEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Detail", "ExportedType:WebVirtualScrollDataRequestEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollDataRequestEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebVirtualScrollDataRequestEventArgs", () => new WebVirtualScrollDataRequestEventArgsDescription());
		context.register("WebVirtualScrollDataRequestEventArgs", WebVirtualScrollDataRequestEventArgsDescriptionMetadata._metadata);
	}
}


