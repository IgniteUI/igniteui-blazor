import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebFilteringOptionsDescriptionMetadata } from "./WebFilteringOptionsDescriptionMetadata";
import { WebComboChangeEventArgsDescriptionMetadata } from "./WebComboChangeEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebComboDescription } from "./WebComboDescription";

/**
 * @hidden 
 */
export class WebComboDescriptionMetadata extends Base {
	static $t: Type = markType(WebComboDescriptionMetadata, 'WebComboDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebComboDescriptionMetadata._metadata == null) {
			WebComboDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebComboDescriptionMetadata.fillMetadata(WebComboDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebComboDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebComboDescriptionMetadata._metadata);
		WebFilteringOptionsDescriptionMetadata.register(context);
		WebComboChangeEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Combo");
		metadata.item("__tagNameWC", "String:igc-combo");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("DataRef", "(w:Data,p:Data)DataRef::object");
		metadata.item("Outlined", "Boolean");
		metadata.item("SingleSelect", "Boolean");
		metadata.item("Autofocus", "Boolean");
		metadata.item("AutofocusList", "Boolean");
		metadata.item("Label", "String");
		metadata.item("Placeholder", "String");
		metadata.item("PlaceholderSearch", "String");
		metadata.item("Open", "Boolean");
		metadata.item("ValueKey", "String");
		metadata.item("DisplayKey", "String");
		metadata.item("GroupKey", "String");
		metadata.item("GroupSorting", "ExportedType:string:GroupingDirection");
		metadata.item("GroupSorting@stringUnion", "WebComponents;React");
		metadata.item("GroupSorting@names", "Asc;Desc;None");
		metadata.item("FilteringOptions", "ExportedType:WebFilteringOptions");
		metadata.item("CaseSensitiveIcon", "Boolean");
		metadata.item("DisableFiltering", "Boolean");
		metadata.item("Value", "Array:object");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("ItemTemplateRef", "(w:ItemTemplate,p:ItemTemplate)TemplateRef::object");
		metadata.item("GroupHeaderTemplateRef", "(w:GroupHeaderTemplate,p:GroupHeaderTemplate)TemplateRef::object");
		metadata.item("ChangeRef", "EventRef:ComboChangeEventHandler:change");
		metadata.item("ChangeRef@args", "ComboChangeEventArgs");
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
		WebComboDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCombo", () => new WebComboDescription());
		context.register("WebCombo", WebComboDescriptionMetadata._metadata);
	}
}


