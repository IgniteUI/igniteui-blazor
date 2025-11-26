import { IList, ICollection, IEnumerable, IEnumerable$1, IEnumerator, IEnumerator$1, IteratorWrapper, IList$1, getEnumerator, getEnumeratorObject, Base, Type, Array_$type } from "./type";

	export function arrayCopyTo(source: any[], dest: any[], index: number) {
		for (var i = 0; i < source.length; i++) {
			dest[ index++ ] = source[ i ];
		}
	}
    export function arrayCopy(source: any[], sourceIndex: number, dest: any[], destIndex: number, count: number) {
		for (var i = 0; i < count; i++) {
			dest[ destIndex + i ] = source[ sourceIndex + i ];
        }
    }
    export function arrayInsert(target: any[], index: number, item: any) {
        target.splice(index, 0, item);
    }
    export function arrayRemoveAt(target: any[], index: number) {
		target.splice(index, 1);
	}

	export function arrayRemoveItem(target: any[], item: any) {
		var index = target.indexOf(item);
		if (index >= 0) {
			target.splice(index, 1);
			return true;
		}
		return false;
    }
    
    export function arrayGetValue(target: any[], index: number) {
        return target[index];
    }
    
    export function arrayGetLength(target: any[], dimension: number) {

		// TODO: Is there a better way to do this? Maybe attach the rank values to the array?

		var array = target;
		var dim = dimension;

		while (array) {
			if (dim === 0) {
				return array.length;
			}

			dim--;
			array = array[ 0 ];
		}

		return -1;
    }

    export function arrayGetRank(target: any[]) {
        return 1;
    }
    
    export function arrayResize<T>(target: T[], length: number) {
        target.length = 0;
    }

    export function arrayInsertRange(target: any[], index: number, items: any[]) {
		var i = 0;
		if (target.length === 0) {
			for (i = 0; i < items.length; i++) {
				target[ index++ ] = items[ i ];
			}
		} else {
			for (i = 0; i < items.length; i++) {
				target.splice(index++, 0, items[ i ]);
			}
		}
	}

	export function arrayInsertRange1(target: any[], index: number, items: any[]) {

		//TODO: adjust this later, but this is the safest change to make right now.
		var i = 0;
		if (target.length === 0) {
			for (i = 0; i < items.length; i++) {
				target[ index++ ] = items[ i ];
			}
		} else {
			for (i = 0; i < items.length; i++) {
				target.splice(index++, 0, items[ i ]);
			}
		}
    }
    
    export function arrayShallowClone(arr: any[]) {
		var newArr = [];
		for (var i = 0; i < arr.length; i++) {
			newArr[ i ] = arr[ i ];
		}
		return newArr;
    }
    
    export function arrayClear(arr: any[]) {
        arr.length = 0;
		}

 //   export function listItem(list: any, index: number, item?: any): void {
 //       if ((<any>list).$type === undefined) {
 //           let arr = <any[]>list;
 //           if (item !== undefined) {
 //               arr[index] = item;
 //               return item;
 //           } else {
 //               return arr[index];
 //           }
 //       }
 //       if (item !== undefined) {
 //           list.item(index, item);
 //           return item;
 //       }
 //       return list.item(index);
 //   }

	//export function  listAdd(list: any, item: any): void {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					arrayInsert(arr, arr.length, item);
	//					return;
	//			}
	//			list.add(item);
	//	}
	//export function  listClear(list: any): void {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					arrayClear(arr);
	//					return;
	//			}
	//			list.clear();
	//	}
	//export function listContains(list: any, item: any): boolean {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					return arr.indexOf(item) > -1;
	//			}
	//			return list.contains(item);
	//	}
	//export function listIndexOf(list: any, item: any): number {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					return arr.indexOf(item);
	//			}
	//			return list.indexOf(item);
	//	}
	//export function listInsert(list: any, index: number, item: any): void {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					arrayInsert(arr, index, item);
	//					return;
	//			}
	//			list.contains(item);
	//	}
	//export function listRemove(list: any, item: any): boolean {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
 //                       arrayRemoveItem(arr, item);
 //                       return true;
	//			}
	//			return list.remove(item);
	//	}
	//export function listRemoveAt(list: any, index: number): void {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
 //                       arrayRemoveAt(arr, index);
 //                       return;
	//			}
	//			list.removeAt(index);
	//	}
	//export function listIsFixedSize(list: any): boolean {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					return false;
	//			}
	//			return list.isFixedSize;
	//	}
	//export function listIsReadOnly(list: any): boolean {
	//			if ((<any>list).$type === undefined) {
	//					let arr = <any[]>list;
	//					return false;
	//			}
	//			return list.isReadOnly;
	//	}
	//export function collectionCount(collection: any): number {
	//			if ((<any>collection).$type === undefined) {
	//					let arr = <any[]>collection;
	//					return arr.length;
	//			}
	//			return collection.count;
	//	}
	//export function collectionCopyTo(collection: any, array: any[], index: number): void {
	//			if ((<any>collection).$type === undefined) {
	//					let arr = <any[]>collection;
	//					arrayCopy(arr, 0, array, index, arr.length);
	//					return;
	//			}
	//			return collection.copyTo(array, index);
	//	}
	//export function collectionIsSynchronized(collection: any): boolean {
	//			if ((<any>collection).$type === undefined) {
	//					let arr = <any[]>collection;
	//					return false;
	//			}
	//			return collection.isSynchronized;
	//	}
	//export function collectionSyncRoot(collection: any): any {
	//			if ((<any>collection).$type === undefined) {
	//					let arr = <any[]>collection;
	//					return arr;
	//			}
	//			return collection.syncRoot;
 //   }

export function boxArray$1<T>(array: any): ArrayBox$1<T> {
    return new ArrayBox$1<T>(array);
}

export function unboxArray<T>(box: any): T[] {
    if (box.$arrayWrapper) {
        return <T[]>box._target;
    }
    return <T[]>box;
}

export function arrayListCreate(): ArrayBox$1<any> {
    return new ArrayBox$1<any>([]);
}

export class ArrayBox$1<T> implements IList, IList$1<T>, ICollection, IEnumerable, IEnumerable$1<T> {
	isFixedSize: boolean = false;
	isSynchronized: boolean = false;
	syncRoot: any = null;

	private _target: any[];
	constructor(target: any) {
		this._target = target;
	}

    item(index: number, value?: T): T {
        if (arguments.length === 2) {
            this._target[index] = value;
            return value;
        } else {
            return <T>this._target[index];
        }
    }
	indexOf(item: T): number {
		return this._target.indexOf(item);
	}
	insert(index: number, item: T): void {
		this._target.splice(index, 0, item);
    }
    insertRange(index: number, items: any[]) {
        arrayInsertRange(this._target, index, items);
    }
    removeRange(index: number, count: number) {
        this._target.splice(index, count);
    }
	removeAt(index: number): void {
		this._target.splice(index, 1);
	}
	get count(): number {
		return this._target.length;
	}
	isReadOnly: boolean = false;
	add(item: T): void {
		this._target.push(item);
	}
	clear(): void {
		this._target.length = 0;
	}
	contains(item: T): boolean {
		return this._target.indexOf(item) >= 0;
	}
	copyTo(array: T[], arrayIndex: number): void {
		for (let i = 0; i < this._target.length; i++) {
			array[i + arrayIndex] = this._target[i];
		}
	}
	remove(item: T): boolean {
		let index = this._target.indexOf(item);
		if (index < 0) {
			return false;
		}

		this._target.splice(index, 1);
		return true;
	}
	getEnumerator(): IEnumerator$1<T> {
		return getEnumerator(this._target);
	}
	getEnumeratorObject(): IEnumerator {
		return getEnumeratorObject(this._target);
    }
    getHashCode(): number {
		return Base.getHashCodeStatic(this._target);
	}
	$arrayWrapper: boolean = true;
	equals(other: any): boolean {
		if (other.$arrayWrapper) {
			other = other._target;
		}
		return this._target === other;
    }
    get $type(): Type {
        return Array_$type;
    }
    reverse(): void {
        let len = this._target.length;
        for (let i = 0; i < len / 2.0; i++) {
            let swap = this._target[(len - 1) - i];
            this._target[(len - 1) - i] = this._target[i];
            this._target[i] = swap;
        }    
    }
}

	
