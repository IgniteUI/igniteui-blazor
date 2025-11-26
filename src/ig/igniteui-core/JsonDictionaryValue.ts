import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { Base, typeGetValue, Type, markType } from "./type";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";
import { JsonWriter } from "./JsonWriter";

/**
 * @hidden 
 */
export class JsonDictionaryValue extends JsonDictionaryItem {
	static $t: Type = markType(JsonDictionaryValue, 'JsonDictionaryValue', (<any>JsonDictionaryItem).$type);
	constructor() {
		super();
	}
	private _value: any = null;
	get value(): any {
		return this._value;
	}
	set value(value: any) {
		this._value = value;
	}
	private _type: JsonDictionaryValueType = <JsonDictionaryValueType>0;
	get type(): JsonDictionaryValueType {
		return this._type;
	}
	set type(value: JsonDictionaryValueType) {
		this._type = value;
	}
	toJsonHelper(writer: JsonWriter): void {
		switch (this.type) {
			case JsonDictionaryValueType.BooleanValue:
			if (<boolean>this.value) {
				writer.write("true");
			} else {
				writer.write("false");
			}
			break;

			case JsonDictionaryValueType.NullValue:
			writer.write("null");
			break;

			case JsonDictionaryValueType.NumberValue:
			let val = this.value;
			if (typeof val === 'number') {
				val = <number>typeGetValue(val);
			}
			writer.write((<number>val).toString());
			break;

			case JsonDictionaryValueType.StringValue:
			writer.write("\"" + this.escape(<string>this.value) + "\"");
			break;

		}

	}
}


