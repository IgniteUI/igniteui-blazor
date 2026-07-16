import { GradientStop } from "./GradientStop";
import { Base, toNullable, Type, markType, typeCast, Point, PointUtil } from "./type";
import { Color } from "./Color";
import { intDivide, isNaN_, isInfinity } from "./number";
import { stringContains } from "./string";

/**
 * @hidden 
 */
export class Brush extends Base {
	static $t: Type = markType(Brush, 'Brush');
	isGradient: boolean = false;
	isRadialGradient: boolean = false;
	isImageFill: boolean = false;
	_fill: string = null;
	get fill(): string {
		if (this._fill == null) {
			this._fill = this.getPrimaryColor();
		}
		return this._fill;
	}
	set fill(value: string) {
		this._fill = value;
	}
	private _cachedFill: string = null;
	private _cachedColor: Color = new Color();
	get color(): Color {
		if (this._fill == null && (this.isGradient || this.isRadialGradient)) {
			this._fill = this.getPrimaryColor();
		}
		if (this._fill == this._cachedFill) {
			return this._cachedColor;
		}
		let color = new Color();
		if (this._fill != null) {
			color.colorString = this._fill;
			this._cachedColor = color;
			this._cachedFill = this._fill;
			if (this._fill.length == 9) {
				this._fill = this._cachedColor.colorString;
				this._cachedFill = this._fill;
			}
		}
		return color;
	}
	set color(value: Color) {
		this._cachedColor = value;
		this._cachedFill = this._cachedColor.colorString;
		this._fill = this._cachedFill;
	}
	getPrimaryColor(): string {
		return null;
	}
	equals(obj: any): boolean {
		if (obj == null) {
			return false;
		}
		let other = <Brush>obj;
		return Base.equalsStatic(this._fill, other._fill) && this.color.equals(other.color) && this.isGradient == other.isGradient && this.isImageFill == other.isImageFill && this.isRadialGradient == other.isRadialGradient;
	}
	getHashCode(): number {
		let code: number = Base.getHashCodeStatic(this.isGradient) ^ Base.getHashCodeStatic(this.isRadialGradient) ^ Base.getHashCodeStatic(this.isImageFill);
		if (this._cachedFill != null) {
			code ^= Base.getHashCodeStatic(this._cachedFill);
		}
		if (Color.l_op_Inequality_Lifted(toNullable<Color>((<any>Color).$type, this._cachedColor), toNullable<Color>((<any>Color).$type, null))) {
			code ^= this._cachedColor.getHashCode();
		}
		return code;
	}
	static create(val_: any): Brush {
		CssGradientUtil.touch();
		let b_: any = new Brush();
		if (!val_) {
			return null;
			}
			
			if (typeof val_ == 'string') {
				if (CssGradientUtil.isGradient(val_)) {
                    b_ = CssGradientUtil.brushFromGradientString(val_);
                } else {
                    b_ = new Brush();
                    b_.fill = val_;
                }
			} else if (val_.type == 'linearGradient') {
				b_ = new LinearGradientBrush();
				if (val_.startPoint && val_.endPoint) {
					b_.useCustomDirection = true;
					b_.startX = val_.startPoint.x;
					b_.startY = val_.startPoint.y;
					b_.endX = val_.endPoint.x;
					b_.endY = val_.endPoint.y;
				}
				
				if (val_.colorStops) {
					let stops: GradientStop[] = [];
					for (var i = 0; i < val_.colorStops.length; i++) {
						let colorStop = new GradientStop();
						colorStop.offset = val_.colorStops[i].offset;
						colorStop.fill = val_.colorStops[i].color;
						stops.push(colorStop);
					}
					b_.gradientStops = stops;
				}};
		return <Brush>b_;
	}
}

/**
 * @hidden 
 */
export class LinearGradientBrush extends Brush {
	static $t: Type = markType(LinearGradientBrush, 'LinearGradientBrush', (<any>Brush).$type);
	static readonly startXDefaultValue: number = 0;
	static readonly startYDefaultValue: number = 0;
	static readonly endXDefaultValue: number = 1;
	static readonly endYDefaultValue: number = 1;
	constructor() {
		super();
		this.useCustomDirection = false;
		this.startX = LinearGradientBrush.startXDefaultValue;
		this.startY = LinearGradientBrush.startYDefaultValue;
		this.endX = LinearGradientBrush.endXDefaultValue;
		this.endY = LinearGradientBrush.endYDefaultValue;
		this.isAbsolute = false;
		this.gradientStops = <GradientStop[]>new Array(0);
		this.isGradient = true;
	}
	useCustomDirection: boolean = false;
	startX: number = 0;
	startY: number = 0;
	endX: number = 0;
	endY: number = 0;
	isAbsolute: boolean = false;
	gradientStops: GradientStop[] = null;
	clone(): LinearGradientBrush {
		let newBrush = new LinearGradientBrush();
		newBrush.startX = this.startX;
		newBrush.startY = this.startY;
		newBrush.endX = this.endX;
		newBrush.endY = this.endY;
		newBrush.useCustomDirection = this.useCustomDirection;
		newBrush.isAbsolute = this.isAbsolute;
		if (this.gradientStops != null) {
			newBrush.gradientStops = <GradientStop[]>new Array(this.gradientStops.length);
			for (let i = 0; i < this.gradientStops.length; i++) {
				newBrush.gradientStops[i] = this.gradientStops[i].clone();
			}
		}
		return newBrush;
	}
	equals(obj: any): boolean {
		if (obj == null) {
			return false;
		}
		let other: LinearGradientBrush = typeCast<LinearGradientBrush>((<any>LinearGradientBrush).$type, obj);
		if (other == null) {
			return false;
		}
		let retval: boolean = super.equals(obj) && this.startX == other.startX && this.startY == other.startY && this.endX == other.endX && this.endY == other.endY && this.isAbsolute == other.isAbsolute && this.useCustomDirection == other.useCustomDirection;
		if (retval == false) {
			return false;
		}
		if (this.gradientStops.length != other.gradientStops.length) {
			return false;
		}
		for (let i: number = 0, length: number = this.gradientStops.length; i < length; i++) {
			if (!this.gradientStops[i].equals(other.gradientStops[i])) {
				return false;
			}
		}
		return true;
	}
	getHashCode(): number {
		return super.getHashCode() ^ (this.startX) ^ (this.startY) ^ (this.endX) ^ (this.endY);
	}
	getPrimaryColor(): string {
		if (this.gradientStops != null && this.gradientStops.length > 0) {
			return this.gradientStops[0].color.colorString;
		}
		return super.getPrimaryColor();
	}
}

/**
 * @hidden 
 */
export class CssGradientUtil extends Base {
	static $t: Type = markType(CssGradientUtil, 'CssGradientUtil');
	static touch(): void {
	}
	static isGradient(value: string): boolean {
		return stringContains(value, "linear-gradient") || stringContains(value, "radial-gradient");
	}
	static brushFromGradientString(value: string): Brush {
		let regex: RegExp = <RegExp>(/hsl\([\s\S]+?\)[\s\S]*?[,\)]|rgba?\([\s\S]+?\)[\s\S]*?[,\)]|[^\(\)]*?[,\)]/gim), percentRegex: RegExp = <RegExp>(/\s*\d*%\s*$/), trimStartRegex: RegExp = <RegExp>(/^\s\s*/), trimEndRegex: RegExp = <RegExp>(/\s\s*$/), trimEndCharactersRegex: RegExp = <RegExp>(/[,\)]?$/);
		let match: string;
		let angle: number, i: number = 1, j: number = 0, length: number, offsetIndex: number;
		let hasUnsetOffsets: boolean = false;
		let stops: GradientStop[];
		let matches = value.match(regex);
		if (matches == null || matches.length <= 1) {
			return null;
		}
		let b: LinearGradientBrush = new LinearGradientBrush();
		length = matches.length;
		match = <string>matches[0];
		if (stringContains(match, "to") || stringContains(match, "deg")) {
			angle = CssGradientUtil.angleFromString(match);
			b.useCustomDirection = true;
			let points: Point[] = CssGradientUtil.calculatePointsFromAngle(angle);
			b.startX = points[0].x;
			b.startY = points[0].y;
			b.endX = points[1].x;
			b.endY = points[1].y;
			stops = <GradientStop[]>new Array(length - 1);
		} else {
			stops = <GradientStop[]>new Array(length);
			i = 0;
		}
		for (; i < length; i++) {
			let stop = new GradientStop();
			match = <string>matches[i];
			match = match.replace(trimStartRegex, "").replace(trimEndRegex, "").replace(trimEndCharactersRegex, "");
			offsetIndex = match.search(percentRegex);
			if (offsetIndex != -1) {
				stop._fill = match.substr(0, offsetIndex);
				stop.offset = parseFloat(match.substr(offsetIndex + 1)) / 100;
			} else {
				stop._fill = match;
				stop.offset = -1;
				hasUnsetOffsets = true;
			}
			stops[j] = stop;
			j++;
		}
		if (hasUnsetOffsets) {
			if (stops[0].offset == -1) {
				stops[0].offset = 0;
			}
			if (stops[stops.length - 1].offset == -1) {
				stops[stops.length - 1].offset = 1;
			}
			CssGradientUtil.fixUnsetOffsets(stops);
		}
		b.gradientStops = stops;
		return b;
	}
	private static fixUnsetOffsets(stops: GradientStop[]): void {
		let i: number, j: number, k: number, offsetRange: number, maxOffset: number = -1, lastSetOffsetIndex: number = -1, lastSetOffset: number = 0;
		let hasUnsetOffsets: boolean = false;
		for (i = lastSetOffsetIndex + 1; i < stops.length; i++) {
			let stop: GradientStop = stops[i];
			if (stop.offset != -1) {
				maxOffset = Math.max(maxOffset, stop.offset);
				stop.offset = maxOffset;
				if (hasUnsetOffsets) {
					k = 1;
					offsetRange = intDivide((maxOffset - lastSetOffset), (i - lastSetOffsetIndex));
					for (j = lastSetOffsetIndex + 1; j < i; j++) {
						stops[j].offset = lastSetOffset + offsetRange * k;
						k++;
					}
					hasUnsetOffsets = false;
				}
				lastSetOffsetIndex = i;
				lastSetOffset = maxOffset;
			} else {
				hasUnsetOffsets = true;
			}
		}
	}
	private static angleFromString(value: string): number {
		let toTopRegex: RegExp = <RegExp>(/to\s*top\s*/i), toRightTopRegex: RegExp = <RegExp>(/to\s*right\s*top\s*/i), toRightRegex: RegExp = <RegExp>(/to\s*right\s*/i), toRightBottomRegex: RegExp = <RegExp>(/to\s*right\s*bottom\s*/i), toBottomRegex: RegExp = <RegExp>(/to\s*bottom\s*/i), toLeftBottomRegex: RegExp = <RegExp>(/to\s*left\s*bottom\s*/i), toLeftRegex: RegExp = <RegExp>(/to\s*left\s*/i), toLeftTopRegex: RegExp = <RegExp>(/to\s*left\s*top\s*/i);
		if (stringContains(value, "deg")) {
			return parseFloat(value);
		}
		if (toTopRegex.test(value)) {
			return 0;
		}
		if (toRightTopRegex.test(value)) {
			return 45;
		}
		if (toRightRegex.test(value)) {
			return 90;
		}
		if (toRightBottomRegex.test(value)) {
			return 135;
		}
		if (toBottomRegex.test(value)) {
			return 180;
		}
		if (toLeftBottomRegex.test(value)) {
			return 225;
		}
		if (toLeftRegex.test(value)) {
			return 270;
		}
		return toLeftTopRegex.test(value) ? 315 : 180;
	}
	private static radians(degrees: number): number {
		return Math.PI * degrees / 180;
	}
	private static simplifyAngle(angle: number): number {
		if (isNaN_(angle) || isInfinity(angle)) {
			return angle;
		}
		while (angle > 360) {
			angle -= 360;
		}
		while (angle < 0) {
			angle += 360;
		}
		return angle;
	}
	private static calculatePointsFromAngle(inputAngle: number): Point[] {
		let points: Point[] = <Point[]>new Array(2);
		let p1: Point = PointUtil.create();
		let p2: Point = PointUtil.create();
		let angle: number = CssGradientUtil.simplifyAngle(inputAngle);
		if (angle >= 0 && angle <= 45) {
			let tan: number = Math.tan(CssGradientUtil.radians(angle));
			p1.x = 0.5 - 0.5 * tan;
			p1.y = 1;
			p2.x = 0.5 + 0.5 * tan;
			p2.y = 0;
		} else if (angle > 180 && angle <= 225) {
			let tan1: number = Math.tan(CssGradientUtil.radians(angle - 180));
			p1.x = 0.5 + 0.5 * tan1;
			p1.y = 0;
			p2.x = 0.5 - 0.5 * tan1;
			p2.y = 1;
		} else if (angle > 135 && angle <= 180) {
			let tan2: number = Math.tan(CssGradientUtil.radians(180 - angle));
			p1.x = 0.5 - 0.5 * tan2;
			p1.y = 0;
			p2.x = 0.5 + 0.5 * tan2;
			p2.y = 1;
		} else if (angle > 315 && angle < 360) {
			let tan3: number = Math.tan(CssGradientUtil.radians(360 - angle));
			p1.x = 0.5 + 0.5 * tan3;
			p1.y = 1;
			p2.x = 0.5 - 0.5 * tan3;
			p2.y = 0;
		} else if (angle > 45 && angle <= 90) {
			let tan4: number = Math.tan(CssGradientUtil.radians(90 - angle));
			p2.y = 0.5 - 0.5 * tan4;
			p2.x = 1;
			p1.y = 0.5 + 0.5 * tan4;
			p1.x = 0;
		} else if (angle > 90 && angle <= 135) {
			let tan5: number = Math.tan(CssGradientUtil.radians(angle - 90));
			p2.y = 0.5 + 0.5 * tan5;
			p2.x = 1;
			p1.y = 0.5 - 0.5 * tan5;
			p1.x = 0;
		} else if (angle > 225 && angle <= 270) {
			let tan6: number = Math.tan(CssGradientUtil.radians(270 - angle));
			p1.y = 0.5 - 0.5 * tan6;
			p1.x = 1;
			p2.y = 0.5 + 0.5 * tan6;
			p2.x = 0;
		} else if (angle > 270 && angle <= 315) {
			let tan7: number = Math.tan(CssGradientUtil.radians(angle - 270));
			p1.y = 0.5 + 0.5 * tan7;
			p1.x = 1;
			p2.y = 0.5 - 0.5 * tan7;
			p2.x = 0;
		}
		points[0] = p1;
		points[1] = p2;
		return points;
	}
}


