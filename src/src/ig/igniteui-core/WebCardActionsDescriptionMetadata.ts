import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebCardActionsDescription } from "./WebCardActionsDescription";

/**
 * @hidden 
 */
export class WebCardActionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebCardActionsDescriptionMetadata, 'WebCardActionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebCardActionsDescriptionMetadata._metadata == null) {
			WebCardActionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebCardActionsDescriptionMetadata.fillMetadata(WebCardActionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebCardActionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebCardActionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:CardActions");
		metadata.item("__tagNameWC", "String:igc-card-actions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Orientation", "ExportedType:string:ContentOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
	}
	static register(context: TypeDescriptionContext): void {
		WebCardActionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCardActions", () => new WebCardActionsDescription());
		context.register("WebCardActions", WebCardActionsDescriptionMetadata._metadata);
	}
}


