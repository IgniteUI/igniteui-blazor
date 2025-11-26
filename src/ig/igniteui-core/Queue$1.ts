import { Base, ICollection, ICollection_$type, IEnumerable, IEnumerable_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator$1, IEnumerator$1_$type, IDisposable, IDisposable_$type, IEnumerator, IEnumerator_$type, NotSupportedException, Type, getBoxIfEnum, toEnum, markType } from "./type";
import { InvalidOperationException } from "./InvalidOperationException";

/**
 * @hidden 
 */
export class Queue$1<T> extends Base implements ICollection, IEnumerable$1<T> {
	static $t: Type = markType(Queue$1, 'Queue$1', (<any>Base).$type, [ICollection_$type, IEnumerable$1_$type.specialize(0)]);
	protected $t: Type = null;
	private _count: number = 0;
	private _head: number = 0;
	private _tail: number = 0;
	private _items: T[] = null;
	constructor($t: Type) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
		this._items = <T[]>new Array(4);
	}
	get count(): number {
		return this._count;
	}
	copyTo(array: any[], index: number): void {
		if (this._head < this._tail) {
			for (let i: number = this._head; i < this._tail; i++) {
				array[index++] = getBoxIfEnum<T>(this.$t, this._items[i]);
			}
		} else {
			for (let i1: number = this._head; i1 < this._items.length; i1++) {
				array[index++] = getBoxIfEnum<T>(this.$t, this._items[i1]);
			}
			for (let i2: number = 0; i2 < this._tail; i2++) {
				array[index++] = getBoxIfEnum<T>(this.$t, this._items[i2]);
			}
		}
	}
	get isSynchronized(): boolean {
		return false;
	}
	get syncRoot(): any {
		return null;
	}
	private *_getEnumerator(): IterableIterator<any> {
		if (this._head < this._tail) {
			for (let i: number = this._head; i < this._tail; i++) {
				yield this._items[i];
			}
		} else {
			for (let i1: number = this._head; i1 < this._items.length; i1++) {
				yield this._items[i1];
			}
			for (let i2: number = 0; i2 < this._tail; i2++) {
				yield this._items[i2];
			}
		}
	}
	getEnumerator(): IEnumerator$1<T> {
		return toEnum(() => this._getEnumerator()).getEnumerator();
	}
	getEnumeratorObject(): IEnumerator {
		return this.getEnumerator();
	}
	enqueue(item: T): void {
		if (this._count == this._items.length) {
			let newItems = <T[]>new Array(Math.max(2, this._items.length * 2));
			this.copyTo(newItems, 0);
			this._head = 0;
			this._tail = this._items.length;
			this._items = newItems;
		}
		this._items[this._tail] = item;
		this._tail = (this._tail + 1) % this._items.length;
		this._count++;
	}
	dequeue(): T {
		if (this._count == 0) {
			throw new InvalidOperationException(1, "The Queue is empty.");
		}
		let result = this._items[this._head];
		this._items[this._head] = <T>(null);
		this._head = (this._head + 1) % this._items.length;
		this._count--;
		return result;
	}
	peek(): T {
		if (this._count == 0) {
			throw new InvalidOperationException(1, "The Queue is empty.");
		}
		return this._items[this._head];
	}
}


