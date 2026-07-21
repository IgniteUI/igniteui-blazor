import { Base, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, Point, fromEnum, Type, markType } from "./type";
import { Brush } from "./Brush";
import { CSSColorUtil } from "./CSSColorUtil";
import { Color } from "./Color";
import { BrushCollection } from "./BrushCollection";
import { List$1 } from "./List$1";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { Size } from "./Size";
import { Rect } from "./Rect";
import { ObservableColorCollection } from "./ObservableColorCollection";
import { DoubleCollection } from "./DoubleCollection";
import { CultureInfo } from "./culture";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";
import { timeSpanTotalMilliseconds } from "./timespan";
import { numberToString } from "./numberExtended";

/**
 * @hidden 
 */
export class ComponentRendererSerializationHelper extends Base {
	static $t: Type = markType(ComponentRendererSerializationHelper, 'ComponentRendererSerializationHelper');
	static serializeBrush(value: any): any {
		if (value == null) {
			return null;
		}
		let b = <Brush>value;
		return CSSColorUtil.brushToString(b);
	}
	static serializeColor(value: any): any {
		if (value == null) {
			return null;
		}
		return CSSColorUtil.colorToString(<Color>value);
	}
	static serializeBrushCollection(value: any): any {
		if (value == null) {
			return null;
		}
		let str: string = "";
		let bc = <BrushCollection>value;
		let first: boolean = true;
		for (let b of fromEnum<Brush>(bc)) {
			if (first) {
				first = false;
			} else {
				str += ", ";
			}
			str += ComponentRendererSerializationHelper.serializeBrush(b);
		}
		return str;
	}
	static serializePoint(value: any): any {
		let p: Point = <Point>value;
		let pt: JsonDictionaryObject = new JsonDictionaryObject();
		pt.item("x", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.x;
			return $ret;
		})()));
		pt.item("y", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.y;
			return $ret;
		})()));
		return pt;
	}
	static serializeSize(value: any): any {
		let p: Size = <Size>value;
		let pt: JsonDictionaryObject = new JsonDictionaryObject();
		pt.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.width;
			return $ret;
		})()));
		pt.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.height;
			return $ret;
		})()));
		return pt;
	}
	static serializeRect(value: any): any {
		let p: Rect = <Rect>value;
		let pt: JsonDictionaryObject = new JsonDictionaryObject();
		pt.item("left", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.left;
			return $ret;
		})()));
		pt.item("top", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.top;
			return $ret;
		})()));
		pt.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.width;
			return $ret;
		})()));
		pt.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = p.height;
			return $ret;
		})()));
		return pt;
	}
	static serializeColorCollection(value: any): any {
		if (value == null) {
			return null;
		}
		let str: string = "";
		let bc = <ObservableColorCollection>value;
		let first: boolean = true;
		for (let b of fromEnum<Color>(bc)) {
			if (first) {
				first = false;
			} else {
				str += ", ";
			}
			str += CSSColorUtil.colorToString(b);
		}
		return str;
	}
	static serializeTimespan(value: any): any {
		if (value == null) {
			return null;
		}
		return timeSpanTotalMilliseconds((<number>value));
	}
	static serializeDoubleCollection(value: any): any {
		if (value == null) {
			return null;
		}
		let str: string = "";
		let bc = <DoubleCollection>value;
		let first: boolean = true;
		for (let b of fromEnum<number>(bc)) {
			if (first) {
				first = false;
			} else {
				str += ", ";
			}
			str += numberToString(b, CultureInfo.invariantCulture);
		}
		return str;
	}
}


