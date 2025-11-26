import { ValueType, Type, markStruct } from "./type";

/**
 * @hidden 
 */
export class KeyValuePair$2<TKey, TValue> extends ValueType {
	static $t: Type = markStruct(KeyValuePair$2, 'KeyValuePair$2');
	protected $tKey: Type = null;
	protected $tValue: Type = null;
	constructor($tKey: Type, $tValue: Type, initNumber: number, key: TKey, value: TValue);
	constructor($tKey: Type, $tValue: Type);
	constructor($tKey: Type, $tValue: Type, initNumber: number, ..._rest: any[]);
	constructor($tKey: Type, $tValue: Type, initNumber?: number, ..._rest: any[]) {
		super();
		this.$tKey = $tKey;
		this.$tValue = $tValue;
		this.$type = this.$type.specialize(this.$tKey, this.$tValue);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0: break;
			case 1:
			{
				let key: TKey = <TKey>_rest[0];
				let value: TValue = <TValue>_rest[1];
				this._key = key;
				this._value = value;
			}
			break;

		}

	}
	private _key: TKey = null;
	private _value: TValue = null;
	get key(): TKey {
		return this._key;
	}
	get value(): TValue {
		return this._value;
	}
}


