import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebVirtualScrollDataRequestEventArgsDetailDescription } from "./WebVirtualScrollDataRequestEventArgsDetailDescription";

/**
 * @hidden
 */
export class WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata, 'WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata == null) {
			WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata.fillMetadata(WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:VirtualScrollDataRequestEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("StartIndex", "Number:double");
		metadata.item("Count", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebVirtualScrollDataRequestEventArgsDetail", () => new WebVirtualScrollDataRequestEventArgsDetailDescription());
		context.register("WebVirtualScrollDataRequestEventArgsDetail", WebVirtualScrollDataRequestEventArgsDetailDescriptionMetadata._metadata);
	}
}


