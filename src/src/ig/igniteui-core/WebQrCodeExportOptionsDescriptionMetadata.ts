import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebQrCodeExportOptionsDescription } from "./WebQrCodeExportOptionsDescription";

/**
 * @hidden
 */
export class WebQrCodeExportOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebQrCodeExportOptionsDescriptionMetadata, 'WebQrCodeExportOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebQrCodeExportOptionsDescriptionMetadata._metadata == null) {
			WebQrCodeExportOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebQrCodeExportOptionsDescriptionMetadata.fillMetadata(WebQrCodeExportOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebQrCodeExportOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebQrCodeExportOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:QrCodeExportOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("__isTSPlainInterface", "Boolean");
		metadata.item("FileName", "String");
		metadata.item("Format", "ExportedType:string:QrCodeExportFormat");
		metadata.item("Format@stringUnion", "WebComponents;React");
		metadata.item("Format@names", "Svg;Png;Jpeg;Webp");
		metadata.item("Scale", "Number:double");
		metadata.item("Download", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebQrCodeExportOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebQrCodeExportOptions", () => new WebQrCodeExportOptionsDescription());
		context.register("WebQrCodeExportOptions", WebQrCodeExportOptionsDescriptionMetadata._metadata);
	}
}


