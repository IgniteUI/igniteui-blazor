import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCheckboxBaseDescriptionMetadata } from "./WebCheckboxBaseDescriptionMetadata";
import { WebSwitchDescription } from "./WebSwitchDescription";

/**
 * @hidden 
 */
export class WebSwitchDescriptionMetadata extends Base {
	static $t: Type = markType(WebSwitchDescriptionMetadata, 'WebSwitchDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSwitchDescriptionMetadata._metadata == null) {
			WebSwitchDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSwitchDescriptionMetadata.fillMetadata(WebSwitchDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSwitchDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSwitchDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebCheckboxBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Switch");
		metadata.item("__tagNameWC", "String:igc-switch");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebCheckboxBaseDescriptionMetadata.register(context);
		WebSwitchDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSwitch", () => new WebSwitchDescription());
		context.register("WebSwitch", WebSwitchDescriptionMetadata._metadata);
	}
}


