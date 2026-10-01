import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNumberEventArgsDescriptionMetadata } from "./WebNumberEventArgsDescriptionMetadata";
import { WebSliderBaseDescriptionMetadata } from "./WebSliderBaseDescriptionMetadata";
import { WebSliderDescription } from "./WebSliderDescription";

/**
 * @hidden 
 */
export class WebSliderDescriptionMetadata extends Base {
	static $t: Type = markType(WebSliderDescriptionMetadata, 'WebSliderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSliderDescriptionMetadata._metadata == null) {
			WebSliderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSliderDescriptionMetadata.fillMetadata(WebSliderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSliderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSliderDescriptionMetadata._metadata);
		WebNumberEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebSliderBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Slider");
		metadata.item("__tagNameWC", "String:igc-slider");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "Number:double");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("InputRef", "EventRef:NumberEventHandler:input");
		metadata.item("InputRef@args", "NumberEventArgs");
		metadata.item("ChangeRef", "EventRef:NumberEventHandler:change");
		metadata.item("ChangeRef@args", "NumberEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionMetadata.register(context);
		WebSliderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSlider", () => new WebSliderDescription());
		context.register("WebSlider", WebSliderDescriptionMetadata._metadata);
	}
}


