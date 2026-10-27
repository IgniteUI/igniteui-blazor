import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { NumberFormatOptionsDescription } from "./NumberFormatOptionsDescription";

/**
 * @hidden
 */
export class NumberFormatOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(NumberFormatOptionsDescriptionMetadata, 'NumberFormatOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (NumberFormatOptionsDescriptionMetadata._metadata == null) {
			NumberFormatOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			NumberFormatOptionsDescriptionMetadata.fillMetadata(NumberFormatOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(NumberFormatOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(NumberFormatOptionsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__marshalByValue", "Boolean");
		metadata.item("CompactDisplay", "String");
		metadata.item("Currency", "String");
		metadata.item("CurrencyDisplay", "String");
		metadata.item("CurrencySign", "String");
		metadata.item("LocaleMatcher", "String");
		metadata.item("Notation", "String");
		metadata.item("NumberingSystem", "String");
		metadata.item("SignDisplay", "String");
		metadata.item("Style", "String");
		metadata.item("Unit", "String");
		metadata.item("UnitDisplay", "String");
		metadata.item("UseGrouping", "Boolean");
		metadata.item("MinimumIntegerDigits", "Number:int");
		metadata.item("MinimumFractionDigits", "Number:int");
		metadata.item("MaximumFractionDigits", "Number:int");
		metadata.item("MinimumSignificantDigits", "Number:int");
		metadata.item("MaximumSignificantDigits", "Number:int");
	}
	static register(context: TypeDescriptionContext): void {
		NumberFormatOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("NumberFormatOptions", () => new NumberFormatOptionsDescription());
		context.register("NumberFormatOptions", NumberFormatOptionsDescriptionMetadata._metadata);
	}
}


