import { Base, ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, IEqualityComparer$1, IEqualityComparer$1_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, Type, fromEnum, typeCastObjTo$t, markType, String_$type, getEnumeratorObject } from "./type";
import { EqualityComparer$1 } from "./EqualityComparer$1";
import { NotImplementedException } from "./NotImplementedException";
import { arrayRemoveItem } from "./array";

/**
 * @hidden 
 */
export class HashSet$1<T> extends Base implements ICollection$1<T>, IEnumerable$1<T>, IEnumerable {
	static $t: Type = markType(HashSet$1, 'HashSet$1', (<any>Base).$type, [ICollection$1_$type.specialize(0), IEnumerable$1_$type.specialize(0), IEnumerable_$type]);
	protected $t: Type = null;
	private _comparer: IEqualityComparer$1<T> = null;
	private _count: number = 0;
	private _values: T[] = null;
	private _useStrings: boolean = false;
	private _stringPlaceholder: any = null;
	private _needsEnsure: boolean = false;
	constructor($t: Type, initNumber: number);
	constructor($t: Type, initNumber: number, collection: IEnumerable$1<T>);
	constructor($t: Type, initNumber: number, comparer: IEqualityComparer$1<T>);
	constructor($t: Type, initNumber: number, collection: IEnumerable$1<T>, comparer: IEqualityComparer$1<T>);
	constructor($t: Type, initNumber: number, ..._rest: any[]);
	constructor($t: Type, initNumber: number, ..._rest: any[]) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				let $outerArgs = <any[]>[ <IEqualityComparer$1<T>><any>null ];
				{
					let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>$outerArgs[0];
					this._values = <T[]>({});
					this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<T>(this.$t);
					this._useStrings = comparer == null && <boolean>(($t === String_$type));
					this._needsEnsure = <boolean>($t === (<any>Base).$type || ($t.InstanceConstructor && !$t.InstanceConstructor.prototype.getHashCode));
					this._stringPlaceholder = <any>({});
				}
			}
			break;

			case 1:
			{
				let collection: IEnumerable$1<T> = <IEnumerable$1<T>>_rest[0];
				let $outerArgs = <any[]>[ collection, null ];
				{
					let collection: IEnumerable$1<T> = <IEnumerable$1<T>>$outerArgs[0];
					let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>$outerArgs[1];
					let $outerArgs1 = <any[]>[ comparer ];
					{
						let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>$outerArgs1[0];
						this._values = <T[]>({});
						this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<T>(this.$t);
						this._useStrings = comparer == null && <boolean>(($t === String_$type));
						this._needsEnsure = <boolean>($t === (<any>Base).$type || ($t.InstanceConstructor && !$t.InstanceConstructor.prototype.getHashCode));
						this._stringPlaceholder = <any>({});
					}
					for (let item of fromEnum<T>(collection)) {
						this.add_1(item);
					}
				}
			}
			break;

			case 2:
			{
				let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>_rest[0];
				this._values = <T[]>({});
				this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<T>(this.$t);
				this._useStrings = comparer == null && <boolean>(($t === String_$type));
				this._needsEnsure = <boolean>($t === (<any>Base).$type || ($t.InstanceConstructor && !$t.InstanceConstructor.prototype.getHashCode));
				this._stringPlaceholder = <any>({});
			}
			break;

			case 3:
			{
				let collection: IEnumerable$1<T> = <IEnumerable$1<T>>_rest[0];
				let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>_rest[1];
				let $outerArgs = <any[]>[ comparer ];
				{
					let comparer: IEqualityComparer$1<T> = <IEqualityComparer$1<T>>$outerArgs[0];
					this._values = <T[]>({});
					this._comparer = comparer || EqualityComparer$1.defaultEqualityComparerValue<T>(this.$t);
					this._useStrings = comparer == null && <boolean>(($t === String_$type));
					this._needsEnsure = <boolean>($t === (<any>Base).$type || ($t.InstanceConstructor && !$t.InstanceConstructor.prototype.getHashCode));
					this._stringPlaceholder = <any>({});
				}
				for (let item of fromEnum<T>(collection)) {
					this.add_1(item);
				}
			}
			break;

		}

	}
	add(item: T): void {
		this.add_1(item);
	}
	get isReadOnly(): boolean {
		return false;
	}
	getEnumeratorObject(): IEnumerator {
		return this.getEnumerator();
	}
	get comparer(): IEqualityComparer$1<T> {
		return this._comparer;
	}
	get count(): number {
		return this._count;
	}
	add_1(item_: T): boolean {
		if (this._useStrings) {
			if (<boolean><any>((<any>this._values)[item_])) {
				return false;
			}
			(<any>this._values)[item_] = <T>this._stringPlaceholder;
			this._count++;
			return true;
		}
		let hashCode = this._comparer.getHashCodeC(item_);
		let current_ = <T>this._values[hashCode];
		if (<boolean><any>(current_)) {
			if (<boolean>((<any>current_).$isHashSetBucket)) {
				let currentList = <T[]><any>(current_);
				for (let i = 0; i < currentList.length; i++) {
					let currentItem = currentList[i];
					if (this._comparer.equalsC(currentItem, item_)) {
						return false;
					}
				}
				(<any>current_).push(item_);
			} else {
				if (this._comparer.equalsC(current_, item_)) {
					return false;
				}
				let bucket_ = typeCastObjTo$t<T>(this.$t, <any>([current_, item_]));
				(<any>bucket_).$isHashSetBucket = true;;
				this._values[hashCode] = bucket_;
			}
		} else {
			this._values[hashCode] = item_;
		}
		this._count++;
		return true;
	}
	clear(): void {
		this._count = 0;
		this._values = <T[]>({});
	}
	contains(item_: T): boolean {
		if (this._useStrings) {
			return <boolean>(!!(<any>this._values)[item_]);
		}
		let hashCode = this._comparer.getHashCodeC(item_);
		let current_ = <T>this._values[hashCode];
		if (<boolean>(current_ !== undefined)) {
			if (<boolean>((<any>current_).$isHashSetBucket)) {
				let currentList = <T[]><any>(current_);
				for (let i = 0; i < currentList.length; i++) {
					let currentItem = currentList[i];
					if (this._comparer.equalsC(currentItem, item_)) {
						return true;
					}
				}
			} else {
				if (this._comparer.equalsC(current_, item_)) {
					return true;
				}
			}
		}
		return false;
	}
	copyTo1(array: T[]): void {
		throw new NotImplementedException(0);
	}
	copyTo(array: T[], arrayIndex: number): void {
		throw new NotImplementedException(0);
	}
	copyTo2(array: T[], arrayIndex: number, count: number): void {
		throw new NotImplementedException(0);
	}
	static createSetComparer<T>($t: Type): IEqualityComparer$1<HashSet$1<T>> {
		throw new NotImplementedException(0);
	}
	exceptWith(other: IEnumerable$1<T>): void {
		throw new NotImplementedException(0);
	}
	getEnumerator(): IEnumerator$1<T> {
		if (this._useStrings) {
			let props = <T[]>(Base.getArrayOfProperties(this._values));
			return <IEnumerator$1<T>><any><any><any>getEnumeratorObject(props);
		}
		let result_ = <any[]>new Array(0);
		let valuesResolved = <any[]>(Base.getArrayOfValues(this._values));
		for (let i = 0; i < valuesResolved.length; i++) {
			let item_ = valuesResolved[i];
			if (<boolean>((<any>item_).$isHashSetBucket)) {
				let currentList = <T[]><any>(item_);
				for (let i1 = 0; i1 < currentList.length; i1++) {
					let subItem_ = currentList[i1];
					(<any>result_).push(subItem_);
				}
			} else {
				(<any>result_).push(item_);
			}
		}
		return <IEnumerator$1<T>><any><any><any>getEnumeratorObject(result_);
	}
	intersectWith(other: IEnumerable$1<T>): void {
		throw new NotImplementedException(0);
	}
	isProperSubsetOf(other: IEnumerable$1<T>): boolean {
		throw new NotImplementedException(0);
	}
	isProperSupersetOf(other: IEnumerable$1<T>): boolean {
		throw new NotImplementedException(0);
	}
	isSubsetOf(other: IEnumerable$1<T>): boolean {
		throw new NotImplementedException(0);
	}
	isSupersetOf(other: IEnumerable$1<T>): boolean {
		throw new NotImplementedException(0);
	}
	onDeserialization(sender: any): void {
		throw new NotImplementedException(0);
	}
	overlaps(other: IEnumerable$1<T>): boolean {
		if (this.count > 0) {
			for (let item of fromEnum<T>(other)) {
				if (this.contains(item)) {
					return true;
				}
			}
		}
		return false;
	}
	remove(item_: T): boolean {
		if (this._useStrings) {
			if (!<boolean><any>((<any>this._values)[item_])) {
				return false;
			}
			delete (<any>this._values)[item_];
			this._count--;
			return true;
		}
		let hashCode_ = this._comparer.getHashCodeC(item_);
		let current_ = this._values[hashCode_];
		if (<boolean><any>(current_)) {
			if (<boolean>((<any>current_).$isHashSetBucket)) {
				let currentList = <T[]><any>(current_);
				for (let i = 0; i < currentList.length; i++) {
					let currentItem_ = currentList[i];
					if (this._comparer.equalsC(currentItem_, item_)) {
						arrayRemoveItem(<any>current_, currentItem_);
						if (<number>((<any>current_).length) == 1) {
							this._values[hashCode_] = <T>((<any>current_)[0]);
						}
						this._count--;
						return true;
					}
				}
			} else {
				if (this._comparer.equalsC(current_, item_)) {
					delete (<any>this._values)[hashCode_];
					this._count--;
					return true;
				}
			}
		}
		return false;
	}
	setEquals(other: IEnumerable$1<T>): boolean {
		throw new NotImplementedException(0);
	}
	symmetricExceptWith(other: IEnumerable$1<T>): void {
		throw new NotImplementedException(0);
	}
	trimExcess(): void {
	}
	unionWith(other: IEnumerable$1<T>): void {
		for (let item of fromEnum<T>(other)) {
			this.add_1(item);
		}
	}
}


