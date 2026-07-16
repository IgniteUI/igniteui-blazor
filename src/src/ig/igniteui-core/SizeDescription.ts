import { Description } from "./Description";
import { Base, typeCast, Type, markType } from "./type";

/**
 * @hidden 
 */
export class SizeDescription extends Description {
	static $t: Type = markType(SizeDescription, 'SizeDescription', (<any>Description).$type);
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
		if (other == null || !(typeCast<SizeDescription>((<any>SizeDescription).$type, other) !== null)) {
			return super.equals(other);
		}
		let op = <SizeDescription>other;
		return this.width == op.width && this.height == op.height;
	}
}


