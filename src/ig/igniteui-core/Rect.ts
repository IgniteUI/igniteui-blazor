import { Base, Point, Type, markType } from "./type";
import { Size } from "./Size";

/**
 * @hidden 
 */
export class Rect extends Base {
	static $t: Type = markType(Rect, 'Rect');
	constructor(initNumber: number, left: number, top: number, width: number, height: number);
	constructor(initNumber: number, left: number, top: number, size: Size);
	constructor(initNumber: number, point1: Point, point2: Point);
	constructor(initNumber: number, point1: Point, size: Size);
	constructor(initNumber: number);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		super();
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				let left: number = <number>_rest[0];
				let top: number = <number>_rest[1];
				let width: number = <number>_rest[2];
				let height: number = <number>_rest[3];
				this.top = top;
				this.left = left;
				this.width = width;
				this.height = height;
			}
			break;

			case 1:
			{
				let left: number = <number>_rest[0];
				let top: number = <number>_rest[1];
				let size: Size = <Size>_rest[2];
				this.top = top;
				this.left = left;
				this.width = size.width;
				this.height = size.height;
			}
			break;

			case 2:
			{
				let point1: Point = <Point>_rest[0];
				let point2: Point = <Point>_rest[1];
				this.top = Math.min(point1.y, point2.y);
				this.left = Math.min(point1.x, point2.x);
				this.width = Math.max(Math.max(point1.x, point2.x) - this.left, 0);
				this.height = Math.max(Math.max(point1.y, point2.y) - this.top, 0);
			}
			break;

			case 3:
			{
				let point1: Point = <Point>_rest[0];
				let size: Size = <Size>_rest[1];
				this.top = point1.y;
				this.left = point1.x;
				this.width = size.width;
				this.height = size.height;
			}
			break;

			case 4:
			{
				this.top = 0;
				this.left = 0;
				this.width = 0;
				this.height = 0;
			}
			break;

		}

	}
	private _x: number = 0;
	get x(): number {
		return this._x;
	}
	set x(value: number) {
		this._x = value;
		this._left = this._x;
		this._right = this._left + this._width;
	}
	private _y: number = 0;
	get y(): number {
		return this._y;
	}
	set y(value: number) {
		this._y = value;
		this._top = this._y;
		this._bottom = this._top + this._height;
	}
	private _width: number = 0;
	get width(): number {
		return this._width;
	}
	set width(value: number) {
		this._width = value;
		this._right = this._left + this._width;
	}
	private _height: number = 0;
	get height(): number {
		return this._height;
	}
	set height(value: number) {
		this._height = value;
		this._bottom = this._top + this._height;
	}
	private _top: number = 0;
	get top(): number {
		return this._top;
	}
	set top(value: number) {
		this._top = value;
		this.y = this._top;
	}
	private _left: number = 0;
	get left(): number {
		return this._left;
	}
	set left(value: number) {
		this._left = value;
		this.x = this._left;
	}
	private _right: number = 0;
	get right(): number {
		return this._right;
	}
	set right(value: number) {
		this._right = value;
		this._width = this._right - this._left;
	}
	private _bottom: number = 0;
	get bottom(): number {
		return this._bottom;
	}
	set bottom(value: number) {
		this._bottom = value;
		this._height = this._bottom - this._top;
	}
	get isEmpty(): boolean {
		return this._width < 0;
	}
	static get empty(): Rect {
		return new Rect(0, Number.POSITIVE_INFINITY, Number.POSITIVE_INFINITY, Number.NEGATIVE_INFINITY, Number.NEGATIVE_INFINITY);
	}
	equals1(value: Rect): boolean {
		if (Rect.l_op_Equality(value, null)) {
			return false;
		}
		if (value.x == this.x && value.y == this.y && value.width == this.width && value.height == this.height) {
			return true;
		}
		return false;
	}
	private containsInternal(x: number, y: number): boolean {
		return x >= this._x && x - this._width <= this._x && y >= this._y && y - this._height <= this._y;
	}
	containsLocation(x: number, y: number): boolean {
		return !this.isEmpty && this.containsInternal(x, y);
	}
	containsPoint(point: Point): boolean {
		return this.containsLocation(point.x, point.y);
	}
	containsRect(rect: Rect): boolean {
		return !this.isEmpty && !rect.isEmpty && (this._x <= rect._x && this._y <= rect._y && this._x + this._width >= rect._x + rect._width) && this._y + this._height >= rect._y + rect._height;
	}
	inflate(width: number, height: number): void {
		this.x -= width;
		this.y -= height;
		this.width += width * 2;
		this.height += height * 2;
		if (this._width < 0 || this._height < 0) {
			this.makeEmpty();
		}
	}
	private makeEmpty(): void {
		this.top = Number.POSITIVE_INFINITY;
		this.left = Number.POSITIVE_INFINITY;
		this.width = Number.NEGATIVE_INFINITY;
		this.height = Number.NEGATIVE_INFINITY;
	}
	intersectsWith(rect: Rect): boolean {
		if (this.isEmpty || rect.isEmpty) {
			return false;
		}
		return rect.left < this.right && this.left < rect.right && rect.top < this.bottom && this.top < rect.bottom;
	}
	intersect(other: Rect): void {
		if (!this.intersectsWith(other)) {
			this.makeEmpty();
		} else {
			let maxX: number = Math.max(this.x, other.x);
			let maxY: number = Math.max(this.y, other.y);
			let newWidth: number = Math.min(this.x + this.width, other.x + other.width) - maxX;
			let newHeight: number = Math.min(this.y + this.height, other.y + other.height) - maxY;
			if (newWidth < 0) {
				newWidth = 0;
			}
			if (newHeight < 0) {
				newHeight = 0;
			}
			this._width = newWidth;
			this._height = newHeight;
			this._x = maxX;
			this._y = maxY;
			this._left = this._x;
			this._top = this._y;
			this._right = this._left + this._width;
			this._bottom = this._top + this._height;
		}
	}
	union(other: Rect): void {
		if (this.isEmpty) {
			this._x = other.x;
			this._y = other.y;
			this._width = other.width;
			this._height = other.height;
			this._left = this._x;
			this._top = this._y;
			this._right = this._left + this._width;
			this._bottom = this._top + this._height;
			return;
		}
		if (!other.isEmpty) {
			let minX: number = Math.min(this.x, other.x);
			let minY: number = Math.min(this.y, other.y);
			let newWidth: number = this.width;
			let newHeight: number = this.height;
			if (other.width == Number.POSITIVE_INFINITY || this.width == Number.POSITIVE_INFINITY) {
				newWidth = Number.POSITIVE_INFINITY;
			} else {
				let maxRight: number = Math.max(this.right, other.right);
				newWidth = maxRight - minX;
			}
			if (other.height == Number.POSITIVE_INFINITY || this.height == Number.POSITIVE_INFINITY) {
				newHeight = Number.POSITIVE_INFINITY;
			} else {
				let maxBottom: number = Math.max(this.bottom, other.bottom);
				newHeight = maxBottom - minY;
			}
			this._x = minX;
			this._y = minY;
			this._width = newWidth;
			this._height = newHeight;
			this._left = this._x;
			this._top = this._y;
			this._right = this._left + this._width;
			this._bottom = this._top + this._height;
		}
	}
	equals(obj: any): boolean {
		if (obj == null) {
			return super.equals(obj);
		}
		let other = <Rect>obj;
		return other.left == this.left && other.top == this.top && other.width == this.width && other.height == this.height;
	}
	getHashCode(): number {
		return (this._x) ^ (this._y) ^ (this._width) ^ (this._height);
	}
	copy(): Rect {
		return new Rect(0, this.x, this.y, this.width, this.height);
	}
	static l_op_Equality(a: Rect, b: Rect): boolean {
		if (<any>a == null) {
			return <any>b == null;
		} else if (<any>b == null) {
			return false;
		}
		return a._x == b._x && a._y == b._y && a._width == b._width && a._height == b._height;
	}
	static l_op_Inequality(a: Rect, b: Rect): boolean {
		if (<any>a == null) {
			return <any>b != null;
		} else if (<any>b == null) {
			return true;
		}
		return a._x != b._x || a._y != b._y || a._width != b._width || a._height != b._height;
	}
}


