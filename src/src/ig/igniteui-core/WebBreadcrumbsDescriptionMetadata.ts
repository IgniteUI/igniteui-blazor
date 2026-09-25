import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebBreadcrumbsDescription } from "./WebBreadcrumbsDescription";

/**
 * @hidden 
 */
export class WebBreadcrumbsDescriptionMetadata extends Base {
	static $t: Type = markType(WebBreadcrumbsDescriptionMetadata, 'WebBreadcrumbsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBreadcrumbsDescriptionMetadata._metadata == null) {
			WebBreadcrumbsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBreadcrumbsDescriptionMetadata.fillMetadata(WebBreadcrumbsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBreadcrumbsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBreadcrumbsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Breadcrumbs");
		metadata.item("__tagNameWC", "String:igc-breadcrumbs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Separator", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebBreadcrumbsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebBreadcrumbs", () => new WebBreadcrumbsDescription());
		context.register("WebBreadcrumbs", WebBreadcrumbsDescriptionMetadata._metadata);
	}
}


