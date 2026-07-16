import { Base, IList$1, IList$1_$type, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, IList, IList_$type, ICollection, ICollection_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, Type, fromEnum, getBoxIfEnum, typeCast, typeCastObjTo$t, fromEn, Number_$type, typeGetValue, Date_$type, runOn, markType, String_$type, getEnumeratorObject } from "./type";
import { IArray, IArray_$type } from "./IArray";
import { IArrayList, IArrayList_$type } from "./IArrayList";
import { ArrayBox$1, arrayCopyTo } from "./array";
import { Comparer$1 } from "./Comparer$1";
import { IComparer$1 } from "./IComparer$1";
import { ReadOnlyCollection$1 } from "./ReadOnlyCollection$1";
import { NotImplementedException } from "./NotImplementedException";
import { intDivide } from "./number";
import { stringCompareTo } from "./string";

/**
 * @hidden 
 */
export class List$1<T> extends Base implements IList$1<T>, IArray, IList {
	static $t: Type = markType(List$1, 'List$1', (<any>Base).$type, [IList$1_$type.specialize(0), IArray_$type, IList_$type]);
	protected $t: Type = null;
	_inner: T[] = null;
	private readonly _useFastCompare: boolean = false;
	constructor($t: Type, initNumber: number);
	constructor($t: Type, initNumber: number, source: IEnumerable$1<T>);
	constructor($t: Type, initNumber: number, capacity: number);
	constructor($t: Type, initNumber: number, ..._rest: any[]);
	constructor($t: Type, initNumber: number, ..._rest: any[]) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				this._inner = <T[]>([]);
				this._useFastCompare = <boolean>(this.$t.InstanceConstructor && this.$t.InstanceConstructor.prototype.equals === Base.prototype.equals);
			}
			break;

			case 1:
			{
				let source: IEnumerable$1<T> = <IEnumerable$1<T>>_rest[0];
				{
					this._inner = <T[]>([]);
					this._useFastCompare = <boolean>(this.$t.InstanceConstructor && this.$t.InstanceConstructor.prototype.equals === Base.prototype.equals);
				}
				if (this.tryArray(0, source, true)) {
					return;
				}
				for (let item of fromEnum<T>(source)) {
					this.add(item);
				}
			}
			break;

			case 2:
			{
				let capacity: number = <number>_rest[0];
				{
					this._inner = <T[]>([]);
					this._useFastCompare = <boolean>(this.$t.InstanceConstructor && this.$t.InstanceConstructor.prototype.equals === Base.prototype.equals);
				}
			}
			break;

		}

	}
	protected setItem(index: number, newItem: T): void {
		this._inner[index] = newItem;
	}
	protected insertItem(index: number, newItem: T): void {
		this._inner.splice(index, 0, newItem);
	}
	protected addItem(newItem: T): void {
		this._inner.push(newItem);
	}
	protected removeItem(index: number): void {
		if (index == 0) {
			this._inner.shift();
			return;
		}
		this._inner.splice(index, 1);
	}
	protected clearItems(): void {
		this._inner = <T[]>([]);
	}
	item(index: number, value?: T): T {
		if (arguments.length === 2) {
			this.setItem(index, value);
			return value;
		} else {
			return this._inner[index];
		}
	}
	indexOf(item: T): number {
		if (this._useFastCompare) {
			return <number>(this._inner.indexOf(item));
		}
		for (let i: number = 0; i < this._inner.length; i++) {
			if (Base.equalsStatic(getBoxIfEnum<T>(this.$t, item), getBoxIfEnum<T>(this.$t, this._inner[i]))) {
				return i;
			}
		}
		return -1;
	}
	indexOf2(item: T, index: number): number {
		if (this._useFastCompare) {
			return <number>(this._inner.indexOf(item, index));
		}
		for (; index < this._inner.length; index++) {
			if (Base.equalsStatic(getBoxIfEnum<T>(this.$t, item), getBoxIfEnum<T>(this.$t, this._inner[index]))) {
				return index;
			}
		}
		return -1;
	}
	lastIndexOf(item: T): number {
		if (this._useFastCompare) {
			return <number>(this._inner.lastIndexOf(item));
		}
		for (let i: number = this._inner.length - 1; i >= 0; i--) {
			if (Base.equalsStatic(getBoxIfEnum<T>(this.$t, item), getBoxIfEnum<T>(this.$t, this._inner[i]))) {
				return i;
			}
		}
		return -1;
	}
	insert(index: number, item: T): void {
		this.insertItem(index, item);
	}
	removeAt(index: number): void {
		this.removeItem(index);
	}
	get count(): number {
		return this._inner.length;
	}
	get isReadOnly(): boolean {
		return false;
	}
	add(item: T): void {
		this.addItem(item);
	}
	clear(): void {
		this.clearItems();
	}
	contains(item: T): boolean {
		return this.indexOf(item) >= 0;
	}
	copyTo(array: T[], arrayIndex: number): void {
		for (let i: number = 0; i < this._inner.length; i++) {
			array[arrayIndex + i] = this._inner[i];
		}
	}
	remove(item: T): boolean {
		let indexOf: number = this.indexOf(item);
		if (indexOf < 0) {
			return false;
		}
		this.removeItem(indexOf);
		return true;
	}
	getEnumerator(): IEnumerator$1<T> {
		return <IEnumerator$1<T>><any>getEnumeratorObject(this._inner);
	}
	getEnumeratorObject(): IEnumerator {
		return getEnumeratorObject(this._inner);
	}
	asArray(): any[] {
		return this._inner;
	}
	private tryArray(index_: number, collection_: any, preciseType: boolean): boolean {
		let asArrayList = typeCast<IArrayList>(IArrayList_$type, collection_);
		if (asArrayList != null) {
			let a_ = asArrayList.asArrayList();
			Array.prototype.splice.apply(this._inner, Array.prototype.concat.apply([index_, 0], a_));
			return true;
		}
		let asArray = typeCast<IArray>(IArray_$type, collection_);
		if (asArray != null) {
			let a_ = asArray.asArray();
			Array.prototype.splice.apply(this._inner, Array.prototype.concat.apply([index_, 0], a_));
			return true;
		}
		let asList_ = typeCast<IList$1<T>>(IList$1_$type.specialize(this.$t), collection_);
		if (asList_ != null) {
			for (let i_: number = 0; i_ < asList_.count; i_++) {
				let item_ = asList_.item(i_);
				this._inner.splice(index_ + i_, 0, item_);
			}
			return true;
		}
		let arr_ = <any[]>(Array.isArray(collection_) ? collection_ : null);
		if (arr_ != null) {
			let inn_ = this._inner;
			if (this._inner.length == 0) {
				if (preciseType) {
					let parr_ = <T[]>arr_;
					let arrLen = arr_.length;
					for (let i_ = 0; i_ < arrLen; i_++) {
						inn_[index_++] = parr_[i_];
					}
				} else {
					let len = arr_.length;
					for (let i_ = 0; i_ < len; i_++) {
						inn_[index_++] = typeCastObjTo$t<T>(this.$t, arr_[i_]);
					}
				}
			} else {
				for (let i_ = 0; i_ < arr_.length; i_++) {
					inn_.splice(index_++, 0, arr_[i_]);
				}
			}
			return true;
		}
		return false;
	}
	insertRange1(index: number, collection: IEnumerable): void {
		if (this.tryArray(index, collection, false)) {
			return;
		}
		let j_: number = index;
		for (let item_ of fromEn<any>(collection)) {
			this._inner.splice(j_, 0, item_);
			j_++;
		}
	}
	insertRange(index: number, collection: IEnumerable$1<T>): void {
		if (this.tryArray(index, collection, true)) {
			return;
		}
		let j_: number = index;
		for (let item_ of fromEnum<T>(collection)) {
			this._inner.splice(j_, 0, item_);
			j_++;
		}
	}
	removeRange(index_: number, numToRemove_: number): void {
		if (index_ == 0 && numToRemove_ == 1) {
			this._inner.shift();
			return;
		}
		this._inner.splice(index_, numToRemove_);
	}
	copyTo1(array: any[], index: number): void {
		arrayCopyTo(this._inner, array, index);
	}
	get isFixedSize(): boolean {
		return false;
	}
	add1(value: any): number {
		this.addItem(typeCastObjTo$t<T>(this.$t, value));
		return this._inner.length - 1;
	}
	contains1(item: any): boolean {
		return this.indexOf1(item) >= 0;
	}
	indexOf1(item: any): number {
		return this.indexOf(typeCastObjTo$t<T>(this.$t, item));
	}
	insert1(index: number, value: any): void {
		this.insertItem(index, typeCastObjTo$t<T>(this.$t, value));
	}
	remove1(value: any): void {
		let indexOf = this.indexOf1(value);
		if (indexOf < 0) {
			return;
		}
		this.removeItem(indexOf);
	}
	sort(): void {
		let c: (obj1: any, obj2: any) => number = null;
		if (this.$t == Number_$type) {
			c = (n1: any, n2: any) => {
				let d1 = <number>n1;
				let d2 = <number>n2;
				if (d1 < d2) {
					return -1;
				}
				if (d1 == d2) {
					return 0;
				}
				return 1;
			};
		} else if (this.$t == Number_$type) {
			c = (n1: any, n2: any) => {
				let f1 = <number>n1;
				let f2 = <number>n2;
				if (f1 < f2) {
					return -1;
				}
				if (f1 == f2) {
					return 0;
				}
				return 1;
			};
		} else if (this.$t == Number_$type) {
			c = (n1: any, n2: any) => {
				let i1 = typeGetValue(n1);
				let i2 = typeGetValue(n2);
				if (i1 < i2) {
					return -1;
				}
				if (i1 == i2) {
					return 0;
				}
				return 1;
			};
		} else if (this.$t == Date_$type) {
			c = (n1: any, n2: any) => {
				let d1 = <Date>n1;
				let d2 = <Date>n2;
				if (d1.getTime() < d2.getTime()) {
					return -1;
				}
				if (d1.getTime() == d2.getTime()) {
					return 0;
				}
				return 1;
			};
		} else {
			c = (n1: any, n2: any) => stringCompareTo((<string>n1), <string>n2);
		}
		this.sortHelper(c);
	}
	private sortHelper(compare_: (obj1: any, obj2: any) => number): void {
		this._inner.sort(compare_);
	}
	sort2(compare_: (arg1: T, arg2: T) => number): void {
		this._inner.sort(compare_);
	}
	private _capacity: number = 0;
	get capacity(): number {
		return this._capacity;
	}
	set capacity(value: number) {
		this._capacity = value;
	}
	addRange(values: IEnumerable$1<T>): void {
		for (let item_ of fromEnum<T>(values)) {
			this._inner.push(item_);
		}
	}
	toArray(): T[] {
		return this._inner;
	}
	forEach(action: (arg1: T) => void): void {
		for (let i: number = 0; i < this._inner.length; i++) {
			action(this._inner[i]);
		}
	}
	get isSynchronized(): boolean {
		return true;
	}
	private readonly _syncRoot: any = {};
	get syncRoot(): any {
		return this._syncRoot;
	}
	binarySearch(item: T): number {
		return this.binarySearch1(item, Comparer$1.defaultComparerValue<T>(this.$t));
	}
	binarySearch1(item: T, comparer: IComparer$1<T>): number {
		let start: number = 0;
		let end: number = this.count - 1;
		while (start <= end) {
			let mid: number = start + (intDivide((end - start), 2));
			let testValue: T = this._inner[mid];
			let compareResult: number = comparer.compare(testValue, item);
			if (compareResult == 0) {
				return mid;
			}
			if (compareResult < 0) {
				start = mid + 1;
			} else {
				end = mid - 1;
			}
		}
		return ~start;
	}
	asReadOnly(): ReadOnlyCollection$1<T> {
		return new ReadOnlyCollection$1<T>(this.$t, 1, this);
	}
	reverse(): void {
		for (let i: number = 0; i < intDivide(this.count, 2); i++) {
			let other = this.count - i - 1;
			let temp = this._inner[i];
			this._inner[i] = this._inner[other];
			this._inner[other] = temp;
		}
	}
	sort1(comparer: IComparer$1<T>): void {
		this.sort2(runOn(comparer, comparer.compare));
	}
	findIndex(match: (obj: T) => boolean): number {
		for (let i: number = 0; i < this._inner.length; i++) {
			if (match(this._inner[i])) {
				return i;
			}
		}
		return -1;
	}
	removeAll(match: (obj: T) => boolean): number {
		throw new NotImplementedException(0);
	}
}


