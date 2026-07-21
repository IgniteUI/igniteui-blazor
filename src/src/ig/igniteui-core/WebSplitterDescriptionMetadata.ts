import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSplitterResizeEventArgsDescriptionMetadata } from "./WebSplitterResizeEventArgsDescriptionMetadata";
import { WebSplitterDescription } from "./WebSplitterDescription";

/**
 * @hidden 
 */
export class WebSplitterDescriptionMetadata extends Base {
	static $t: Type = markType(WebSplitterDescriptionMetadata, 'WebSplitterDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSplitterDescriptionMetadata._metadata == null) {
			WebSplitterDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSplitterDescriptionMetadata.fillMetadata(WebSplitterDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSplitterDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSplitterDescriptionMetadata._metadata);
		WebSplitterResizeEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Splitter");
		metadata.item("__tagNameWC", "String:igc-splitter");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Orientation", "ExportedType:string:SplitterOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("DisableCollapse", "Boolean");
		metadata.item("DisableResize", "Boolean");
		metadata.item("HideCollapseButtons", "Boolean");
		metadata.item("HideDragHandle", "Boolean");
		metadata.item("StartMinSize", "String");
		metadata.item("EndMinSize", "String");
		metadata.item("StartMaxSize", "String");
		metadata.item("EndMaxSize", "String");
		metadata.item("StartSize", "String");
		metadata.item("EndSize", "String");
		metadata.item("ResizeStartRef", "EventRef:SplitterResizeEventHandler:resizeStart");
		metadata.item("ResizeStartRef@args", "SplitterResizeEventArgs");
		metadata.item("ResizingRef", "EventRef:SplitterResizeEventHandler:resizing");
		metadata.item("ResizingRef@args", "SplitterResizeEventArgs");
		metadata.item("ResizeEndRef", "EventRef:SplitterResizeEventHandler:resizeEnd");
		metadata.item("ResizeEndRef@args", "SplitterResizeEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebSplitterDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSplitter", () => new WebSplitterDescription());
		context.register("WebSplitter", WebSplitterDescriptionMetadata._metadata);
	}
}


