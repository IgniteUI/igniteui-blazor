import { Base, IEqualityComparer, IEqualityComparer_$type, IEqualityComparer$1, IEqualityComparer$1_$type, Type, typeCastObjTo$t, markType, getBoxIfEnum } from "./type";

/**
 * @hidden 
 */
export abstract class EqualityComparer$1<T> extends Base implements IEqualityComparer, IEqualityComparer$1<T> {
	static $t: Type = markType(EqualityComparer$1, 'EqualityComparer$1', (<any>Base).$type, [IEqualityComparer_$type, IEqualityComparer$1_$type.specialize(0)]);
	protected $t: Type = null;
	constructor($t: Type) {
		super();
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
	}
	static defaultEqualityComparerValue<T>($t: Type): EqualityComparer$1<T> {
		return new DefaultEqualityComparer$1<T>($t);
	}
	equalsC(x: any, y: any): boolean {
		return this.equalsC(typeCastObjTo$t<T>(this.$t, x), typeCastObjTo$t<T>(this.$t, y));
	}
	getHashCodeC(obj: any): number {
		return this.getHashCodeC(typeCastObjTo$t<T>(this.$t, obj));
	}
}

/**
 * @hidden 
 */
export class DefaultEqualityComparer$1<T> extends EqualityComparer$1<T> {
	static $t: Type = markType(DefaultEqualityComparer$1, 'DefaultEqualityComparer$1', (<any>EqualityComparer$1).$type.specialize(0));
	protected $t: Type = null;
	constructor($t: Type) {
		super($t);
		this.$t = $t;
		this.$type = this.$type.specialize(this.$t);
	}
	equalsC(x: T, y: T): boolean {
		return Base.equalsStatic(getBoxIfEnum<T>(this.$t, x), getBoxIfEnum<T>(this.$t, y));
	}
	getHashCodeC(obj: T): number {
		return Base.getHashCodeStatic(obj);
	}
}


