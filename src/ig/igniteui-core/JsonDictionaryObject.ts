import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { Dictionary$2 } from "./Dictionary$2";
import { List$1 } from "./List$1";
import { JsonWriter } from "./JsonWriter";
import { Base, String_$type, Type, markType } from "./type";

/**
 * @hidden 
 */
export class JsonDictionaryObject extends JsonDictionaryItem {
	static $t: Type = markType(JsonDictionaryObject, 'JsonDictionaryObject', (<any>JsonDictionaryItem).$type);
	private _propertyKeys: List$1<string> = new List$1<string>(String_$type, 0);
	private _items: Dictionary$2<string, JsonDictionaryItem> = new Dictionary$2<string, JsonDictionaryItem>(String_$type, (<any>JsonDictionaryItem).$type, 0);
	constructor() {
		super();
	}
	addItem(key: string, item: JsonDictionaryItem): void {
		if (!this._items.containsKey(key)) {
			this._propertyKeys.add(key);
		}
		this._items.item(key, item);
	}
	removeItem(key: string): void {
		if (this._items.containsKey(key)) {
			this._propertyKeys.remove(key);
		}
		this._items.removeItem(key);
	}
	clearItems(): void {
		this._propertyKeys.clear();
		this._items.clear();
	}
	item(key: string, value?: JsonDictionaryItem): JsonDictionaryItem {
		if (arguments.length === 2) {
			if (!this._items.containsKey(key)) {
				this._propertyKeys.add(key);
			}
			this._items.item(key, value);
			return value;
		} else {
			return this._items.item(key);
		}
	}
	containsKey(key: string): boolean {
		return this._items.containsKey(key);
	}
	getKeys(): string[] {
		return this._propertyKeys.toArray();
	}
	toJsonHelper(writer: JsonWriter): void {
		writer.write("{");
		writer.newLine();
		writer.increaseIndent();
		let keys = this.getKeys();
		if (keys != null) {
			for (let i = 0; i < keys.length; i++) {
				if (i > 0) {
					writer.writeLine(",");
				}
				let key = keys[i];
				let value = this._items.item(key);
				writer.write("\"" + this.escape(key) + "\": ");
				if (value == null) {
					writer.write("null");
				} else {
					value.toJsonHelper(writer);
				}
			}
		}
		writer.newLine();
		writer.decreaseIndent();
		writer.write("}");
	}
}


