import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebThemeProviderDescription } from "./WebThemeProviderDescription";

/**
 * @hidden 
 */
export class WebThemeProviderDescriptionMetadata extends Base {
	static $t: Type = markType(WebThemeProviderDescriptionMetadata, 'WebThemeProviderDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebThemeProviderDescriptionMetadata._metadata == null) {
			WebThemeProviderDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebThemeProviderDescriptionMetadata.fillMetadata(WebThemeProviderDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebThemeProviderDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebThemeProviderDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ThemeProvider");
		metadata.item("__tagNameWC", "String:igc-theme-provider");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Theme", "ExportedType:string:Theme");
		metadata.item("Theme@stringUnion", "WebComponents;React");
		metadata.item("Theme@names", "Material;Bootstrap;Indigo;Fluent");
		metadata.item("Variant", "ExportedType:string:ThemeVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Light;Dark");
	}
	static register(context: TypeDescriptionContext): void {
		WebThemeProviderDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebThemeProvider", () => new WebThemeProviderDescription());
		context.register("WebThemeProvider", WebThemeProviderDescriptionMetadata._metadata);
	}
}


