import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentBoolValueChangedEventArgsDescriptionMetadata } from "./WebComponentBoolValueChangedEventArgsDescriptionMetadata";
import { WebChipDescription } from "./WebChipDescription";

/**
 * @hidden 
 */
export class WebChipDescriptionMetadata extends Base {
	static $t: Type = markType(WebChipDescriptionMetadata, 'WebChipDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChipDescriptionMetadata._metadata == null) {
			WebChipDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChipDescriptionMetadata.fillMetadata(WebChipDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChipDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChipDescriptionMetadata._metadata);
		WebComponentBoolValueChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Chip");
		metadata.item("__tagNameWC", "String:igc-chip");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Disabled", "Boolean");
		metadata.item("Removable", "Boolean");
		metadata.item("Selectable", "Boolean");
		metadata.item("Selected", "Boolean");
		metadata.item("Variant", "ExportedType:string:StyleVariant");
		metadata.item("Variant@stringUnion", "WebComponents;React");
		metadata.item("Variant@names", "Primary;Info;Success;Warning;Danger");
		metadata.item("RemoveRef", "EventRef:ComponentBoolValueChangedEventHandler:remove");
		metadata.item("RemoveRef@args", "ComponentBoolValueChangedEventArgs");
		metadata.item("SelectRef", "EventRef:ComponentBoolValueChangedEventHandler:select");
		metadata.item("SelectRef@args", "ComponentBoolValueChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebChipDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChip", () => new WebChipDescription());
		context.register("WebChip", WebChipDescriptionMetadata._metadata);
	}
}


