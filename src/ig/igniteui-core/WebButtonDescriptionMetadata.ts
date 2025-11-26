import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebButtonBaseDescriptionMetadata } from "./WebButtonBaseDescriptionMetadata";
import { WebButtonDescription } from "./WebButtonDescription";

/**
 * @hidden 
 */
export class WebButtonDescriptionMetadata extends Base {
	static $t: Type = markType(WebButtonDescriptionMetadata, 'WebButtonDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebButtonDescriptionMetadata._metadata == null) {
			WebButtonDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebButtonDescriptionMetadata.fillMetadata(WebButtonDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebButtonDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebButtonDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebButtonBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Button");
		metadata.item("__tagNameWC", "String:igc-button");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Variant", "ExportedType:string:ButtonVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Contained;Flat;Outlined;Fab");
	}
	static register(context: TypeDescriptionContext): void {
		WebButtonBaseDescriptionMetadata.register(context);
		WebButtonDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebButton", () => new WebButtonDescription());
		context.register("WebButton", WebButtonDescriptionMetadata._metadata);
	}
}


