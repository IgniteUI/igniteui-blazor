import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNumberEventArgsDescriptionMetadata } from "./WebNumberEventArgsDescriptionMetadata";
import { WebRatingDescription } from "./WebRatingDescription";

/**
 * @hidden 
 */
export class WebRatingDescriptionMetadata extends Base {
	static $t: Type = markType(WebRatingDescriptionMetadata, 'WebRatingDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRatingDescriptionMetadata._metadata == null) {
			WebRatingDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRatingDescriptionMetadata.fillMetadata(WebRatingDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRatingDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRatingDescriptionMetadata._metadata);
		WebNumberEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Rating");
		metadata.item("__tagNameWC", "String:igc-rating");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Max", "Number:double");
		metadata.item("Step", "Number:double");
		metadata.item("Label", "String");
		metadata.item("ValueFormat", "String");
		metadata.item("Value", "Number:double");
		metadata.item("HoverPreview", "Boolean");
		metadata.item("ReadOnly", "Boolean");
		metadata.item("Single", "Boolean");
		metadata.item("AllowReset", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("ChangeRef", "EventRef:NumberEventHandler:change");
		metadata.item("ChangeRef@args", "NumberEventArgs");
		metadata.item("HoverRef", "EventRef:NumberEventHandler:hover");
		metadata.item("HoverRef@args", "NumberEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebRatingDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRating", () => new WebRatingDescription());
		context.register("WebRating", WebRatingDescriptionMetadata._metadata);
	}
}


