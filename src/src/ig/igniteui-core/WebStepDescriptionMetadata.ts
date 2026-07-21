import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebStepDescription } from "./WebStepDescription";

/**
 * @hidden 
 */
export class WebStepDescriptionMetadata extends Base {
	static $t: Type = markType(WebStepDescriptionMetadata, 'WebStepDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebStepDescriptionMetadata._metadata == null) {
			WebStepDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebStepDescriptionMetadata.fillMetadata(WebStepDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebStepDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebStepDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Step");
		metadata.item("__tagNameWC", "String:igc-step");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Invalid", "Boolean");
		metadata.item("Active", "Boolean");
		metadata.item("Optional", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("Complete", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebStepDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebStep", () => new WebStepDescription());
		context.register("WebStep", WebStepDescriptionMetadata._metadata);
	}
}


