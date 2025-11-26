import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCarouselIndicatorDescription } from "./WebCarouselIndicatorDescription";

/**
 * @hidden 
 */
export class WebCarouselIndicatorDescriptionMetadata extends Base {
	static $t: Type = markType(WebCarouselIndicatorDescriptionMetadata, 'WebCarouselIndicatorDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCarouselIndicatorDescriptionMetadata._metadata == null) {
			WebCarouselIndicatorDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCarouselIndicatorDescriptionMetadata.fillMetadata(WebCarouselIndicatorDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCarouselIndicatorDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCarouselIndicatorDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CarouselIndicator");
		metadata.item("__tagNameWC", "String:igc-carousel-indicator");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebCarouselIndicatorDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCarouselIndicator", () => new WebCarouselIndicatorDescription());
		context.register("WebCarouselIndicator", WebCarouselIndicatorDescriptionMetadata._metadata);
	}
}


