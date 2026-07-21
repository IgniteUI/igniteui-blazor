import { IList$1, Base } from './type';
import { List$1 } from './List$1';
import { SyncableObservableCollection$2 } from './SyncableObservableCollection$2';
import { SyncableObservableCollection$1 } from './SyncableObservableCollection$1';

export abstract class IgCollection<T, T2> {
    item(index: number, value?: T): T {
        if (this._innerColl != null) {
            return this._innerColl.get(index);
        } else {
            if (value) {
                value = this._ensureOuter(value);
                let innerValue: T2 = null;
                if ((<any>value)._implementation) {
                    innerValue = (<any>value)._implementation;
                } else {
                    innerValue = <T2><any>value;
                }
                this._inner.item(index, innerValue);
                return value;
            } else {
                let item: any = this._inner.item(index);
                item = this._toExternal(item);
                return <T>item;
            }
        }
    }
    indexOf(item: T): number {
        if (this._innerColl != null) {
            return this._innerColl.indexOf(this._ensureOuter(item));
        } else {
            let actualItem: any = this._ensureOuter(item);
            if (actualItem._implementation) {
                actualItem = actualItem._implementation;
            }
            return this._inner.indexOf(actualItem);
        }
    }
    insert(index: number, item: T): void {
        if (this._innerColl != null) {
            this._innerColl.add1(index, item);
        } else {
            let actualItem: any = this._ensureOuter(item);
            if (actualItem._implementation) {
                actualItem = actualItem._implementation;
            }
            this._inner.insert(index, actualItem);
        }
    }
    removeAt(index: number): void {
        if (this._innerColl != null) {
            this._innerColl.remove(index);
        } else {
            this._inner.removeAt(index);
        }
    }
    get count(): number {
        if (this._innerColl != null) {
            return this._innerColl.size();
        } else {
            return this._inner.count;
        }
    }

    add(item: T): void {
        if (this._innerColl != null) {
            this._innerColl.add(item);
        } else {
            let actualItem: any = this._ensureOuter(item);
            if (actualItem._implementation) {
                actualItem = actualItem._implementation;
            }
            this._inner.add(actualItem);
        }
    }
    clear(): void {
        if (this._innerColl != null) {
            this._innerColl.clear();
        } else {
            this._inner.clear();
        }
    }
    contains(item: T): boolean {
        if (this._innerColl != null) {
            return this._innerColl.all.contains(item);
        } else {
            let actualItem: any = item;
            if (actualItem._implementation) {
                actualItem = actualItem._implementation;
            }

            return this._inner.contains(actualItem);
        }
    }

    remove(item: T): boolean {
        if (this._innerColl != null) {
            return this._innerColl.remove1(item);
        } else {
            let actualItem: any = this._ensureOuter(item);
            if (actualItem._implementation) {
                actualItem = actualItem._implementation;
            }

            return this._inner.remove(actualItem);
        }
    }

    public findByName(name: string): any {
        for (let i = 0; i < this.count; i++) {
            var curr = this.item(i);
            if (curr && (curr as any).name) {
                if ((curr as any).name == name) {
                    return curr;
                }
            }
            if (curr) {
                if ((curr as any).findByName) {
                    let found = (curr as any).findByName(name);
                    if (found) {
                        return found;
                    }
                }
            }
        }
        return null;
    }

    public hasName(name: string): boolean {
        return this.findByName(name) != null;
    }

    
    public filter(predicate: (value: T, index: number, array: T[]) => unknown, thisArg?: any): T[] {
        return this.toArray().filter(predicate, thisArg);
    }

    public toArray(): T[] {
        let arr: T[] = [];
        for (let i = 0; i < this.count; i++) {
            arr[i] = this.item(i);
        }
        return arr;
    }

    *[Symbol.iterator]() {
        for (let i = 0; i < this.count; i++) {
            let item: any = this.item(i);

            yield item;
        }
    }

    private _setSyncTarget(list: SyncableObservableCollection$1<T2>) {
        if (this._innerColl != null) {
            this._innerColl.syncTarget = list;
        }
    }

    private _isIgxCollection: boolean = true;
    private _inner: IList$1<T2> = null;
    private _innerColl: SyncableObservableCollection$2<T, T2> = null;
    private _createFrom: (item: T2) => T;
    private _createTo: (item: T) => T2;

    private _fromInner(inner: IList$1<T2>): any {
        if (Array.isArray(inner)) {
            let l = new List$1<any>(Base.prototype.$type, 0);
            l._inner = inner;
            inner = l;
        }

        this._inner = inner;
        if (this._innerColl) {
            this._createFrom = this._innerColl.createFrom;
            this._createTo = this._innerColl.createTo;
        }
        this._innerColl = null;
        return this;
    }

    protected _toExternal(item: T2): T {
        if (!item) {
            return null;
        }
        let ext: T = (item as any).externalObject as T;
        if (ext) {
            return ext;
        }
        if (this._createFrom) {
            ext = this._createFrom(item);
        }
        if (!ext) {
            return item as any as T;
        }
        return ext;
    }

    protected _ensureOuter(item: any) {
        return item;
    }

    protected _splitOuter(item: string): string[] {
        if (item == null) {
            return [];
        }
        if (item.indexOf(",") == -1) {
            return [item];
        }
        let parts = item.split(",");
        for (let i = 0; i < parts.length; i++) {
            if (parts[i]) {
                parts[i] = parts[i].trim();
            }
        }

        return parts;
    }

    private _fromOuter(outer: any): any {
        if (outer._isIgxCollection) {
            return outer;
        }
        if (Array.isArray(outer)) {
            for (let i = 0; i < outer.length; i++) {
                this.add(this._ensureOuter(outer[i]));
            }
            return this;
        }
        let split = this._splitOuter(outer);
        for (let i = 0; i < split.length; i++) {
            this.add(this._ensureOuter(split[i]));
        }

        return this;
    }

    protected abstract _createInnerColl(): SyncableObservableCollection$2<T, T2>;

    constructor() {
        this._innerColl = this._createInnerColl();
    }
}