import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { FormatSpecifierDescription } from "./FormatSpecifierDescription";

/**
 * @hidden 
 */
export class FormatSpecifierDescriptionMetadata extends Base {
	static $t: Type = markType(FormatSpecifierDescriptionMetadata, 'FormatSpecifierDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (FormatSpecifierDescriptionMetadata._metadata == null) {
			FormatSpecifierDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			FormatSpecifierDescriptionMetadata.fillMetadata(FormatSpecifierDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(FormatSpecifierDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(FormatSpecifierDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__marshalByValue", "Boolean");
	}
	static register(context: TypeDescriptionContext): void {
		FormatSpecifierDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("FormatSpecifier", () => new FormatSpecifierDescription());
		context.register("FormatSpecifier", FormatSpecifierDescriptionMetadata._metadata);
	}
}


