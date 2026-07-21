import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebDialogDescription } from "./WebDialogDescription";

/**
 * @hidden 
 */
export class WebDialogDescriptionMetadata extends Base {
	static $t: Type = markType(WebDialogDescriptionMetadata, 'WebDialogDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebDialogDescriptionMetadata._metadata == null) {
			WebDialogDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebDialogDescriptionMetadata.fillMetadata(WebDialogDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebDialogDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebDialogDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Dialog");
		metadata.item("__tagNameWC", "String:igc-dialog");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("KeepOpenOnEscape", "Boolean");
		metadata.item("CloseOnOutsideClick", "Boolean");
		metadata.item("HideDefaultAction", "Boolean");
		metadata.item("Open", "Boolean");
		metadata.item("Title", "String");
		metadata.item("ReturnValue", "String");
		metadata.item("ClosingRef", "EventRef:VoidHandler:closing");
		metadata.item("ClosingRef@args", "VoidEventArgs");
		metadata.item("ClosedRef", "EventRef:VoidHandler:closed");
		metadata.item("ClosedRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebDialogDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebDialog", () => new WebDialogDescription());
		context.register("WebDialog", WebDialogDescriptionMetadata._metadata);
	}
}


