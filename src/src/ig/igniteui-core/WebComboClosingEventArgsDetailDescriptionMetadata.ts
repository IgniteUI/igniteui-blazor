import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboClosingEventArgsDetailDescription } from "./WebComboClosingEventArgsDetailDescription";

/**
 * @hidden
 */
export class WebComboClosingEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboClosingEventArgsDetailDescriptionMetadata, 'WebComboClosingEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboClosingEventArgsDetailDescriptionMetadata._metadata == null) {
			WebComboClosingEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboClosingEventArgsDetailDescriptionMetadata.fillMetadata(WebComboClosingEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboClosingEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboClosingEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboClosingEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Trigger", "ExportedType:string:ComboCloseTrigger");
		metadata.item("Trigger@stringUnion", "WebComponents;React");
		metadata.item("Trigger@names", "Enter;Escape;Tab;ArrowUp;OutsideClick;Scroll;Toggle;Selection;Apply;Cancel");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboClosingEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboClosingEventArgsDetail", () => new WebComboClosingEventArgsDetailDescription());
		context.register("WebComboClosingEventArgsDetail", WebComboClosingEventArgsDetailDescriptionMetadata._metadata);
	}
}


