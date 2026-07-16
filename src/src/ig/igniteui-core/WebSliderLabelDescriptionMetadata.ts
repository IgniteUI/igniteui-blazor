import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSliderLabelDescription } from "./WebSliderLabelDescription";

/**
 * @hidden 
 */
export class WebSliderLabelDescriptionMetadata extends Base {
	static $t: Type = markType(WebSliderLabelDescriptionMetadata, 'WebSliderLabelDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSliderLabelDescriptionMetadata._metadata == null) {
			WebSliderLabelDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSliderLabelDescriptionMetadata.fillMetadata(WebSliderLabelDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSliderLabelDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSliderLabelDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SliderLabel");
		metadata.item("__tagNameWC", "String:igc-slider-label");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebSliderLabelDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSliderLabel", () => new WebSliderLabelDescription());
		context.register("WebSliderLabel", WebSliderLabelDescriptionMetadata._metadata);
	}
}


