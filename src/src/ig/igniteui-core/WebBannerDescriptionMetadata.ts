import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebBannerDescription } from "./WebBannerDescription";

/**
 * @hidden 
 */
export class WebBannerDescriptionMetadata extends Base {
	static $t: Type = markType(WebBannerDescriptionMetadata, 'WebBannerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBannerDescriptionMetadata._metadata == null) {
			WebBannerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBannerDescriptionMetadata.fillMetadata(WebBannerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBannerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBannerDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Banner");
		metadata.item("__tagNameWC", "String:igc-banner");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Open", "Boolean");
		metadata.item("ClosingRef", "EventRef:VoidHandler:closing");
		metadata.item("ClosingRef@args", "VoidEventArgs");
		metadata.item("ClosedRef", "EventRef:VoidHandler:closed");
		metadata.item("ClosedRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBannerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebBanner", () => new WebBannerDescription());
		context.register("WebBanner", WebBannerDescriptionMetadata._metadata);
	}
}


