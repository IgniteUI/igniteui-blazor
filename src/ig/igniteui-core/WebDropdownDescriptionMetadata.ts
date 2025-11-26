import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebDropdownItemComponentEventArgsDescriptionMetadata } from "./WebDropdownItemComponentEventArgsDescriptionMetadata";
import { WebBaseComboBoxLikeDescriptionMetadata } from "./WebBaseComboBoxLikeDescriptionMetadata";
import { WebDropdownDescription } from "./WebDropdownDescription";

/**
 * @hidden 
 */
export class WebDropdownDescriptionMetadata extends Base {
	static $t: Type = markType(WebDropdownDescriptionMetadata, 'WebDropdownDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDropdownDescriptionMetadata._metadata == null) {
			WebDropdownDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDropdownDescriptionMetadata.fillMetadata(WebDropdownDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDropdownDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDropdownDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
		WebDropdownItemComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Dropdown");
		metadata.item("__tagNameWC", "String:igc-dropdown");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Placement", "ExportedType:string:PopoverPlacement");
		metadata.item("Placement@stringUnion", "WebComponents;React");
		metadata.item("Placement@names", "Top;TopStart;TopEnd;Bottom;BottomStart;BottomEnd;Right;RightStart;RightEnd;Left;LeftStart;LeftEnd");
		metadata.item("ScrollStrategy", "ExportedType:string:PopoverScrollStrategy");
		metadata.item("ScrollStrategy@stringUnion", "WebComponents;React");
		metadata.item("ScrollStrategy@names", "Scroll;Block;Close");
		metadata.item("Flip", "Boolean");
		metadata.item("Distance", "Number:double");
		metadata.item("SameWidth", "Boolean");
		metadata.item("OpeningRef", "EventRef:VoidHandler:opening");
		metadata.item("OpeningRef@args", "VoidEventArgs");
		metadata.item("OpenedRef", "EventRef:VoidHandler:opened");
		metadata.item("OpenedRef@args", "VoidEventArgs");
		metadata.item("ClosingRef", "EventRef:VoidHandler:closing");
		metadata.item("ClosingRef@args", "VoidEventArgs");
		metadata.item("ClosedRef", "EventRef:VoidHandler:closed");
		metadata.item("ClosedRef@args", "VoidEventArgs");
		metadata.item("ChangeRef", "EventRef:DropdownItemComponentEventHandler:change");
		metadata.item("ChangeRef@args", "DropdownItemComponentEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionMetadata.register(context);
		WebDropdownDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDropdown", () => new WebDropdownDescription());
		context.register("WebDropdown", WebDropdownDescriptionMetadata._metadata);
	}
}


