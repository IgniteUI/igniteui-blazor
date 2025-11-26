import { Description } from "./Description";
import { Base, typeCast, Type, markType } from "./type";

/**
 * @hidden 
 */
export class PointDescription extends Description {
	static $t: Type = markType(PointDescription, 'PointDescription', (<any>Description).$type);
	private _x: number = 0;
	get x(): number {
		return this._x;
	}
	set x(value: number) {
		this._x = value;
	}
	private _y: number = 0;
	get y(): number {
		return this._y;
	}
	set y(value: number) {
		this._y = value;
	}
	equals(other: any): boolean {
		if (other == null || !(typeCast<PointDescription>((<any>PointDescription).$type, other) !== null)) {
			return super.equals(other);
		}
		let op = <PointDescription>other;
		return this.x == op.x && this.y == op.y;
	}
}


