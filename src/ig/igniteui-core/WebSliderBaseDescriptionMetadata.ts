import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebSliderBaseDescription } from "./WebSliderBaseDescription";

/**
 * @hidden 
 */
export class WebSliderBaseDescriptionMetadata extends Base {
	static $t: Type = markType(WebSliderBaseDescriptionMetadata, 'WebSliderBaseDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebSliderBaseDescriptionMetadata._metadata == null) {
			WebSliderBaseDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebSliderBaseDescriptionMetadata.fillMetadata(WebSliderBaseDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebSliderBaseDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebSliderBaseDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:SliderBase");
		metadata.item("__tagNameWC", "String:igc-slider-base");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Min", "Number:double");
		metadata.item("Max", "Number:double");
		metadata.item("LowerBound", "Number:double");
		metadata.item("UpperBound", "Number:double");
		metadata.item("Disabled", "Boolean");
		metadata.item("DiscreteTrack", "Boolean");
		metadata.item("HideTooltip", "Boolean");
		metadata.item("Step", "Number:double");
		metadata.item("PrimaryTicks", "Number:double");
		metadata.item("SecondaryTicks", "Number:double");
		metadata.item("TickOrientation", "ExportedType:string:SliderTickOrientation");
		metadata.item("TickOrientation@stringUnion", "WebComponents;React");
		metadata.item("TickOrientation@names", "End;Mirror;Start");
		metadata.item("HidePrimaryLabels", "Boolean");
		metadata.item("HideSecondaryLabels", "Boolean");
		metadata.item("Locale", "String");
		metadata.item("ValueFormat", "String");
		metadata.item("TickLabelRotation", "ExportedType:string:SliderTickLabelRotation");
		metadata.item("TickLabelRotation@names", "Zero;Ninety;NegativeNinety");
		metadata.item("ValueFormatOptions", "ExportedType");
	}
	static register(context: TypeDescriptionContext): void {
		WebSliderBaseDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebSliderBase", () => new WebSliderBaseDescription());
		context.register("WebSliderBase", WebSliderBaseDescriptionMetadata._metadata);
	}
}


