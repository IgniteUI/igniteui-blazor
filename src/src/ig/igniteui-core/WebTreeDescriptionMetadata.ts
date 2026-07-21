import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTreeSelectionEventArgsDescriptionMetadata } from "./WebTreeSelectionEventArgsDescriptionMetadata";
import { WebTreeItemComponentEventArgsDescriptionMetadata } from "./WebTreeItemComponentEventArgsDescriptionMetadata";
import { WebTreeDescription } from "./WebTreeDescription";

/**
 * @hidden 
 */
export class WebTreeDescriptionMetadata extends Base {
	static $t: Type = markType(WebTreeDescriptionMetadata, 'WebTreeDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTreeDescriptionMetadata._metadata == null) {
			WebTreeDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTreeDescriptionMetadata.fillMetadata(WebTreeDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTreeDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTreeDescriptionMetadata._metadata);
		WebTreeSelectionEventArgsDescriptionMetadata.register(context);
		WebTreeItemComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Tree");
		metadata.item("__tagNameWC", "String:igc-tree");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("SingleBranchExpand", "Boolean");
		metadata.item("ToggleNodeOnClick", "Boolean");
		metadata.item("Selection", "ExportedType:string:TreeSelection");
		metadata.item("Selection@stringUnion", "WebComponents;React");
		metadata.item("Selection@names", "None;Multiple;Cascade");
		metadata.item("SelectionChangedRef", "EventRef:TreeSelectionEventHandler:selectionChanged");
		metadata.item("SelectionChangedRef@args", "TreeSelectionEventArgs");
		metadata.item("ItemExpandingRef", "EventRef:TreeItemComponentEventHandler:itemExpanding");
		metadata.item("ItemExpandingRef@args", "TreeItemComponentEventArgs");
		metadata.item("ItemExpandedRef", "EventRef:TreeItemComponentEventHandler:itemExpanded");
		metadata.item("ItemExpandedRef@args", "TreeItemComponentEventArgs");
		metadata.item("ItemCollapsingRef", "EventRef:TreeItemComponentEventHandler:itemCollapsing");
		metadata.item("ItemCollapsingRef@args", "TreeItemComponentEventArgs");
		metadata.item("ItemCollapsedRef", "EventRef:TreeItemComponentEventHandler:itemCollapsed");
		metadata.item("ItemCollapsedRef@args", "TreeItemComponentEventArgs");
		metadata.item("ActiveItemRef", "EventRef:TreeItemComponentEventHandler:activeItem");
		metadata.item("ActiveItemRef@args", "TreeItemComponentEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebTreeDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTree", () => new WebTreeDescription());
		context.register("WebTree", WebTreeDescriptionMetadata._metadata);
	}
}


