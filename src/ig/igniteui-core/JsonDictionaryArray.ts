import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { JsonWriter } from "./JsonWriter";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class JsonDictionaryArray extends JsonDictionaryItem {
	static $t: Type = markType(JsonDictionaryArray, 'JsonDictionaryArray', (<any>JsonDictionaryItem).$type);
	private _items: JsonDictionaryItem[] = null;
	get items(): JsonDictionaryItem[] {
		return this._items;
	}
	set items(value: JsonDictionaryItem[]) {
		this._items = value;
	}
	toJsonHelper(writer: JsonWriter): void {
		writer.write("[");
		writer.newLine();
		writer.increaseIndent();
		if (this.items != null) {
			for (let i = 0; i < this.items.length; i++) {
				if (i > 0) {
					writer.writeLine(",");
				}
				let item = this.items[i];
				if (item == null) {
					writer.write("null");
				} else {
					item.toJsonHelper(writer);
				}
			}
		}
		writer.newLine();
		writer.decreaseIndent();
		writer.write("]");
	}
}


