import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebNavDrawerDescription } from "./WebNavDrawerDescription";

/**
 * @hidden 
 */
export class WebNavDrawerDescriptionMetadata extends Base {
	static $t: Type = markType(WebNavDrawerDescriptionMetadata, 'WebNavDrawerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebNavDrawerDescriptionMetadata._metadata == null) {
			WebNavDrawerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebNavDrawerDescriptionMetadata.fillMetadata(WebNavDrawerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebNavDrawerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebNavDrawerDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:NavDrawer");
		metadata.item("__tagNameWC", "String:igc-nav-drawer");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Position", "ExportedType:string:NavDrawerPosition");
		metadata.item("Position@stringUnion", "WebComponents;React");
		metadata.item("Position@names", "Start;End;Top;Bottom;Relative");
		metadata.item("Open", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebNavDrawerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebNavDrawer", () => new WebNavDrawerDescription());
		context.register("WebNavDrawer", WebNavDrawerDescriptionMetadata._metadata);
	}
}


