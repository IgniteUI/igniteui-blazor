import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebDropdownItemDescriptionMetadata } from "./WebDropdownItemDescriptionMetadata";
import { WebDropdownItemComponentEventArgsDescription } from "./WebDropdownItemComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebDropdownItemComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebDropdownItemComponentEventArgsDescriptionMetadata, 'WebDropdownItemComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDropdownItemComponentEventArgsDescriptionMetadata._metadata == null) {
			WebDropdownItemComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDropdownItemComponentEventArgsDescriptionMetadata.fillMetadata(WebDropdownItemComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDropdownItemComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDropdownItemComponentEventArgsDescriptionMetadata._metadata);
		WebDropdownItemDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:DropdownItemComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebDropdownItem");
	}
	static register(context: TypeDescriptionContext): void {
		WebDropdownItemComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDropdownItemComponentEventArgs", () => new WebDropdownItemComponentEventArgsDescription());
		context.register("WebDropdownItemComponentEventArgs", WebDropdownItemComponentEventArgsDescriptionMetadata._metadata);
	}
}


