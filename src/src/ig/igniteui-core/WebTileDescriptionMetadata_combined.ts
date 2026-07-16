import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebTileDescription } from "./WebTileDescription";
import { WebTileChangeStateEventArgsDescription } from "./WebTileChangeStateEventArgsDescription";
import { WebTileChangeStateEventArgsDetailDescription } from "./WebTileChangeStateEventArgsDetailDescription";
import { WebTileComponentEventArgsDescription } from "./WebTileComponentEventArgsDescription";

/**
 * @hidden 
 */
export class WebTileDescriptionMetadata extends Base {
	static $t: Type = markType(WebTileDescriptionMetadata, 'WebTileDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTileDescriptionMetadata._metadata == null) {
			WebTileDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTileDescriptionMetadata.fillMetadata(WebTileDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTileDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTileDescriptionMetadata._metadata);
		WebTileChangeStateEventArgsDescriptionMetadata.register(context);
		WebTileComponentEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Tile");
		metadata.item("__tagNameWC", "String:igc-tile");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("ColSpan", "Number:double");
		metadata.item("RowSpan", "Number:double");
		metadata.item("ColStart", "Number:double");
		metadata.item("RowStart", "Number:double");
		metadata.item("Maximized", "Boolean");
		metadata.item("DisableResize", "Boolean");
		metadata.item("DisableFullscreen", "Boolean");
		metadata.item("DisableMaximize", "Boolean");
		metadata.item("Position", "Number:double");
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
		WebTileDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTile", () => new WebTileDescription());
		context.register("WebTile", WebTileDescriptionMetadata._metadata);
	}
}

/**
 * @hidden 
 */
export class WebTileChangeStateEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTileChangeStateEventArgsDescriptionMetadata, 'WebTileChangeStateEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTileChangeStateEventArgsDescriptionMetadata._metadata == null) {
			WebTileChangeStateEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTileChangeStateEventArgsDescriptionMetadata.fillMetadata(WebTileChangeStateEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTileChangeStateEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTileChangeStateEventArgsDescriptionMetadata._metadata);
		WebTileChangeStateEventArgsDetailDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TileChangeStateEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebTileChangeStateEventArgsDetail");
	}
	static register(context: TypeDescriptionContext): void {
		WebTileChangeStateEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTileChangeStateEventArgs", () => new WebTileChangeStateEventArgsDescription());
		context.register("WebTileChangeStateEventArgs", WebTileChangeStateEventArgsDescriptionMetadata._metadata);
	}
}

/**
 * @hidden 
 */
export class WebTileChangeStateEventArgsDetailDescriptionMetadata extends Base {
	static $t: Type = markType(WebTileChangeStateEventArgsDetailDescriptionMetadata, 'WebTileChangeStateEventArgsDetailDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata == null) {
			WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTileChangeStateEventArgsDetailDescriptionMetadata.fillMetadata(WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata);
		WebTileDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TileChangeStateEventArgsDetail");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("__skipSuffix", "Boolean");
		metadata.item("Tile", "ExportedType:WebTile");
		metadata.item("State", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		WebTileChangeStateEventArgsDetailDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTileChangeStateEventArgsDetail", () => new WebTileChangeStateEventArgsDetailDescription());
		context.register("WebTileChangeStateEventArgsDetail", WebTileChangeStateEventArgsDetailDescriptionMetadata._metadata);
	}
}

/**
 * @hidden 
 */
export class WebTileComponentEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(WebTileComponentEventArgsDescriptionMetadata, 'WebTileComponentEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTileComponentEventArgsDescriptionMetadata._metadata == null) {
			WebTileComponentEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTileComponentEventArgsDescriptionMetadata.fillMetadata(WebTileComponentEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTileComponentEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTileComponentEventArgsDescriptionMetadata._metadata);
		WebTileDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:TileComponentEventArgs");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("Detail", "ExportedType:WebTile");
	}
	static register(context: TypeDescriptionContext): void {
		WebTileComponentEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTileComponentEventArgs", () => new WebTileComponentEventArgsDescription());
		context.register("WebTileComponentEventArgs", WebTileComponentEventArgsDescriptionMetadata._metadata);
	}
}


