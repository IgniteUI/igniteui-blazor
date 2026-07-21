import { ValueType, Base, Nullable$1, markStruct, Type } from "./type";

/**
 * @hidden 
 */
export class Size extends ValueType {
	static $t: Type = markStruct(Size, 'Size');
	constructor(initNumber: number, width: number, height: number);
	constructor();
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber?: number, ..._rest: any[]) {
		super();
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0: break;
			case 1:
			{
				let width: number = <number>_rest[0];
				let height: number = <number>_rest[1];
				this._width = width;
				this._height = height;
			}
			break;

		}

	}
	equals(obj: any): boolean {
		if (obj == null) {
			return super.equals(obj);
		}
		let other = <Size>obj;
		return other._width == this._width && other._height == this._height;
	}
	getHashCode(): number {
		return (this._width) ^ (this._height);
	}
	private _width: number = 0;
	private _height: number = 0;
	get width(): number {
		return this._width;
	}
	set width(value: number) {
		this._width = value;
	}
	get height(): number {
		return this._height;
	}
	set height(value: number) {
		this._height = value;
	}
	get isEmpty(): boolean {
		return this._width < 0;
	}
	static get empty(): Size {
		let s = new Size(0);
		s._width = Number.NEGATIVE_INFINITY;
		s._height = Number.NEGATIVE_INFINITY;
		return s;
	}
	static l_op_Inequality(left: Size, right: Size): boolean {
		return !(Size.l_op_Equality(left, right));
	}
	static l_op_Inequality_Lifted(left: Nullable$1<Size>, right: Nullable$1<Size>): boolean {
		if (!left.hasValue) {
			return right.hasValue;
		} else if (!right.hasValue) {
			return true;
		}
		return Size.l_op_Inequality(left.value, right.value);
	}
	static l_op_Equality(left: Size, right: Size): boolean {
		return left._width == right._width && left._height == right._height;
	}
	static l_op_Equality_Lifted(left: Nullable$1<Size>, right: Nullable$1<Size>): boolean {
		if (!left.hasValue) {
			return !right.hasValue;
		} else if (!right.hasValue) {
			return false;
		}
		return Size.l_op_Equality(left.value, right.value);
	}
}


