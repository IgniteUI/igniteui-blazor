import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebInputBaseDescriptionMetadata } from "./WebInputBaseDescriptionMetadata";

/**
 * @hidden 
 */
export class WebMaskInputBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebMaskInputBaseDescriptionMetadata, 'WebMaskInputBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebMaskInputBaseDescriptionMetadata._metadata == null) {
			WebMaskInputBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebMaskInputBaseDescriptionMetadata.fillMetadata(WebMaskInputBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebMaskInputBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebMaskInputBaseDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebInputBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:MaskInputBase");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Prompt", "String");
	}
	static register(context: TypeDescriptionContext): void {
		WebInputBaseDescriptionMetadata.register(context);
		WebMaskInputBaseDescriptionMetadata.ensureMetadata(context);
		context.register("WebMaskInputBase", WebMaskInputBaseDescriptionMetadata._metadata);
	}
}


