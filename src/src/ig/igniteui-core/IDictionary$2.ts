import { ICollection$1, ICollection$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerable, IEnumerable_$type, Type } from "./type";
import { KeyValuePair$2 } from "./KeyValuePair$2";

/**
 * @hidden 
 */
export interface IDictionary$2<TKey, TValue> extends ICollection$1<KeyValuePair$2<TKey, TValue>>, IEnumerable$1<KeyValuePair$2<TKey, TValue>>, IEnumerable { 
	addItem(key: TKey, value: TValue): void;
removeItem(key: TKey): boolean;
item(key: TKey, value?: TValue): TValue;
readonly count: number;
readonly keys: ICollection$1<TKey>;
readonly values: ICollection$1<TValue>;
tryGetValue(key: TKey, value: TValue): { ret: boolean; p1: TValue; };
containsKey(key: TKey): boolean;
clear(): void;
}

/**
 * @hidden 
 */
export let IDictionary$2_$type = new Type(null, 'IDictionary$2', null, [ICollection$1_$type.specialize((<any>KeyValuePair$2).$type.specialize(0, 1)), IEnumerable$1_$type.specialize((<any>KeyValuePair$2).$type.specialize(0, 1)), IEnumerable_$type]);


