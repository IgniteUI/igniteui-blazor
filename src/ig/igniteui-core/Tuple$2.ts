import { Base, Type, typeCast, getBoxIfEnum, markType } from "./type";

/**
 * @hidden 
 */
export class Tuple$2<T1, T2> extends Base {
	static $t: Type = markType(Tuple$2, 'Tuple$2');
	protected $t1: Type = null;
	protected $t2: Type = null;
	private _item1: T1 = null;
	get item1(): T1 {
		return this._item1;
	}
	set item1(value: T1) {
		this._item1 = value;
	}
	private _item2: T2 = null;
	get item2(): T2 {
		return this._item2;
	}
	set item2(value: T2) {
		this._item2 = value;
	}
	constructor($t1: Type, $t2: Type, item1: T1, item2: T2) {
		super();
		this.$t1 = $t1;
		this.$t2 = $t2;
		this.$type = this.$type.specialize(this.$t1, this.$t2);
		this.item1 = item1;
		this.item2 = item2;
	}
	equals(other: any): boolean {
		let tOther = typeCast<Tuple$2<T1, T2>>((<any>Tuple$2).$type.specialize(this.$t1, this.$t2), other);
		return tOther != null && Base.equalsStatic(getBoxIfEnum<T1>(this.$t1, this.item1), getBoxIfEnum<T1>(this.$t1, tOther.item1)) && Base.equalsStatic(getBoxIfEnum<T2>(this.$t2, this.item2), getBoxIfEnum<T2>(this.$t2, tOther.item2));
	}
	getHashCode(): number {
		let hash = 0;
		if (getBoxIfEnum<T1>(this.$t1, this.item1) != null) {
			hash = Base.getHashCodeStatic(this.item1);
		}
		if (getBoxIfEnum<T2>(this.$t2, this.item2) != null) {
			hash = hash ^ Base.getHashCodeStatic(this.item2) << 16;
		}
		return hash;
	}
}


