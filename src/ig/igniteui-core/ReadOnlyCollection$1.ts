import { Base, IList$1, IList$1_$type, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, IList, IList_$type, ICollection, ICollection_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, Type, typeCastObjTo$t, markType, getEnumerator } from "./type";

/**
 * @hidden 
 */
export class ReadOnlyCollection$1<T> extends Base implements IList$1<T>, IList {
	static $t: Type = markType(ReadOnlyCollection$1, 'ReadOnlyCollection$1', (<any>Base).$type, [IList$1_$type.specialize(0), IList_$type]);
	protected $t: Type = null;
	constructor($t: Type, initNumber: number);
	constructor($t: Type, initNumber: number, source: IList$1<T>);
	constructor($t: Type, initNumber: number, ..._rest: any[]);
	constructor($t: Type, initNumber: number, ..._rest: any[]) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0: break;
			case 1:
			{
				let source: IList$1<T> = <IList$1<T>>_rest[0];
				this._inner = source;
			}
			break;

		}

	}
	private _inner: IList$1<T> = null;
	item(index: number, value?: T): T {
		if (arguments.length === 2) {
			this._inner.item(index, value);
			return value;
		} else {
			return this._inner.item(index);
		}
	}
	indexOf(item: T): number {
		return this._inner.indexOf(item);
	}
	insert(index: number, item: T): void {
	}
	removeAt(index: number): void {
	}
	get count(): number {
		return this._inner.count;
	}
	get isReadOnly(): boolean {
		return true;
	}
	add(item: T): void {
	}
	clear(): void {
	}
	contains(item: T): boolean {
		return this._inner.contains(item);
	}
	copyTo(array: T[], arrayIndex: number): void {
		this._inner.copyTo(array, arrayIndex);
	}
	remove(item: T): boolean {
		return false;
	}
	getEnumerator(): IEnumerator$1<T> {
		return getEnumerator(this._inner);
	}
	getEnumeratorObject(): IEnumerator {
		return getEnumerator(this._inner);
	}
	get isFixedSize(): boolean {
		return true;
	}
	add1(value: any): number {
		return -1;
	}
	contains1(value: any): boolean {
		return this._inner.contains(typeCastObjTo$t<T>(this.$t, value));
	}
	indexOf1(value: any): number {
		return this._inner.indexOf(typeCastObjTo$t<T>(this.$t, value));
	}
	insert1(index: number, value: any): void {
	}
	remove1(value: any): void {
	}
	copyTo1(array: any[], index: number): void {
		this._inner.copyTo(<T[]>array, index);
	}
	protected get items(): IList$1<T> {
		return this._inner;
	}
	get isSynchronized(): boolean {
		return true;
	}
	private readonly _syncRoot: any = {};
	get syncRoot(): any {
		return this._syncRoot;
	}
}


