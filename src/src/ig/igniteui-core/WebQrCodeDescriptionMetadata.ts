import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebQrCodeDescription } from "./WebQrCodeDescription";

/**
 * @hidden
 */
export class WebQrCodeDescriptionMetadata extends Base {
	static $t: Type = markType(WebQrCodeDescriptionMetadata, 'WebQrCodeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebQrCodeDescriptionMetadata._metadata == null) {
			WebQrCodeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebQrCodeDescriptionMetadata.fillMetadata(WebQrCodeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebQrCodeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebQrCodeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:QrCode");
		metadata.item("__tagNameWC", "String:igc-qr-code");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("Version", "Number:double");
		metadata.item("ErrorLevel", "ExportedType:string:QrErrorCorrectionLevel");
		metadata.item("ErrorLevel@stringUnion", "WebComponents;React");
		metadata.item("ErrorLevel@names", "Low;Medium;Quartile;High");
		metadata.item("Size", "Number:double");
		metadata.item("Margin", "Number:double");
		metadata.item("LogoSrc", "String");
		metadata.item("LogoSize", "Number:double");
		metadata.item("LogoMargin", "Number:double");
		metadata.item("DotStyle", "ExportedType:string:QrDotStyle");
		metadata.item("DotStyle@stringUnion", "WebComponents;React");
		metadata.item("DotStyle@names", "Square;Circle;Rounded");
		metadata.item("SquareStyle", "ExportedType:string:QrCornerSquareStyle");
		metadata.item("SquareStyle@stringUnion", "WebComponents;React");
		metadata.item("SquareStyle@names", "Square;Circle;Rounded");
	}
	static register(context: TypeDescriptionContext): void {
		WebQrCodeDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebQrCode", () => new WebQrCodeDescription());
		context.register("WebQrCode", WebQrCodeDescriptionMetadata._metadata);
	}
}


