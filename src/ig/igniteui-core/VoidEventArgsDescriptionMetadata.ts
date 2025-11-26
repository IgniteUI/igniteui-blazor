import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { VoidEventArgsDescription } from "./VoidEventArgsDescription";

/**
 * @hidden 
 */
export class VoidEventArgsDescriptionMetadata extends Base {
	static $t: Type = markType(VoidEventArgsDescriptionMetadata, 'VoidEventArgsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (VoidEventArgsDescriptionMetadata._metadata == null) {
			VoidEventArgsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			VoidEventArgsDescriptionMetadata.fillMetadata(VoidEventArgsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(VoidEventArgsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(VoidEventArgsDescriptionMetadata._metadata);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
	}
	static register(context: TypeDescriptionContext): void {
		VoidEventArgsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("VoidEventArgs", () => new VoidEventArgsDescription());
		context.register("VoidEventArgs", VoidEventArgsDescriptionMetadata._metadata);
	}
}


