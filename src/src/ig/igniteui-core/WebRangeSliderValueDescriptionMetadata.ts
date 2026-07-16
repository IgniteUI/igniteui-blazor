import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRangeSliderValueDescription } from "./WebRangeSliderValueDescription";

/**
 * @hidden 
 */
export class WebRangeSliderValueDescriptionMetadata extends Base {
	static $t: Type = markType(WebRangeSliderValueDescriptionMetadata, 'WebRangeSliderValueDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRangeSliderValueDescriptionMetadata._metadata == null) {
			WebRangeSliderValueDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRangeSliderValueDescriptionMetadata.fillMetadata(WebRangeSliderValueDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRangeSliderValueDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRangeSliderValueDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RangeSliderValue");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Lower", "Number:double");
		metadata.item("Upper", "Number:double");
	}
	static register(context: TypeDescriptionContext): void {
		WebRangeSliderValueDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRangeSliderValue", () => new WebRangeSliderValueDescription());
		context.register("WebRangeSliderValue", WebRangeSliderValueDescriptionMetadata._metadata);
	}
}


