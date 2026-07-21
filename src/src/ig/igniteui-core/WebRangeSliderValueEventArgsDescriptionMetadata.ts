import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRangeSliderValueDescriptionMetadata } from "./WebRangeSliderValueDescriptionMetadata";
import { WebRangeSliderValueEventArgsDescription } from "./WebRangeSliderValueEventArgsDescription";

/**
 * @hidden 
 */
export class WebRangeSliderValueEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebRangeSliderValueEventArgsDescriptionMetadata, 'WebRangeSliderValueEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRangeSliderValueEventArgsDescriptionMetadata._metadata == null) {
			WebRangeSliderValueEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRangeSliderValueEventArgsDescriptionMetadata.fillMetadata(WebRangeSliderValueEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRangeSliderValueEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRangeSliderValueEventArgsDescriptionMetadata._metadata);
		WebRangeSliderValueDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RangeSliderValueEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Detail", "ExportedType:WebRangeSliderValue");
	}
	static register(context: TypeDescriptionContext): void {
		WebRangeSliderValueEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRangeSliderValueEventArgs", () => new WebRangeSliderValueEventArgsDescription());
		context.register("WebRangeSliderValueEventArgs", WebRangeSliderValueEventArgsDescriptionMetadata._metadata);
	}
}


