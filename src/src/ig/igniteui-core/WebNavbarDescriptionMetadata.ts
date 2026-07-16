import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNavbarDescription } from "./WebNavbarDescription";

/**
 * @hidden 
 */
export class WebNavbarDescriptionMetadata extends Base {
	static $t: Type = markType(WebNavbarDescriptionMetadata, 'WebNavbarDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebNavbarDescriptionMetadata._metadata == null) {
			WebNavbarDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebNavbarDescriptionMetadata.fillMetadata(WebNavbarDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebNavbarDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebNavbarDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Navbar");
		metadata.item("__tagNameWC", "String:igc-navbar");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebNavbarDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebNavbar", () => new WebNavbarDescription());
		context.register("WebNavbar", WebNavbarDescriptionMetadata._metadata);
	}
}


