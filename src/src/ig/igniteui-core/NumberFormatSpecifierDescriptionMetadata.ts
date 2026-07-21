import { Base, String_$type, Type, markType } from "./type";
import { Dictionary$2 } from "./Dictionary$2";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { FormatSpecifierDescriptionMetadata } from "./FormatSpecifierDescriptionMetadata";
import { NumberFormatSpecifierDescription } from "./NumberFormatSpecifierDescription";

/**
 * @hidden 
 */
export class NumberFormatSpecifierDescriptionMetadata extends Base {
	static $t: Type = markType(NumberFormatSpecifierDescriptionMetadata, 'NumberFormatSpecifierDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (NumberFormatSpecifierDescriptionMetadata._metadata == null) {
			NumberFormatSpecifierDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			NumberFormatSpecifierDescriptionMetadata.fillMetadata(NumberFormatSpecifierDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(NumberFormatSpecifierDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(NumberFormatSpecifierDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		FormatSpecifierDescriptionMetadata.fillMetadata(metadata);
		metadata.item("Locale", "String");
		metadata.item("CompactDisplay", "String");
		metadata.item("Currency", "String");
		metadata.item("CurrencyDisplay", "String");
		metadata.item("CurrencySign", "String");
		metadata.item("CurrencyCode", "String");
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
		NumberFormatSpecifierDescriptionMetadata.registerOtherMetadata(metadata);
	}
	static register(context: TypeDescriptionContext): void {
		FormatSpecifierDescriptionMetadata.register(context);
		NumberFormatSpecifierDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("NumberFormatSpecifier", () => new NumberFormatSpecifierDescription());
		context.register("NumberFormatSpecifier", NumberFormatSpecifierDescriptionMetadata._metadata);
	}
	private static registerOtherMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents-core");
		metadata.item("__importTypesReact", "String:igniteui-react-core");
		metadata.item("__importTypesAngular", "String:igniteui-angular-core");
		metadata.item("__skipSuffix", "Boolean");
	}
}


