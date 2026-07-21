import { Base, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, IEqualityComparer$1, IEqualityComparer$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, IEnumerator$1, IEnumerator$1_$type, Type, getBoxIfEnum, fromEn, markType, getEnumeratorObject } from "./type";
import { Dictionary$2 } from "./Dictionary$2";
import { InvalidOperationException } from "./InvalidOperationException";

/**
 * @hidden 
 */
export class Dictionary_EnumerableCollection$3<TKey, TValue, T> extends Base implements ICollection$1<T> {
	static $t: Type = markType(Dictionary_EnumerableCollection$3, 'Dictionary_EnumerableCollection$3', (<any>Base).$type, [ICollection$1_$type.specialize(2)]);
	protected $tKey: Type = null;
	protected $tValue: Type = null;
	protected $t: Type = null;
	private readonly _collection: IEnumerable = null;
	private readonly _comparer: IEqualityComparer$1<T> = null;
	private readonly _owner: Dictionary$2<TKey, TValue> = null;
	constructor($tKey: Type, $tValue: Type, $t: Type, owner: Dictionary$2<TKey, TValue>, collection: IEnumerable, comparer: IEqualityComparer$1<T>) {
		super();
		this.$tKey = $tKey;
		this.$tValue = $tValue;
		this.$t = $t;
		this.$type = this.$type.specialize(this.$tKey, this.$tValue, this.$t);
		this._collection = collection;
		this._comparer = comparer;
		this._owner = owner;
	}
	get count(): number {
		return this._owner.count;
	}
	get isReadOnly(): boolean {
		return true;
	}
	add(item: T): void {
		throw new InvalidOperationException(0);
	}
	clear(): void {
		throw new InvalidOperationException(0);
	}
	contains(item: T): boolean {
		for (let i of fromEn<T>(this._collection)) {
			if (Base.equalsStatic(getBoxIfEnum<T>(this.$t, i), getBoxIfEnum<T>(this.$t, item))) {
				return true;
			}
		}
		return false;
	}
	copyTo(array: T[], arrayIndex: number): void {
		for (let item of fromEn<T>(this._collection)) {
			array[arrayIndex++] = item;
		}
	}
	remove(item: T): boolean {
		throw new InvalidOperationException(0);
	}
	getEnumerator(): IEnumerator$1<T> {
		return <IEnumerator$1<T>><any>getEnumeratorObject(this._collection);
	}
	getEnumeratorObject(): IEnumerator {
		return getEnumeratorObject(this._collection);
	}
}


