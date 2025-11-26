import { WebInputBaseDescription } from "./WebInputBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebMaskInputBaseDescription extends WebInputBaseDescription {
	static $t: Type = markType(WebMaskInputBaseDescription, 'WebMaskInputBaseDescription', (<any>WebInputBaseDescription).$type);
	protected get_type(): string {
		return "WebMaskInputBase";
	}
	constructor() {
		super();
	}
	private _prompt: string = null;
	get prompt(): string {
		return this._prompt;
	}
	set prompt(value: string) {
		this._prompt = value;
		this.markDirty("Prompt");
	}
}


