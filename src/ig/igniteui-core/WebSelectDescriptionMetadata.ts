import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSelectItemComponentEventArgsDescriptionMetadata } from "./WebSelectItemComponentEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebBaseComboBoxLikeDescriptionMetadata } from "./WebBaseComboBoxLikeDescriptionMetadata";
import { WebSelectDescription } from "./WebSelectDescription";

/**
 * @hidden 
 */
export class WebSelectDescriptionMetadata extends Base {
	static $t: Type = markType(WebSelectDescriptionMetadata, 'WebSelectDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSelectDescriptionMetadata._metadata == null) {
			WebSelectDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSelectDescriptionMetadata.fillMetadata(WebSelectDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSelectDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSelectDescriptionMetadata._metadata);
		WebSelectItemComponentEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Select");
		metadata.item("__tagNameWC", "String:igc-select");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("Outlined", "Boolean");
		metadata.item("Autofocus", "Boolean");
		metadata.item("Distance", "Number:double");
		metadata.item("Label", "String");
		metadata.item("Placeholder", "String");
		metadata.item("Placement", "ExportedType:string:PopoverPlacement");
		metadata.item("Placement@stringUnion", "WebComponents;React");
		metadata.item("Placement@names", "Top;TopStart;TopEnd;Bottom;BottomStart;BottomEnd;Right;RightStart;RightEnd;Left;LeftStart;LeftEnd");
		metadata.item("ScrollStrategy", "ExportedType:string:PopoverScrollStrategy");
		metadata.item("ScrollStrategy@stringUnion", "WebComponents;React");
		metadata.item("ScrollStrategy@names", "Scroll;Block;Close");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("ChangeRef", "EventRef:SelectItemComponentEventHandler:change");
		metadata.item("ChangeRef@args", "SelectItemComponentEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
		metadata.item("OpeningRef", "EventRef:VoidHandler:opening");
		metadata.item("OpeningRef@args", "VoidEventArgs");
		metadata.item("OpenedRef", "EventRef:VoidHandler:opened");
		metadata.item("OpenedRef@args", "VoidEventArgs");
		metadata.item("ClosingRef", "EventRef:VoidHandler:closing");
		metadata.item("ClosingRef@args", "VoidEventArgs");
		metadata.item("ClosedRef", "EventRef:VoidHandler:closed");
		metadata.item("ClosedRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxLikeDescriptionMetadata.register(context);
		WebSelectDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSelect", () => new WebSelectDescription());
		context.register("WebSelect", WebSelectDescriptionMetadata._metadata);
	}
}


