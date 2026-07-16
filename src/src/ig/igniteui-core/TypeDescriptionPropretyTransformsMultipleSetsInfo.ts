import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class TypeDescriptionPropretyTransformsMultipleSetsInfo extends Base {
	static $t: Type = markType(TypeDescriptionPropretyTransformsMultipleSetsInfo, 'TypeDescriptionPropretyTransformsMultipleSetsInfo');
	private _propertyName: string = null;
	get propertyName(): string {
		return this._propertyName;
	}
	set propertyName(value: string) {
		this._propertyName = value;
	}
	private _newValue: any = null;
	get newValue(): any {
		return this._newValue;
	}
	set newValue(value: any) {
		this._newValue = value;
	}
	private _oldValue: any = null;
	get oldValue(): any {
		return this._oldValue;
	}
	set oldValue(value: any) {
		this._oldValue = value;
	}
}


