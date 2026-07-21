import { Base, Type, typeCastObjTo$t, markType, IComparable$1, IComparable$1_$type, typeCast } from "./type";
import { IComparer, IComparer_$type } from "./IComparer";
import { IComparer$1, IComparer$1_$type } from "./IComparer$1";
import { CompareUtil } from "./compareUtil";

/**
 * @hidden 
 */
export abstract class Comparer$1<T> extends Base implements IComparer, IComparer$1<T> {
	static $t: Type = markType(Comparer$1, 'Comparer$1', (<any>Base).$type, [IComparer_$type, IComparer$1_$type.specialize(0)]);
	protected $t: Type = null;
	constructor($t: Type) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
	}
	static defaultComparerValue<T>($t: Type): Comparer$1<T> {
		return new DefaultComparer$1<T>($t);
	}
	abstract compare(x: T, y: T): number;
	static create<T>($t: Type, comparison: (x: T, y: T) => number): Comparer$1<T> {
		return null;
	}
	compareObject(x: any, y: any): number {
		return this.compare(typeCastObjTo$t<T>(this.$t, x), typeCastObjTo$t<T>(this.$t, y));
	}
}

/**
 * @hidden 
 */
export class DefaultComparer$1<T> extends Comparer$1<T> {
	static $t: Type = markType(DefaultComparer$1, 'DefaultComparer$1', (<any>Comparer$1).$type.specialize(0));
	protected $t: Type = null;
	constructor($t: Type) {
		super($t);
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
	}
	compare(x: T, y: T): number {
		let xComparable = typeCast<IComparable$1<T>>(IComparable$1_$type.specialize(this.$t), x);
		if (xComparable != null) {
			return CompareUtil.compareTo(xComparable, y);
		}
		let yComparable = typeCast<IComparable$1<T>>(IComparable$1_$type.specialize(this.$t), y);
		if (yComparable != null) {
			return -CompareUtil.compareTo(yComparable, x);
		}
		return <number>(Base.compare(x, y));
	}
}


