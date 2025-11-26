import { WebMaskInputBaseDescription } from "./WebMaskInputBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebMaskInputDescription extends WebMaskInputBaseDescription {
	static $t: Type = markType(WebMaskInputDescription, 'WebMaskInputDescription', (<any>WebMaskInputBaseDescription).$type);
	protected get_type(): string {
		return "WebMaskInput";
	}
	constructor() {
		super();
	}
	private _valueMode: string = null;
	get valueMode(): string {
		return this._valueMode;
	}
	set valueMode(value: string) {
		this._valueMode = value;
		this.markDirty("ValueMode");
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _mask: string = null;
	get mask(): string {
		return this._mask;
	}
	set mask(value: string) {
		this._mask = value;
		this.markDirty("Mask");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


