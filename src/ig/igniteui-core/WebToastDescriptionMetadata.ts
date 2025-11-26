import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBaseAlertLikeDescriptionMetadata } from "./WebBaseAlertLikeDescriptionMetadata";
import { WebToastDescription } from "./WebToastDescription";

/**
 * @hidden 
 */
export class WebToastDescriptionMetadata extends Base {
	static $t: Type = markType(WebToastDescriptionMetadata, 'WebToastDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebToastDescriptionMetadata._metadata == null) {
			WebToastDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebToastDescriptionMetadata.fillMetadata(WebToastDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebToastDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebToastDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseAlertLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Toast");
		metadata.item("__tagNameWC", "String:igc-toast");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseAlertLikeDescriptionMetadata.register(context);
		WebToastDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebToast", () => new WebToastDescription());
		context.register("WebToast", WebToastDescriptionMetadata._metadata);
	}
}


