import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebFilteringOptionsDescriptionMetadata } from "./WebFilteringOptionsDescriptionMetadata";
import { WebComboChangeEventArgsDescriptionMetadata } from "./WebComboChangeEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebComboClosingEventArgsDescriptionMetadata } from "./WebComboClosingEventArgsDescriptionMetadata";
import { WebComboSearchInputEventArgsDescriptionMetadata } from "./WebComboSearchInputEventArgsDescriptionMetadata";
import { WebVirtualScrollDataRequestEventArgsDescriptionMetadata } from "./WebVirtualScrollDataRequestEventArgsDescriptionMetadata";
import { WebBaseComboBoxDescriptionMetadata } from "./WebBaseComboBoxDescriptionMetadata";
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
		WebComboClosingEventArgsDescriptionMetadata.register(context);
		WebComboSearchInputEventArgsDescriptionMetadata.register(context);
		WebVirtualScrollDataRequestEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:Combo");
		metadata.item("__tagNameWC", "String:igc-combo");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("DataRef", "(w:Data,p:Data)DataRef::object");
		metadata.item("Outlined", "Boolean");
		metadata.item("SingleSelect", "Boolean");
		metadata.item("Autofocus", "Boolean");
		metadata.item("AutofocusList", "Boolean");
		metadata.item("Locale", "String");
		metadata.item("Label", "String");
		metadata.item("FormName", "(wc:Name)String");
		metadata.item("Placeholder", "String");
		metadata.item("PlaceholderSearch", "String");
		metadata.item("ValueKey", "String");
		metadata.item("DisplayKey", "String");
		metadata.item("GroupKey", "String");
		metadata.item("GroupSorting", "ExportedType:string:GroupingDirection");
		metadata.item("GroupSorting@stringUnion", "WebComponents;React");
		metadata.item("GroupSorting@names", "Asc;Desc;None");
		metadata.item("FilteringOptions", "ExportedType:WebFilteringOptions");
		metadata.item("CaseSensitiveIcon", "Boolean");
		metadata.item("DisableFiltering", "Boolean");
		metadata.item("DisableClear", "Boolean");
		metadata.item("TotalItemCount", "Number:double");
		metadata.item("Loading", "Boolean");
		metadata.item("ShowSelectAll", "Boolean");
		metadata.item("GroupSelection", "Boolean");
		metadata.item("SelectionCommit", "ExportedType:string:ComboSelectionCommit");
		metadata.item("SelectionCommit@stringUnion", "WebComponents;React");
		metadata.item("SelectionCommit@names", "Immediate;Confirm");
		metadata.item("Value", "Array:object");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Invalid", "Boolean");
		metadata.item("ItemTemplateRef", "(w:ItemTemplate,p:ItemTemplate)TemplateRef::object");
		metadata.item("GroupHeaderTemplateRef", "(w:GroupHeaderTemplate,p:GroupHeaderTemplate)TemplateRef::object");
		metadata.item("ChangeRef", "EventRef:ComboChangeEventHandler:change");
		metadata.item("ChangeRef@args", "ComboChangeEventArgs");
		metadata.item("PendingChangeRef", "EventRef:ComboChangeEventHandler:pendingChange");
		metadata.item("PendingChangeRef@args", "ComboChangeEventArgs");
		metadata.item("FocusRef", "EventRef:VoidHandler:focus:skipWCPrefix");
		metadata.item("FocusRef@args", "VoidEventArgs");
		metadata.item("BlurRef", "EventRef:VoidHandler:blur:skipWCPrefix");
		metadata.item("BlurRef@args", "VoidEventArgs");
		metadata.item("OpeningRef", "EventRef:VoidHandler:opening");
		metadata.item("OpeningRef@args", "VoidEventArgs");
		metadata.item("OpenedRef", "EventRef:VoidHandler:opened");
		metadata.item("OpenedRef@args", "VoidEventArgs");
		metadata.item("ClosingRef", "EventRef:ComboClosingEventHandler:closing");
		metadata.item("ClosingRef@args", "ComboClosingEventArgs");
		metadata.item("ClosedRef", "EventRef:ComboClosingEventHandler:closed");
		metadata.item("ClosedRef@args", "ComboClosingEventArgs");
		metadata.item("SearchInputRef", "EventRef:ComboSearchInputEventHandler:searchInput");
		metadata.item("SearchInputRef@args", "ComboSearchInputEventArgs");
		metadata.item("DataRequestRef", "EventRef:VirtualScrollDataRequestEventHandler:dataRequest");
		metadata.item("DataRequestRef@args", "VirtualScrollDataRequestEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebBaseComboBoxDescriptionMetadata.register(context);
		WebComboDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebCombo", () => new WebComboDescription());
		context.register("WebCombo", WebComboDescriptionMetadata._metadata);
	}
}


