import { Description } from "./Description";
import { Base, typeCast, Type, markType } from "./type";

/**
 * @hidden 
 */
export class RectDescription extends Description {
	static $t: Type = markType(RectDescription, 'RectDescription', (<any>Description).$type);
	private _left: number = 0;
	get left(): number {
		return this._left;
	}
	set left(value: number) {
		this._left = value;
	}
	private _top: number = 0;
	get top(): number {
		return this._top;
	}
	set top(value: number) {
		this._top = value;
	}
	private _width: number = 0;
	get width(): number {
		return this._width;
	}
	set width(value: number) {
		this._width = value;
	}
	private _height: number = 0;
	get height(): number {
		return this._height;
	}
	set height(value: number) {
		this._height = value;
	}
	equals(other: any): boolean {
		if (other == null || !(typeCast<RectDescription>((<any>RectDescription).$type, other) !== null)) {
			return super.equals(other);
		}
		let op = <RectDescription>other;
		return this.left == op.left && this.top == op.top && this.width == op.width && this.height == op.height;
	}
}


