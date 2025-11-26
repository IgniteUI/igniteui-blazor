import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebProgressBaseDescriptionMetadata } from "./WebProgressBaseDescriptionMetadata";
import { WebLinearProgressDescription } from "./WebLinearProgressDescription";

/**
 * @hidden 
 */
export class WebLinearProgressDescriptionMetadata extends Base {
	static $t: Type = markType(WebLinearProgressDescriptionMetadata, 'WebLinearProgressDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebLinearProgressDescriptionMetadata._metadata == null) {
			WebLinearProgressDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebLinearProgressDescriptionMetadata.fillMetadata(WebLinearProgressDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebLinearProgressDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebLinearProgressDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		WebProgressBaseDescriptionMetadata.fillMetadata(metadata);
		metadata.item("__qualifiedNameTS", "String:LinearProgress");
		metadata.item("__tagNameWC", "String:igc-linear-progress");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Striped", "Boolean");
		metadata.item("LabelAlign", "ExportedType:string:LinearProgressLabelAlign");
		metadata.item("LabelAlign@stringUnion", "WebComponents;React");
		metadata.item("LabelAlign@names", "TopStart;Top;TopEnd;BottomStart;Bottom;BottomEnd");
	}
	static register(context: TypeDescriptionContext): void {
		WebProgressBaseDescriptionMetadata.register(context);
		WebLinearProgressDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebLinearProgress", () => new WebLinearProgressDescription());
		context.register("WebLinearProgress", WebLinearProgressDescriptionMetadata._metadata);
	}
}


