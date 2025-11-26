import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebRatingSymbolDescription } from "./WebRatingSymbolDescription";

/**
 * @hidden 
 */
export class WebRatingSymbolDescriptionMetadata extends Base {
	static $t: Type = markType(WebRatingSymbolDescriptionMetadata, 'WebRatingSymbolDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebRatingSymbolDescriptionMetadata._metadata == null) {
			WebRatingSymbolDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebRatingSymbolDescriptionMetadata.fillMetadata(WebRatingSymbolDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebRatingSymbolDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebRatingSymbolDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:RatingSymbol");
		metadata.item("__tagNameWC", "String:igc-rating-symbol");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		WebRatingSymbolDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebRatingSymbol", () => new WebRatingSymbolDescription());
		context.register("WebRatingSymbol", WebRatingSymbolDescriptionMetadata._metadata);
	}
}


