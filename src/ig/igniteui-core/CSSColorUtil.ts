import { Base, Point, Point_$type, typeCast, Type, markType, PointUtil } from "./type";
import { Brush } from "./Brush";
import { MathUtil } from "./MathUtil";
import { LinearGradientBrush } from "./LinearGradientBrush";
import { StringBuilder } from "./StringBuilder";
import { GradientStop } from "./GradientStop";
import { CultureInfo } from "./culture";
import { Color } from "./Color";
import { isNaN_, isInfinity } from "./number";
import { numberToString } from "./numberExtended";

/**
 * @hidden 
 */
export class CSSColorUtil extends Base {
	static $t: Type = markType(CSSColorUtil, 'CSSColorUtil');
	static stringToBrush(str: string): Brush {
		return Brush.create(str);
	}
	private static calculateAngleFromPoints(points: Point[]): number {
		let point1 = points[0];
		let point2 = points[1];
		if (isNaN_(point1.x) || isNaN_(point1.y)) {
			point1 = <Point>{ $type: Point_$type, x: 0.5, y: 1 };
		}
		if (isNaN_(point2.x) || isNaN_(point2.y)) {
			point2 = <Point>{ $type: Point_$type, x: 0.5, y: 0 };
		}
		let angle = Math.atan2(point2.y - point1.y, point2.x - point1.x);
		angle -= Math.PI;
		angle = MathUtil.degrees(angle);
		angle = CSSColorUtil.simplifyAngle(angle);
		return angle;
	}
	static brushToString(value: Brush): string {
		if (value == null) {
			return null;
		}
		if (typeCast<LinearGradientBrush>((<any>LinearGradientBrush).$type, value) !== null) {
			return CSSColorUtil.linearGradientBrushToString(<LinearGradientBrush>value);
		} else {
			return CSSColorUtil.solidColorBrushToString(<Brush>value);
		}
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
	private static linearGradientBrushToString(value: LinearGradientBrush): string {
		let ang = CSSColorUtil.calculateAngleFromPoints(<Point[]>[ <Point>{ $type: Point_$type, x: value.startX, y: value.startY }, <Point>{ $type: Point_$type, x: value.endX, y: value.endY } ]);
		let sb: StringBuilder = new StringBuilder(0);
		sb.append5("linear-gradient(");
		sb.append5(numberToString(ang, CultureInfo.invariantCulture));
		sb.append5("deg");
		if (value.gradientStops != null && value.gradientStops.length > 0) {
			let $t = value.gradientStops;
			for (let i = 0; i < $t.length; i++) {
				let stop = $t[i];
				sb.append5(", ");
				sb.append5(CSSColorUtil.colorToString(stop.color));
				if (!isNaN_(stop.offset)) {
					sb.append5(" ");
					sb.append5(numberToString((stop.offset * 100), CultureInfo.invariantCulture) + "%");
				}
			}
		}
		sb.append5(")");
		return sb.toString();
	}
	private static solidColorBrushToString(value: Brush): string {
		return CSSColorUtil.colorToString(value.color);
	}
	static colorToString(color: Color): string {
		return "rgba(" + color.r.toString() + ", " + color.g.toString() + ", " + color.b.toString() + ", " + (color.a / 255).toString() + ")";
	}
}


