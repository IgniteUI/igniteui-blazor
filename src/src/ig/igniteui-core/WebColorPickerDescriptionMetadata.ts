import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebComponentValueChangedEventArgsDescriptionMetadata } from "./WebComponentValueChangedEventArgsDescriptionMetadata";
import { VoidEventArgsDescriptionMetadata } from "./VoidEventArgsDescriptionMetadata";
import { WebBaseComboBoxDescriptionMetadata } from "./WebBaseComboBoxDescriptionMetadata";
import { WebColorPickerDescription } from "./WebColorPickerDescription";

/**
 * @hidden
 */
export class WebColorPickerDescriptionMetadata extends Base {
	static $t: Type = markType(WebColorPickerDescriptionMetadata, 'WebColorPickerDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebColorPickerDescriptionMetadata._metadata == null) {
			WebColorPickerDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebColorPickerDescriptionMetadata.fillMetadata(WebColorPickerDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebColorPickerDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebColorPickerDescriptionMetadata._metadata);
		WebComponentValueChangedEventArgsDescriptionMetadata.register(context);
		VoidEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebBaseComboBoxDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:ColorPicker");
		metadata.item("__tagNameWC", "String:igc-color-picker");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Value", "String");
		metadata.item("Label", "String");
		metadata.item("Format", "ExportedType:string:ColorFormat");
		metadata.item("Format@stringUnion", "WebComponents;React");
		metadata.item("Format@names", "Hex;Rgb;Hsl");
		metadata.item("HideFormats", "Boolean");
		metadata.item("ShowAlpha", "Boolean");
		metadata.item("Mode", "ExportedType:string:ColorPickerMode");
		metadata.item("Mode@stringUnion", "WebComponents;React");
		metadata.item("Mode@names", "Default;Input");
		metadata.item("Swatches", "Array:string");
		metadata.item("Disabled", "Boolean");
		metadata.item("Required", "Boolean");
		metadata.item("DefaultValue", "Unknown");
		metadata.item("Name", "String");
		metadata.item("Invalid", "Boolean");
		metadata.item("ChangeRef", "EventRef:ComponentValueChangedEventHandler:change");
		metadata.item("ChangeRef@args", "ComponentValueChangedEventArgs");
		metadata.item("InputRef", "EventRef:ComponentValueChangedEventHandler:input");
		metadata.item("InputRef@args", "ComponentValueChangedEventArgs");
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
		WebBaseComboBoxDescriptionMetadata.register(context);
		WebColorPickerDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebColorPicker", () => new WebColorPickerDescription());
		context.register("WebColorPicker", WebColorPickerDescriptionMetadata._metadata);
	}
}


