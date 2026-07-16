import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTileChangeStateEventArgsDescriptionMetadata } from "./WebTileChangeStateEventArgsDescriptionMetadata";
import { WebTileComponentEventArgsDescriptionMetadata } from "./WebTileComponentEventArgsDescriptionMetadata";
import { WebTileManagerDescription } from "./WebTileManagerDescription";

/**
 * @hidden 
 */
export class WebTileManagerDescriptionMetadata extends Base {
	static $t: Type = markType(WebTileManagerDescriptionMetadata, 'WebTileManagerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTileManagerDescriptionMetadata._metadata == null) {
			WebTileManagerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTileManagerDescriptionMetadata.fillMetadata(WebTileManagerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTileManagerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTileManagerDescriptionMetadata._metadata);
		WebTileChangeStateEventArgsDescriptionMetadata.register(context);
		WebTileComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TileManager");
		metadata.item("__tagNameWC", "String:igc-tile-manager");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("ResizeMode", "ExportedType:string:TileManagerResizeMode");
		metadata.item("ResizeMode@stringUnion", "WebComponents;React");
		metadata.item("ResizeMode@names", "None;Hover;Always");
		metadata.item("DragMode", "ExportedType:string:TileManagerDragMode");
		metadata.item("DragMode@stringUnion", "WebComponents;React");
		metadata.item("DragMode@names", "None;TileHeader;Tile");
		metadata.item("ColumnCount", "Number:double");
		metadata.item("MinColumnWidth", "String");
		metadata.item("MinRowHeight", "String");
		metadata.item("Gap", "String");
		metadata.item("TileFullscreenRef", "EventRef:TileChangeStateEventHandler:tileFullscreen");
		metadata.item("TileFullscreenRef@args", "TileChangeStateEventArgs");
		metadata.item("TileMaximizeRef", "EventRef:TileChangeStateEventHandler:tileMaximize");
		metadata.item("TileMaximizeRef@args", "TileChangeStateEventArgs");
		metadata.item("TileDragStartRef", "EventRef:TileComponentEventHandler:tileDragStart");
		metadata.item("TileDragStartRef@args", "TileComponentEventArgs");
		metadata.item("TileDragEndRef", "EventRef:TileComponentEventHandler:tileDragEnd");
		metadata.item("TileDragEndRef@args", "TileComponentEventArgs");
		metadata.item("TileDragCancelRef", "EventRef:TileComponentEventHandler:tileDragCancel");
		metadata.item("TileDragCancelRef@args", "TileComponentEventArgs");
		metadata.item("TileResizeStartRef", "EventRef:TileComponentEventHandler:tileResizeStart");
		metadata.item("TileResizeStartRef@args", "TileComponentEventArgs");
		metadata.item("TileResizeEndRef", "EventRef:TileComponentEventHandler:tileResizeEnd");
		metadata.item("TileResizeEndRef@args", "TileComponentEventArgs");
		metadata.item("TileResizeCancelRef", "EventRef:TileComponentEventHandler:tileResizeCancel");
		metadata.item("TileResizeCancelRef@args", "TileComponentEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebTileManagerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTileManager", () => new WebTileManagerDescription());
		context.register("WebTileManager", WebTileManagerDescriptionMetadata._metadata);
	}
}


