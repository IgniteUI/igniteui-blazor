import { Base, toNullable, Type, markType } from "./type";
import { Color } from "./Color";

/**
 * @hidden 
 */
export class GradientStop extends Base {
	static $t: Type = markType(GradientStop, 'GradientStop');
	constructor() {
		super();
		this.offset = 0;
	}
	offset: number = 0;
	clone(): GradientStop {
		let newStop = new GradientStop();
		newStop.offset = this.offset;
		newStop._fill = this._fill;
		return newStop;
	}
	_fill: string = null;
	get fill(): string {
		return this._fill;
	}
	set fill(value: string) {
		this._fill = value;
	}
	private _cachedFill: string = null;
	private _cachedColor: Color = new Color();
	get color(): Color {
		if (this._fill == this._cachedFill) {
			return this._cachedColor;
		}
		let color = new Color();
		if (this._fill != null) {
			color.colorString = this._fill;
			this._cachedColor = color;
			this._cachedFill = this._fill;
		}
		return color;
	}
	set color(value: Color) {
		this._cachedColor = value;
		this._cachedFill = this._cachedColor.colorString;
		this._fill = this._cachedFill;
	}
	equals(obj: any): boolean {
		if (obj == null) {
			return false;
		}
		let other = <GradientStop>obj;
		return this.offset == other.offset && this.color.equals(other.color) && Base.equalsStatic(this._fill, other._fill);
	}
	getHashCode(): number {
		let code: number = (this.offset);
		if (Color.l_op_Inequality_Lifted(toNullable<Color>((<any>Color).$type, this._cachedColor), toNullable<Color>((<any>Color).$type, null))) {
			code ^= this._cachedColor.getHashCode();
		}
		return code;
	}
}


