import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTabComponentEventArgsDescriptionMetadata } from "./WebTabComponentEventArgsDescriptionMetadata";
import { WebTabsDescription } from "./WebTabsDescription";

/**
 * @hidden 
 */
export class WebTabsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTabsDescriptionMetadata, 'WebTabsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTabsDescriptionMetadata._metadata == null) {
			WebTabsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTabsDescriptionMetadata.fillMetadata(WebTabsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTabsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTabsDescriptionMetadata._metadata);
		WebTabComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Tabs");
		metadata.item("__tagNameWC", "String:igc-tabs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__manageCollectionInMarkup", "Boolean");
		metadata.item("__manageItemInMarkup", "Boolean");
		metadata.item("TabsCollection", "Collection:WebTab:Tabs_TabCollection:WebTab");
		metadata.item("Alignment", "ExportedType:string:TabsAlignment");
		metadata.item("Alignment@stringUnion", "WebComponents;React");
		metadata.item("Alignment@names", "Start;End;Center;Justify");
		metadata.item("Activation", "ExportedType:string:TabsActivation");
		metadata.item("Activation@stringUnion", "WebComponents;React");
		metadata.item("Activation@names", "Auto;Manual");
		metadata.item("ChangeRef", "EventRef:TabComponentEventHandler:change");
		metadata.item("ChangeRef@args", "TabComponentEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebTabsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTabs", () => new WebTabsDescription());
		context.register("WebTabs", WebTabsDescriptionMetadata._metadata);
	}
}


