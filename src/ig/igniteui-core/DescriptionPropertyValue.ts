import { Base, Type, markType } from "./type";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";

/**
 * @hidden 
 */
export class DescriptionPropertyValue extends Base {
	static $t: Type = markType(DescriptionPropertyValue, 'DescriptionPropertyValue');
	private _propertyName: string = null;
	get propertyName(): string {
		return this._propertyName;
	}
	set propertyName(value: string) {
		this._propertyName = value;
	}
	private _value: any = null;
	get value(): any {
		return this._value;
	}
	set value(value: any) {
		this._value = value;
	}
	private _metadata: TypeDescriptionMetadata = null;
	get metadata(): TypeDescriptionMetadata {
		return this._metadata;
	}
	set metadata(value: TypeDescriptionMetadata) {
		this._metadata = value;
	}
}


