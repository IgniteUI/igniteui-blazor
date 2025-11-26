import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class EmbeddedRefDescription extends Description {
	static $t: Type = markType(EmbeddedRefDescription, 'EmbeddedRefDescription', (<any>Description).$type);
	protected get_type(): string {
		return "EmbeddedRef";
	}
	get type(): string {
		return this.get_type();
	}
	private _refType: string = null;
	get refType(): string {
		return this._refType;
	}
	set refType(value: string) {
		this._refType = value;
		this.markDirty("RefType");
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
}


