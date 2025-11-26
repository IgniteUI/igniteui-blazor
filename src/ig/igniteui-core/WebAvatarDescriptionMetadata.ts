import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebAvatarDescription } from "./WebAvatarDescription";

/**
 * @hidden 
 */
export class WebAvatarDescriptionMetadata extends Base {
	static $t: Type = markType(WebAvatarDescriptionMetadata, 'WebAvatarDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebAvatarDescriptionMetadata._metadata == null) {
			WebAvatarDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebAvatarDescriptionMetadata.fillMetadata(WebAvatarDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebAvatarDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebAvatarDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:Avatar");
		metadata.item("__tagNameWC", "String:igc-avatar");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("Src", "String");
		metadata.item("Alt", "String");
		metadata.item("Initials", "String");
		metadata.item("Shape", "ExportedType:string:AvatarShape");
		metadata.item("Shape@stringUnion", "WebComponents;React");
		metadata.item("Shape@names", "Square;Circle;Rounded");
	}
	static register(context: TypeDescriptionContext): void {
		WebAvatarDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebAvatar", () => new WebAvatarDescription());
		context.register("WebAvatar", WebAvatarDescriptionMetadata._metadata);
	}
}


