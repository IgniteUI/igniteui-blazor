import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebStepDescriptionMetadata } from "./WebStepDescriptionMetadata";
import { WebActiveStepChangingEventArgsDescriptionMetadata } from "./WebActiveStepChangingEventArgsDescriptionMetadata";
import { WebActiveStepChangedEventArgsDescriptionMetadata } from "./WebActiveStepChangedEventArgsDescriptionMetadata";
import { WebStepperDescription } from "./WebStepperDescription";

/**
 * @hidden 
 */
export class WebStepperDescriptionMetadata extends Base {
	static $t: Type = markType(WebStepperDescriptionMetadata, 'WebStepperDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebStepperDescriptionMetadata._metadata == null) {
			WebStepperDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebStepperDescriptionMetadata.fillMetadata(WebStepperDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebStepperDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebStepperDescriptionMetadata._metadata);
		WebStepDescriptionMetadata.register(context);
		WebActiveStepChangingEventArgsDescriptionMetadata.register(context);
		WebActiveStepChangedEventArgsDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Stepper");
		metadata.item("__tagNameWC", "String:igc-stepper");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Steps", "Array:WebStepDescription:Step");
		metadata.item("Orientation", "ExportedType:string:StepperOrientation");
		metadata.item("Orientation@stringUnion", "WebComponents;React");
		metadata.item("Orientation@names", "Horizontal;Vertical");
		metadata.item("StepType", "ExportedType:string:StepperStepType");
		metadata.item("StepType@stringUnion", "WebComponents;React");
		metadata.item("StepType@names", "Full;Indicator;Title");
		metadata.item("Linear", "Boolean");
		metadata.item("ContentTop", "Boolean");
		metadata.item("VerticalAnimation", "ExportedType:string:StepperVerticalAnimation");
		metadata.item("VerticalAnimation@stringUnion", "WebComponents;React");
		metadata.item("VerticalAnimation@names", "Grow;Fade;None");
		metadata.item("HorizontalAnimation", "ExportedType:string:HorizontalTransitionAnimation");
		metadata.item("HorizontalAnimation@stringUnion", "WebComponents;React");
		metadata.item("HorizontalAnimation@names", "Slide;Fade;None");
		metadata.item("AnimationDuration", "Number:double");
		metadata.item("TitlePosition", "ExportedType:string:StepperTitlePosition");
		metadata.item("TitlePosition@stringUnion", "WebComponents;React");
		metadata.item("TitlePosition@names", "Auto;Bottom;Top;End;Start");
		metadata.item("ActiveStepChangingRef", "EventRef:ActiveStepChangingEventHandler:activeStepChanging");
		metadata.item("ActiveStepChangingRef@args", "ActiveStepChangingEventArgs");
		metadata.item("ActiveStepChangedRef", "EventRef:ActiveStepChangedEventHandler:activeStepChanged");
		metadata.item("ActiveStepChangedRef@args", "ActiveStepChangedEventArgs");
	}
	static register(context: TypeDescriptionContext): void {
		WebStepperDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebStepper", () => new WebStepperDescription());
		context.register("WebStepper", WebStepperDescriptionMetadata._metadata);
	}
}


