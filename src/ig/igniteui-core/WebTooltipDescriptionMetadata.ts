import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebTooltipDescription } from "./WebTooltipDescription";

/**
 * @hidden 
 */
export class WebTooltipDescriptionMetadata extends Base {
	static $t: Type = markType(WebTooltipDescriptionMetadata, 'WebTooltipDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebTooltipDescriptionMetadata._metadata == null) {
			WebTooltipDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebTooltipDescriptionMetadata.fillMetadata(WebTooltipDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebTooltipDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebTooltipDescriptionMetadata._metadata);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Tooltip");
		metadata.item("__tagNameWC", "String:igc-tooltip");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Open", "Boolean");
		metadata.item("DisableArrow", "Boolean");
		metadata.item("WithArrow", "Boolean");
		metadata.item("Offset", "Number:double");
		metadata.item("Placement", "ExportedType:string:PopoverPlacement");
		metadata.item("Placement@stringUnion", "WebComponents;React");
		metadata.item("Placement@names", "Top;TopStart;TopEnd;Bottom;BottomStart;BottomEnd;Right;RightStart;RightEnd;Left;LeftStart;LeftEnd");
		metadata.item("Anchor", "String");
		metadata.item("ShowTriggers", "String");
		metadata.item("HideTriggers", "String");
		metadata.item("ShowDelay", "Number:double");
		metadata.item("HideDelay", "Number:double");
		metadata.item("Message", "String");
		metadata.item("Sticky", "Boolean");
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
		WebTooltipDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebTooltip", () => new WebTooltipDescription());
		context.register("WebTooltip", WebTooltipDescriptionMetadata._metadata);
	}
}


