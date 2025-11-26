import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { WebMaskInputBaseDescriptionMetadata } from "./WebMaskInputBaseDescriptionMetadata";
import { WebMaskInputDescription } from "./WebMaskInputDescription";

/**
 * @hidden 
 */
export class WebMaskInputDescriptionMetadata extends Base {
	static $t: Type = markType(WebMaskInputDescriptionMetadata, 'WebMaskInputDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebMaskInputDescriptionMetadata._metadata == null) {
			WebMaskInputDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebMaskInputDescriptionMetadata.fillMetadata(WebMaskInputDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebMaskInputDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebMaskInputDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebMaskInputBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:MaskInput");
		metadata.item("__tagNameWC", "String:igc-mask-input");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("ValueMode", "ExportedType:string:MaskInputValueMode");
		metadata.item("ValueMode@stringUnion", "WebComponents;React");
		metadata.item("ValueMode@names", "Raw;WithFormatting");
		metadata.item("Value", "String");
		metadata.item("Mask", "String");
		metadata.item("ChangeRef", "EventRef:ComponentValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebMaskInputBaseDescriptionMetadata.register(context);
		WebMaskInputDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebMaskInput", () => new WebMaskInputDescription());
		context.register("WebMaskInput", WebMaskInputDescriptionMetadata._metadata);
	}
}


