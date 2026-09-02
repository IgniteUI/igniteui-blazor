import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebVirtualScrollStateChangeEventArgsDescriptionMetadata } from "./WebVirtualScrollStateChangeEventArgsDescriptionMetadata";
import { WebVirtualScrollDataRequestEventArgsDescriptionMetadata } from "./WebVirtualScrollDataRequestEventArgsDescriptionMetadata";
import { WebVirtualScrollDescription } from "./WebVirtualScrollDescription";

/**
 * @hidden
 */
export class WebVirtualScrollDescriptionMetadata extends Base {
	static $t: Type = markType(WebVirtualScrollDescriptionMetadata, 'WebVirtualScrollDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebVirtualScrollDescriptionMetadata._metadata == null) {
			WebVirtualScrollDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebVirtualScrollDescriptionMetadata.fillMetadata(WebVirtualScrollDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebVirtualScrollDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebVirtualScrollDescriptionMetadata._metadata);
		WebVirtualScrollStateChangeEventArgsDescriptionMetadata.register(context);
		WebVirtualScrollDataRequestEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:VirtualScroll");
		metadata.item("__tagNameWC", "String:igc-virtual-scroll");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("DataRef", "(w:Data,p:Data)DataRef::object");
		metadata.item("Orientation", "ExportedType:string:ContentOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("OverScan", "Number:double");
		metadata.item("EstimatedItemSize", "Number:double");
		metadata.item("ItemTemplateRef", "(w:ItemTemplate,p:ItemTemplate)TemplateRef::object");
		metadata.item("StateChangeRef", "EventRef:VirtualScrollStateChangeEventHandler:stateChange");
		metadata.item("StateChangeRef@args", "VirtualScrollStateChangeEventArgs");
		metadata.item("DataRequestRef", "EventRef:VirtualScrollDataRequestEventHandler:dataRequest");
		metadata.item("DataRequestRef@args", "VirtualScrollDataRequestEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebVirtualScrollDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebVirtualScroll", () => new WebVirtualScrollDescription());
		context.register("WebVirtualScroll", WebVirtualScrollDescriptionMetadata._metadata);
	}
}


