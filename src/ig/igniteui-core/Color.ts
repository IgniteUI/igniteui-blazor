import { ValueType, Base, typeCast, Nullable$1, markStruct, Type } from "./type";
import { ArgumentException } from "./ArgumentException";
import { truncate } from "./number";
import { strToColor } from "./colorCore";
import { stringStartsWith } from "./string";

/**
 * @hidden 
 */
export class Color extends ValueType {
	static $t: Type = markStruct(Color, 'Color');
	constructor() {
		super();
	}
	private _a: number = 0;
	get a(): number {
		return this._a;
	}
	set a(value: number) {
		this._a = <number>truncate(Math.round(<number>value));
		this._stringDirty = true;
	}
	private _r: number = 0;
	get r(): number {
		return this._r;
	}
	set r(value: number) {
		this._r = <number>truncate(Math.round(<number>value));
		this._stringDirty = true;
	}
	private _g: number = 0;
	get g(): number {
		return this._g;
	}
	set g(value: number) {
		this._g = <number>truncate(Math.round(<number>value));
		this._stringDirty = true;
	}
	private _b: number = 0;
	get b(): number {
		return this._b;
	}
	set b(value: number) {
		this._b = <number>truncate(Math.round(<number>value));
		this._stringDirty = true;
	}
	private _colorString: string = null;
	get colorString(): string {
		if (this._stringDirty || this._colorString == null) {
			this._stringDirty = false;
			this.updateColorString();
		}
		return this._colorString;
	}
	set colorString(value: string) {
		this._colorString = value;
		this.updateColors();
	}
	private _stringDirty: boolean = false;
	static create(value: any): Color {
		if (typeCast<Color>((<any>Color).$type, value) !== null) {
			return <Color>value;
		}
		let ret = new Color();
		if (typeof value === 'string') {
			ret.colorString = <string>value;
		} else if (value != null) {
			throw new ArgumentException(1, "Unknown color type");
		}
		return ret;
	}
	private updateColorString(): void {
		this._colorString = "rgba(" + this._r + "," + this._g + "," + this._b + "," + this._a / 255 + ")";
	}
	private updateColors(): void {
		if (this.colorString == null) {
			this.a = this.r = this.g = this.b = 0;
			return;
		}
		let obj_ = strToColor(this._colorString);
		this._a = typeof obj_.a != 'undefined' ? Math.round(obj_.a) : 0;
		this._r = typeof obj_.r != 'undefined' ? Math.round(obj_.r) : 0;
		this._g = typeof obj_.g != 'undefined' ? Math.round(obj_.g) : 0;
		this._b = typeof obj_.b != 'undefined' ? Math.round(obj_.b) : 0;
		if (stringStartsWith(this._colorString, "#") && this._colorString.length == 9) {
			this.updateColorString();
		}
	}
	static fromArgb(a_: number, r_: number, g_: number, b_: number): Color {
		let c: Color = new Color();
		c._a = <number>(a_ | 0);
		c._r = <number>(r_ | 0);
		c._g = <number>(g_ | 0);
		c._b = <number>(b_ | 0);
		c._stringDirty = true;
		return c;
	}
	equals(obj: any): boolean {
		if ((typeCast<Color>((<any>Color).$type, obj) !== null) == false) {
			return false;
		}
		let other = <Color>obj;
		return this._a == other._a && this._r == other._r && this._g == other._g && this._b == other._b;
	}
	getHashCode(): number {
		return (this._a << 24) | (this._r << 16) | (this._g << 8) | this._b;
	}
	static l_op_Inequality(left: Color, right: Color): boolean {
		return !(Color.l_op_Equality(left, right));
	}
	static l_op_Inequality_Lifted(left: Nullable$1<Color>, right: Nullable$1<Color>): boolean {
		if (!left.hasValue) {
			return right.hasValue;
		} else if (!right.hasValue) {
			return true;
		}
		return Color.l_op_Inequality(left.value, right.value);
	}
	static l_op_Equality(left: Color, right: Color): boolean {
		return left._a == right._a && left._r == right._r && left._g == right._g && left._b == right._b;
	}
	static l_op_Equality_Lifted(left: Nullable$1<Color>, right: Nullable$1<Color>): boolean {
		if (!left.hasValue) {
			return !right.hasValue;
		} else if (!right.hasValue) {
			return false;
		}
		return Color.l_op_Equality(left.value, right.value);
	}
}


