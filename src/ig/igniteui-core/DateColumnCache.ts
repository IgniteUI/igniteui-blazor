import { Base, String_$type, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { HashSet$1 } from "./HashSet$1";

/**
 * @hidden 
 */
export class DateColumnCache extends Base {
	static $t: Type = markType(DateColumnCache, 'DateColumnCache');
	private _columns: HashSet$1<string> = new HashSet$1<string>(String_$type, 0);
	private _indexedCaches: List$1<DateColumnCache> = new List$1<DateColumnCache>((<any>DateColumnCache).$type, 0);
	addIndexedCache(index: number): DateColumnCache {
		this._indexedCaches.insert(index, new DateColumnCache());
		return this._indexedCaches._inner[index];
	}
	getIndexedCache(index: number): DateColumnCache {
		return this._indexedCaches._inner[index];
	}
	addDateColumn(columnName: string): void {
		this._columns.add_1(columnName);
	}
	isDateColumn(columnName: string): boolean {
		return this._columns.contains(columnName);
	}
}


