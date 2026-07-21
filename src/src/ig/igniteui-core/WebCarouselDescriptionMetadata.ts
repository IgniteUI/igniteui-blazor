import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNumberEventArgsDescriptionMetadata } from "./WebNumberEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebCarouselDescription } from "./WebCarouselDescription";

/**
 * @hidden 
 */
export class WebCarouselDescriptionMetadata extends Base {
	static $t: Type = markType(WebCarouselDescriptionMetadata, 'WebCarouselDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCarouselDescriptionMetadata._metadata == null) {
			WebCarouselDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCarouselDescriptionMetadata.fillMetadata(WebCarouselDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCarouselDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCarouselDescriptionMetadata._metadata);
		WebNumberEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Carousel");
		metadata.item("__tagNameWC", "String:igc-carousel");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("DisableLoop", "Boolean");
		metadata.item("DisablePauseOnInteraction", "Boolean");
		metadata.item("HideNavigation", "Boolean");
		metadata.item("HideIndicators", "Boolean");
		metadata.item("Vertical", "Boolean");
		metadata.item("IndicatorsOrientation", "ExportedType:string:CarouselIndicatorsOrientation");
		metadata.item("IndicatorsOrientation@stringUnion", "WebComponents;React");
		metadata.item("IndicatorsOrientation@names", "End;Start");
		metadata.item("IndicatorsLabelFormat", "String");
		metadata.item("SlidesLabelFormat", "String");
		metadata.item("Interval", "Number:double");
		metadata.item("MaximumIndicatorsCount", "Number:double");
		metadata.item("AnimationType", "ExportedType:string:HorizontalTransitionAnimation");
		metadata.item("AnimationType@stringUnion", "WebComponents;React");
		metadata.item("AnimationType@names", "Slide;Fade;None");
		metadata.item("SlideChangedRef", "EventRef:NumberEventHandler:slideChanged");
		metadata.item("SlideChangedRef@args", "NumberEventArgs");
		metadata.item("PlayingRef", "EventRef:VoidHandler:playing");
		metadata.item("PlayingRef@args", "VoidEventArgs");
		metadata.item("PausedRef", "EventRef:VoidHandler:paused");
		metadata.item("PausedRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebCarouselDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCarousel", () => new WebCarouselDescription());
		context.register("WebCarousel", WebCarouselDescriptionMetadata._metadata);
	}
}


