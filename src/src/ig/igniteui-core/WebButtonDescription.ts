import { WebButtonBaseDescription } from "./WebButtonBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebButtonDescription extends WebButtonBaseDescription {
	static $t: Type = markType(WebButtonDescription, 'WebButtonDescription', (<any>WebButtonBaseDescription).$type);
	protected get_type(): string {
		return "WebButton";
	}
	constructor() {
		super();
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
}


