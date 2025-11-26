import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComboChangeEventArgsDetailDescription } from "./WebComboChangeEventArgsDetailDescription";

/**
 * @hidden 
 */
export class WebComboChangeEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboChangeEventArgsDetailDescriptionMetadata, 'WebComboChangeEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboChangeEventArgsDetailDescriptionMetadata._metadata == null) {
			WebComboChangeEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboChangeEventArgsDetailDescriptionMetadata.fillMetadata(WebComboChangeEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboChangeEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboChangeEventArgsDetailDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ComboChangeEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("NewValueRef", "(w:NewValue,p:NewValue)DataRef:object");
		metadata.item("ItemsRef", "(w:Items,p:Items)DataRef:object");
		metadata.item("ChangeType", "(wc:Type)ExportedType:string:ComboChangeType");
		metadata.item("ChangeType@stringUnion", "WebComponents;React");
		metadata.item("ChangeType@names", "Selection;Deselection;Addition");
	}
	static register(context: TypeDescriptionContext): void {
		WebComboChangeEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebComboChangeEventArgsDetail", () => new WebComboChangeEventArgsDetailDescription());
		context.register("WebComboChangeEventArgsDetail", WebComboChangeEventArgsDetailDescriptionMetadata._metadata);
	}
}


