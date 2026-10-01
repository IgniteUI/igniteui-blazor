import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRangeSliderValueEventArgsDescriptionMetadata } from "./WebRangeSliderValueEventArgsDescriptionMetadata";
import { WebSliderBaseDescriptionMetadata } from "./WebSliderBaseDescriptionMetadata";
import { WebRangeSliderDescription } from "./WebRangeSliderDescription";

/**
 * @hidden 
 */
export class WebRangeSliderDescriptionMetadata extends Base {
	static $t: Type = markType(WebRangeSliderDescriptionMetadata, 'WebRangeSliderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRangeSliderDescriptionMetadata._metadata == null) {
			WebRangeSliderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRangeSliderDescriptionMetadata.fillMetadata(WebRangeSliderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRangeSliderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRangeSliderDescriptionMetadata._metadata);
		WebRangeSliderValueEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebSliderBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:RangeSlider");
		metadata.item("__tagNameWC", "String:igc-range-slider");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Lower", "Number:double");
		metadata.item("Upper", "Number:double");
		metadata.item("ThumbLabelLower", "String");
		metadata.item("ThumbLabelUpper", "String");
		metadata.item("InputRef", "EventRef:RangeSliderValueEventHandler:input");
		metadata.item("InputRef@args", "RangeSliderValueEventArgs");
		metadata.item("ChangeRef", "EventRef:RangeSliderValueEventHandler:change");
		metadata.item("ChangeRef@args", "RangeSliderValueEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionMetadata.register(context);
		WebRangeSliderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRangeSlider", () => new WebRangeSliderDescription());
		context.register("WebRangeSlider", WebRangeSliderDescriptionMetadata._metadata);
	}
}


