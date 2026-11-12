import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBreadcrumbDescription } from "./WebBreadcrumbDescription";

/**
 * @hidden 
 */
export class WebBreadcrumbDescriptionMetadata extends Base {
	static $t: Type = markType(WebBreadcrumbDescriptionMetadata, 'WebBreadcrumbDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBreadcrumbDescriptionMetadata._metadata == null) {
			WebBreadcrumbDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBreadcrumbDescriptionMetadata.fillMetadata(WebBreadcrumbDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBreadcrumbDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBreadcrumbDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Breadcrumb");
		metadata.item("__tagNameWC", "String:igc-breadcrumb");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Current", "Boolean");
		metadata.item("Disabled", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebBreadcrumbDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebBreadcrumb", () => new WebBreadcrumbDescription());
		context.register("WebBreadcrumb", WebBreadcrumbDescriptionMetadata._metadata);
	}
}


