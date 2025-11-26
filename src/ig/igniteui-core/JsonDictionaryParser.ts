import { Base, String_$type, typeCast, Array_$type, Boolean_$type, Type, markType } from "./type";
import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { List$1 } from "./List$1";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryArray } from "./JsonDictionaryArray";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";

/**
 * @hidden 
 */
export class JsonDictionaryParser extends Base {
	static $t: Type = markType(JsonDictionaryParser, 'JsonDictionaryParser');
	parse(json_: string): JsonDictionaryItem {
		let obj = JSON.parse(json_);
		return JsonDictionaryParser.objectToDictionary(obj);
	}
	static getPropertyKeys(item_: any): List$1<string> {
		let propertyKey_: string = null;
		let ret_: List$1<string> = new List$1<string>(String_$type, 0);
		let exclusions_ = {};
		for (propertyKey_ in item_) {
				
				if (!isNaN(<any>propertyKey_)) {
					continue;
				}
				if (!exclusions_.hasOwnProperty(propertyKey_)) {
					ret_.add(propertyKey_);
				}
};
		return ret_;
	}
	private static objectToDictionary(obj_: any): JsonDictionaryItem {
		if (typeCast<any[]>(Array_$type, obj_) !== null) {
			return JsonDictionaryParser.convertValue(obj_);
		}
		if (typeof obj_ === 'number') {
			return JsonDictionaryParser.convertValue(obj_);
		}
		if (typeof obj_ === 'string') {
			return JsonDictionaryParser.convertValue(obj_);
		}
		if (typeCast<boolean>(Boolean_$type, obj_) !== null) {
			return JsonDictionaryParser.convertValue(obj_);
		}
		let dict: JsonDictionaryObject = new JsonDictionaryObject();
		let keys = JsonDictionaryParser.getPropertyKeys(obj_);
		for (let i = 0; i < keys.count; i++) {
			let key_ = keys._inner[i];
			let val = obj_[key_];
			val = JsonDictionaryParser.convertValue(val);
			dict.item(key_, <JsonDictionaryItem>val);
		}
		return dict;
	}
	private static convertValue(val: any): JsonDictionaryItem {
		if (typeCast<any[]>(Array_$type, val) !== null) {
			let arr = <any[]>val;
			let newArr: JsonDictionaryItem[] = <JsonDictionaryItem[]>new Array(arr.length);
			for (let i = 0; i < arr.length; i++) {
				newArr[i] = JsonDictionaryParser.convertValue(arr[i]);
			}
			return ((() => {
				let $ret = new JsonDictionaryArray();
				$ret.items = newArr;
				return $ret;
			})());
		} else if (typeof val === 'number') {
			return ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.value = val;
				$ret.type = JsonDictionaryValueType.NumberValue;
				return $ret;
			})());
		} else if (typeof val === 'string') {
			return ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.value = val;
				$ret.type = JsonDictionaryValueType.StringValue;
				return $ret;
			})());
		} else if (typeCast<boolean>(Boolean_$type, val) !== null) {
			return ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.value = val;
				$ret.type = JsonDictionaryValueType.BooleanValue;
				return $ret;
			})());
		} else if (val == null) {
			return ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.value = val;
				$ret.type = JsonDictionaryValueType.NullValue;
				return $ret;
			})());
		} else {
			return JsonDictionaryParser.objectToDictionary(val);
		}
	}
}


