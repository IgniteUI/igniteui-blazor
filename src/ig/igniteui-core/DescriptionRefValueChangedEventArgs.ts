import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DescriptionRefValueChangedEventArgs extends Base {
	static $t: Type = markType(DescriptionRefValueChangedEventArgs, 'DescriptionRefValueChangedEventArgs');
	private _oldValue: any = null;
	get oldValue(): any {
		return this._oldValue;
	}
	set oldValue(value: any) {
		this._oldValue = value;
	}
	private _newValue: any = null;
	get newValue(): any {
		return this._newValue;
	}
	set newValue(value: any) {
		this._newValue = value;
	}
}


