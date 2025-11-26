import { Base, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, IDictionary, IDictionary_$type, IEqualityComparer$1, IEqualityComparer$1_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, NotSupportedException, Type, fromEnum, typeCastObjTo$t, getBoxIfEnum, toEnum, markType, String_$type, getEnumerator } from "./type";
import { IDictionary$2, IDictionary$2_$type } from "./IDictionary$2";
import { KeyValuePair$2 } from "./KeyValuePair$2";
import { EqualityComparer$1 } from "./EqualityComparer$1";
import { Dictionary_EnumerableCollection$3 } from "./Dictionary_EnumerableCollection$3";
import { ArgumentException } from "./ArgumentException";
import { NotImplementedException } from "./NotImplementedException";
import { Thread } from "./culture";
import { stringToString$1 } from "./string";
import { arrayRemoveItem } from "./array";

/**
 * @hidden 
 */
export class Dictionary$2<TKey, TValue> extends Base implements IDictionary$2<TKey, TValue>, IDictionary {
	static $t: Type = markType(Dictionary$2, 'Dictionary$2', (<any>Base).$type, [IDictionary$2_$type.specialize(0, 1), IDictionary_$type]);
	protected $tKey: Type = null;
	protected $tValue: Type = null;
	private _comparer: IEqualityComparer$1<TKey> = null;
	private _count: number = 0;
	private _useStrings: boolean = false;
	private _needsEnsure: boolean = false;
	private _assumeUniqueKeys: boolean = false;
	private _keysUnique: TKey[] = null;
	private _values: TValue[] = null;
	constructor($tKey: Type, $tValue: Type, initNumber: number);
	constructor($tKey: Type, $tValue: Type, initNumber: number, capacity: number);
	constructor($tKey: Type, $tValue: Type, initNumber: number, comparer: IEqualityComparer$1<TKey>);
	constructor($tKey: Type, $tValue: Type, initNumber: number, dictionary: IDictionary$2<TKey, TValue>);
	constructor($tKey: Type, $tValue: Type, initNumber: number, capacity: number, comparer: IEqualityComparer$1<TKey>);
	constructor($tKey: Type, $tValue: Type, initNumber: number, ..._rest: any[]);
	constructor($tKey: Type, $tValue: Type, initNumber: number, ..._rest: any[]) {
		super();
		this.$tKey = $tKey;
		this.$tValue = $tValue;
		this.$type = this.$type.specialize(this.$tKey, this.$tValue);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				let $outerArgs = <any[]>[ 0, null ];
				{
					let capacity: number = <number>$outerArgs[0];
					let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>$outerArgs[1];
					this._keysUnique = <TKey[]>({});
					this._values = <TValue[]>({});
					this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey);
					this._useStrings = comparer == null && <boolean>(($tKey === String_$type));
					this._needsEnsure = <boolean>($tKey === (<any>Base).$type || ($tKey.InstanceConstructor && !$tKey.InstanceConstructor.prototype.getHashCode));
					this._assumeUniqueKeys = comparer == null && (this._useStrings || this._needsEnsure || <boolean>($tKey.InstanceConstructor && $tKey.InstanceConstructor.prototype.getHashCode == Base.prototype.getHashCode));
				}
			}
			break;

			case 1:
			{
				let capacity: number = <number>_rest[0];
				let $outerArgs = <any[]>[ capacity, null ];
				{
					let capacity: number = <number>$outerArgs[0];
					let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>$outerArgs[1];
					this._keysUnique = <TKey[]>({});
					this._values = <TValue[]>({});
					this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey);
					this._useStrings = comparer == null && <boolean>(($tKey === String_$type));
					this._needsEnsure = <boolean>($tKey === (<any>Base).$type || ($tKey.InstanceConstructor && !$tKey.InstanceConstructor.prototype.getHashCode));
					this._assumeUniqueKeys = comparer == null && (this._useStrings || this._needsEnsure || <boolean>($tKey.InstanceConstructor && $tKey.InstanceConstructor.prototype.getHashCode == Base.prototype.getHashCode));
				}
			}
			break;

			case 2:
			{
				let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>_rest[0];
				let $outerArgs = <any[]>[ 0, comparer ];
				{
					let capacity: number = <number>$outerArgs[0];
					let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>$outerArgs[1];
					this._keysUnique = <TKey[]>({});
					this._values = <TValue[]>({});
					this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey);
					this._useStrings = comparer == null && <boolean>(($tKey === String_$type));
					this._needsEnsure = <boolean>($tKey === (<any>Base).$type || ($tKey.InstanceConstructor && !$tKey.InstanceConstructor.prototype.getHashCode));
					this._assumeUniqueKeys = comparer == null && (this._useStrings || this._needsEnsure || <boolean>($tKey.InstanceConstructor && $tKey.InstanceConstructor.prototype.getHashCode == Base.prototype.getHashCode));
				}
			}
			break;

			case 3:
			{
				let dictionary: IDictionary$2<TKey, TValue> = <IDictionary$2<TKey, TValue>>_rest[0];
				let $outerArgs = <any[]>[ dictionary.count ];
				{
					let capacity: number = <number>$outerArgs[0];
					let $outerArgs1 = <any[]>[ capacity, null ];
					{
						let capacity: number = <number>$outerArgs1[0];
						let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>$outerArgs1[1];
						this._keysUnique = <TKey[]>({});
						this._values = <TValue[]>({});
						this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey);
						this._useStrings = comparer == null && <boolean>(($tKey === String_$type));
						this._needsEnsure = <boolean>($tKey === (<any>Base).$type || ($tKey.InstanceConstructor && !$tKey.InstanceConstructor.prototype.getHashCode));
						this._assumeUniqueKeys = comparer == null && (this._useStrings || this._needsEnsure || <boolean>($tKey.InstanceConstructor && $tKey.InstanceConstructor.prototype.getHashCode == Base.prototype.getHashCode));
					}
				}
				for (let pair of fromEnum<KeyValuePair$2<TKey, TValue>>(dictionary)) {
					this.item(pair.key, pair.value);
				}
			}
			break;

			case 4:
			{
				let capacity: number = <number>_rest[0];
				let comparer: IEqualityComparer$1<TKey> = <IEqualityComparer$1<TKey>>_rest[1];
				this._keysUnique = <TKey[]>({});
				this._values = <TValue[]>({});
				this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey);
				this._useStrings = comparer == null && <boolean>(($tKey === String_$type));
				this._needsEnsure = <boolean>($tKey === (<any>Base).$type || ($tKey.InstanceConstructor && !$tKey.InstanceConstructor.prototype.getHashCode));
				this._assumeUniqueKeys = comparer == null && (this._useStrings || this._needsEnsure || <boolean>($tKey.InstanceConstructor && $tKey.InstanceConstructor.prototype.getHashCode == Base.prototype.getHashCode));
			}
			break;

		}

	}
	get count(): number {
		return this._count;
	}
	item(key_: TKey, value?: TValue): TValue {
		if (arguments.length === 2) {
			this.addHelper(key_, value, false);
			return value;
		} else {
			let result_: TValue = this.tryGetValueFast(key_);
			let present: boolean = <boolean>(result_ !== undefined);
			return present ? result_ : Type.getDefaultValue<TValue>(this.$tValue);
		}
	}
	get length(): number {
		return this._count;
	}
	containsKey(key: TKey): boolean {
		if (this._assumeUniqueKeys) {
			let hash = this.hashUnique(key);
			return <boolean>(this._keysUnique.hasOwnProperty(hash));
		} else {
			let hashCode = this.hashCode(key);
			let current = this._values[hashCode];
			if (<boolean>(<any>current)) {
				if (<boolean>((<any>current).$isHashSetBucket)) {
					let $t = (<any[]><any>(current));
					for (let i = 0; i < $t.length; i++) {
						let currentItem = $t[i];
						if (this._comparer.equalsC(<TKey>((<any>currentItem).key), key)) {
							return true;
						}
					}
				} else {
					return this._comparer.equalsC(<TKey>((<any>current).key), key);
				}
			}
		}
		return false;
	}
	removeItem(key: TKey): boolean {
		if (this._assumeUniqueKeys) {
			let hash = this.hashUnique(key);
			if (<boolean>(!this._keysUnique.hasOwnProperty(hash))) {
				return false;
			}
			delete (<any>this._keysUnique)[hash];
			delete (<any>this._values)[hash];
			this._count--;
			return true;
		}
		let hashCode = this.hashCode(key);
		let current = this._values[hashCode];
		if (<boolean>(<any>current)) {
			if (<boolean>((<any>current).$isHashSetBucket)) {
				let $t = (<any[]><any>(current));
				for (let i = 0; i < $t.length; i++) {
					let currentItem = $t[i];
					if (this._comparer.equalsC(<TKey>((<any>currentItem).key), key)) {
						arrayRemoveItem(<any[]><any>current, currentItem);
						if (<number>((<any>current).length) == 1) {
							this._values[hashCode] = <TValue>((<any>current)[0]);
						}
						this._count--;
						return true;
					}
				}
			} else {
				if (this._comparer.equalsC(<TKey>((<any>current).key), key)) {
					delete (<any>this._values)[hashCode];
					this._count--;
					return true;
				}
			}
		}
		return false;
	}
	clear(): void {
		this._count = 0;
		this._keysUnique = <TKey[]>({});
		this._values = <TValue[]>({});
	}
	private hashUnique(key: TKey): string {
		if (this._useStrings) {
			return stringToString$1<TKey>(this.$tKey, key);
		} else {
			return Base.getHashCodeStatic(key).toString();
		}
	}
	private hashCode(key: TKey): number {
		return this._comparer.getHashCodeC(key);
	}
	addItem(key: TKey, value: TValue): void {
		this.addHelper(key, value, true);
	}
	private addHelper(key: TKey, value: TValue, add: boolean): void {
		if (this._assumeUniqueKeys) {
			let hash = this.hashUnique(key);
			if (<boolean>(!this._keysUnique.hasOwnProperty(hash))) {
				this._count++;
			} else if (add) {
				throw new ArgumentException(1, "Duplicate key added to the dictionary");
			}
			(<any>this._keysUnique)[hash] = key;
			(<any>this._values)[hash] = value;
		} else {
			let hashCode = this.hashCode(key);
			let current = this._values[hashCode];
			if (<boolean>(<any>current)) {
				if (<boolean>((<any>current).$isHashSetBucket)) {
					let $t = (<any[]><any>(current));
					for (let i = 0; i < $t.length; i++) {
						let currentItem = $t[i];
						if (this._comparer.equalsC(<TKey>((<any>currentItem).key), key)) {
							if (add) {
								throw new ArgumentException(1, "Duplicate key added to the dictionary");
							}
							(<any>currentItem).value = value;
							return;
						}
					}
					(<any>current).push(<any>{key: key, value: value});
					this._count++;
				} else {
					if (this._comparer.equalsC(<TKey>((<any>current).key), key)) {
						if (add) {
							throw new ArgumentException(1, "Duplicate key added to the dictionary");
						}
						(<any>current).value = value;
					} else {
						let bucket = typeCastObjTo$t<TValue>(this.$tValue, <any>([current, {key: key, value: value}]));
						(<any>bucket).$isHashSetBucket = true;;
						this._values[hashCode] = bucket;
						this._count++;
					}
				}
			} else {
				this._values[hashCode] = <TValue>(<any>{key: key, value: value});
				this._count++;
			}
		}
	}
	tryGetValueFast(key: TKey): TValue {
		if (this._assumeUniqueKeys) {
			let hash = this.hashUnique(key);
			if (<boolean>(this._keysUnique.hasOwnProperty(hash))) {
				let value = <TValue>((<any>this._values)[hash]);
				return value;
			}
		} else {
			let hashCode = this.hashCode(key);
			let current = this._values[hashCode];
			if (<boolean>(<any>current)) {
				if (<boolean>((<any>current).$isHashSetBucket)) {
					let $t = (<any[]><any>(current));
					for (let i = 0; i < $t.length; i++) {
						let currentItem = $t[i];
						if (this._comparer.equalsC(<TKey>((<any>currentItem).key), key)) {
							let value1 = <TValue>((<any>currentItem).value);
							return value1;
						}
					}
				} else {
					if (this._comparer.equalsC(<TKey>((<any>current).key), key)) {
						let value2 = <TValue>((<any>current).value);
						return value2;
					}
				}
			}
		}
		return typeCastObjTo$t<TValue>(this.$tValue, <any>(undefined));
	}
	tryGetValue(key: TKey, value: TValue): { ret: boolean; p1: TValue; } {
		if (this._assumeUniqueKeys) {
			let hash = this.hashUnique(key);
			if (<boolean>(this._keysUnique.hasOwnProperty(hash))) {
				value = <TValue>((<any>this._values)[hash]);
				return {
					ret: true,
					p1: value

				};
			}
		} else {
			let hashCode = this.hashCode(key);
			let current = this._values[hashCode];
			if (<boolean>(<any>current)) {
				if (<boolean>((<any>current).$isHashSetBucket)) {
					let $t = (<any[]><any>(current));
					for (let i = 0; i < $t.length; i++) {
						let currentItem = $t[i];
						if (this._comparer.equalsC(<TKey>((<any>currentItem).key), key)) {
							value = <TValue>((<any>currentItem).value);
							return {
								ret: true,
								p1: value

							};
						}
					}
				} else {
					if (this._comparer.equalsC(<TKey>((<any>current).key), key)) {
						value = <TValue>((<any>current).value);
						return {
							ret: true,
							p1: value

						};
					}
				}
			}
		}
		value = Type.getDefaultValue<TValue>(this.$tValue);
		return {
			ret: false,
			p1: value

		};
	}
	get isReadOnly(): boolean {
		return false;
	}
	add(item: KeyValuePair$2<TKey, TValue>): void {
		this.addItem(item.key, item.value);
	}
	contains(item: KeyValuePair$2<TKey, TValue>): boolean {
		let test: TValue;
		return ((() => { let $ret = this.tryGetValue(item.key, test); test = $ret.p1; return $ret.ret; })()) && Base.equalsStatic(getBoxIfEnum<TValue>(this.$tValue, test), getBoxIfEnum<TValue>(this.$tValue, item.value));
	}
	copyTo(array: KeyValuePair$2<TKey, TValue>[], arrayIndex: number): void {
		throw new NotImplementedException(0);
	}
	remove(item: KeyValuePair$2<TKey, TValue>): boolean {
		this.removeItem(item.key);
		return true;
	}
	getEnumerator(): IEnumerator$1<KeyValuePair$2<TKey, TValue>> {
		return getEnumerator(this.toEnumerable());
	}
	private *_toEnumerable(): IterableIterator<any> {
		if (this._assumeUniqueKeys) {
			let array = <number[]>(Base.getArrayOfProperties(this._keysUnique));
			for (let i: number = 0; i < array.length; i++) {
				yield new KeyValuePair$2<TKey, TValue>(this.$tKey, this.$tValue, 1, this._keysUnique[array[i]], <TValue>this._values[array[i]]);
			}
		} else {
			let array1 = <number[]>(Base.getArrayOfProperties(this._values));
			for (let i1: number = 0; i1 < array1.length; i1++) {
				let pair = this._values[array1[i1]];
				if (<boolean>((<any>pair).$isHashSetBucket)) {
					let pArray = <any[]><any>(pair);
					for (let j: number = 0; j < pArray.length; j++) {
						let subItem_ = pArray[j];
						yield new KeyValuePair$2<TKey, TValue>(this.$tKey, this.$tValue, 1, <TKey>((<any>subItem_).key), <TValue>((<any>subItem_).value));
					}
				} else {
					yield new KeyValuePair$2<TKey, TValue>(this.$tKey, this.$tValue, 1, <TKey>((<any>pair).key), <TValue>((<any>pair).value));
				}
			}
		}
	}
	private toEnumerable(): IEnumerable$1<KeyValuePair$2<TKey, TValue>> {
		return toEnum(() => this._toEnumerable());
	}
	private *_toEnumerableKeys(): IterableIterator<any> {
		if (this._assumeUniqueKeys) {
			let array = <number[]>(Base.getArrayOfProperties(this._keysUnique));
			for (let i: number = 0; i < array.length; i++) {
				yield this._keysUnique[array[i]];
			}
		} else {
			let array1 = <number[]>(Base.getArrayOfProperties(this._values));
			for (let i1: number = 0; i1 < array1.length; i1++) {
				let pair = this._values[array1[i1]];
				if (<boolean>((<any>pair).$isHashSetBucket)) {
					let pArray = <any[]><any>(pair);
					for (let j: number = 0; j < pArray.length; j++) {
						let subItem_ = pArray[j];
						yield <TKey>((<any>subItem_).key);
					}
				} else {
					yield <TKey>((<any>pair).key);
				}
			}
		}
	}
	private toEnumerableKeys(): IEnumerable$1<TKey> {
		return toEnum(() => this._toEnumerableKeys());
	}
	private *_toEnumerableValues(): IterableIterator<any> {
		if (this._assumeUniqueKeys) {
			let array = <number[]>(Base.getArrayOfProperties(this._keysUnique));
			for (let i: number = 0; i < array.length; i++) {
				yield <TValue>this._values[array[i]];
			}
		} else {
			let array1 = <number[]>(Base.getArrayOfProperties(this._values));
			for (let i1: number = 0; i1 < array1.length; i1++) {
				let pair = this._values[array1[i1]];
				if (<boolean>((<any>pair).$isHashSetBucket)) {
					let pArray = <any[]><any>(pair);
					for (let j: number = 0; j < pArray.length; j++) {
						let subItem_ = pArray[j];
						yield <TValue>((<any>subItem_).value);
					}
				} else {
					yield <TValue>((<any>pair).value);
				}
			}
		}
	}
	private toEnumerableValues(): IEnumerable$1<TValue> {
		return toEnum(() => this._toEnumerableValues());
	}
	getEnumeratorObject(): IEnumerator {
		return getEnumerator(this.toEnumerable());
	}
	get keys(): ICollection$1<TKey> {
		return new Dictionary_EnumerableCollection$3<TKey, TValue, TKey>(this.$tKey, this.$tValue, this.$tKey, this, this.toEnumerableKeys(), this._comparer || EqualityComparer$1.defaultEqualityComparerValue<TKey>(this.$tKey));
	}
	get values(): ICollection$1<TValue> {
		return new Dictionary_EnumerableCollection$3<TKey, TValue, TValue>(this.$tKey, this.$tValue, this.$tValue, this, this.toEnumerableValues(), EqualityComparer$1.defaultEqualityComparerValue<TValue>(this.$tValue));
	}
}


