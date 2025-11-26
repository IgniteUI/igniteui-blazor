import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebToggleButtonDescription } from "./WebToggleButtonDescription";

/**
 * @hidden 
 */
export class WebToggleButtonDescriptionMetadata extends Base {
	static $t: Type = markType(WebToggleButtonDescriptionMetadata, 'WebToggleButtonDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebToggleButtonDescriptionMetadata._metadata == null) {
			WebToggleButtonDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebToggleButtonDescriptionMetadata.fillMetadata(WebToggleButtonDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebToggleButtonDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebToggleButtonDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ToggleButton");
		metadata.item("__tagNameWC", "String:igc-toggle-button");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("Selected", "Boolean");
		metadata.item("Disabled", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebToggleButtonDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebToggleButton", () => new WebToggleButtonDescription());
		context.register("WebToggleButton", WebToggleButtonDescriptionMetadata._metadata);
	}
}


