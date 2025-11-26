import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class WebBaseAlertLikeDescriptionMetadata extends Base {
	static $t: Type = markType(WebBaseAlertLikeDescriptionMetadata, 'WebBaseAlertLikeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebBaseAlertLikeDescriptionMetadata._metadata == null) {
			WebBaseAlertLikeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebBaseAlertLikeDescriptionMetadata.fillMetadata(WebBaseAlertLikeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebBaseAlertLikeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebBaseAlertLikeDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:BaseAlertLike");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Open", "Boolean");
		metadata.item("DisplayTime", "Number:double");
		metadata.item("KeepOpen", "Boolean");
		metadata.item("Position", "ExportedType:string:AbsolutePosition");
		metadata.item("Position@stringUnion", "WebComponents;React");
		metadata.item("Position@names", "Bottom;Middle;Top");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseAlertLikeDescriptionMetadata.ensureMetadata(context);
		context.register("WebBaseAlertLike", WebBaseAlertLikeDescriptionMetadata._metadata);
	}
}


