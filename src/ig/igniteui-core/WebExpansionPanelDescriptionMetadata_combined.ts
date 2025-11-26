import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebExpansionPanelDescription } from "./WebExpansionPanelDescription";
import { WebExpansionPanelComponentEventArgsDescription } from "./WebExpansionPanelComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebExpansionPanelDescriptionMetadata extends Base {
	static $t: Type = markType(WebExpansionPanelDescriptionMetadata, 'WebExpansionPanelDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebExpansionPanelDescriptionMetadata._metadata == null) {
			WebExpansionPanelDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebExpansionPanelDescriptionMetadata.fillMetadata(WebExpansionPanelDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebExpansionPanelDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebExpansionPanelDescriptionMetadata._metadata);
		WebExpansionPanelComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ExpansionPanel");
		metadata.item("__tagNameWC", "String:igc-expansion-panel");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Open", "Boolean");
		metadata.item("Disabled", "Boolean");
		metadata.item("IndicatorPosition", "ExportedType:string:ExpansionPanelIndicatorPosition");
		metadata.item("IndicatorPosition@stringUnion", "WebComponents;React");
		metadata.item("IndicatorPosition@names", "Start;End;None");
		metadata.item("OpeningRef", "EventRef:ExpansionPanelComponentEventHandler:opening");
		metadata.item("OpeningRef@args", "ExpansionPanelComponentEventArgs");
		metadata.item("OpenedRef", "EventRef:ExpansionPanelComponentEventHandler:opened");
		metadata.item("OpenedRef@args", "ExpansionPanelComponentEventArgs");
		metadata.item("ClosingRef", "EventRef:ExpansionPanelComponentEventHandler:closing");
		metadata.item("ClosingRef@args", "ExpansionPanelComponentEventArgs");
		metadata.item("ClosedRef", "EventRef:ExpansionPanelComponentEventHandler:closed");
		metadata.item("ClosedRef@args", "ExpansionPanelComponentEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebExpansionPanelDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebExpansionPanel", () => new WebExpansionPanelDescription());
		context.register("WebExpansionPanel", WebExpansionPanelDescriptionMetadata._metadata);
	}
}

/**
 * @hidden 
 */
export class WebExpansionPanelComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebExpansionPanelComponentEventArgsDescriptionMetadata, 'WebExpansionPanelComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata == null) {
			WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebExpansionPanelComponentEventArgsDescriptionMetadata.fillMetadata(WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata);
		WebExpansionPanelDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ExpansionPanelComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebExpansionPanel");
	}
	static register(context: TypeDescriptionContext): void {
		WebExpansionPanelComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebExpansionPanelComponentEventArgs", () => new WebExpansionPanelComponentEventArgsDescription());
		context.register("WebExpansionPanelComponentEventArgs", WebExpansionPanelComponentEventArgsDescriptionMetadata._metadata);
	}
}


