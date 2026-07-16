import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebExpansionPanelComponentEventArgsDescriptionMetadata } from "./WebExpansionPanelComponentEventArgsDescriptionMetadata";
import { WebAccordionDescription } from "./WebAccordionDescription";

/**
 * @hidden 
 */
export class WebAccordionDescriptionMetadata extends Base {
	static $t: Type = markType(WebAccordionDescriptionMetadata, 'WebAccordionDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebAccordionDescriptionMetadata._metadata == null) {
			WebAccordionDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebAccordionDescriptionMetadata.fillMetadata(WebAccordionDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebAccordionDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebAccordionDescriptionMetadata._metadata);
		WebExpansionPanelComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Accordion");
		metadata.item("__tagNameWC", "String:igc-accordion");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("SingleExpand", "Boolean");
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
		WebAccordionDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebAccordion", () => new WebAccordionDescription());
		context.register("WebAccordion", WebAccordionDescriptionMetadata._metadata);
	}
}


