import { Base, Point, Type, toNullable, Point_$type, Number_$type, markType, PointUtil, getInstanceType } from "./type";
import { Brush } from "./Brush";
import { BrushCollection } from "./BrushCollection";
import { BrushCollectionUtil } from "./BrushCollectionUtil";
import { Color } from "./Color";
import { Size } from "./Size";
import { Rect } from "./Rect";
import { DoubleCollection } from "./DoubleCollection";
import { List$1 } from "./List$1";

/**
 * @hidden 
 */
export class PlatformAPIHelper extends Base {
	static $t: Type = markType(PlatformAPIHelper, 'PlatformAPIHelper');
	static externalizeBrushValue(brush: Brush): any {
		if (brush == null) {
			return null;
		}
		return brush._fill;
	}
	static externalizeBrushCollectionValue(brush: BrushCollection): any {
		if (brush == null) {
			return null;
		}
		return BrushCollectionUtil.fromBrushCollection(brush);
	}
	static externalizeColorValue(color: Color): any {
		if (Color.l_op_Equality_Lifted(toNullable<Color>((<any>Color).$type, color), toNullable<Color>((<any>Color).$type, null))) {
			return null;
		}
		return color.colorString;
	}
	static externalizePointValue(point: Point): any {
		if (PointUtil.equals(point, null)) {
			return null;
		}
		let ret = {};
		let d = <any>ret;
		d["x"] = point.x;
		d["y"] = point.y;
		return d;
	}
	static externalizeSizeValue(size: Size): any {
		if (Size.l_op_Equality_Lifted(toNullable<Size>((<any>Size).$type, size), toNullable<Size>((<any>Size).$type, null))) {
			return null;
		}
		let ret = {};
		let d = <any>ret;
		d["width"] = size.width;
		d["height"] = size.height;
		return d;
	}
	static externalizeRectValue(rect: Rect): any {
		if (Rect.l_op_Equality(rect, null)) {
			return null;
		}
		let ret = {};
		let d = <any>ret;
		d["left"] = rect.left;
		d["top"] = rect.top;
		d["width"] = rect.width;
		d["height"] = rect.height;
		return d;
	}
	static externalizeDoubleCollectionValue(value: DoubleCollection): any {
		if (value == null) {
			return null;
		}
		return value.toArray();
	}
	static internalizeBrushValue(value: any): any {
		if (value == null) {
			return null;
		}
		if (getInstanceType(value) == (<any>Brush).$type) {
			return value;
		}
		return Brush.create(value);
	}
	static internalizeColorValue(value: any): any {
		if (value == null) {
			return null;
		}
		return Brush.create(value).color;
	}
	static internalizeBrushCollectionValue(value: any): any {
		if (value == null) {
			return null;
		}
		return BrushCollectionUtil.toBrushCollection(value);
	}
	static internalizePointValue(value: any): any {
		if (value == null) {
			return null;
		}
		if (getInstanceType(value) == Point_$type) {
			return value;
		}
		let d = <any>value;
		return <Point>{ $type: Point_$type, x: <number>d["x"], y: <number>d["y"] };
	}
	static internalizeSizeValue(value: any): any {
		if (value == null) {
			return null;
		}
		if (getInstanceType(value) == (<any>Size).$type) {
			return value;
		}
		let d = <any>value;
		return new Size(1, <number>d["width"], <number>d["height"]);
	}
	static internalizeRectValue(value: any): any {
		if (value == null) {
			return null;
		}
		if (getInstanceType(value) == (<any>Rect).$type) {
			return value;
		}
		let d = <any>value;
		return new Rect(0, <number>d["left"], <number>d["top"], <number>d["width"], <number>d["height"]);
	}
	static internalizeDoubleCollectionValue(value: any): any {
		if (value == null) {
			return null;
		}
		if (getInstanceType(value) == (<any>DoubleCollection).$type) {
			return value;
		}
		let dArr = <number[]>value;
		let ret = new DoubleCollection();
		for (let i = 0; i < dArr.length; i++) {
			ret.add(dArr[i]);
		}
		return ret;
	}
	static internalizeDataSource(value_: any): any {
		return value_;
	}
	static externalizePropertyValue(t: Type, value: any): any {
		if (t == (<any>Brush).$type) {
			return PlatformAPIHelper.externalizeBrushValue(<Brush>value);
		}
		if (t == (<any>Color).$type) {
			return PlatformAPIHelper.externalizeColorValue(<Color>value);
		}
		if (t == Point_$type) {
			return PlatformAPIHelper.externalizePointValue(<Point>value);
		}
		if (t == (<any>Size).$type) {
			return PlatformAPIHelper.externalizeSizeValue(<Size>value);
		}
		if (t == (<any>Rect).$type) {
			return PlatformAPIHelper.externalizeRectValue(<Rect>value);
		}
		if (t == (<any>DoubleCollection).$type) {
			return PlatformAPIHelper.externalizeDoubleCollectionValue(<DoubleCollection>value);
		}
		return value;
	}
	static internalizePropertyValue(t: Type, value: any): any {
		if (t == (<any>Brush).$type) {
			return PlatformAPIHelper.internalizeBrushValue(value);
		}
		if (t == (<any>Color).$type) {
			let b = <Brush>PlatformAPIHelper.internalizeBrushValue(value);
			if (b == null) {
				return null;
			}
			return b._fill;
		}
		if (t == Point_$type) {
			return PlatformAPIHelper.internalizePointValue(value);
		}
		if (t == (<any>Size).$type) {
			return PlatformAPIHelper.internalizeSizeValue(value);
		}
		if (t == (<any>Rect).$type) {
			return PlatformAPIHelper.internalizeRectValue(value);
		}
		if (t == (<any>DoubleCollection).$type) {
			return PlatformAPIHelper.internalizeDoubleCollectionValue(value);
		}
		if (typeof value === 'string') {
			if (t == Number_$type) {
				return parseFloat(value.toString());
			}
			if (t == Number_$type) {
				return parseInt(value.toString());
			}
		}
		return value;
	}
	static camelify(value: string): string {
		if (value == null) {
			return null;
		}
		if (value.length == 1) {
			return value.toLowerCase();
		}
		return value.substr(0, 1).toLowerCase() + value.substr(1);
	}
	static pascalify(value: string): string {
		if (value == null) {
			return null;
		}
		if (value.length == 1) {
			return value.toUpperCase();
		}
		return value.substr(0, 1).toUpperCase() + value.substr(1);
	}
}


