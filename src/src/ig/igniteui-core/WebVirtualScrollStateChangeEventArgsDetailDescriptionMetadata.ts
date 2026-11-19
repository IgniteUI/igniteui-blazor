import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebVirtualScrollStateChangeEventArgsDetailDescription } from "./WebVirtualScrollStateChangeEventArgsDetailDescription";

/**
 * @hidden
 */
export class WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata, 'WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata == null) {
			WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata.fillMetadata(WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:VirtualScrollStateChangeEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("StartIndex", "Number:double");
		metadata.item("EndIndex", "Number:double");
		metadata.item("ViewportSize", "Number:double");
		metadata.item("TotalSize", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebVirtualScrollStateChangeEventArgsDetail", () => new WebVirtualScrollStateChangeEventArgsDetailDescription());
		context.register("WebVirtualScrollStateChangeEventArgsDetail", WebVirtualScrollStateChangeEventArgsDetailDescriptionMetadata._metadata);
	}
}


