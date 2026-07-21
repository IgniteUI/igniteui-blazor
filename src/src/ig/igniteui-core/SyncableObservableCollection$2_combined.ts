import { Base, IList$1, IList$1_$type, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, IList, IList_$type, Type, ICollection, ICollection_$type, runOn, fromEnum, Number_$type, getBoxIfEnum, typeCastObjTo$t, delegateCombine, delegateRemove, Delegate_$type, markType } from "./type";
import { ObservableCollection$1 } from "./ObservableCollection$1";
import { List$1 } from "./List$1";
import { NotifyCollectionChangedEventArgs } from "./NotifyCollectionChangedEventArgs";
import { HashSet$1 } from "./HashSet$1";
import { InvalidOperationException } from "./InvalidOperationException";
import { NotifyCollectionChangedAction } from "./NotifyCollectionChangedAction";
import { SyncableObservableCollectionChangedListener, SyncableObservableCollectionChangedListener_$type } from "./SyncableObservableCollectionChangedListener";

/**
 * @hidden 
 */
export class SyncableObservableCollection$2<T1, T2> extends Base {
	static $t: Type = markType(SyncableObservableCollection$2, 'SyncableObservableCollection$2');
	protected $t1: Type = null;
	protected $t2: Type = null;
	private _syncTarget: SyncableObservableCollection$1<T2> = null;
	get syncTarget(): SyncableObservableCollection$1<T2> {
		return this._syncTarget;
	}
	set syncTarget(value: SyncableObservableCollection$1<T2>) {
		let oldValue = this._syncTarget;
		this._syncTarget = value;
		this.onSyncTargetChanged(oldValue, this._syncTarget);
	}
	private _oneWayTargets: List$1<SyncableObservableCollection$1<T2>> = new List$1<SyncableObservableCollection$1<T2>>((<any>SyncableObservableCollection$1).$type.specialize(this.$t2), 0);
	addOneWayTarget(target: SyncableObservableCollection$1<T2>): void {
		this._oneWayTargets.add(target);
		this.syncWithTarget(target, true);
	}
	removeOneWayTarget(target: SyncableObservableCollection$1<T2>): void {
		this._oneWayTargets.remove(target);
	}
	private _compare: (arg1: T1, arg2: T2) => boolean = null;
	get compare(): (arg1: T1, arg2: T2) => boolean {
		return this._compare;
	}
	set compare(value: (arg1: T1, arg2: T2) => boolean) {
		this._compare = value;
	}
	private _createTo: (arg1: T1) => T2 = null;
	get createTo(): (arg1: T1) => T2 {
		return this._createTo;
	}
	set createTo(value: (arg1: T1) => T2) {
		this._createTo = value;
	}
	private _createFrom: (arg1: T2) => T1 = null;
	get createFrom(): (arg1: T2) => T1 {
		return this._createFrom;
	}
	set createFrom(value: (arg1: T2) => T1) {
		this._createFrom = value;
	}
	private _shouldDetachOnTargetChange: boolean = false;
	get shouldDetachOnTargetChange(): boolean {
		return this._shouldDetachOnTargetChange;
	}
	set shouldDetachOnTargetChange(value: boolean) {
		this._shouldDetachOnTargetChange = value;
	}
	private onSyncTargetChanged(oldValue: SyncableObservableCollection$1<T2>, newValue: SyncableObservableCollection$1<T2>): void {
		if (oldValue == newValue) {
			return;
		}
		if (oldValue != null) {
			oldValue.removeListener(runOn(this, this.targetChanged));
		}
		this.syncWithTarget(this._syncTarget, false);
		if (newValue != null) {
			newValue.addListener(runOn(this, this.targetChanged));
		}
	}
	private syncWithTarget(target: SyncableObservableCollection$1<T2>, oneWay: boolean): void {
		if (target == null) {
			return;
		}
		this._duringSync = true;
		let union: List$1<T1> = new List$1<T1>(this.$t1, 0);
		let seen: HashSet$1<T1> = new HashSet$1<T1>(this.$t1, 0);
		for (let i = 0; i < this.all.count; i++) {
			if (!seen.contains(this.all.item(i))) {
				union.add(this.all.item(i));
				seen.add_1(this.all.item(i));
			}
		}
		if (!oneWay) {
			for (let i1 = 0; i1 < target.all.count; i1++) {
				if (!this.listContains(seen, target.all.item(i1))) {
					let syncItem = this.createFrom(target.all.item(i1));
					union.add(syncItem);
					seen.add_1(syncItem);
				}
			}
		}
		if (!oneWay) {
			this.syncWithUnion(union, this.all);
		}
		this.syncWithUnion1(union, target.all);
		this._duringSync = false;
	}
	private listContains(list: IEnumerable$1<T1>, item: T2): boolean {
		if (this.compare == null) {
			throw new InvalidOperationException(1, "SyncableObservableCollection has no Compare function provided.");
		}
		for (let i of fromEnum<T1>(list)) {
			if (this.compare(i, item)) {
				return true;
			}
		}
		return false;
	}
	private syncWithUnion(union: List$1<T1>, list: IList$1<T1>): void {
		let toRemove: List$1<number> = new List$1<number>(Number_$type, 0);
		let j: number = 0;
		for (let i = 0; i < list.count; i++) {
			if (getBoxIfEnum<T1>(this.$t1, list.item(i)) == null) {
				toRemove.add(i);
				continue;
			}
			if (j > union.count - 1) {
				toRemove.add(i);
				continue;
			}
			if (Base.equalsStatic(list.item(i), getBoxIfEnum<T1>(this.$t1, union._inner[j]))) {
				j++;
				continue;
			}
			list.insert(i, union._inner[j]);
			j++;
		}
		for (let i1 = toRemove.count - 1; i1 >= 0; i1--) {
			list.removeAt(i1);
		}
		for (; j < union.count; j++) {
			list.add(union._inner[j]);
		}
	}
	private syncWithUnion1(union: List$1<T1>, list: IList$1<T2>): void {
		let toRemove: List$1<number> = new List$1<number>(Number_$type, 0);
		let j: number = 0;
		for (let i = 0; i < list.count; i++) {
			if (getBoxIfEnum<T2>(this.$t2, list.item(i)) == null) {
				toRemove.add(i);
				continue;
			}
			if (j > union.count - 1) {
				toRemove.add(i);
				continue;
			}
			let t2Value = this.createTo(union._inner[j]);
			if (Base.equalsStatic(list.item(i), getBoxIfEnum<T2>(this.$t2, t2Value))) {
				j++;
				continue;
			}
			list.insert(i, t2Value);
			j++;
		}
		for (let i1 = toRemove.count - 1; i1 >= 0; i1--) {
			list.removeAt(i1);
		}
		for (; j < union.count; j++) {
			list.add(this.createTo(union._inner[j]));
		}
	}
	private targetChanged(sender: any, e: NotifyCollectionChangedEventArgs): void {
		if (this._syncTarget == null) {
			return;
		}
		if (this._duringSync) {
			return;
		}
		if (this._duringSelfChange) {
			return;
		}
		if (this.shouldDetachOnTargetChange) {
			this.syncTarget = null;
			this._oneWayTargets.clear();
			return;
		}
		let inner = this._syncTarget.all;
		this._duringTargetChange = true;
		this.syncChange$2<T1, T2>(this.$t1, this.$t2, e, this.all, inner);
		this._duringTargetChange = false;
	}
	protected onSelfChanged(e: NotifyCollectionChangedEventArgs): void {
	}
	private selfChanged(sender: any, e: NotifyCollectionChangedEventArgs): void {
		this.onSelfChanged(e);
		if (this._syncTarget == null && this._oneWayTargets.count == 0) {
			return;
		}
		if (this._duringSync) {
			return;
		}
		if (this._duringTargetChange) {
			return;
		}
		if (this._syncTarget != null) {
			let inner = this._syncTarget.all;
			this._duringSelfChange = true;
			this.syncChange$2<T2, T1>(this.$t2, this.$t1, e, inner, this.all);
			this._duringSelfChange = false;
		}
		if (this._oneWayTargets.count > 0) {
			for (let i = 0; i < this._oneWayTargets.count; i++) {
				let inner2 = this._oneWayTargets._inner[i].all;
				this._duringSelfChange = true;
				this.syncChange$2<T2, T1>(this.$t2, this.$t1, e, inner2, this.all);
				this._duringSelfChange = false;
			}
		}
	}
	private syncChange$2<T, F>($t: Type, $f: Type, e: NotifyCollectionChangedEventArgs, to: IList$1<T>, from: IList$1<F>): void {
		switch (e.action) {
			case NotifyCollectionChangedAction.Add:
			if (e.newItems != null) {
				for (let i = 0; i < e.newItems.count; i++) {
					let item: any = e.newItems.item(i);
					if ($f != $t) {
						if ($f == this.$t1) {
							item = getBoxIfEnum<T2>(this.$t2, this.createTo(typeCastObjTo$t<T1>(this.$t1, e.newItems.item(i))));
						}
						if ($f == this.$t2) {
							item = getBoxIfEnum<T1>(this.$t1, this.createFrom(typeCastObjTo$t<T2>(this.$t2, e.newItems.item(i))));
						}
					}
					to.insert(i + e.newStartingIndex, typeCastObjTo$t<T>($t, item));
				}
			}
			break;

			case NotifyCollectionChangedAction.Remove:
			if (e.oldItems != null) {
				for (let j = 0; j < e.oldItems.count; j++) {
					to.removeAt(e.oldStartingIndex);
				}
			}
			break;

			case NotifyCollectionChangedAction.Replace:
			if (e.oldItems != null) {
				for (let k = 0; k < e.oldItems.count; k++) {
					to.removeAt(e.oldStartingIndex);
				}
			}
			if (e.newItems != null) {
				for (let m = 0; m < e.newItems.count; m++) {
					let item1: any = e.newItems.item(m);
					if ($f != $t) {
						if ($f == this.$t1) {
							item1 = getBoxIfEnum<T2>(this.$t2, this.createTo(typeCastObjTo$t<T1>(this.$t1, e.newItems.item(m))));
						}
						if ($f == this.$t2) {
							item1 = getBoxIfEnum<T1>(this.$t1, this.createFrom(typeCastObjTo$t<T2>(this.$t2, e.newItems.item(m))));
						}
					}
					to.insert(m + e.newStartingIndex, typeCastObjTo$t<T>($t, e.newItems.item(m)));
				}
			}
			break;

			case NotifyCollectionChangedAction.Reset:
			to.clear();
			for (let item2 of fromEnum<F>(from)) {
				let i_o: any = getBoxIfEnum<F>($f, item2);
				if ($f != $t) {
					if ($f == this.$t1) {
						i_o = getBoxIfEnum<T2>(this.$t2, this.createTo(typeCastObjTo$t<T1>(this.$t1, i_o)));
					}
					if ($f == this.$t2) {
						i_o = getBoxIfEnum<T1>(this.$t1, this.createFrom(typeCastObjTo$t<T2>(this.$t2, i_o)));
					}
				}
				to.add(typeCastObjTo$t<T>($t, i_o));
			}
			break;

		}

	}
	private _duringSync: boolean = false;
	private _duringTargetChange: boolean = false;
	private _duringSelfChange: boolean = false;
	private _inner: ObservableCollection$1<T1> = null;
	constructor($t1: Type, $t2: Type, initNumber: number);
	constructor($t1: Type, $t2: Type, initNumber: number, _internalDescriptors: ObservableCollection$1<T1>);
	constructor($t1: Type, $t2: Type, initNumber: number, ..._rest: any[]);
	constructor($t1: Type, $t2: Type, initNumber: number, ..._rest: any[]) {
		super();
		this.$t1 = $t1;
		this.$t2 = $t2;
		this.$type = this.$type.specialize(this.$t1, this.$t2);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				this._inner = new ObservableCollection$1<T1>(this.$t1, 0);
				this.addListener(runOn(this, this.selfChanged));
				let $t = this._inner;
				$t.collectionChanged = delegateCombine($t.collectionChanged, runOn(this, this._sortDescriptions_CollectionChanged));
			}
			break;

			case 1:
			{
				let _internalDescriptors: ObservableCollection$1<T1> = <ObservableCollection$1<T1>>_rest[0];
				this._inner = _internalDescriptors;
			}
			break;

		}

	}
	changeInner(inner: ObservableCollection$1<T1>): void {
		if (this._inner != null) {
			let $t = this._inner;
			$t.collectionChanged = delegateRemove($t.collectionChanged, runOn(this, this._sortDescriptions_CollectionChanged));
		}
		this._inner = inner;
		if (this._inner != null) {
			let $t1 = this._inner;
			$t1.collectionChanged = delegateCombine($t1.collectionChanged, runOn(this, this._sortDescriptions_CollectionChanged));
		}
	}
	private _sortDescriptions_CollectionChanged(sender: any, e: NotifyCollectionChangedEventArgs): void {
		if (this.onChanged != null) {
			this.onChanged();
		}
		for (let listener of fromEnum<SyncableObservableCollectionChangedListener>(this._listeners)) {
			listener.onChanged(this);
		}
	}
	add(item: T1): boolean {
		this._inner.add(item);
		return true;
	}
	add1(index: number, item: T1): void {
		this._inner.insert(index, item);
	}
	clear(): void {
		let toAct: List$1<() => void> = new List$1<() => void>(Delegate_$type, 0);
		for (let item of fromEnum<() => void>(this._clearListeners)) {
			toAct.add(item);
		}
		for (let item1 of fromEnum<() => void>(toAct)) {
			item1();
		}
		this._inner.clear();
	}
	get(index: number): T1 {
		return this._inner._inner[index];
	}
	indexOf(item: T1): number {
		return this._inner.indexOf(item);
	}
	remove1(item: T1): boolean {
		let has = this._inner.contains(item);
		this._inner.remove(item);
		return has;
	}
	contains(item: T1): boolean {
		return this._inner.contains(item);
	}
	remove(index: number): T1 {
		let item = this._inner._inner[index];
		this._inner.removeAt(index);
		return item;
	}
	set(index: number, value: T1): T1 {
		this._inner.item(index, value);
		return value;
	}
	size(): number {
		return this._inner.count;
	}
	private _listeners: List$1<SyncableObservableCollectionChangedListener> = new List$1<SyncableObservableCollectionChangedListener>(SyncableObservableCollectionChangedListener_$type, 0);
	addChangedListener(listener: SyncableObservableCollectionChangedListener): void {
		this._listeners.add(listener);
	}
	removeChangedListener(listener: SyncableObservableCollectionChangedListener): void {
		this._listeners.remove(listener);
	}
	addListener(eventHandler: (sender: any, e: NotifyCollectionChangedEventArgs) => void): void {
		let $t = this._inner;
		$t.collectionChanged = delegateCombine($t.collectionChanged, eventHandler);
	}
	removeListener(eventHandler: (sender: any, e: NotifyCollectionChangedEventArgs) => void): void {
		let $t = this._inner;
		$t.collectionChanged = delegateRemove($t.collectionChanged, eventHandler);
	}
	private _clearListeners: List$1<() => void> = new List$1<() => void>(Delegate_$type, 0);
	addClearListener(eventHandler: () => void): void {
		this._clearListeners.add(eventHandler);
	}
	removeClearListener(eventHandler: () => void): void {
		this._clearListeners.remove(eventHandler);
	}
	private _onChanged: () => void = null;
	get onChanged(): () => void {
		return this._onChanged;
	}
	set onChanged(value: () => void) {
		this._onChanged = value;
	}
	get all(): IList$1<T1> {
		return this._inner;
	}
}

/**
 * @hidden 
 */
export class SyncableObservableCollection$1<T> extends SyncableObservableCollection$2<T, T> {
	static $t: Type = markType(SyncableObservableCollection$1, 'SyncableObservableCollection$1', (<any>SyncableObservableCollection$2).$type.specialize(0, 0));
	protected $t: Type = null;
	constructor($t: Type) {
		super($t, $t, 0);
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
		this.compare = (left: T, right: T) => Base.equalsStatic(left, getBoxIfEnum<T>(this.$t, right));
		this.createFrom = (right: T) => right;
		this.createTo = (left: T) => left;
	}
}


