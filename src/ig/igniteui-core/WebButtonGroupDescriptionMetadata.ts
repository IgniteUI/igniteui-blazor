import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { WebButtonGroupDescription } from "./WebButtonGroupDescription";

/**
 * @hidden 
 */
export class WebButtonGroupDescriptionMetadata extends Base {
	static $t: Type = markType(WebButtonGroupDescriptionMetadata, 'WebButtonGroupDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebButtonGroupDescriptionMetadata._metadata == null) {
			WebButtonGroupDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebButtonGroupDescriptionMetadata.fillMetadata(WebButtonGroupDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebButtonGroupDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebButtonGroupDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ButtonGroup");
		metadata.item("__tagNameWC", "String:igc-button-group");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Disabled", "Boolean");
		metadata.item("Alignment", "ExportedType:string:ContentOrientation");
		metadata.item("Alignment@stringUnion", "WebComponents;React");
		metadata.item("Alignment@names", "Horizontal;Vertical");
		metadata.item("Selection", "ExportedType:string:ButtonGroupSelection");
		metadata.item("Selection@stringUnion", "WebComponents;React");
		metadata.item("Selection@names", "Single;SingleRequired;Multiple");
		metadata.item("SelectedItems", "Array:string");
		metadata.item("SelectRef", "EventRef:ComponentValueChangedEventHandler:select");
		metadata.item("SelectRef@args", "ComponentValueChangedEventArgs");
		metadata.item("DeselectRef", "EventRef:ComponentValueChangedEventHandler:deselect");
		metadata.item("DeselectRef@args", "ComponentValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebButtonGroupDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebButtonGroup", () => new WebButtonGroupDescription());
		context.register("WebButtonGroup", WebButtonGroupDescriptionMetadata._metadata);
	}
}


