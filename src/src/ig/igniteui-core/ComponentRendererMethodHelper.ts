import { Base, Type, Enum, typeCast, markType, EnumUtil } from "./type";
import { ComponentRendererMethodHelperBuilder } from "./ComponentRendererMethodHelperBuilder";
import { JsonDictionaryParser } from "./JsonDictionaryParser";
import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { JsonDictionaryArray } from "./JsonDictionaryArray";
import { EmbeddedRefDescription } from "./EmbeddedRefDescription";
import { PointDescription } from "./PointDescription";
import { SizeDescription } from "./SizeDescription";
import { RectDescription } from "./RectDescription";
import { truncate } from "./number";
import { dateTryParse } from "./dateExtended";
import { dateMinValue } from "./date";

/**
 * @hidden 
 */
export class ComponentRendererMethodHelper extends Base {
	static $t: Type = markType(ComponentRendererMethodHelper, 'ComponentRendererMethodHelper');
	static call(methodName: string, targetRef: string = null): ComponentRendererMethodHelperBuilder {
		return new ComponentRendererMethodHelperBuilder(methodName, targetRef);
	}
	static returnAsInt(returnValue: string): number {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return <number>truncate(<number>val);
		}
		return -2147483648;
	}
	private static extractValue(returnValue: string): any {
		let parser: JsonDictionaryParser = new JsonDictionaryParser();
		let obj = parser.parse(returnValue);
		if (obj != null && typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, obj) !== null) {
			let jObj = <JsonDictionaryObject>obj;
			if (jObj.containsKey("result")) {
				if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, jObj.item("result")) !== null) {
					return <JsonDictionaryObject>jObj.item("result");
				}
				return (<JsonDictionaryValue>jObj.item("result")).value;
			}
		}
		return null;
	}
	static returnAsDouble(returnValue: string): number {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return ComponentRendererMethodHelper.fromDoubleValue(val);
		}
		return NaN;
	}
	private static fromDoubleValue(val: any): number {
		if (val == null) {
			return NaN;
		}
		if (typeof val === 'string') {
			let strVal = <string>val;
			if (strVal == "@dbl:INFINITY") {
				return Number.POSITIVE_INFINITY;
			}
			if (strVal == "@dbl:-INFINITY") {
				return Number.NEGATIVE_INFINITY;
			}
		}
		return <number>val;
	}
	static returnAsDoubleArray(returnValue: string): number[] {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let arr = <JsonDictionaryArray>val;
			if (arr != null && arr.items != null) {
				let ret: number[] = <number[]>new Array(arr.items.length);
				for (let i = 0; i < arr.items.length; i++) {
					ret[i] = <number>(<JsonDictionaryValue>arr.items[i]).value;
				}
				return ret;
			}
		}
		return null;
	}
	static returnAsIntArray(returnValue: string): number[] {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let arr = <JsonDictionaryArray>val;
			if (arr != null && arr.items != null) {
				let ret: number[] = <number[]>new Array(arr.items.length);
				for (let i = 0; i < arr.items.length; i++) {
					ret[i] = <number>truncate(<number>(<JsonDictionaryValue>arr.items[i]).value);
				}
				return ret;
			}
		}
		return null;
	}
	static returnAsStringArray(returnValue: string): string[] {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let arr = <JsonDictionaryArray>val;
			if (arr != null && arr.items != null) {
				let ret: string[] = <string[]>new Array(arr.items.length);
				for (let i = 0; i < arr.items.length; i++) {
					ret[i] = <string>(<JsonDictionaryValue>arr.items[i]).value;
				}
				return ret;
			}
		}
		return null;
	}
	static returnAsPrimitiveArray(returnValue: string): any[] {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let arr = <JsonDictionaryArray>val;
			if (arr != null && arr.items != null) {
				let ret: any[] = <any[]>new Array(arr.items.length);
				for (let i = 0; i < arr.items.length; i++) {
					ret[i] = ComponentRendererMethodHelper.coercePrimitive(arr.items[i]);
				}
				return ret;
			}
		}
		return null;
	}
	static returnAsShort(returnValue: string): number {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return <number>truncate(<number>val);
		}
		return -32768;
	}
	static returnAsLong(returnValue: string): number {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return <number>truncate(<number>val);
		}
		return -9.2233720368547758E+18;
	}
	static returnAsFloat(returnValue: string): number {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			if (typeof val === 'string') {
				let strVal = <string>val;
				if (strVal == "@flt:INFINITY") {
					return Number.POSITIVE_INFINITY;
				}
				if (strVal == "@flt:-INFINITY") {
					return Number.NEGATIVE_INFINITY;
				}
			}
			return <number><number>val;
		}
		return NaN;
	}
	static returnAsEnum(enumType: Type, returnValue: string): any {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return EnumUtil.parse(enumType, <string>val, true);
		}
		return null;
	}
	static returnAsString(returnValue: string): string {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return <string>val;
		}
		return null;
	}
	static returnAsPublicTypeRef(typeName: string, returnValue: string): EmbeddedRefDescription {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let jObj = <JsonDictionaryObject>val;
			let refr: EmbeddedRefDescription = ((() => {
				let $ret = new EmbeddedRefDescription();
				$ret.refType = "name";
				$ret.value = (<JsonDictionaryValue>jObj.item("id")).value.toString();
				return $ret;
			})());
			return refr;
		}
		return null;
	}
	static returnAsPublicTypeRefArray(typeName: string, returnValue: string): EmbeddedRefDescription[] {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null && typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, val) !== null) {
			let arr = <JsonDictionaryArray>val;
			if (arr.items != null) {
				let ret = <EmbeddedRefDescription[]>new Array(arr.items.length);
				for (let i = 0; i < arr.items.length; i++) {
					let curr = arr.items[i];
					let jObj = <JsonDictionaryObject>curr;
					let refr: EmbeddedRefDescription = ((() => {
						let $ret = new EmbeddedRefDescription();
						$ret.refType = "name";
						$ret.value = (<JsonDictionaryValue>jObj.item("id")).value.toString();
						return $ret;
					})());
					ret[i] = refr;
				}
				return ret;
			}
		}
		return null;
	}
	static returnAsPoint(returnValue: string): PointDescription {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let jObj = <JsonDictionaryObject>val;
			let pt: PointDescription = new PointDescription();
			pt.x = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("x")).value);
			pt.y = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("y")).value);
			return pt;
		}
		return null;
	}
	static returnAsSize(returnValue: string): SizeDescription {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let jObj = <JsonDictionaryObject>val;
			let pt: SizeDescription = new SizeDescription();
			pt.width = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("width")).value);
			pt.height = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("height")).value);
			return pt;
		}
		return null;
	}
	static returnAsRect(returnValue: string): RectDescription {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let jObj = <JsonDictionaryObject>val;
			let pt: RectDescription = new RectDescription();
			pt.left = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("left")).value);
			pt.top = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("top")).value);
			pt.width = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("width")).value);
			pt.height = ComponentRendererMethodHelper.fromDoubleValue((<JsonDictionaryValue>jObj.item("height")).value);
			return pt;
		}
		return null;
	}
	static asMethodRef(returnValue: string): EmbeddedRefDescription {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let jObj = <JsonDictionaryObject>val;
			let refr: EmbeddedRefDescription = ((() => {
				let $ret = new EmbeddedRefDescription();
				$ret.refType = "name";
				$ret.value = (<JsonDictionaryValue>jObj.item("id")).value.toString();
				return $ret;
			})());
			return refr;
		}
		return null;
	}
	static returnAsBool(returnValue: string): boolean {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			return <boolean>val;
		}
		return false;
	}
	static returnAsDate(returnValue: string): Date {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null) {
			let strVal = <string>val;
			let dt: Date;
			if (((() => { let $ret = dateTryParse(strVal, dt); dt = $ret.p1; return $ret.ret; })())) {
				return dt;
			}
		}
		return dateMinValue();
	}
	static returnAsPrimitive(returnValue: string): any {
		let val = ComponentRendererMethodHelper.extractValue(returnValue);
		if (val != null && typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, val) !== null) {
			val = ComponentRendererMethodHelper.coercePrimitive(val);
			if (typeof val === 'string') {
				let strVal = <string>val;
				if (strVal == "@dbl:INFINITY") {
					return Number.POSITIVE_INFINITY;
				}
				if (strVal == "@dbl:-INFINITY") {
					return Number.NEGATIVE_INFINITY;
				}
			}
		}
		return val;
	}
	private static coercePrimitive(val: any): any {
		let jObj = <JsonDictionaryObject>val;
		if (jObj.containsKey("refType")) {
			if ((<JsonDictionaryValue>jObj.item("refType")).value.toString() == "name") {
				let refr: EmbeddedRefDescription = ((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "name";
					$ret.value = (<JsonDictionaryValue>jObj.item("id")).value.toString();
					return $ret;
				})());
				return refr;
			}
			if ((<JsonDictionaryValue>jObj.item("refType")).value.toString() == "uuid") {
				let refr1: EmbeddedRefDescription = ((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = (<JsonDictionaryValue>jObj.item("id")).value.toString();
					return $ret;
				})());
				return refr1;
			}
		}
		return val;
	}
}


