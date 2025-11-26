import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCarouselSlideDescription } from "./WebCarouselSlideDescription";

/**
 * @hidden 
 */
export class WebCarouselSlideDescriptionMetadata extends Base {
	static $t: Type = markType(WebCarouselSlideDescriptionMetadata, 'WebCarouselSlideDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCarouselSlideDescriptionMetadata._metadata == null) {
			WebCarouselSlideDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCarouselSlideDescriptionMetadata.fillMetadata(WebCarouselSlideDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCarouselSlideDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCarouselSlideDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CarouselSlide");
		metadata.item("__tagNameWC", "String:igc-carousel-slide");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Active", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebCarouselSlideDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCarouselSlide", () => new WebCarouselSlideDescription());
		context.register("WebCarouselSlide", WebCarouselSlideDescriptionMetadata._metadata);
	}
}


