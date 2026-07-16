import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebBaseAlertLikeDescriptionMetadata } from "./WebBaseAlertLikeDescriptionMetadata";
import { WebSnackbarDescription } from "./WebSnackbarDescription";

/**
 * @hidden 
 */
export class WebSnackbarDescriptionMetadata extends Base {
	static $t: Type = markType(WebSnackbarDescriptionMetadata, 'WebSnackbarDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSnackbarDescriptionMetadata._metadata == null) {
			WebSnackbarDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSnackbarDescriptionMetadata.fillMetadata(WebSnackbarDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSnackbarDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSnackbarDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseAlertLikeDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Snackbar");
		metadata.item("__tagNameWC", "String:igc-snackbar");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("ActionText", "String");
		metadata.item("ActionRef", "EventRef:VoidHandler:action");
		metadata.item("ActionRef@args", "VoidEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseAlertLikeDescriptionMetadata.register(context);
		WebSnackbarDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSnackbar", () => new WebSnackbarDescription());
		context.register("WebSnackbar", WebSnackbarDescriptionMetadata._metadata);
	}
}


