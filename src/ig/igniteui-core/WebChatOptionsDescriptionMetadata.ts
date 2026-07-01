import { Base, String_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Dictionary$2 } from "./Dictionary$2";
import { WebChatRenderersDescriptionMetadata } from "./WebChatRenderersDescriptionMetadata";
import { WebChatOptionsDescription } from "./WebChatOptionsDescription";

/**
 * @hidden 
 */
export class WebChatOptionsDescriptionMetadata extends Base {
	static $t: Type = markType(WebChatOptionsDescriptionMetadata, 'WebChatOptionsDescriptionMetadata');
	private static _metadata: Dictionary$2<string, string> = null;
	private static ensureMetadata(context: TypeDescriptionContext): void {
		if (WebChatOptionsDescriptionMetadata._metadata == null) {
			WebChatOptionsDescriptionMetadata._metadata = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			WebChatOptionsDescriptionMetadata.fillMetadata(WebChatOptionsDescriptionMetadata._metadata);
		}
		if (context.hasMetadata(WebChatOptionsDescriptionMetadata._metadata)) {
			return;
		}
		context.markSeen(WebChatOptionsDescriptionMetadata._metadata);
		WebChatRenderersDescriptionMetadata.register(context);
	}
	static fillMetadata(metadata: Dictionary$2<string, string>): void {
		metadata.item("__qualifiedNameTS", "String:ChatOptions");
		metadata.item("__skipModuleRegisterWebComponents", "Boolean");
		metadata.item("__importTypesWebComponents", "String:igniteui-webcomponents");
		metadata.item("__isPlainObject", "Boolean");
		metadata.item("CurrentUserId", "String");
		metadata.item("DisableAutoScroll", "Boolean");
		metadata.item("DisableInputAttachments", "Boolean");
		metadata.item("IsTyping", "Boolean");
		metadata.item("HeaderText", "String");
		metadata.item("InputPlaceholder", "String");
		metadata.item("Suggestions", "Array:string");
		metadata.item("SuggestionsPosition", "ExportedType:string:ChatSuggestionsPosition");
		metadata.item("SuggestionsPosition@stringUnion", "WebComponents;React");
		metadata.item("SuggestionsPosition@names", "BelowInput;BelowMessages");
		metadata.item("StopTypingDelay", "Number:double");
		metadata.item("AdoptRootStyles", "Boolean");
		metadata.item("Renderers", "ExportedType:WebChatRenderers");
	}
	static register(context: TypeDescriptionContext): void {
		WebChatOptionsDescriptionMetadata.ensureMetadata(context);
		context.registerDescriptionConstructor("WebChatOptions", () => new WebChatOptionsDescription());
		context.register("WebChatOptions", WebChatOptionsDescriptionMetadata._metadata);
	}
}


